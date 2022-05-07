FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["YourSport.Api/YourSport.Api.csproj", "YourSport.Api/"]
COPY ["YourSport.Application/YourSport.Application.csproj", "YourSport.Application/"]
COPY ["YourSport.Data/YourSport.Data.csproj", "YourSport.Data/"]
COPY ["YourSport.Business/YourSport.Business.csproj", "YourSport.Business/"]
RUN dotnet restore "YourSport.Api/YourSport.Api.csproj"
COPY . .
WORKDIR "YourSport.Api"
RUN dotnet build "YourSport.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YourSport.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YourSport.Api.dll"]
