import os

import discord
from discord.ext import commands
import json
from tabulate import tabulate

dict = {'🔼':1,'🔽':-1,'⏫':2,'⏬':-2}

current_standing = {}


def handle_response(message: str, standings) -> str:
    p_message = message.lower()

    if p_message == 'test':
        return 'TEST'

    if p_message == 'standing':

        result = ""

        items = standings.items()

        col_names = ['User','Points']

        return tabulate(items, col_names,tablefmt="github")


    return 'Not a known command'


async def send_message(message, user_message, is_private):
    try:
        response = handle_response(user_message,current_standing)
        await message.author.send(response) if is_private else await message.channel.send(response)

    except Exception as e:
        print(e)

def run_discord_bot():

    s = open('app/secret.json')
    data = json.load(s)
    TOKEN = data['bot_token']

    print(TOKEN)

    s.close()

    intents = discord.Intents.default()
    intents.message_content = True
    intents.reactions = True

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

        if message_type == '?':
            await send_message(message, user_message, is_private=True)
        elif message_type == '!':
            await send_message(message,user_message, is_private=False)

    @client.event
    async def on_reaction_add(reaction,user):
        if user == client.user:
            return

        user_message = str(reaction.emoji)

        if user_message not in dict.keys():
            return
        change = dict[user_message]

        user_name = str(reaction.message.author)

        if user_name not in current_standing.keys():
            current_standing[user_name] = change
        else:
            current_standing[user_name] += change

    async def dm(user_id, message):
        user = client.get_user(user_id)
        await user.send(message)

    client.run(TOKEN)

