using System.Diagnostics;
using System.Text;


public class EncodeDecodeString
{
    public static string Encode(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }
        else if (input.Length == 1)
        {
            return input;
        }

        StringBuilder encoded = new StringBuilder();
        char currentChar = input[0];
        int count = 1;

        for (int i = 1; i < input.Length; i++)
        {
            if (input[i] == currentChar)
            {
                count++;
            }
            else
            {
                if (count == 1)
                {
                    encoded.Append(currentChar);
                    currentChar = input[i];
                }
                else
                {
                    encoded.Append(count);
                    encoded.Append(currentChar);
                    currentChar = input[i];
                    count = 1;
                }
            }
        }

        if (count != 1)
        {
            encoded.Append(count);
        }
        encoded.Append(currentChar);

        return encoded.ToString();
    }

    public static void Main(string[] args)
    {
        string stringToEncode = "aaabbcccdde";   
        string result = Encode(stringToEncode);
        Debug.WriteLine(result);
    }
}





