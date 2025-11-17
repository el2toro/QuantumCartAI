# Services/Cart/Cart.API/Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

ENV NUGET_FALLBACK_PACKAGES=""

# CORRECT PATHS – using "Services" (with s) as you confirmed
COPY ["Services/Cart/Cart.API/Cart.API.csproj", "Services/Cart/Cart.API/"]
COPY ["Services/Cart/Cart.Domain/Cart.Domain.csproj", "Services/Cart/Cart.Domain/"]
COPY ["Services/Cart/Cart.Application/Cart.Application.csproj", "Services/Cart/Cart.Application/"]
COPY ["Services/Cart/Cart.Infrastructure/Cart.Infrastructure.csproj", "Services/Cart/Cart.Infrastructure/"]

# Restore
RUN dotnet restore "Services/Cart/Cart.API/Cart.API.csproj"

# Copy all source code
COPY . .

# Publish
WORKDIR "/src/Services/Cart/Cart.API"
RUN dotnet publish -c Release -o /app/publish --no-restore

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "Cart.API.dll"]