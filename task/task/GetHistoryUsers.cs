using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
namespace task
{
    internal class GetHistoryUsers
    {
        public static void GetHistory()
        {
            // Ask the user for a date and time
            Console.Write("Enter a date (dd.mm.yyyy): ");
            string inputDate = Console.ReadLine();

            Console.Write("Enter a time (HH:mm:ss): ");
            string inputTime = Console.ReadLine();

            // Call the function to get the online user count for the given date and time
            string filePath = "outuput.csv"; // Replace with your CSV file path
            int onlineUserCount = GetOnlineUserCountByDateTime(filePath, inputDate, inputTime);

            // Print the result
            if (onlineUserCount >= 0)
            {
                Console.WriteLine($"Number of online users on {inputDate} at {inputTime}: {onlineUserCount}");
            }
            else
            {
                Console.WriteLine("No records are available for the specified date and time.");
            }
        }

        // Function to get the online user count for a given date and time
        static int GetOnlineUserCountByDateTime(string filePath, string inputDate, string inputTime)
        {
            try
            {
                // Parse the input date and time and format them
                if (DateTime.TryParseExact(inputDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate)
                    && DateTime.TryParseExact(inputTime, "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedTime))
                {
                    string formattedDate = parsedDate.ToString("dd.MM.yyyy");
                    string formattedTime = parsedTime.ToString("HH:mm:ss");

                    // Read the CSV file and count online users for the given date and time
                    using (var reader = new StreamReader(filePath))
                    {
                        int onlineUserCount = 0;
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');
                            if (values.Length >= 4 && values[3].Trim().ToLower() == "true")
                            {
                                // Check if the time in the CSV record matches the input time
                                if (DateTime.TryParseExact(values[2], "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime recordTime)
                                    && recordTime.ToString("HH:mm:ss") == formattedTime)
                                {
                                    onlineUserCount++;
                                }
                            }
                        }
                        return onlineUserCount;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date or time format. Please use the formats 'dd.mm.yyyy' and 'HH:mm:ss'.");
                    return -1; // Error code for an invalid date or time format
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return -2; // Error code for a general error
            }
        }
    }
}

