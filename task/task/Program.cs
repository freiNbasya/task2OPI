using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

public class UsersResponse
{
    public int total;
    public List<LastSeenUsers> data;
};
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