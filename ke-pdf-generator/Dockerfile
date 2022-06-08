#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
RUN apt-get update \
    && apt-get install -y \
        curl \
        libxrender1 \
        libjpeg62-turbo \
        fontconfig \
        libxtst6 \
        xfonts-75dpi \
        xfonts-base \
        xz-utils

RUN curl "https://github.com/wkhtmltopdf/packaging/releases/download/0.12.6-1/wkhtmltox_0.12.6-1.buster_amd64.deb" -L -o "wkhtmltopdf.deb"
RUN dpkg -i wkhtmltopdf.deb
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ke-pdf-generator/ke-pdf-generator.csproj", "ke-pdf-generator/"]
RUN dotnet restore "ke-pdf-generator/ke-pdf-generator.csproj"
COPY . .
WORKDIR "/src/ke-pdf-generator"
RUN dotnet build "ke-pdf-generator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ke-pdf-generator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ke-pdf-generator.dll"]