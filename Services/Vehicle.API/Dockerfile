#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/VehicleAPI/VehicleAPI.csproj", "Services/VehicleAPI/"]
RUN dotnet restore "Services/VehicleAPI/VehicleAPI.csproj"
COPY . .
WORKDIR "/src/Services/VehicleAPI"
RUN dotnet build "VehicleAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VehicleAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VehicleAPI.dll"]