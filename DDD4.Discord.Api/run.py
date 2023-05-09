import test_bot
import test_queue
import asyncio

async def start(c,t):
    await c.start(t)

if __name__ == '__main__':
    db = test_bot.bot()
    loop = asyncio.get_event_loop()
    loop.run_until_complete(db.run_discord_bot())





