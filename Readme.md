# Auth Solution

A compact .NET solution containing shared utilities, a small base API library, and tooling used across projects.

Contents

- `Auth.sln` — solution file that aggregates projects in this repository.
- `Base/` — base API library and core entities.
- `Base.Tests/` — unit tests and coverage configuration for `Base`.

Getting started

Prerequisites

- .NET SDK (recommended: the same runtime used to build these projects; see project files for target frameworks).

Clone the repo

```bash
git clone <repo-url>
cd auth
```

Build

Build the entire solution:

```bash
dotnet build Auth.sln
```

Run tests

Run all unit tests:

```bash
dotnet test Auth.sln
```

Project structure and quick notes

- `Base/` — contains `BaseEntity.cs` and `IEntity.cs` providing a lightweight base model for other projects. See [Base/Readme.md](Base/Readme.md) for details.
- Test results and coverage reports are placed under `TestResults/` after test runs.

Contributing

- Open an issue or submit a PR. Keep changes small and focused.

License

- See the top-level `License` file for license information.

Contact

- If you need help, add an issue or contact the repository maintainers.

For per-project details (build/run instructions or specific configuration), check the README inside each project folder.
