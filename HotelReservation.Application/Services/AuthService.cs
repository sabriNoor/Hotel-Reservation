using AutoMapper;
using HotelReservation.Application.DTOs.Auth;
using HotelReservation.Application.Exceptions;
using HotelReservation.Application.IAuth;
using HotelReservation.Application.IRepository;
using HotelReservation.Application.IServices;
using HotelReservation.Domain.Entities;
using Microsoft.Extensions.Logging;
using UnauthorizedAccessException = HotelReservation.Application.Exceptions.UnauthorizedAccessException;

namespace HotelReservation.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly ILogger<AuthService> _logger;
        private readonly IMapper _mapper;

        public AuthService(
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            IJwtGenerator jwtGenerator,
            ILogger<AuthService> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = unitOfWork.UserRepository;
            _passwordHasher = passwordHasher;
            _jwtGenerator = jwtGenerator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
        {
            _logger.LogInformation("Login attempt for username: {Username}", dto.Username);

            try
            {
                var user = await _userRepository.FindOneAsync(u => u.Username == dto.Username);
                if (user is null)
                {
                    _logger.LogWarning("Login failed. User not found: {Username}", dto.Username);
                    throw new NotFoundException("User", dto.Username);
                }

                var validPassword = _passwordHasher.VerifyPassword(dto.Password, user.PasswordHash);
                if (!validPassword)
                {
                    _logger.LogWarning("Invalid password attempt for username: {Username}", dto.Username);
                    throw new InvalidCredentialsException();
                }

                var token = _jwtGenerator.GenerateJwtToken(user);

                _logger.LogInformation("Login successful for username: {Username}", dto.Username);

                return new LoginResponseDto()
                {
                    Token = token,
                    Name = $"{user.PersonalInformation.FirstName} {user.PersonalInformation.LastName}"
                };
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "User not found during login for username: {Username}", dto.Username);
                throw;
            }
            catch (InvalidCredentialsException ex)
            {
                _logger.LogError(ex, "Unauthorized login attempt for username: {Username}", dto.Username);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during login for username: {Username}", dto.Username);
                throw;
            }
        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            try
            {
                _logger.LogInformation("Starting registration for user: {Username}, Email: {Email}", dto.Username, dto.Email);

                var existingUser = await _userRepository.FindOneAsync(u => u.Username == dto.Username || u.Email == dto.Email);
                if (existingUser is not null)
                {
                    _logger.LogWarning("Registration failed: Username or Email already exists. Username: {Username}, Email: {Email}", dto.Username, dto.Email);
                    throw new InvalidOperationException("Username or Email already exists.");
                }

                var newUser = _mapper.Map<User>(dto);
                newUser.PasswordHash = _passwordHasher.HashPassword(newUser.PasswordHash);
                _logger.LogInformation("User mapped and password hashed for: {Username}", newUser.Username);

                await _userRepository.AddAsync(newUser);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("User successfully registered: {Username}", newUser.Username);

                return _mapper.Map<RegisterResponseDto>(newUser);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Username or Email already exists. Username: {Username}, Email: {Email}", dto.Username, dto.Email);
                throw;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during registration for user: {Username}", dto.Username);
                throw;
            }
        }

    }
}
