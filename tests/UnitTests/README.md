# Unit Tests

Unit tests validate the behavior of individual units of code within the application. They ensure that each component functions correctly in isolation.

## Collect code coverage result with dotnet cli

```bash
dotnet test --results-directory ./tests/CodeCoverageResults/UnitTestResults --collect:"XPlat Code Coverage"
```
