using Cleverence2;
using System.Diagnostics;



public static class MyServer
{
    static void Main(string[] args)
    {

        Parallel.For(0, 100, i =>
        {
            if (i % 3 == 0)
                ThreadSafeCounter.AddToCount(3);
            else
                Debug.WriteLine(ThreadSafeCounter.GetCount());
        });
    }
}
