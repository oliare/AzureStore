# asp-store

Create docker hub repository - publish
```
docker build -t asp-store-api . 
docker run -it --rm -p 5817:8080 --name asp-store_container asp-store-api
docker run -d --restart=always --name asp-store_container -p 5817:8080 asp-store-api
docker run -d --restart=always -v d:/volumes/asp-store/images:/app/images --name asp-store_container -p 5817:8080 asp-store-api
docker run -d --restart=always -v /volumes/asp-store/images:/app/images --name asp-store_container -p 5817:8080 asp-store-api
docker ps -a
docker stop asp-store_container
docker rm asp-store_container

docker images --all
docker rmi asp-store-api

docker login
docker tag asp-store-api:latest olia1/asp-store-api:latest
docker push olia1/asp-store-api:latest

docker pull olia1/asp-store-api:latest
docker ps -a
docker run -d --restart=always --name asp-store_container -p 5817:8080 olia1/asp-store-api

docker run -d --restart=always -v /volumes/asp-store/images:/app/images --name asp-store_container -p 5817:8080 olia1/asp-store-api


docker pull olia1/asp-store-api:latest
docker images --all
docker ps -a
docker stop asp-store_container
docker rm asp-store_container
docker run -d --restart=always --name asp-store_container -p 5817:8080 olia1/asp-store-api
```

```nginx options /etc/nginx/sites-available/default
server {
    server_name   api-asp-store.itstep.click *.api-asp-store.itstep.click;
    location / {
       proxy_pass         http://localhost:5817;
       proxy_http_version 1.1;
       proxy_set_header   Upgrade $http_upgrade;
       proxy_set_header   Connection keep-alive;
       proxy_set_header   Host $host;
       proxy_cache_bypass $http_upgrade;
       proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
       proxy_set_header   X-Forwarded-Proto $scheme;
    }
}

server {
		server_name   qubix.itstep.click *.qubix.itstep.click;
		root /var/dist;
		index index.html;

		location / {
			try_files $uri /index.html;
			#try_files $uri $uri/ =404;
		}
}

server {
		server_name   admin-qubix.itstep.click *.admin-qubix.itstep.click;
		root /var/admin-qubix.itstep.click;
		index index.html;

		location / {
			try_files $uri /index.html;
			#try_files $uri $uri/ =404;
		}
}

sudo systemctl restart nginx
certbot
```

/var/api-qubix.itstep.click/