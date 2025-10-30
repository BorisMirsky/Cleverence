
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;



public class ProcessString
{

    public static string[] SplitInputString(string inputString)
    {
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
        string result = String.Empty;
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
        }
        return result;
    }


    public static string CallingMethod(string inputString)
    {
        string result = String.Empty;
        if (inputString.Contains('|'))
        {
            string[] splitted = inputString.Split('|');
            result = splitted[2];
            result = result.TrimEnd('|');
        }
        else
        {
            result = "DEFAULT";
        }
            return result;
    }


    public static void Main(string[] args)
    {
        string input1 = "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'";
        string input2 = "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'";
        string[] splitted = SplitInputString(input1);
        var dateValue = ProcessDate(splitted[0]);
        string timeValue = ProcessTime(splitted[1]);
        string logLevel = LoggingLevel(splitted[2]);
        string callingMethod = CallingMethod(splitted[2]);
        string message = splitted[^3] + " " + splitted[^2] + " " + splitted[^1];
        string result = dateValue + '\t' + timeValue + '\t' + logLevel + '\t' + callingMethod + '\t' + message;
        Debug.WriteLine(result);
        var directory = AppContext.BaseDirectory.Split(Path.DirectorySeparatorChar);
        var slice = new ArraySegment<string>(directory, 0, directory.Length - 4);
        var path = Path.Combine(slice.ToArray());
        string fileName = "result.txt";
        // При каких условиях запись невалидна?
        //  ?????            "problems.txt";
        string fileNamePath = Path.Combine(path, fileName);
        //using (StreamWriter writer = new StreamWriter(fileName, true))
        //{
        //    writer.WriteLine(result);
        //}
    }
}





