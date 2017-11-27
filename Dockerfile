FROM microsoft/dotnet:2.0.3-sdk

RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    pdftk \
    ghostscript \
    imagemagick \
    gsfonts

COPY ./src /app

# Configure the listening port to 80
ENV ASPNETCORE_URLS http://*:80

# Configurate to Brazil date time
ENV TZ America/Sao_Paulo
RUN echo $TZ | tee /etc/timezone && dpkg-reconfigure --frontend noninteractive tzdata

EXPOSE 80

# Start the app
WORKDIR /app/src/FileTools
ENTRYPOINT dotnet ./bin/Release/netcoreapp2.0/publish/FileTools.dll
