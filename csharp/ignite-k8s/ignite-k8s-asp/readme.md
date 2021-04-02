from root
```
docker build -t ignite-k8s-asp -f ignite-k8s-asp/Dockerfile ./ignite-k8s-asp/
```

use local docker in minikube
```
eval $(minikube docker-env)
```

```
kubectl apply -f deployment-ignite-k8s-app.yml
kubectl apply -f service-ignite-k8s-app.yml
```

Requests in busybox
```
kubectl run -it --rm --restart=Never busybox --image=yauritux/busybox-curl sh

curl http://172.17.0.3:80/IgniteNodeCount
curl http://k8s-ignite-service:7777/IgniteNodeCount
```

```
kubectl expose deployment k8s-ignite-app  --port=80 --type=NodePort
minikube service k8s-ignite-app
```


https://192.168.49.2/api/v1/namespaces/default/endpoints/k8s-ignite-service 