FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
ARG PublishPath
WORKDIR app
COPY $PublishPath .
ENV SOLUTION_DLL = default
ENTRYPOINT ["dotnet", ${SOLUTION_DLL}] 