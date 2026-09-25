---
description: Run a lesson's .NET and/or Python implementation
argument-hint: <lesson-number> [dotnet|python|both]
---

Run lesson `$1` (zero-padded, e.g. `01`) for `$2` (default: `both`).

1. Confirm the required env vars are set in this shell: `FOUNDRY_PROJECT_ENDPOINT`, `FOUNDRY_API_KEY`, `FOUNDRY_MODEL`. If any are missing, tell the user instead of running (the root `.env` has real local values but is not auto-loaded — the user needs to have sourced/exported it, or set them via `setx` beforehand).
2. Find the matching project/script:
   - .NET: `dotnet/src/Lesson$1.*` (glob for the exact PascalCase topic name)
   - Python: `python/lessons/lesson$1_*.py`
3. Run what was asked:
   - .NET: `dotnet run --project <matched project dir>` (from `dotnet/`)
   - Python: `python lessons/<matched file>` (from `python/`, with `.venv` activated if present)
4. Show the console output as-is — these are demo scripts meant to be read, not parsed.
