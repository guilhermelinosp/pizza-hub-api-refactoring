FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Source/Common/PizzaHub.API/PizzaHub.API.csproj", "Source/Common/PizzaHub.API/"]
RUN dotnet restore "Source/Common/PizzaHub.API/PizzaHub.API.csproj"
COPY . .
WORKDIR "/src/Source/Common/PizzaHub.API"
RUN dotnet build "PizzaHub.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PizzaHub.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PizzaHub.API.dll"]
