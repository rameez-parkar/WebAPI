FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR app
COPY anotherapi/Publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "WebAPIExample.dll"]