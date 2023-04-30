import test_bot
import test_queue

if __name__ == '__main__':
    test_queue.connect()
    test_bot.run_discord_bot()