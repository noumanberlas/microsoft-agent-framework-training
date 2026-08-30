# MAF Training — .NET

## Setup

```bash
dotnet restore
setx OPENAI_API_KEY "sk-..."   # or set it in your shell session
```

## Run a lesson

```bash
dotnet run --project src/Lesson01.HelloWorldAgent
```

## Lessons

### Lesson 01 — Hello World Agent

Package: `Microsoft.Agents.AI.OpenAI` (verified against `1.19.0`, prerelease).

Demonstrates:
- Wrapping an `OpenAI.Chat.ChatClient` as an `AIAgent` via `.AsAIAgent(instructions:, name:)`
- Non-streaming run: `await agent.RunAsync(message)`
- Streaming run: `await foreach (var update in agent.RunStreamingAsync(message))`

Note: the extension method is named `AsAIAgent`, not `CreateAIAgent` — some older docs/samples online use the latter name from an earlier preview.
