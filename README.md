# Drone Management Microservice

## About
Simple microservice running on Docker and written in ASP.NET Core for managing drones.

The project is inspired by the article `Designing microservices: Domain analysis`:

  * https://docs.microsoft.com/en-us/azure/architecture/microservices/domain-analysis

The microservice consists of three services:
  * API Server
  * Migrator Console
  * Postgresql Database

## Prerequisites
  * Docker 
    - Installed from https://www.docker.com/

## Setup & Run
  * Open terminal (or powershell on windows) inside repo and type:
    * .\run.sh (Linux)
    * .\run.ps1 (Windows/powershell)
  * Or, use Docker:
    * cd src/
    * docker-compose build
    * docker-compose run
