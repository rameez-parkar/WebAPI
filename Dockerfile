FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR app
COPY anotherapi/Publish .
EXPOSE 8111
ENTRYPOINT ["dotnet", "anotherapi.dll"]