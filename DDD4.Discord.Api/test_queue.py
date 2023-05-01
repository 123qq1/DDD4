import pika
import time
import socket

class rabbit:

    def __init__(self, client):
        self.client = client
        self.credentials = pika.PlainCredentials('guest','guest')
        self.props = {'connection_name': 'DDD4.Discord.Api'}

        self.parameters = pika.ConnectionParameters('rabbitmq',virtual_host='/',
                                                    credentials=self.credentials, heartbeat=50,
                                                    client_properties=self.props)

        self.connection = pika.SelectConnection(self.parameters,self.on_connected)

    def on_connected(self, connection):
        """
        Called by pika when a connection is established.
        """
        print("channel")
        self.connection.channel(on_open_callback=self.on_channel_open)

    def on_channel_open(self, channel):
        """
        Called by pika when the channel is opened.
        """
        print("consuming")
        self.channel = channel
        self.channel.basic_qos(prefetch_count=10)
        self.channel.add_on_close_callback(self.on_channel_closed)
        self.start_consuming()

    def on_channel_closed(self, channel, reply_code, reply_text):
        """
        Called by pika when the channel is closed.
        """
        print(reply_text)

        self.connection.close()

    def on_consumer_cancelled(self, frame):
        """
        Called by pika when the RabbitMQ connection is lost.
        """
        print("Cancelled")
        self.channel.close()

    def start_consuming(self):
        """
        Start consuming messages.
        """
        self.channel.add_on_cancel_callback(self.on_consumer_cancelled)
        self.consumer_tag = self.channel.basic_consume(queue="LinkCustomer",
                                                       auto_ack=True, on_message_callback=self.reciveMessage)


    def reciveMessage(self, ch, method, properties, body):
        print(body)

        print(type(body))
        #user = self.client.get_user("123qq1#4884")
        #user.send(body)

    def CheckService(self):
        pingcounter = 0
        isreachable = False
        while isreachable is False and pingcounter < 5:
            s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            try:
                s.connect(('rabbitmq', 5672))
                isreachable = True
            except socket.error as e:
                time.sleep(15)
                pingcounter += 1
            s.close()

    def run(self):
        self.connection.ioloop.start()