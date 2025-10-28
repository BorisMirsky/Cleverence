
using System.Diagnostics;
using System.Text;



public class EncodeDecodeString
{
    public static string[] EncodeString(string inputString)
    {
        
        string[] result = inputString.Split(" ");
        return result;
    }




    public static void Main(string[] args)
    {
        string input1 = "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'\r\n";
        string input2 = "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'\r\n";
        string[] result = EncodeString(input2);
        foreach (string str in result) 
        { 
            Debug.WriteLine(str); 
        }
    }
}





