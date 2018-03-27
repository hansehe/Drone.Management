# Run "docker build -f Dockerfile -t <docker_image> .." in powershell to build this Dockerfile locally, and test solution.

# NOTE: Not working properly with current aspnetcore image build.

FROM microsoft/aspnetcore-build:1.0-2.0 AS build
WORKDIR /src
COPY . .

RUN dotnet restore Drone.Management.sln -nowarn:msb3202,nu1503
RUN dotnet test Drone.Management.sln
ENTRYPOINT ["dotnet", "test", "Drone.Management.sln"]