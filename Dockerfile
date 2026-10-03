FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY OrderCRUDApp/OrderCRUDApp.csproj OrderCRUDApp/
ENV NUGET_XMLDOC_MODE=skip \
    DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    DOTNET_NOLOGO=1

# Clear all NuGet caches to avoid corrupted package state, then restore from
# nuget.org explicitly. Retry up to 3 times, each bounded by a timeout.
RUN dotnet nuget locals all --clear \
    && for i in 1 2 3; do \
         timeout 600 dotnet restore OrderCRUDApp/OrderCRUDApp.csproj \
           --source https://api.nuget.org/v3/index.json \
           --verbosity normal \
         && break; \
         echo "Restore attempt $i failed, clearing caches and retrying..."; \
         dotnet nuget locals all --clear; \
         if [ "$i" -eq 3 ]; then exit 1; fi; \
         sleep 5; \
       done

COPY OrderCRUDApp/ OrderCRUDApp/
RUN dotnet publish OrderCRUDApp/OrderCRUDApp.csproj -c Release -o /app/publish --no-restore --verbosity minimal

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "OrderCRUDApp.dll"]
