FROM mcr.microsoft.com/dotnet/core/aspnet:2.2

ARG PublishPath

ENV SOLUTION_DLL="anotherapi.dll"

WORKDIR app

COPY ${PublishPath} .

ENTRYPOINT dotnet ${SOLUTION_DLL}