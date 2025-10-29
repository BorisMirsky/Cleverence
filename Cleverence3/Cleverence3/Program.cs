
using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;



public class ProcessString
{

    public static string[] SplitInputString(string inputString)
    {     
        string[] result = inputString.Split(" ");
        //foreach (string s in result) 
        //{
        //    Debug.WriteLine(s);
        //}
        return result;
    }


    public static string ProcessDate(string inputString)
    {
        DateTime dt = DateTime.Parse(inputString, CultureInfo.GetCultureInfo("en-US"));
        string mydateFormat = dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        return mydateFormat;
    }


    public static string ProcessTime(string inputString)
    {
        if (inputString.EndsWith('|'))
        {
            inputString = inputString.TrimEnd('|');
        }
        return inputString;
    }


    public static string LoggingLevel(string inputString)
    {
        string[] values = ["INFORMATION", "INFO", "WARNING", "WARN", "ERROR", "DEBUG"];
        string result = "";
        foreach (string value in values) 
        {
            Debug.WriteLine(value);
            Debug.WriteLine(inputString);
            Debug.WriteLine("");
            if (inputString.Contains(value))
                {
                   result = value;
                }
            else
            {
                result = "novalid";
            }
        }
        return result;
    }


    public static void Main(string[] args)
    {
        string input1 = "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'\r\n";
        string input2 = "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'\r\n";
        //
        string[] result = SplitInputString(input2);
        // date
        var dateValue = ProcessDate(result[0]);
        //Debug.WriteLine(dateValue);
        string timeValue = ProcessTime(result[1]);
        //Debug.WriteLine(timeValue);
        string logLevel = LoggingLevel(result[2]);
        //Debug.WriteLine(logLevel);
        //Debug.WriteLine(result[2]);

    }
}





