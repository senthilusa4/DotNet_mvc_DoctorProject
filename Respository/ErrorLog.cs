using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DocterSpot.Respository
{
    public class ErrorLog
    {
        public static void LogError(Exception ex)
        {
            try
            {
                string logPath = @"C:\Users\Admin\Desktop\ErrorLog.txt";

                // Create or open the error log file for appending
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    sw.WriteLine($"Error occurred at {DateTime.Now}");
                    sw.WriteLine($"Message: {ex.Message}");
                    sw.WriteLine($"Stack Trace: {ex.StackTrace}");
                    sw.WriteLine(new string('-', 50)); 
                }
            }
            catch (Exception logEx)
            {
                // Handle any exceptions that occur while logging, you can log them to the console or another log file.
                Console.WriteLine($"An error occurred while logging: {logEx.Message}");
            }
        }
    }
}