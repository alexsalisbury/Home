FROM node:12

COPY package.json /app
RUN npm i

COPY . /app
WORKDIR /app

EXPOSE 80
ENTRYPOINT ["npm", "start"]