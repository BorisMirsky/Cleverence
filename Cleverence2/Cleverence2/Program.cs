using System.Diagnostics;



public static class MyServer
{

    private static int count;

    private static ReaderWriterLock locker = new ReaderWriterLock();

    // писатели пишут последовательно
    public static void AddToCount(int value)
    {
        locker.AcquireWriterLock(Timeout.InfiniteTimeSpan);
        count += value;
        locker.ReleaseWriterLock();
    }

    // читатели читают параллельно
    // если писатели пишут, то читатели ждут
    public static int GetCount()
    {
        locker.AcquireReaderLock(Timeout.InfiniteTimeSpan);
        try
        {
            return count;
        }
        finally
        {
            locker.ReleaseReaderLock();
        }
    }



    static void Main(string[] args)
    {
        Parallel.For(0, 50, i =>
        {
            if (i % 2 == 0)
                AddToCount(2);
            else
                Debug.WriteLine(GetCount());
        });
        Debug.WriteLine(GetCount());
        Debug.WriteLine("Done.");
    }
}
