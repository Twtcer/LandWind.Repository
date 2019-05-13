FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["LandWind.Repository.Web/LandWind.Repository.Web.csproj", "LandWind.Repository.Web/"]
RUN dotnet restore "LandWind.Repository.Web/LandWind.Repository.Web.csproj"
COPY . .
WORKDIR "/src/LandWind.Repository.Web"
RUN dotnet build "LandWind.Repository.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "LandWind.Repository.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LandWind.Repository.Web.dll"]