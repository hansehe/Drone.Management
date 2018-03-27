# Run "docker build -f Dockerfile -t <docker_image> .." in powershell to build this Dockerfile locally.

FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY . .

WORKDIR Drone.Management.Migrator
RUN dotnet restore -nowarn:msb3202,nu1503
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY DroneManagementSettings.xml .
ENTRYPOINT ["dotnet", "Drone.Management.Migrator.dll"]
