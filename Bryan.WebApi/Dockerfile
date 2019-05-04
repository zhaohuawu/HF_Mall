FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Bryan.WebApi/Bryan.WebApi.csproj", "Bryan.WebApi/"]
RUN dotnet restore "Bryan.WebApi/Bryan.WebApi.csproj"
COPY . .
WORKDIR "/src/Bryan.WebApi"
RUN dotnet build "Bryan.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Bryan.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Bryan.WebApi.dll"]
