# MAF Training

Lesson-by-lesson walkthrough of the [Microsoft Agent Framework](https://github.com/microsoft/agent-framework) (public preview), with a **.NET** and a **Python** implementation of every lesson side by side so the two stay directly comparable.

## Structure

```
maf-training/
  dotnet/                          .NET solution (MafTraining.slnx)
    README.md                      .NET setup + per-lesson notes
    src/
      Lesson01.HelloWorldAgent/    one project per lesson
  python/                          Python project
    README.md                      Python setup + per-lesson notes
    requirements.txt
    .env.example                   template for python/.env (not committed)
    lessons/
      lesson01_hello_world_agent.py  one script per lesson
  .env                              root env vars used to run BOTH sides locally (gitignored)
```

Lessons are numbered identically on both sides (`Lesson01` / `lesson01_...`) and the root `README.md` has a table mapping lesson number -> topic -> .NET path -> Python path. When adding a lesson, both implementations and that table are meant to move together.

Only Lesson01 (hello world agent: non-streaming + streaming run) exists today. Planned topics per the root README: threads, function tools, structured output, agent backends, multi-agent orchestration, workflows, middleware/observability.

## Stack

- **.NET**: net10.0, `Microsoft.Agents.AI.OpenAI` (verified 1.19.0, prerelease — installed with `--prerelease`). Solution file is `MafTraining.slnx` (new XML solution format, not `.sln`).
- **Python**: 3.10+, `agent-framework-azure-ai` (verified against `agent-framework-core` 1.16.0, prerelease `1.0.0rc6`). `requirements.txt` starts with `--pre` — required while the framework is in public preview.
- Backend: Microsoft Foundry project (OpenAI-compatible endpoint), not a plain OpenAI account.

## Build / run / test

.NET:
```bash
cd dotnet
dotnet restore
dotnet run --project src/Lesson01.HelloWorldAgent
```

Python:
```bash
cd python
python -m venv .venv
.venv\Scripts\activate
pip install -r requirements.txt
python lessons/lesson01_hello_world_agent.py
```

No test suite in either side yet — each lesson is a runnable sample verified by eyeballing its console output.

## Required environment variables

Both sides read the same three vars directly from the process environment (no config files consulted at runtime):

- `FOUNDRY_PROJECT_ENDPOINT` — e.g. `https://<resource>.services.ai.azure.com/api/projects/<project>`
- `FOUNDRY_API_KEY`
- `FOUNDRY_MODEL` — e.g. `gpt-4o`

Set globally on Windows for the .NET side via `setx`; the root `.env` (gitignored) holds the same three values and is the one actually used when running lessons locally.

**Known inconsistency**: `python/.env.example` documents `OPENAI_API_KEY` / `OPENAI_MODEL_ID`, and `requirements.txt` includes `python-dotenv` — but `lesson01_hello_world_agent.py` never calls `load_dotenv()` and reads `FOUNDRY_*` via `os.environ.get(...)`, not the `OPENAI_*` names. Until this is reconciled, the vars that actually matter at runtime are the three `FOUNDRY_*` ones from the real environment (root `.env` or `setx`), regardless of what `.env.example` says.

## Conventions

- **Lesson naming**: `LessonNN.PascalCaseTopic` for the .NET project folder/csproj, `lessonNN_snake_case_topic.py` for the Python script. `NN` is always zero-padded two digits and matches across languages for the same topic.
- **Env var access**: both langs read `FOUNDRY_PROJECT_ENDPOINT` / `FOUNDRY_API_KEY` / `FOUNDRY_MODEL` directly from the environment and fail fast with a clear error (`InvalidOperationException` in C#, `RuntimeError` in Python) if any are missing — no silent defaults.
- **Agent construction**: wrap a chat client into an agent (`.AsAIAgent(instructions:, name:)` in C#, `.as_agent(name=, instructions=)` in Python) rather than constructing an agent type directly.
- **Demonstrate both run modes**: every lesson script/program shows a non-streaming call (`RunAsync` / `await agent.run(...)`) followed by a streaming call (`RunStreamingAsync` / `agent.run(..., stream=True)`), each on a distinct prompt so the console output is easy to tell apart.
- **Naming gotcha to preserve in docs**: the .NET extension method is `AsAIAgent`, not `CreateAIAgent` (older docs/samples use the old name). Python has one unified `run()` with a `stream=` flag, not a separate `run_stream()`.
- Each lesson's README section states the exact package version it was verified against, since both SDKs are pre-1.0 and the API surface still moves.

## Source control

No commits or pushes without explicit approval (standing rule, not specific to this repo).
