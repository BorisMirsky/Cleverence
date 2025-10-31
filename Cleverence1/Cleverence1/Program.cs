using Cleverence1;
using System.Diagnostics;


public class EncodeDecodeString
{
    public static void Main(string[] args)
    {
        string input = "qaaabbcccdde";
        EncodeDecode encDec = new EncodeDecode();
        bool stringOnly = encDec.StringIsDigits(input);
        if (stringOnly)
        {
            string[] result = encDec.Handle(input);
            Debug.WriteLine(result[0]);
            Debug.WriteLine(result[1]);
        }
        else
        {
            Debug.WriteLine("false!!!");
        }
    }
}






