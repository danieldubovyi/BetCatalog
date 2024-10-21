# Stage 1: Build
FROM node:18 AS build
WORKDIR /app
ENV NODE_ENV=production
COPY betcatalog.webui/package.json ./
COPY betcatalog.webui/package-lock.json ./
COPY betcatalog.webui/vite.config.ts ./
COPY betcatalog.webui/tsconfig.json ./
COPY betcatalog.webui/tsconfig.app.json ./
COPY betcatalog.webui/tsconfig.node.json ./
COPY betcatalog.webui/index.html ./

COPY betcatalog.webui/src/* ./src/
COPY betcatalog.webui/src/assets/* ./src/assets/
COPY betcatalog.webui/src/Accounts/* ./src/Accounts/
COPY betcatalog.webui/src/Persons/* ./src/Persons/

# Install dependencies (including devDependencies for the build)
RUN npm install --include=dev

# Run the build process
RUN npm run build

# Stage 2: Serve
FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
