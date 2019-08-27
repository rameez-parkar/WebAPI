FROM mcr.microsoft.com/dotnet/core/aspnet:2.2

ENV SOLUTION_DLL="anotherapi.dll"

WORKDIR /work

COPY ./artifacts/. .

ENTRYPOINT dotnet ${SOLUTION_DLL}