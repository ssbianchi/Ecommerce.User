#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
 
COPY . .
RUN dotnet publish "Ecommerce.User.API/Ecommerce.User.API.csproj" -c Release -o /app/publish
 
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
EXPOSE 80
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.User.API.dll"]
 
 
ENV ASPNETCORE_HTTP_PORTS=80