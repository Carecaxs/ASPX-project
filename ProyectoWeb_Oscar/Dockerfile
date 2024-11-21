# Usa la imagen base de ASP.NET Framework 4.8 sobre Windows Server
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8 AS base
WORKDIR /inetpub/wwwroot

# Expón el puerto 80
EXPOSE 80

# Usa la imagen base de SDK de .NET Framework para compilar la aplicación
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build
WORKDIR /src

# Copia el archivo de proyecto (.csproj) a la carpeta del contenedor
COPY ["ProyectoWeb_Oscar/ProyectoWeb_Oscar.csproj", "ProyectoWeb_Oscar/"]

# Restaura las dependencias del proyecto
RUN nuget restore "ProyectoWeb_Oscar/ProyectoWeb_Oscar.csproj"

# Copia el resto de los archivos de tu proyecto
COPY . .

# Establece la carpeta de trabajo para compilar
WORKDIR "/src/ProyectoWeb_Oscar"

# Compila el proyecto en modo Release y lo coloca en la carpeta /app/build dentro del contenedor
RUN msbuild "ProyectoWeb_Oscar.csproj" /p:Configuration=Release /p:OutputPath=/app/build

# Publica la aplicación
FROM build AS publish
RUN msbuild "ProyectoWeb_Oscar.csproj" /p:Configuration=Release /p:OutputPath=/app/publish /p:DeployOnBuild=true /p:PublishProfile="FolderProfile"

# Crea la imagen final a partir de la imagen base
FROM base AS final
WORKDIR /inetpub/wwwroot

# Copia la versión publicada del proyecto al contenedor final
COPY --from=publish /app/publish .

# Exposición de puerto por defecto
ENTRYPOINT ["C:\\Windows\\System32\\inetsrv\\iisreset.exe"]
