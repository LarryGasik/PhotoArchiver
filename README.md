# PhotoArchiver

Archive your photos in a simple, reproducible way. Point to a source, point to a destination, and let the tool handle naming, folder creation, and copying so your images end up organized by date.

## Project structure
- **PhotoArchiver**: Console entry point that reads settings and kicks off the archive process.
- **PhotoArchiver.Logic**: Core logic for generating album names, constructing unique destination filenames, and orchestrating file moves.
- **LarryGasik.FileSystem**: Lightweight file system abstraction that supplies file metadata and copy/existence helpers.
- **PhotoArchiver.WinForm**: Windows Forms front-end scaffold (net10.0-windows) for future UI work.
- **PhotoArchiver.Tests** and **PhotoArchiver.Logic.Tests**: Automated tests covering logic and integration points.

## Technologies & dependencies
- **.NET 10** for all projects, enabling modern C# features and cross-project sharing. [Documentation](https://learn.microsoft.com/dotnet/)
- **Windows Forms** for the desktop UI project. [Documentation](https://learn.microsoft.com/dotnet/desktop/winforms/)
- **xUnit 2.8** for unit testing. [Package](https://www.nuget.org/packages/xunit/) | [Docs](https://xunit.net/)
- **Moq 4.20** for mocking collaborators in tests. [Package](https://www.nuget.org/packages/Moq/) | [Docs](https://github.com/moq/moq4/wiki)
- **Microsoft.NET.Test.Sdk 17.11** for running tests via `dotnet test`. [Package](https://www.nuget.org/packages/Microsoft.NET.Test.Sdk/) | [Docs](https://learn.microsoft.com/visualstudio/test/test-concepts)
- **coverlet.collector 6.0** for code coverage collection. [Package](https://www.nuget.org/packages/coverlet.collector/) | [Docs](https://github.com/coverlet-coverage/coverlet/blob/master/Documentation/Collector.md)

## Getting started
1. Install the [.NET SDK 10](https://dotnet.microsoft.com/en-us/download/dotnet/10.0).
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Run the console archiver (supply your own source/destination paths via `appsettings.json` or arguments):
   ```bash
   dotnet run --project PhotoArchiver/PhotoArchiver.csproj
   ```

## Running tests
Execute the full test suite (unit and integration):
```bash
dotnet test
```
This uses xUnit with Moq for isolating dependencies and `coverlet.collector` for gathering coverage data during test runs.

## How it works (high level)
- Reads settings (source and destination) and invokes the archive process from `Program.Main`.
- Enumerates files from the source, determines album folders by date, and constructs unique destination filenames.
- Copies files into the destination structure, creating folders as needed via the file system abstraction.
