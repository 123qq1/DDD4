import test_bot
import test_queue
import asyncio

async def start(c,t):
    await c.start(t)

if __name__ == '__main__':
    db = test_bot.bot()

    (client, token) = db.run_discord_bot()
    print("test")
    rb = test_queue.rabbit(client)
    rb.CheckService()
    print("connected")
    rb.run()
    #client.run(token)
    #asyncio.create_task(start(client,token))




