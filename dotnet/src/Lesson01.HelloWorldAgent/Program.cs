// Lesson 01 - Your first Microsoft Agent Framework agent (.NET)
//
// Setup:
//   1. dotnet add package Microsoft.Agents.AI.OpenAI --prerelease   (already added to this project)
//   2. Set an OPENAI_API_KEY environment variable with your OpenAI API key.
//   3. dotnet run

using Microsoft.Agents.AI;
using OpenAI;
using OpenAI.Chat;

string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")
    ?? throw new InvalidOperationException("Set the OPENAI_API_KEY environment variable before running this sample.");
string modelId = Environment.GetEnvironmentVariable("OPENAI_MODEL_ID") ?? "gpt-4o-mini";

// An AIAgent = a model client + instructions (+ optional tools, added in later lessons).
AIAgent agent = new OpenAIClient(apiKey)
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
