#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MarktGuru.Products.Api/MarktGuru.Products.Api.csproj", "MarktGuru.Products.Api/"]
COPY ["MarktGuru.Products.Application/MarktGuru.Products.Application.csproj", "MarktGuru.Products.Application/"]
COPY ["MarktGuru.Products.Domain/MarktGuru.Products.Domain.csproj", "MarktGuru.Products.Domain/"]
COPY ["MarktGuru.Products.Infrastructure/MarktGuru.Products.Infrastructure.csproj", "MarktGuru.Products.Infrastructure/"]
COPY ["MarktGuru.Products.Common/MarktGuru.Products.Common.csproj", "MarktGuru.Products.Common/"]

RUN dotnet restore "./MarktGuru.Products.Api/MarktGuru.Products.Api.csproj"
COPY . .
WORKDIR "/src/MarktGuru.Products.Api"
RUN dotnet build "./MarktGuru.Products.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MarktGuru.Products.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarktGuru.Products.Api.dll"]