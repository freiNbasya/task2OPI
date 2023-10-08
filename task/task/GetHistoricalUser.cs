using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace task
{
    internal class GetHistoricalUser
    {



        public static void GetHistUser()
        {
            // Ask the user for a date and user ID
            Console.Write("Enter a date (dd.mm.yyyy HH:mm:ss): ");
            string inputDate = Console.ReadLine();

            Console.Write("Enter a user ID: ");
            string userId = Console.ReadLine();

            // Call the function to check if the user was online and get the result
            string filePath = "C:\\Labs_Kse\\OPI\\task2OPI\\task\\task\\data\\outuput.csv"; // Replace with your CSV file path
            string result = CheckUserOnlineStatus(filePath, inputDate, userId);

            // Print the result
            Console.WriteLine(result);
        }

        // Function to check if the user was online and return the result
        static string CheckUserOnlineStatus(string filePath, string inputDate, string userId)
        {
            try
            {
                // Parse the input date and format it to match the CSV date format
                if (DateTime.TryParseExact(inputDate, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    // Read the CSV file and check if the user was online at the given date and time
                    using (var reader = new StreamReader(filePath))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');

                            if (values.Length >= 4 && values[0] == userId)
                            {
                                // Parse the date from the CSV data
                                if (DateTime.TryParseExact(values[2], "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime userDate))
                                {
                                    if (userDate == parsedDate)
                                    {
                                        if (values[3].Trim().ToLower() == "true")
                                        {
                                            return "User was online at the specified date and time.";
                                        }
                                        else
                                        {
                                            return "User was not online. Nearest date online: " + values[2];
                                        }
                                    }
                                    else
                                    {
                                        return "User was not online. Nearest date online: " + values[2];
                                    }
                                }
                            }
                        }
                    }

                    return "User not found in the CSV file.";
                }
                else
                {
                    return "Invalid date format. Please use the format 'dd.mm.yyyy HH:mm:ss'.";
                }
            }
            catch (Exception ex)
            {
                return "An error occurred: " + ex.Message;
            }
        }
    }

}

