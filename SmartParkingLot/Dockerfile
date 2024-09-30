FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/SmartParkingLot.Api/SmartParkingLot.Api.csproj", "src/SmartParkingLot.Api/"]
RUN dotnet restore "src\SmartParkingLot.Api\SmartParkingLot.Api.csproj"
COPY . .
WORKDIR "/src/src/SmartParkingLot.Api"
RUN dotnet build "SmartParkingLot.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "SmartParkingLot.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartParkingLot.Api.dll"]
