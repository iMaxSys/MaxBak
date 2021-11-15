#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["samples/1.Presentation/Kylin.Api/Kylin.Api.csproj", "samples/1.Presentation/Kylin.Api/"]
RUN dotnet restore "samples/1.Presentation/Kylin.Api/Kylin.Api.csproj"
COPY . .
WORKDIR "/src/samples/1.Presentation/Kylin.Api"
RUN dotnet build "Kylin.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kylin.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kylin.Api.dll"]