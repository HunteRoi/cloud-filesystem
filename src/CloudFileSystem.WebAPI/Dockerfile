#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY ["src/CloudFileSystem.WebAPI/CloudFileSystem.WebAPI.csproj", "CloudFileSystem.WebAPI/"]
COPY ["src/CloudFileSystem.Infrastructure/CloudFileSystem.Infrastructure.csproj", "CloudFileSystem.Infrastructure/"]
COPY ["src/CloudFileSystem.Domain/CloudFileSystem.Domain.csproj", "CloudFileSystem.Domain/"]
COPY ["src/CloudFileSystem.Application/CloudFileSystem.Application.csproj", "CloudFileSystem.Application/"]
RUN dotnet restore "source/CloudFileSystem.WebAPI/CloudFileSystem.WebAPI.csproj"
COPY . .
WORKDIR "/source/CloudFileSystem.WebAPI"
RUN dotnet build "CloudFileSystem.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CloudFileSystem.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CloudFileSystem.WebAPI.dll"]