# Use the official .NET 9 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY HotelReservation.Api/*.csproj ./HotelReservation.Api/
COPY HotelReservation.Application/*.csproj ./HotelReservation.Application/
COPY HotelReservation.Domain/*.csproj ./HotelReservation.Domain/
COPY HotelReservation.Infrastructure/*.csproj ./HotelReservation.Infrastructure/
RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR /app/HotelReservation.Api
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/HotelReservation.Api/out .
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "HotelReservation.Api.dll"]
