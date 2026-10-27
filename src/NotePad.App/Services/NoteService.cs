using NotePad.App.Models;

namespace NotePad.App.Services;

public class NoteService
{
    private readonly string _baseDirectory;

    public NoteService()
    {
        _baseDirectory = FileSystem.AppDataDirectory;
    }

    public NoteService(string baseDirectory)
    {
        _baseDirectory = baseDirectory;
    }

    public IEnumerable<Note> LoadAllNotes()
    {
        if (!Directory.Exists(_baseDirectory))
            yield break;

        foreach (var filename in Directory.EnumerateFiles(_baseDirectory, "*.notes.txt"))
        {
            yield return new Note
            {
                Filename = filename,
                Text = File.ReadAllText(filename),
                Date = File.GetCreationTime(filename)
            };
        }
    }

    public Note LoadNote(string filename)
    {
        var note = new Note
        {
            Filename = filename
        };

        if (File.Exists(filename))
        {
            note.Date = File.GetCreationTime(filename);
            note.Text = File.ReadAllText(filename);
        }

        return note;
    }

    public void SaveNote(Note note)
    {
        File.WriteAllText(note.Filename, note.Text);
    }

    public void DeleteNote(string filename)
    {
        if (File.Exists(filename))
        {
            File.Delete(filename);
        }
    }
}