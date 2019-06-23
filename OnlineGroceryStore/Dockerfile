FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["OnlineGroceryStore/OnlineGroceryStore.csproj", "OnlineGroceryStore/"]
RUN dotnet restore "OnlineGroceryStore/OnlineGroceryStore.csproj"
COPY . .
WORKDIR "/src/OnlineGroceryStore"
RUN dotnet build "OnlineGroceryStore.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "OnlineGroceryStore.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OnlineGroceryStore.dll"]