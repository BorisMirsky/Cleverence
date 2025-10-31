using System.Diagnostics;


public class ProcessString
{
    public static void Main(string[] args)
    {
        PreProcessString cls = new();
        string input1 = "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'";
        string input2 = "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'";
        string result = cls.Handle(input2);
        Debug.WriteLine(result);
    }
}
