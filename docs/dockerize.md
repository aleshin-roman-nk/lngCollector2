# 🚀 Deployment Instructions

```bash
# =========================
# 🔑 Подключение к серверу
# =========================
ssh root@45.131.41.112


# =========================
# 📂 Передача файлов
# =========================
sftp root@45.131.41.112
# навигация к нужной директории
put d:\lng2webapi\appsettings.json   # путь на локальном компьютере


# =========================
# 🛠 Web API
# =========================

# Сборка и публикация
docker build -t kalinared/lng2webapi:latest .
docker push kalinared/lng2webapi:latest
docker pull kalinared/lng2webapi:latest

# Запуск контейнера (разные варианты)
docker run --rm --name lng2webapi-container -d -p 9999:8080 \
  --network my-network -e DB_IP=mysql-container kalinared/lng2webapi

docker run --rm --name lng2webapi-container -d -p 9999:8080 \
  --network my-network \
  -v /lng2webapi/appsettings.json:/app/appsettings.json kalinared/lng2webapi

docker run --rm --name lng2webapi-container -d -p 9999:8080 \
  -v d:/lng2webapi/appsettings.json:/app/appsettings.json kalinared/lng2webapi:latest


# =========================
# 🌐 Web App (Front-end)
# =========================

# Сборка и публикация
docker build -t kalinared/lng2front:latest .
docker push kalinared/lng2front:latest
docker pull kalinared/lng2front:latest


# =========================
# ⚙️ Начальная настройка
# =========================

# 1. Создать сеть
docker network create my-network

# 2. Создать контейнер базы данных
docker run --rm --name mysql-container -d -p 3306:3306 \
  --network my-network \
  -e MYSQL_ROOT_PASSWORD=<set> \
  -v /mysql-data:/var/lib/mysql mysql

# 3. Создать контейнер Web API
docker run --rm --name lng2webapi-container -d -p 9999:8080 kalinared/lng2webapi

# 4. Создать контейнер Front-end
docker run --rm --name lng2front-container -d -p 80:80 \
  --network my-network kalinared/lng2front

docker run --rm --name lng2front-container -d -p 80:80 kalinared/lng2front


# =========================
# ✅ Проверка
# =========================

# API доступ
http://45.131.41.112:9999/api/Computers

# Проверка curl внутри контейнера
docker exec -it frontexamp-container curl http://webapimysql-container:9999/api/Computers
docker exec -it frontexamp-container curl webapimysql-container:8080/api/Computers
