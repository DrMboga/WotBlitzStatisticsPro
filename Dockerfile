FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
RUN dotnet workload install wasm-tools
RUN apt-get update && apt-get install -y python3
COPY ["WotBlitzStatisticsPro/WotBlitzStatisticsPro.csproj", "WotBlitzStatisticsPro/"]
COPY ["WotBlitzStatisticsPro.Application/WotBlitzStatisticsPro.Application.csproj", "WotBlitzStatisticsPro.Application/"]
COPY ["WotBlitzStatisticsPro.Persistence/WotBlitzStatisticsPro.Persistence.csproj", "WotBlitzStatisticsPro.Persistence/"]
COPY ["WotBlitzStatisticsPro.WargamingApi/WotBlitzStatisticsPro.WargamingApi.csproj", "WotBlitzStatisticsPro.WargamingApi/"]
COPY ["WotBlitzStatisticsPro.WebUi/WotBlitzStatisticsPro.WebUi.csproj", "WotBlitzStatisticsPro.WebUi/"]
RUN dotnet restore "WotBlitzStatisticsPro/WotBlitzStatisticsPro.csproj"
RUN dotnet restore "WotBlitzStatisticsPro.Application/WotBlitzStatisticsPro.Application.csproj"
RUN dotnet restore "WotBlitzStatisticsPro.Persistence/WotBlitzStatisticsPro.Persistence.csproj"
RUN dotnet restore "WotBlitzStatisticsPro.WargamingApi/WotBlitzStatisticsPro.WargamingApi.csproj"
RUN dotnet restore "WotBlitzStatisticsPro.WebUi/WotBlitzStatisticsPro.WebUi.csproj"
COPY . .
WORKDIR "/src/WotBlitzStatisticsPro.WebUi"
RUN dotnet build "WotBlitzStatisticsPro.WebUi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WotBlitzStatisticsPro.WebUi.csproj" -c Release -o /app/publish

# We then get the base image for Nginx and set the 
# work directory 
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

# We'll copy all the contents from wwwroot in the publish
# folder into nginx/html for nginx to serve. The destination
# should be the same as what you set in the nginx.conf.
COPY --from=publish /app/publish/wwwroot /usr/local/webapp/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf