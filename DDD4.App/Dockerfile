#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DDD4.App/DDD4.App.csproj", "DDD4.App/"]
RUN dotnet restore "DDD4.App/DDD4.App.csproj"
COPY . .
WORKDIR "/src/DDD4.App"
RUN dotnet build "DDD4.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DDD4.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DDD4.App.dll"]