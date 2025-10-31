using Cleverence2;
using System.Diagnostics;


public static class MyServer
{
    static void Main(string[] args)
    {
        ReaderWriter cls = new();
        Parallel.For(0, 100, i =>
        {
            if (i % 3 == 0)
                cls.AddToCount(3);
            else
                Debug.WriteLine(cls.GetCount());
        });
    }
}
