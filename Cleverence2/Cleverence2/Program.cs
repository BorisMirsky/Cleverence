using Cleverence2;
using System.Diagnostics;


public static class MyServer
{
    static void Main(string[] args)
    {
        ReaderWriter cls = new();
        cls.Handle(1000);
        Debug.WriteLine(cls.GetCount());
        Debug.WriteLine("Done.");
    }
}
