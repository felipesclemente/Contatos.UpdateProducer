FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Contatos.UpdateProducer/Contatos.UpdateProducer.csproj", "Contatos.UpdateProducer/"]
RUN dotnet restore "./Contatos.UpdateProducer/Contatos.UpdateProducer.csproj"
COPY . .
WORKDIR "/src/Contatos.UpdateProducer"
RUN dotnet build "./Contatos.UpdateProducer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Contatos.UpdateProducer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contatos.UpdateProducer.dll"]