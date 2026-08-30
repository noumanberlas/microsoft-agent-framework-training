# Microsoft Agent Framework Training

Master code for a lesson-by-lesson walkthrough of the [Microsoft Agent Framework](https://github.com/microsoft/agent-framework) (public preview), covering both **.NET** and **Python** side by side.

Each lesson gets one project per language, numbered `Lesson01`, `Lesson02`, ... so the two implementations always stay directly comparable.

## Structure

```
maf-training/
  dotnet/                  .NET solution
    MafTraining.sln
    src/
      Lesson01.HelloWorldAgent/
  python/                  Python project
    requirements.txt
    lessons/
      lesson01_hello_world_agent.py
```

## Lessons

| # | Topic | .NET | Python |
|---|---|---|---|
| 01 | Your first agent (hello world, run, streaming) | [Lesson01.HelloWorldAgent](dotnet/src/Lesson01.HelloWorldAgent) | [lesson01_hello_world_agent.py](python/lessons/lesson01_hello_world_agent.py) |

More lessons (threads, function tools, structured output, agent backends, multi-agent orchestration, workflows, middleware/observability) will be added here as we go.

## Prerequisites

- An OpenAI API key (`OPENAI_API_KEY` environment variable) — used for the simplest possible "hello world" backend. Later lessons also cover Azure OpenAI and Microsoft Foundry (`AIProjectClient`) as alternative backends.
- .NET 8+ SDK
- Python 3.10+

See [dotnet/README.md](dotnet/README.md) and [python/README.md](python/README.md) for language-specific setup.

## Note on the SDK

Microsoft Agent Framework is in **public preview**. Both the .NET packages (`--prerelease`) and the Python packages (`--pre`) ship pre-1.0 and the API surface can change between releases. Each lesson's README notes the exact package versions it was verified against.
