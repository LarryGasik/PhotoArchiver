using PhotoArchiver.Logic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IArchiveProcess, ArchiveProcess>();

var app = builder.Build();

app.MapPost("/archive", (ArchiveRequest request, IArchiveProcess archiveProcess) =>
{
    if (string.IsNullOrWhiteSpace(request.SourceDirectory))
    {
        return Results.BadRequest("Source directory is required.");
    }

    if (string.IsNullOrWhiteSpace(request.DestinationDirectory))
    {
        return Results.BadRequest("Destination directory is required.");
    }

    archiveProcess.ArchivePhotosBasedOnDays(request.SourceDirectory, request.DestinationDirectory, request.CleanUpSource);

    return Results.Ok(new ArchiveResponse("Archive completed."));
});

app.Run();

public sealed record ArchiveRequest(string SourceDirectory, string DestinationDirectory, bool CleanUpSource = false);

public sealed record ArchiveResponse(string Message);
