FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FeatureDemo/FeatureDemo.csproj", "FeatureDemo/"]
RUN dotnet restore "FeatureDemo/FeatureDemo.csproj"
COPY . .
WORKDIR "/src/FeatureDemo"
RUN dotnet build "FeatureDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FeatureDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FeatureDemo.dll"]
