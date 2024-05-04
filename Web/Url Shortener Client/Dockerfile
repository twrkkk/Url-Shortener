FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Web/Url Shortener Client/Url Shortener Client.csproj", "Web/Url Shortener Client/"] .
RUN dotnet restore "./Web/Url Shortener Client/Url Shortener Client.csproj"
COPY . .
RUN dotnet build "./Web/Url Shortener Client/Url Shortener Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Web/Url Shortener Client/Url Shortener Client.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf