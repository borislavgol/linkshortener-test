#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LinkShortener.Web/LinkShortener.Web.csproj", "LinkShortener.Web/"]
COPY ["LinkShortener.Services/LinkShortener.Services.csproj", "LinkShortener.Services/"]
COPY ["LinkShortener.Dal/LinkShortener.Dal.csproj", "LinkShortener.Dal/"]
COPY ["LinkShortener.Dal.Entities/LinkShortener.Dal.Entities.csproj", "LinkShortener.Dal.Entities/"]
COPY ["LinkShortener.Models/LinkShortener.Models.csproj", "LinkShortener.Models/"]
COPY ["LinkShortener.Dtos/LinkShortener.Dtos.csproj", "LinkShortener.Dtos/"]
COPY ["LinkShortener.Mediatr/LinkShortener.Mediatr.csproj", "LinkShortener.Mediatr/"]
COPY ["LinkShortener.Exceptions/LinkShortener.Exceptions.csproj", "LinkShortener.Exceptions/"]
RUN dotnet restore "LinkShortener.Web/LinkShortener.Web.csproj"
COPY . .
WORKDIR "/src/LinkShortener.Web"
RUN dotnet build "LinkShortener.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LinkShortener.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LinkShortener.Web.dll"]