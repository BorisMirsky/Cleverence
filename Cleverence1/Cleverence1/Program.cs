
using System.Diagnostics;
using System.Text;



public class EncodeDecodeString
{

    public static string EncodeString(string inputString)
    {

        StringBuilder encoded = new StringBuilder();
        int count = 1;
        if (inputString.Length  == 1)
        {
            encoded = new StringBuilder(inputString);
        }
        else
        {
            for (int i=0; i < inputString.Length - 1; i++)
            //for (int i = 1; i <= inputString.Length; i++)
                {
                if (inputString[i] == inputString[i + 1])
                    {
                            count = count + 1;
                    }
                else
                {
                    if (inputString[i] != inputString[i - 1])
                    {
                        StringBuilder encoded1 = new StringBuilder(inputString);
                        encoded.Append(encoded1);
                    }
                    else
                    {
                        //encoded = encoded + count.ToString() + inputString[i];
                        encoded.Append(count.ToString());
                        encoded.Append(inputString[i]);
                        count = 1;
                    }
                }
            }
        }
        return encoded.ToString();
    }

    public static string Encode(string input)
    {

        StringBuilder encodedString = new StringBuilder();
        int count = 1;

        if (string.IsNullOrEmpty(input))
            return string.Empty;

        else if(input.Length == 1)
        {
            return input;
        }

        else 
        {
            for (int i = 1; i <= input.Length; i++)
            {
                if (i == input.Length || input[i] != input[i - 1])
                {
                    if (count != 1)
                    {
                        encodedString.Append(count);
                        encodedString.Append(input[i - 1]);
                    }
                    else
                    {
                        //encodedString.Append(count);
                        encodedString.Append(input[i - 1]);
                        //count = 1;
                    }
                }
                else
                {
                    count++;
                }
            }
        } 

            
        return encodedString.ToString();
    }



    public static void Main(string[] args)
    {
        string stringToEncode = "qqw";   //"caaabcccdde";  
        //int[] numbers1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };       
        string result = EncodeString(stringToEncode);

        string result1 = Encode(stringToEncode);
        Debug.WriteLine(result1);
    }
}





