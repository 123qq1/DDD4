FROM python:3.9-slim-buster

COPY /DDD4.Discord.Api/. app/.

RUN pip3 install discord.py
RUN pip3 install tabulate
RUN pip3 install pika==1.2.1
RUN pip3 install vaderSentiment

CMD ["python3", "app/run.py"]