#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["UrbanClap.ServiceConsumer/UrbanClap.CustomerService.csproj", "UrbanClap.ServiceConsumer/"]
RUN dotnet restore "UrbanClap.ServiceConsumer/UrbanClap.CustomerService.csproj"
COPY . .
WORKDIR "/src/UrbanClap.ServiceConsumer"
RUN dotnet build "UrbanClap.CustomerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UrbanClap.CustomerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UrbanClap.CustomerService.dll"]
