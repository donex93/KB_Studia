FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["KnowledgeBase.Services.Articles/KnowledgeBase.Services.Articles.csproj", "KnowledgeBase.Services.Articles/"]
COPY ["KnowledgeBase.Common/KnowledgeBase.Common.csproj", "KnowledgeBase.Common/"]
RUN dotnet restore "KnowledgeBase.Services.Articles/KnowledgeBase.Services.Articles.csproj"
COPY . .
WORKDIR "/src/KnowledgeBase.Services.Articles"
RUN dotnet build "KnowledgeBase.Services.Articles.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KnowledgeBase.Services.Articles.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KnowledgeBase.Services.Articles.dll"]