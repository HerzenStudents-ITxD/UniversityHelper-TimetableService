#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/TimetableService/TimetableService.csproj", "src/TimetableService/"]
RUN dotnet restore "src/TimetableService/TimetableService.csproj"
COPY . .
WORKDIR "/src/src/TimetableService"
RUN dotnet build "TimetableService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TimetableService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TimetableService.dll"]