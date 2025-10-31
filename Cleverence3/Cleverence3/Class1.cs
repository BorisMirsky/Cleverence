using System.Globalization;


public class PreProcessString
{
    public static string[] SplitInputString(string inputString)
    {
        string[] result = inputString.Split();
        return result;
    }

    public string ProcessDate(string inputString)
    {
        DateTime dt;
        string result = String.Empty;
        if (DateTime.TryParse(inputString, out dt))
        {
            result = dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
        else
        {
            result = "NOVALID";
        }
        return result; 
    }

    public static string ProcessTime(string inputString)
    {
        if (inputString.EndsWith('|'))
        {
            inputString = inputString.TrimEnd('|');
        }
        return inputString;
    }

    public string LoggingLevel(string inputString)
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
            else
            {
                result = "NOVALID";
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


    public string Handle(string input)
    {
        string[] splitted = SplitInputString(input);
        var dateValue = ProcessDate(splitted[0]);
        string timeValue = ProcessTime(splitted[1]);
        string logLevel = LoggingLevel(splitted[2]);
        string callingMethod = CallingMethod(splitted[2]);
        string message = splitted[^3] + " " + splitted[^2] + " " + splitted[^1];
        string result = dateValue + '\t' + timeValue + '\t' + logLevel + '\t' + callingMethod + '\t' + message;
        //
        var directory = AppContext.BaseDirectory.Split(Path.DirectorySeparatorChar);
        var slice = new ArraySegment<string>(directory, 0, directory.Length - 4);
        var path = Path.Combine(slice.ToArray());
        string fileName = String.Empty;
        if (result.Contains("NOVALID"))
        {
            fileName = "problems.txt";
        }
        else
        {
            fileName = "result.txt";
        }
        string fileNamePath = Path.Combine(path, fileName);
        using (StreamWriter writer = new StreamWriter(fileNamePath, true))
        {
            writer.WriteLine(result);
        }
        return result;
    }
}






