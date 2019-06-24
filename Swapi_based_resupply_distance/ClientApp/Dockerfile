FROM node:11

WORKDIR /usr/src/app

COPY ./Swapi_based_resupply_distance/ClientApp/package*.json ./

RUN npm install
COPY ./Swapi_based_resupply_distance/ClientApp/ .
RUN ls

ENV PORT 80
EXPOSE 80   
CMD [ "npm", "start" ]