FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5164

ENV ASPNETCORE_URLS=http://+:5164

USER app
FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["TestInitProject.Web.csproj", "TestInitProject.Web/"]
RUN dotnet restore "TestInitProject.Web/TestInitProject.Web.csproj"

WORKDIR "/src/TestInitProject.Web"
COPY . .

RUN dotnet build "TestInitProject.Web.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "TestInitProject.Web.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestInitProject.Web.dll"]
