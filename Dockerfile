FROM mcr.microsoft.com/dotnet/core/aspnet:2.2

ENV SOLUTION_DLL=Default

WORKDIR /work

COPY artifacts .

ENTRYPOINT dotnet "${SOLUTION_DLL}.dll"