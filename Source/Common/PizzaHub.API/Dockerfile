FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Source/Common/RecipeBook.API/PizzaHub.API.csproj", "Source/Common/PizzaHub.API/"]
COPY ["Source/Shared/RecipeBook.Exceptions/PizzaHub.Exceptions.csproj", "Source/Shared/PizzaHub.Exceptions/"]
COPY ["Source/Common/RecipeBook.Application/PizzaHub.Application.csproj", "Source/Common/PizzaHub.Application/"]
COPY ["Source/Common/RecipeBook.Domain/PizzaHub.Domain.csproj", "Source/Common/PizzaHub.Domain/"]
COPY ["Source/Common/RecipeBook.Infrastructure/PizzaHub.Infrastructure.csproj", "Source/Common/PizzaHub.Infrastructure/"]
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
