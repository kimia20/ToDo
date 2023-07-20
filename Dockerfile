FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /src
COPY ["src/WebEndpoints.ToDo.Api/WebEndpoints.ToDo.Api.csproj","src/WebEndpoints.ToDo.Api/"]
WORKDIR /publish
COPY ["src/WebEndpoints.ToDo.Api/","/publish"]
RUN dotnet publish -c Release -o /publish
ENTRYPOINT ["dotnet", "WebEndpoints.ToDo.Api.dll"]