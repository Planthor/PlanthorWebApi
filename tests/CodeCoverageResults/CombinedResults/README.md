# Combined Results

## Generate test result collector files

- Check README.md of the project (run `dotnet test...`)

## Generate coverage report

To generate report from multiple test result collector files:

- Standing at CombinedResults directory, use the commands:

```bash
dotnet reportgenerator -reports:"**\coverage.opencover.xml" -targetdir:"." -reporttypes:"MarkdownSummary"
```
