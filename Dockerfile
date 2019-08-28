FROM mcr.microsoft.com/dotnet/core/aspnet:2.2

ARG PublishPath
ARG SOLUTION_DLL
ENV SOLUTION_DLL=${SOLUTION_DLL}

WORKDIR app

COPY ${PublishPath} .

ENTRYPOINT dotnet ${SOLUTION_DLL}