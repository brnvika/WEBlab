# Docker-развертывание TRKApp

## Предварительные требования

- Установлен [Docker Desktop](https://www.docker.com/products/docker-desktop) для Windows
- Docker запущен

## Способы запуска

### Вариант 1: С помощью Docker Compose (рекомендуется)

```powershell
# Собрать и запустить контейнер
docker-compose up --build

# Или в фоновом режиме
docker-compose up -d --build

# Остановить контейнер
docker-compose down
```

### Вариант 2: С помощью Docker CLI

```powershell
# Собрать образ
docker build -t trkapp:latest .

# Запустить контейнер
docker run -d -p 5000:8080 --name trkapp_container trkapp:latest

# Просмотр логов
docker logs trkapp_container

# Остановить и удалить контейнер
docker stop trkapp_container
docker rm trkapp_container
```

## Доступ к приложению

После запуска приложение будет доступно по адресу:
- **http://localhost:5000**

## Полезные команды

```powershell
# Список запущенных контейнеров
docker ps

# Список всех образов
docker images

# Просмотр логов в реальном времени
docker logs -f trkapp_container

# Зайти внутрь контейнера
docker exec -it trkapp_container /bin/bash

# Удалить образ
docker rmi trkapp:latest

# Очистить неиспользуемые образы и контейнеры
docker system prune -a
```

## Структура Dockerfile

- **Этап 1 (build)**: Использует SDK-образ .NET 9.0 для сборки проекта
- **Этап 2 (publish)**: Публикует приложение в Release-конфигурации
- **Этап 3 (final)**: Использует легкий runtime-образ aspnet:9.0 для запуска

Такой многоэтапный подход уменьшает финальный размер образа.

## Переменные окружения

В `docker-compose.yml` можно настроить:
- `ASPNETCORE_ENVIRONMENT` - окружение (Development/Production)
- `ASPNETCORE_URLS` - адреса для прослушивания

## Проблемы и решения

**Порт уже занят:**
```powershell
# Измените порт в docker-compose.yml, например:
ports:
  - "5001:8080"  # Вместо 5000
```

**Образ не собирается:**
```powershell
# Убедитесь, что Docker Desktop запущен
# Проверьте, что вы находитесь в папке проекта
cd "c:\Users\victo\OneDrive\Документы\Проекты WEB\Обучение\WEBlab1\TRKApp"
```
