#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DDD4.Customer.Api/DDD4.Customer.Api.csproj", "DDD4.Customer.Api/"]
COPY ["DDD4.Customer.Application/DDD4.Customer.Application.csproj", "DDD4.Customer.Application/"]
COPY ["DDD4.Customer.Domain/DDD4.Customer.Domain.csproj", "DDD4.Customer.Domain/"]
COPY ["DDD4.Customer.Infrastructure/DDD4.Customer.Infrastructure.csproj", "DDD4.Customer.Infrastructure/"]
COPY ["DDD4.Contracts/DDD4.Contracts.csproj", "DDD4.Contracts/"]
RUN dotnet restore "DDD4.Customer.Api/DDD4.Customer.Api.csproj"
COPY . .
WORKDIR "/src/DDD4.Customer.Api"
RUN dotnet build "DDD4.Customer.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DDD4.Customer.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DDD4.Customer.Api.dll"]