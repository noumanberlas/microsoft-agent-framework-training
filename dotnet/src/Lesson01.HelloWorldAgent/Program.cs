// Lesson 01 - Your first Microsoft Agent Framework agent (.NET)
//
// Setup:
//   1. dotnet add package Microsoft.Agents.AI.OpenAI --prerelease   (already added to this project)
//   2. Set FOUNDRY_PROJECT_ENDPOINT, FOUNDRY_API_KEY, and FOUNDRY_MODEL environment variables.
//   3. dotnet run

using System.ClientModel;
using DotNetEnv;
using Microsoft.Agents.AI;
using OpenAI;
using OpenAI.Chat;

// Load the nearest .env by walking up from the output directory (so lessons run without setx).
for (DirectoryInfo? dir = new(AppContext.BaseDirectory); dir is not null; dir = dir.Parent)
{
    string candidate = Path.Combine(dir.FullName, ".env");
    if (File.Exists(candidate))
    {
        Env.Load(candidate);
        break;
    }
}

string projectEndpoint = Environment.GetEnvironmentVariable("FOUNDRY_PROJECT_ENDPOINT")
    ?? throw new InvalidOperationException("Set the FOUNDRY_PROJECT_ENDPOINT environment variable before running this sample.");
string apiKey = Environment.GetEnvironmentVariable("FOUNDRY_API_KEY")
    ?? throw new InvalidOperationException("Set the FOUNDRY_API_KEY environment variable before running this sample.");
string modelId = Environment.GetEnvironmentVariable("FOUNDRY_MODEL")
    ?? throw new InvalidOperationException("Set the FOUNDRY_MODEL environment variable before running this sample.");

// An AIAgent = a model client + instructions (+ optional tools, added in later lessons).
AIAgent agent = new OpenAIClient(
    new ApiKeyCredential(apiKey),
    new OpenAIClientOptions { Endpoint = new Uri($"{projectEndpoint.TrimEnd('/')}/openai/v1/") })
    .GetChatClient(modelId)
    .AsAIAgent(instructions: "You are a helpful assistant.", name: "Assistant");

// Non-streaming run: wait for the full response.
Console.WriteLine(await agent.RunAsync("What's the capital of France?"));

// Streaming run: print tokens/chunks as they arrive.
Console.WriteLine();
Console.Write("Streaming: ");
await foreach (var update in agent.RunStreamingAsync("Now give me one fun fact about that city."))
{
    Console.Write(update);
}
Console.WriteLine();
