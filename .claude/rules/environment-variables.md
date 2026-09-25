# Environment variables

Both languages read Foundry credentials directly from the process environment at run time — never from a config file, and (currently) never via `python-dotenv`'s `load_dotenv()` even though that package is in `python/requirements.txt`:

- `FOUNDRY_PROJECT_ENDPOINT`
- `FOUNDRY_API_KEY`
- `FOUNDRY_MODEL`

Follow this pattern for any new lesson that talks to Foundry:

- Read each var with `Environment.GetEnvironmentVariable(...)` (C#) / `os.environ.get(...)` (Python).
- If any is missing, fail fast with a clear message before doing any network call — `InvalidOperationException` in C#, `RuntimeError` in Python. Do not silently fall back to a default endpoint/model.
- Don't introduce a second naming scheme (e.g. `OPENAI_API_KEY`) for the same values — `python/.env.example` currently uses `OPENAI_*` names that don't match what the code reads, which is a known doc/code drift, not a pattern to copy.
- Real values for local runs live in the root `.env` (gitignored) or are set via `setx` on Windows — never hardcode a key/endpoint in a lesson file, and never commit `.env`.
