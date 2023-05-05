import pika
import time
import socket
import json
import asyncio
from pika.exchange_type import ExchangeType

class rabbit:

    def __init__(self, client,loop, script):
        self.script = script
        self.client = client
        self.credentials = pika.PlainCredentials('guest','guest')
        self.props = {'connection_name': 'DDD4.Discord.Api'}
        self.loop = loop
        self.parameters = pika.ConnectionParameters('rabbit',virtual_host='/',
                                                    credentials=self.credentials, heartbeat=50,
                                                    client_properties=self.props)

        self.connection = pika.SelectConnection(self.parameters,self.on_connected)
        self.currentKeys = {}

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
        self.channel = channel
        self.channel.basic_qos(prefetch_count=10)
        self.channel.add_on_close_callback(self.on_channel_closed)
        self.setup_exchange()

    def on_channel_closed(self, channel, reason):
        """
        Called by pika when the channel is closed.
        """
        print(reason)

        self.connection.close()

    def on_consumer_cancelled(self, frame):
        """
        Called by pika when the RabbitMQ connection is lost.
        """
        print("Cancelled")
        self.channel.close()

    def setup_exchange(self):
        """Setup the exchange on RabbitMQ by invoking the Exchange.Declare RPC
        command. When it is complete, the on_exchange_declareok method will
        be invoked by pika.
        :param str|unicode exchange_name: The name of the exchange to declare
        """

        self.channel.exchange_declare(
            exchange='DDD4.Contracts:LinkCustomer',
            durable=True,
            exchange_type=ExchangeType.fanout,
            callback=self.on_exchange_declareok)

    def on_exchange_declareok(self, _unused_frame):
        """Invoked by pika when RabbitMQ has finished the Exchange.Declare RPC
        command.
        :param pika.Frame.Method unused_frame: Exchange.DeclareOk response frame
        :param str|unicode userdata: Extra user data (exchange name)
        """
        self.setup_queue()

    def setup_queue(self):
        """Setup the queue on RabbitMQ by invoking the Queue.Declare RPC
        command. When it is complete, the on_queue_declareok method will
        be invoked by pika.
        :param str|unicode queue_name: The name of the queue to declare.
        """
        self.channel.queue_declare(queue='LinkCustomer', callback=self.on_queue_declareok)

    def on_queue_declareok(self, _unused_frame):
        """Method invoked by pika when the Queue.Declare RPC call made in
        setup_queue has completed. In this method we will bind the queue
        and exchange together with the routing key by issuing the Queue.Bind
        RPC command. When this command is complete, the on_bindok method will
        be invoked by pika.
        :param pika.frame.Method _unused_frame: The Queue.DeclareOk frame
        :param str|unicode userdata: Extra user data (queue name)
        """
        self.channel.queue_bind(
            'LinkCustomer',
            'DDD4.Contracts:LinkCustomer',
            callback=self.on_bindok)

    def on_bindok(self, _unused_frame):
        """Invoked by pika when the Queue.Bind method has completed. At this
        point we will set the prefetch count for the channel.
        :param pika.frame.Method _unused_frame: The Queue.BindOk response frame
        :param str|unicode userdata: Extra user data (queue name)
        """
        self.start_consuming()

    def start_consuming(self):
        """
        Start consuming messages.
        """
        print("consuming")
        time.sleep(5)

        self.channel.add_on_cancel_callback(self.on_consumer_cancelled)
        self.consumer_tag = self.channel.basic_consume(queue="LinkCustomer",
                                                       auto_ack=True, on_message_callback=self.reciveMessage)


    def reciveMessage(self, ch, method, properties, body):

        print("message")

        body_js = json.loads(body)

        message = body_js['message']

        id = message['customerId']
        name = message['customerName']
        d_name = message['discordName']
        a_name = message['accountName']
        key = message['linkingKey']
        self.currentKeys[d_name] = (body_js,key)

        print(message)

        try:
            (user,discrim) = d_name.split("#")

            d_user = next(filter(lambda u: u.name == user and u.discriminator == discrim, self.client.users))
            print(d_user.id,d_user.name)
            self.MessageToDiscord("Please respond with correct linking key.",d_user)
        except:
            print("Discord name not valid.")

    def PublishMessage(self, body_js):

        body_js['destinationAddress'] = 'rabbitmq://rabbitmq/DDD4.Contracts:CustomerLinked'
        body_js['sourceAddress'] = 'rabbitmq://rabbitmq/LinkCustomer'
        body_js['messageType'] = ['urn:message:DDD4.Contracts:CustomerLinked']

        print(body_js)

        self.channel.basic_publish('DDD4.Contracts:CustomerLinked','',body=json.dumps(body_js,ensure_ascii=False));

    def MessageToDiscord(self,message,user):
        asyncio.run_coroutine_threadsafe(user.send(message),self.loop)

    def ConfirmLink(self, key,user):

        print("Confirming Link")

        try:
            (body,true_key) = self.currentKeys[user.name +"#" +user.discriminator]
            print("Key :",key,true_key)
            if key == true_key:
                self.PublishMessage(body)
                self.MessageToDiscord("Link Confirmed",user)
        except:
            self.MessageToDiscord("Wrong key",user)

    def CheckService(self):
        pingcounter = 0
        isreachable = False
        while isreachable is False and pingcounter < 5:
            s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            try:
                s.connect(('rabbit', 5672))
                isreachable = True
            except socket.error as e:
                time.sleep(30)
                pingcounter += 1
            s.close()

    def run(self):
        self.connection.ioloop.start()
