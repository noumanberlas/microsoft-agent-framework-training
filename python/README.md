# MAF Training — Python

## Setup

```bash
python -m venv .venv
.venv\Scripts\activate          # Windows
pip install -r requirements.txt # note: requirements.txt sets --pre, required while in preview
copy .env.example .env          # then fill in OPENAI_API_KEY
```

## Run a lesson

```bash
python lessons/lesson01_hello_world_agent.py
```

## Lessons

### Lesson 01 — Hello World Agent

Package: `agent-framework-azure-ai` (verified against `agent-framework-core 1.16.0`, prerelease `1.0.0rc6`).

Demonstrates:
- Wrapping an `agent_framework.openai.OpenAIChatClient` as an agent via `.as_agent(name=, instructions=)`
- Non-streaming run: `await agent.run(message)`
- Streaming run: `async for update in agent.run(message, stream=True)`

Note: there's a single unified `run()` method with a `stream=` flag — there is no separate `run_stream()` method (that's a .NET-only naming split).
