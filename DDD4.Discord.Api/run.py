import test_bot
import test_queue
import asyncio

def start(c, t):
    await c.start(t)

if __name__ == '__main__':
    db = test_bot.bot()

    (client, token) = db.run_discord_bot()

    print("test")
    rb = test_queue.rabbit(client)
    rb.CheckService()
    print("test")
    rb.connect()
    asyncio.run(start(client,token))




