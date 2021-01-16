FROM node:12

WORKDIR /app

COPY . .
RUN npm i

EXPOSE 80
ENTRYPOINT ["npm", "start"]