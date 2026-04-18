using Cleverence3;



public class PreProcessString
{
    public ParsedLog? Parse(string line)
    {
        if (line.Contains('|'))
        {
            string[] parts = line.Split('|');

            if (parts.Length < 2) return null;

            string[] dateTime = parts[0].Trim().Split(' ');

            string method = parts.Length > 3 ? parts[3].Trim() : "DEFAULT";
            string message = parts.Length > 4 ? parts[4].Trim() : "";

            return new ParsedLog
            {
                Date = dateTime.Length > 0 ? dateTime[0] : null,
                Time = dateTime.Length > 1 ? dateTime[1] : "",
                Level = NormalizeLevel(parts[1].Trim()),
                Method = method,
                Message = message
            };
        }

        string[] spaces = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (spaces.Length < 3) return null;

        return new ParsedLog
        {
            Date = spaces[0],
            Time = spaces[1],
            Level = NormalizeLevel(spaces[2]),
            Method = "DEFAULT",
            Message = spaces.Length > 3 ? string.Join(" ", spaces, 3, spaces.Length - 3) : ""
        };
    }

    private string NormalizeLevel(string level)
    {
        return level switch
        {
            "INFORMATION" => "INFO",
            "WARNING" => "WARN",
            _ => level
        };
    }

    public string Handle(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            WriteToFile("problems.txt", input ?? "");
            return "INVALID";
        }

        var parsedLog = Parse(input);

        if (parsedLog == null ||
            string.IsNullOrEmpty(parsedLog.Date) ||
            string.IsNullOrEmpty(parsedLog.Time) ||
            string.IsNullOrEmpty(parsedLog.Level))
        {
            WriteToFile("problems.txt", input);
            return "INVALID";
        }

        if (DateTime.TryParse(parsedLog.Date, out DateTime dt))
        {
            parsedLog.Date = dt.ToString("dd-MM-yyyy");
        }

        string result = $"{parsedLog.Date}\t{parsedLog.Time}\t{parsedLog.Level}\t{parsedLog.Method}\t{parsedLog.Message}";
        WriteToFile("result.txt", result);

        return result;
    }

    private void WriteToFile(string fileName, string content)
    {
        string path = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(path, fileName);

        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine(content);
        }
    }
}
