```shell
docker exec -i kafkakraft kafka-topics --create --bootstrap-server kafkakraft:29092 --topic kinaction_helloworld --partitions 3 --replication-factor 1
```
Since we have single broker - can't have replicating factor more than 1

```shell
docker exec -i kafkakraft kafka-topics --list --bootstrap-server kafkakraft:29092 
```

```shell
docker exec -i kafkakraft kafka-topics --describe --topic kinaction_helloworld --bootstrap-server kafkakraft:29092 
```

#### Listing 2.4 Kafka producer console command
```shell
docker exec -i kafkakraft kafka-console-producer --topic kinaction_helloworld --bootstrap-server kafkakraft:29092 
```

#### Listing 2.4 Kafka consumer console command
```shell
docker exec -i kafkakraft kafka-console-consumer --topic kinaction_helloworld --from-beginning --bootstrap-server kafkakraft:29092 
```
remove `--from-beginning` to get only new ones since start


Stopped at 2.3.2 Topics overview - p. 51
