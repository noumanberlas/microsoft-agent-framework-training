"""Lesson 01 - Your first Microsoft Agent Framework agent (Python).
   (uses --pre, since Agent Framework is in public preview)
    2. Set FOUNDRY_PROJECT_ENDPOINT, FOUNDRY_API_KEY, and FOUNDRY_MODEL environment variables.
    3. python lessons/lesson01_hello_world_agent.py
"""

import asyncio
import os
from pathlib import Path

from agent_framework.openai import OpenAIChatClient
from dotenv import load_dotenv

# Load vars from the root .env (one level above python/), then any local python/.env.
load_dotenv(Path(__file__).resolve().parents[2] / ".env")
load_dotenv()


async def main() -> None:
    project_endpoint = os.environ.get("FOUNDRY_PROJECT_ENDPOINT")
    api_key = os.environ.get("FOUNDRY_API_KEY")
    model_id = os.environ.get("FOUNDRY_MODEL")
    if not project_endpoint or not api_key or not model_id:
        raise RuntimeError(
            "Set FOUNDRY_PROJECT_ENDPOINT, FOUNDRY_API_KEY, and FOUNDRY_MODEL before running this sample."
        )

    # An Agent = a chat client + instructions (+ optional tools, added in later lessons).
    agent = OpenAIChatClient(
        model=model_id,
        api_key=api_key,
        base_url=f"{project_endpoint.rstrip('/')}/openai/v1/",
    ).as_agent(
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
