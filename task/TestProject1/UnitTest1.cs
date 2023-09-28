using Newtonsoft.Json;
using task;
namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task testLanguageSelectionEnglish()
        {

            var httpClient = new HttpClient();
            var phrases = Languages.English;
            var expectedLanguagePhrase = phrases[0];


            var response = await httpClient.GetAsync("https://sef.podkolzin.consulting/api/users/lastSeen?offset=0");
            var content = await response.Content.ReadAsStringAsync();
            var usersResponse = JsonConvert.DeserializeObject<UsersResponse>(content);
            var user = usersResponse.data[0];


            Assert.IsTrue(phrases[0] == " is online ");
        }
        [TestMethod]
        public async Task testLanguageSelectionUkranian()
        {

            var httpClient = new HttpClient();
            var phrases = Languages.Ukrainian;
            var expectedLanguagePhrase = phrases[0];


            var response = await httpClient.GetAsync("https://sef.podkolzin.consulting/api/users/lastSeen?offset=0");
            var content = await response.Content.ReadAsStringAsync();
            var usersResponse = JsonConvert.DeserializeObject<UsersResponse>(content);
            var user = usersResponse.data[0];


            Assert.IsTrue(phrases[0] == " онлайн ");
        }

        [TestMethod]
        public async Task ApiAvailability()
        {

            var httpClient = new HttpClient();


            var response = await httpClient.GetAsync("https://sef.podkolzin.consulting/api/users/lastSeen?offset=0");


            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task TotalChanges()
        {

            var httpClient = new HttpClient();
            var initialTotal = 200;


            var response = await httpClient.GetAsync("https://sef.podkolzin.consulting/api/users/lastSeen?offset=0");
            var content = await response.Content.ReadAsStringAsync();
            var usersResponse = JsonConvert.DeserializeObject<UsersResponse>(content);
            var newTotal = usersResponse.total;


            Assert.AreNotEqual(initialTotal, newTotal);



        }

        [TestMethod]
        public void GetStatus_10DaysAgoFromNow_LongTimeAgo()
        {
            List<string> phrases = Languages.English;

            var lastSeenDate = DateTime.Now.AddDays(-10);


            var status = UserStatus.GetStatus(lastSeenDate, phrases);


            Assert.AreEqual(Languages.English[2], status);
        }

        [TestMethod]
        public void GetStatus_5DaysFromNow_ThisWeek()
        {
            List<string> phrases = Languages.English;

            var lastSeenDate = DateTime.Now.AddDays(-5);


            var status = UserStatus.GetStatus(lastSeenDate, phrases);


            Assert.AreEqual(Languages.English[3], status);
        }

        [TestMethod]
        public void GetStatus_DayAgoFromNow_Yesterday()
        {
            List<string> phrases = Languages.English;

            var lastSeenDate = DateTime.Now.AddDays(-1);


            var status = UserStatus.GetStatus(lastSeenDate, phrases);


            Assert.AreEqual(Languages.English[4], status);
        }

        [TestMethod]
        public void GetStatus_3HoursFromNow_Today()
        {
            List<string> phrases = Languages.English;
            string expected;
            var lastSeenDate = DateTime.Now.AddHours(-3);


            var status = UserStatus.GetStatus(lastSeenDate, phrases);
            if (lastSeenDate.Day == DateTime.Now.Day)
            {
                expected = Languages.English[5];
            }
            else
            {
                expected = Languages.English[4];
            }

            Assert.AreEqual(expected, status);
        }

        [TestMethod]
        public void GetStatus_70MinutesFromNow_HourAgo()
        {
            List<string> phrases = Languages.English;

            var lastSeenDate = DateTime.Now.AddMinutes(-70);


            var status = UserStatus.GetStatus(lastSeenDate, phrases);


            Assert.AreEqual(Languages.English[6], status);
        }
        [TestMethod]
        public void GetStatus_2MinutesFromNow_CoupleOfMinutesAgo()
        {
            List<string> phrases = Languages.English;

            var lastSeenDate = DateTime.Now.AddMinutes(-2);


            var status = UserStatus.GetStatus(lastSeenDate, phrases);


            Assert.AreEqual(Languages.English[7], status);
        }

        [TestMethod]
        public void GetStatus_31SecondsFromNow_LessThanMinuteAgo()
        {
            List<string> phrases = Languages.English;

            var lastSeenDate = DateTime.Now.AddSeconds(-31);


            var status = UserStatus.GetStatus(lastSeenDate, phrases);


            Assert.AreEqual(Languages.English[8], status);
        }

        [TestMethod]
        public void GetStatus_10SecondsFromNow_JustNow()
        {
            List<string> phrases = Languages.English;

            var lastSeenDate = DateTime.Now.AddSeconds(-10);


            var status = UserStatus.GetStatus(lastSeenDate, phrases);


            Assert.AreEqual(Languages.English[9], status);
        }
    }
}