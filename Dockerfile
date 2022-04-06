FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
    
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MyFeedback.Webapi/MyFeedback.csproj", "MyFeedback.Webapi/"]
RUN dotnet restore "MyFeedback.Webapi/MyFeedback.csproj"
COPY ./MyFeedback.Webapi ./MyFeedback
WORKDIR "/src/MyFeedback"
RUN dotnet build "MyFeedback.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyFeedback.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

CMD ASPNETCORE_URLS="https://*:$PORT" dotnet MyFeedback.dll
