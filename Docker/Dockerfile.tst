FROM microsoft/aspnetcore

ENV ASPNETCORE_ENVIRONMENT=tst

WORKDIR /app

COPY ./publish .

ENTRYPOINT ["dotnet", "NorthWind.WebApi.dll"]