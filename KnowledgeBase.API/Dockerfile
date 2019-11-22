FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["KnowledgeBase.API/KnowledgeBase.API.csproj", "KnowledgeBase.API/"]
COPY ["KnowledgeBase.Common/KnowledgeBase.Common.csproj", "KnowledgeBase.Common/"]
RUN dotnet restore "KnowledgeBase.API/KnowledgeBase.API.csproj"
COPY . .
WORKDIR "/src/KnowledgeBase.API"
RUN dotnet build "KnowledgeBase.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KnowledgeBase.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KnowledgeBase.API.dll"]