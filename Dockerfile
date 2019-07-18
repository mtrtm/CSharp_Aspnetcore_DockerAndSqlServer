FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["CSharp_Aspnetcore_DockerAndSqlServer.csproj", "./"]
RUN dotnet restore "CSharp_Aspnetcore_DockerAndSqlServer.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "CSharp_Aspnetcore_DockerAndSqlServer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CSharp_Aspnetcore_DockerAndSqlServer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CSharp_Aspnetcore_DockerAndSqlServer.dll"]
