FROM node

WORKDIR /app

COPY . .
RUN npm i

EXPOSE 1337
ENTRYPOINT ["npm", "start"]