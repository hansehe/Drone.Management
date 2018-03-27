FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY . .

WORKDIR /src/Drone.Management.Tests/Drone.Management.Migrator.Tests
RUN dotnet restore -nowarn:msb3202,nu1503
RUN dotnet test
ENTRYPOINT ["dotnet", "test"]