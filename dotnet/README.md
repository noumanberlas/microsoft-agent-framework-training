# MAF Training — .NET

## Setup

```bash
dotnet restore
setx FOUNDRY_PROJECT_ENDPOINT "https://<resource>.services.ai.azure.com/api/projects/<project>"
setx FOUNDRY_API_KEY "<foundry-api-key>"
setx FOUNDRY_MODEL "gpt-4o"
```

## Run a lesson

```bash
dotnet run --project src/Lesson01.HelloWorldAgent
```

## Lessons

### Lesson 01 — Hello World Agent

Package: `Microsoft.Agents.AI.OpenAI` (verified against `1.19.0`, prerelease).

Demonstrates:
- Connecting `OpenAIClient` to a Microsoft Foundry project endpoint
- Wrapping an `OpenAI.Chat.ChatClient` as an `AIAgent` via `.AsAIAgent(instructions:, name:)`
- Non-streaming run: `await agent.RunAsync(message)`
- Streaming run: `await foreach (var update in agent.RunStreamingAsync(message))`

Note: the extension method is named `AsAIAgent`, not `CreateAIAgent` — some older docs/samples online use the latter name from an earlier preview.
