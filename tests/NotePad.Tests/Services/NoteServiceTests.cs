using NotePad.App.Models;
using NotePad.App.Services;

namespace NotePad.Tests.Services;

public class NoteServiceTests : IDisposable
{
    private readonly string _testDirectory;
    private readonly NoteService _noteService;

    public NoteServiceTests()
    {
        _testDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(_testDirectory);

        _noteService = new NoteService(_testDirectory);
    }

    [Fact]
    public void SaveNote_ShouldCreateFileWithCorrectContent()
    {
        // Arrange
        var filePath = Path.Combine(_testDirectory, "test_note.notes.txt");
        var note = new Note
        {
            Filename = filePath,
            Text = "Hello testing!",
            Date = DateTime.Now
        };

        // Act
        _noteService.SaveNote(note);

        // Assert
        Assert.True(File.Exists(filePath));
        var savedContent = File.ReadAllText(filePath);
        Assert.Equal("Hello testing!", savedContent);
    }

    [Fact]
    public void DeleteNote_ShouldRemoveExistingFile()
    {
        // Arrange
        var filePath = Path.Combine(_testDirectory, "delete_note.notes.txt");
        File.WriteAllText(filePath, "Content to delete");
        Assert.True(File.Exists(filePath));

        // Act
        _noteService.DeleteNote(filePath);

        // Assert
        Assert.False(File.Exists(filePath));
    }

    public void Dispose()
    {
        if (Directory.Exists(_testDirectory))
        {
            Directory.Delete(_testDirectory, true);
        }
    }
}
