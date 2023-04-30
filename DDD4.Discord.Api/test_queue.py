import pika

def connect():
    credentials = pika.PlainCredentials('guest','guest')

    parameters = pika.ConnectionParameters('rabbitmq',5672,'/',credentials)

    connection = pika.BlockingConnection(parameters)

    channel = connection.channel()
    channel.queue_declare(queue="LinkCustomer")
    channel.basic_consume(queue="LinkCustomer", auto_ack=True, on_message_callback=reciveMessage)

def reciveMessage(ch, method, properties, body):
    print(body)
