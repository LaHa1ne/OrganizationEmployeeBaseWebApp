#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OrganizationEmployeeBaseWebApp/OrganizationEmployeeBaseWebApp.csproj", "OrganizationEmployeeBaseWebApp/"]
COPY ["OrganizationEmployeeBaseWebApp.DataLayer/OrganizationEmployeeBaseWebApp.DataLayer.csproj", "OrganizationEmployeeBaseWebApp.DataLayer/"]
COPY ["OrganizationEmployeeBaseWebApp.Services/OrganizationEmployeeBaseWebApp.Services.csproj", "OrganizationEmployeeBaseWebApp.Services/"]
COPY ["OrganizationEmployeeBaseWebApp.DataAccessLayer/OrganizationEmployeeBaseWebApp.DataAccessLayer.csproj", "OrganizationEmployeeBaseWebApp.DataAccessLayer/"]
RUN dotnet restore "OrganizationEmployeeBaseWebApp/OrganizationEmployeeBaseWebApp.csproj"
COPY . .
WORKDIR "/src/OrganizationEmployeeBaseWebApp"
RUN dotnet build "OrganizationEmployeeBaseWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrganizationEmployeeBaseWebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrganizationEmployeeBaseWebApp.dll"]