#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.2-aspnetcore-runtime-nanoserver-sac2016 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk-nanoserver-sac2016 AS build
WORKDIR /src
COPY ["Bryan.Gateway/Bryan.Gateway.csproj", "Bryan.Gateway/"]
RUN dotnet restore "Bryan.Gateway/Bryan.Gateway.csproj"
COPY . .
WORKDIR "/src/Bryan.Gateway"
RUN dotnet build "Bryan.Gateway.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Bryan.Gateway.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Bryan.Gateway.dll"]