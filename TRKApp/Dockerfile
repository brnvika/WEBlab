# Этап 1: Сборка приложения
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Копируем csproj и восстанавливаем зависимости
COPY *.csproj ./
RUN dotnet restore

# Копируем весь проект и собираем
COPY . ./
RUN dotnet build -c Release -o /app/build

# Этап 2: Публикация приложения
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Этап 3: Финальный образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Копируем опубликованное приложение
COPY --from=publish /app/publish .

# Копируем дополнительные файлы проекта (Pages, base, images, styles, scripts)
COPY --from=build /app/Pages ./Pages
COPY --from=build /app/base ./base
COPY --from=build /app/images ./images
COPY --from=build /app/styles ./styles
COPY --from=build /app/scripts ./scripts

# Устанавливаем переменные окружения
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Запускаем приложение
ENTRYPOINT ["dotnet", "TRKApp.dll"]
