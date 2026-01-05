# Docker

```sh
docker rm auth
docker rmi auth
docker build -t auth:latest -f Dockerfile ../ # run from current folder
docker run --rm -p 4501:8080 --name auth-api auth-api:latest # http://localhost:4501
docker exec -it auth /bin/bash
```
