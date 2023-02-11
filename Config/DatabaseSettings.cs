
namespace Abyster_Test_Project.Config;

public class DatabaseSettings
{
    public string Host { get; set; }
    public string Port { get; set; }
    public string DatabaseName { get; set; }
    public string UserName { get; set; }

    public string Password { get; set; }

    public override string ToString()
    {
        return string.Format("server={0};uid={1};pwd={2};database={3};port={4}",
                                Host,
                                UserName,
                                Password,
                                DatabaseName,
                                Port);
    }


}