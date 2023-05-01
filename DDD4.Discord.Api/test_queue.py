import pika
import time
import socket

class rabbit:

    def __init__(self, client):
        self.client = client

    def connect(self):
        credentials = pika.PlainCredentials('guest','guest')

        parameters = pika.ConnectionParameters('rabbitmq',virtual_host='/',credentials=credentials)

        connection = pika.BlockingConnection(parameters)

        channel = connection.channel()
        channel.basic_consume(queue="LinkCustomer", auto_ack=True, on_message_callback=self.reciveMessage)
        print(f'RabbitMQ is now running')

    def reciveMessage(self, ch, method, properties, body):
        print(body)
        user = self.client.get_user("123qq1#4884")
        user.send(body)


    def CheckService(self):
        pingcounter = 0
        isreachable = False
        while isreachable is False and pingcounter < 5:
            s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            try:
                s.connect(('rabbitmq', 5672))
                isreachable = True
            except socket.error as e:
                time.sleep(3)
                pingcounter += 1
            s.close()