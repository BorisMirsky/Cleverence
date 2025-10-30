
using System.Diagnostics;
using System.Globalization;



public class ProcessString
{

    public static string[] SplitInputString(string inputString)
    {
        // Ux0020 - SPACE
        // Ux00A0 - NO-BREAK SPACE
        //char[] visibleSpaceSplitter = new char[] { char.ConvertFromUtf32(0x0020), '0x00A0' };
        string[] result = inputString.Split();
        return result;
    }


    public static string ProcessDate(string inputString)
    {
        DateTime dt = DateTime.Parse(inputString, CultureInfo.GetCultureInfo("en-US"));
        string mydateFormat = dt.ToString("yyyy-dd-MM", CultureInfo.InvariantCulture);
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
            if (inputString.Contains(value))
            {
                if (value == "INFORMATION")
                {
                    result = "INFO";
                }
                else if (value == "WARNING")
                {
                    result = "WARN"; 
                }
                else
                {
                    result = value;
                }
                break;
            }
            else
            {
                result = "novalid";
            }
        }
        return result;
    }


    public static string CallingMethod(string inputString)
    {
        //   ....
        return inputString;
    }


    public static void Main(string[] args)
    {
        string input1 = "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'";
        string input2 = "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'";
        string[] splitted = SplitInputString(input2);
        var dateValue = ProcessDate(splitted[0]);
        string timeValue = ProcessTime(splitted[1]);
        string logLevel = LoggingLevel(splitted[2]);
        //string callingMethod = CallingMethod(result[2]);
        string message = splitted[^3] + " " + splitted[^2] + " " + splitted[^1];
        string result = dateValue + " " + timeValue + " " + logLevel + " " + message;
        Debug.WriteLine(result);
    }
}





