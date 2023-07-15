FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
WORKDIR /src
COPY ["src/WebEndpoints.ToDo.Api/WebEndpoints.ToDo.Api.csproj","src/WebEndpoints.ToDo.Api/"]
RUN dotnet build "WebEndpoints.ToDo.Api.csproj" -c Release -o /app/build
ENTRYPOINT ["dotnet", "WebEndpoints.ToDo.Api.dll"]