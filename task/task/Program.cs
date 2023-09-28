using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

public class LastSeenUsers
{
    public string userId;
    public string nickname;
    public string firstName;
    public string lastName;
    public DateTime? registrationDate;
    public DateTime? lastSeenDate;
    public bool isOnline;
};