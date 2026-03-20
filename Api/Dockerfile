# Fase base: Ejecución
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER app
WORKDIR /app
# Render usa el puerto 10000 por defecto
EXPOSE 10000
ENV ASPNETCORE_URLS=http://+:10000

# Fase de compilación
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiamos los archivos de proyecto respetando la estructura de carpetas
COPY ["Api/Api.csproj", "Api/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

# Restauramos dependencias
RUN dotnet restore "./Api/Api.csproj"

# Copiamos el resto del código y compilamos
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "./Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Fase de publicación
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final: Producción
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]