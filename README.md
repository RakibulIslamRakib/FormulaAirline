Ticketing Service with RabbitMQ and Kafka
Project Overview
This project implements a simple ticketing service that utilizes both RabbitMQ and Kafka for message handling and processing. It serves as a learning platform for understanding how to integrate and use these two popular messaging systems in a microservices architecture.

Technologies Used
C#: Programming language for building the service.
RabbitMQ: Message broker for handling queues and message routing.
Kafka: Distributed event streaming platform for processing high-throughput data.
.NET 8: Framework for building the application.
Features
RabbitMQ Consumer: Listens for ticket creation messages and processes them.
Kafka Consumer: Listens for events related to ticket status updates and processes them.
Simple CLI Interface: Provides a console interface for displaying messages being processed.
Getting Started
Prerequisites
Before you begin, ensure you have the following installed:

.NET 8 SDK
RabbitMQ (and RabbitMQ Management Plugin for UI)
Apache Kafka (including Zookeeper)
Docker (optional, for containerized environments)
Setup Instructions
1. Clone the Repository(bash)
![image](https://github.com/user-attachments/assets/5d32c173-09bc-477a-84ba-fe495f8ca046)

2. Start RabbitMQ
You can run RabbitMQ using Docker:
![image](https://github.com/user-attachments/assets/520dece5-39d9-4e8a-ab28-bae5a304fa59)

Access the RabbitMQ management UI at http://localhost:15672 with the default username and password (guest / guest).

3. Start Kafka and Zookeeper
If you prefer Docker, you can set up Kafka and Zookeeper using a Docker Compose file. Here’s an example docker-compose.yml:
version: '3.8'
services:
  rabbitmq:
    container_name: "rabbitmq"
    image: rabbitmq:3.8-management-alpine
    environment:
      - RABBITMQ_DEFAULT_USER=guest
      - RABBITMQ_DEFAULT_PASS=guest
    ports:
      - '5672:5672'
      - '15672:15672'
      
  zookeeper:
    image: confluentinc/cp-zookeeper:latest
    environment:
      ZOOKEEPER_CLIENT_PORT: 2181
    ports:
      - "2181:2181"

  kafka:
    image: confluentinc/cp-kafka:latest
    depends_on:
      - zookeeper
    environment:
      KAFKA_BROKER_ID: 1
      KAFKA_ZOOKEEPER_CONNECT: zookeeper:2181
      KAFKA_LISTENER_SECURITY_PROTOCOL_MAP: PLAINTEXT:PLAINTEXT
      KAFKA_ADVERTISED_LISTENERS: PLAINTEXT://localhost:9092
      KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR: 1
    ports:
      - "9092:9092"

![image](https://github.com/user-attachments/assets/8b943e4c-49d8-4e20-8822-71434c3cdc37)

6. Consume Messages
The application will now consume messages from both RabbitMQ and Kafka. You should see output in the console for any messages being processed.

Learning Objectives
Understand the differences between RabbitMQ and Kafka and how to implement them in a project.
Learn how to produce and consume messages using RabbitMQ and Kafka.
Gain insights into real-time data processing and message handling.
Conclusion
This project serves as a practical demonstration of using both RabbitMQ and Kafka in a ticketing service. You can explore further by modifying the message processing logic or adding additional features.

License
This project is licensed under the MIT License - see the LICENSE file for details.
Acknowledgments
RabbitMQ Documentation
Kafka Documentation
Docker Documentation
