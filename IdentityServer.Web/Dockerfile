FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5164

ENV ASPNETCORE_URLS=http://+:5164

USER app
FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["IdentityServer.Web/IdentityServer.Web.csproj", "IdentityServer.Web/"]
RUN dotnet restore "IdentityServer.Web/IdentityServer.Web.csproj"

COPY ["IdentityServer.Domain/IdentityServer.Domain.csproj", "IdentityServer.Domain/"]
RUN dotnet restore "IdentityServer.Domain/IdentityServer.Domain.csproj"

COPY ["IdentityServer.Infrastructure/IdentityServer.Infrastructure.csproj", "IdentityServer.Infrastructure/"]
RUN dotnet restore "IdentityServer.Infrastructure/IdentityServer.Infrastructure.csproj"

COPY . .

RUN dotnet build "IdentityServer.Web/IdentityServer.Web.csproj" -c "$configuration" -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "IdentityServer.Web/IdentityServer.Web.csproj" \
    -c "$configuration" \ 
    -o /app/publish \
    /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityServer.Web.dll"]
