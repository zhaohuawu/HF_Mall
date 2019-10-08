FROM microsoft/dotnet:2.2-aspnetcore-runtime AS baseservice
WORKDIR /app
EXPOSE 5010
COPY . /app
ENTRYPOINT ["dotnet", "Bryan.Base.dll"]
