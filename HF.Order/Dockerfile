FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["HF.Order/HF.Order.csproj", "HF.Order/"]
RUN dotnet restore "HF.Order/HF.Order.csproj"
COPY . .
WORKDIR "/src/HF.Order"
RUN dotnet build "HF.Order.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HF.Order.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HF.Order.dll"]