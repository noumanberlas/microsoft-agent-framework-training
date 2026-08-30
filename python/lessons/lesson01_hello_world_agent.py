"""Lesson 01 - Your first Microsoft Agent Framework agent (Python).

Setup:
    1. pip install -r requirements.txt   (uses --pre, since Agent Framework is in public preview)
    2. Set an OPENAI_API_KEY environment variable with your OpenAI API key.
    3. python lessons/lesson01_hello_world_agent.py
"""

import asyncio
import os

from agent_framework.openai import OpenAIChatClient


async def main() -> None:
    model_id = os.environ.get("OPENAI_MODEL_ID", "gpt-4o-mini")
    if not os.environ.get("OPENAI_API_KEY"):
        raise RuntimeError("Set the OPENAI_API_KEY environment variable before running this sample.")

    # An Agent = a chat client + instructions (+ optional tools, added in later lessons).
    agent = OpenAIChatClient(model=model_id).as_agent(
        name="Assistant",
        instructions="You are a helpful assistant.",
    )

    # Non-streaming run: wait for the full response.
    response = await agent.run("What's the capital of France?")
    print(response)

    # Streaming run: print chunks as they arrive.
    print("\nStreaming: ", end="", flush=True)
    async for update in agent.run("Now give me one fun fact about that city.", stream=True):
        print(update, end="", flush=True)
    print()


if __name__ == "__main__":
    asyncio.run(main())
