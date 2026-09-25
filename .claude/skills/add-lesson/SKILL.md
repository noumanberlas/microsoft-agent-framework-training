---
name: add-lesson
description: Scaffold a new MAF Training lesson (both .NET and Python implementations, plus doc updates). Use when the user asks to add a new lesson, add the next lesson, or create a lesson for a new MAF topic.
---

# Add a lesson

This repo pairs every lesson across `dotnet/` and `python/` under a shared zero-padded number (see `.claude/rules/lesson-structure.md` and `.claude/rules/environment-variables.md` — read both first). Adding a lesson touches five things; do all five.

## Steps

1. **Pick `NN` and the topic name.** `NN` is the next unused two-digit number. Get a PascalCase name for .NET (`LessonNN.Topic`) and the matching snake_case name for Python (`lessonNN_topic.py`), from the topic list in root `README.md` ("More lessons ... will be added here as we go").

2. **.NET project**
   - Create `dotnet/src/LessonNN.Topic/LessonNN.Topic.csproj`, modeled on `dotnet/src/Lesson01.HelloWorldAgent/Lesson01.HelloWorldAgent.csproj` (same `TargetFramework`, add whatever `Microsoft.Agents.AI.*` package the topic needs, `--prerelease` if it's still pre-1.0).
   - Create `Program.cs` following Lesson01's pattern: read `FOUNDRY_PROJECT_ENDPOINT` / `FOUNDRY_API_KEY` / `FOUNDRY_MODEL` from the environment with fail-fast checks, build the agent via `.AsAIAgent(...)`, then demonstrate the topic.
   - Add `<Project Path="src/LessonNN.Topic/LessonNN.Topic.csproj" />` under the `/src/` folder in `dotnet/MafTraining.slnx`.

3. **Python script**
   - Create `python/lessons/lessonNN_topic.py` following Lesson01's pattern: same env var handling via `os.environ.get(...)` with a `RuntimeError` if missing, agent built via `.as_agent(...)`, `asyncio.run(main())` entry point.

4. **Docs — update all three together:**
   - Root `README.md`: add a row to the lessons table (topic, link to both new files).
   - `dotnet/README.md`: add a "### Lesson NN — Topic" section naming the package + exact version verified against, and a bullet list of what it demonstrates.
   - `python/README.md`: same, for the Python package/version.

5. **Sanity check, don't fabricate a run.** `dotnet build` for the new project is safe to run directly (already allow-listed). Actually *running* either lesson calls a real Foundry endpoint and costs a request — only run it if the user has FOUNDRY_* env vars set and asks you to verify it live (see `/run-lesson`).

## Non-goals

Don't invent a test suite — this repo has none by design (samples are verified by reading console output). Don't add a second env-var naming scheme; reuse `FOUNDRY_PROJECT_ENDPOINT` / `FOUNDRY_API_KEY` / `FOUNDRY_MODEL`.
