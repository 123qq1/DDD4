FROM python:3.9-slim-buster

COPY /DDD4.Discord.Api/. app/.

RUN pip3 install discord.py
RUN pip3 install tabulate

CMD ["python3", "app/run.py"]