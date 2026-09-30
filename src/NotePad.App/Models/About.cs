namespace NotePad.App.Models;

public class About
{
    public string Title { get; set; }
    public string Version { get; set; }
    public string Description { get; set; }
    public string MoreInfoUrl { get; set; }

    public About()
    {
        Title = "NotePad";
        Version = AppInfo.VersionString;
        Description = "This app aims to simulate a notepad where you " +
                      "can write down anything you want.";
        MoreInfoUrl = "https://github.com/pauloalvesm";
    }
}
