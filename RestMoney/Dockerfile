FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY [ "RestMoney.csproj", "./" ]
RUN dotnet restore "RestMoney.csproj"
COPY . .
RUN dotnet publish "RestMoney.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT [ "dotnet", "RestMoney.dll" ]
