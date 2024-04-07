FROM node:20-alpine as build
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm i
COPY . .
RUN npm run build

# docker build -t tukko:latest .
# docker run -d --name tukko -p 5173:80 tukko:latest
# OR
# npm run prod
FROM nginx:latest

# Copy custom NGINX configuration file
COPY nginx.conf /etc/nginx/nginx.conf

COPY --from=build /app/dist /usr/share/nginx/html

# Start NGINX when the container starts
CMD ["nginx", "-g", "daemon off;"]
