# Lesson structure

Every lesson exists twice, kept in lockstep: once under `dotnet/src/`, once under `python/lessons/`.

- **.NET**: `dotnet/src/LessonNN.PascalCaseTopic/` containing a `LessonNN.PascalCaseTopic.csproj` and `Program.cs`. Add the new project to `dotnet/MafTraining.slnx` under the `/src/` folder.
- **Python**: a single file `python/lessons/lessonNN_snake_case_topic.py`.
- `NN` is a zero-padded two-digit lesson number and must match across both languages for the same topic (e.g. `Lesson02.Threads` <-> `lesson02_threads.py`).
- When a lesson is added or changed, update all three docs together:
  - root `README.md` lessons table (topic + links to both implementations)
  - `dotnet/README.md` (new "### Lesson NN — Topic" section: package + version verified against, what it demonstrates)
  - `python/README.md` (same, for the Python package/version)
- Each lesson demonstrates the same concept in both languages using equivalent, idiomatic calls — not a line-by-line port. Note real naming differences between the two SDKs in the lesson's README section (e.g. `AsAIAgent` vs `.as_agent`) rather than papering over them.
