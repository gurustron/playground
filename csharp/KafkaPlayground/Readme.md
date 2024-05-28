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

##### Kafka APIs 
- KafkaStreams
  - This API was made to ensure that creating streaming applications is as easy as possible,
  - Kafka Streams takes the core parts of Kafka and works on top of those smaller pieces by adding stateful processing and distributed joins, for example, without much more complexity or overhead 
- Kafka Connect
  - Is designed to integrate well with stream-processing frameworks to copy data. 
  - Is an excellent choice for making quick and simple data pipelines that tie together common systems.
- AdminClient package
- ksqlDB
  - SQL engine for Kafka
  - Queries are continuously running and updating
- 

Stopped at Part 2 Applying Kafka p.66
