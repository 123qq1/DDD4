import os

import discord
from discord.ext import commands
import json
from tabulate import tabulate
import test_queue
import threading
import asyncio

class bot:

    dict = {'🔼':1,'🔽':-1,'⏫':2,'⏬':-2}

    current_standing = {}

    def handle_response(self, message: str, standings) -> str:
        p_message = message.lower()

        if p_message == 'test':
            return 'TEST'

        if p_message == 'standing':

            result = ""

            items = standings.items()

            col_names = ['User','Points']

            return tabulate(items, col_names,tablefmt="github")


        return 'Not a known command'


    async def send_message(self, message, user_message, is_private):
        try:
            response = self.handle_response(user_message,self.current_standing)
            await message.author.send(response) if is_private else await message.channel.send(response)

        except Exception as e:
            print(e)
    def set_rabbit(self,rabbit):
        self.rabbit = rabbit

    async def run_discord_bot(self):

        s = open('secret.json')
        data = json.load(s)
        TOKEN = data['bot_token']

        print(TOKEN)

        s.close()

        intents = discord.Intents.default()
        intents.message_content = True
        intents.reactions = True
        intents.members = True

        client = discord.Client(intents = intents)

        @client.event
        async def on_ready():
            print(f'{client.user} is now running')

        @client.event
        async def on_message(message):
            if message.author == client.user:
                return

            username = str(message.author)
            raw_message = str(message.content)
            message_type = raw_message[0]
            user_message = raw_message[1:]
            channel = str(message.channel)

            if isinstance(message.channel,discord.DMChannel):
                print("DM from ",message.author)
                print(type(self.rabbit))
                self.rabbit.ConfirmLink(raw_message,message.author)

            if message_type == '?':
                await self.send_message(message, user_message, is_private=True)
            elif message_type == '!':
                await self.send_message(message,user_message, is_private=False)

        @client.event
        async def on_reaction_add(reaction,user):
            if user == client.user:
                return

            user_message = str(reaction.emoji)

            if user_message not in dict.keys():
                return
            change = dict[user_message]

            user_name = str(reaction.message.author)

            if user_name not in self.current_standing.keys():
                self.current_standing[user_name] = change
            else:
                self.current_standing[user_name] += change


        print("Bot Starting")
        loop = asyncio.get_event_loop()
        rb = test_queue.rabbit(client,loop,self)

        self.set_rabbit(rb)

        print('connecting')
        rb.CheckService()
        print("connected")

        thread = threading.Thread(target=rb.run)
        thread.start()

        await client.start(TOKEN)

