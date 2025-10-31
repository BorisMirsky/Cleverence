using System.Diagnostics;


namespace Cleverence2
{
    public class ReaderWriter
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
        public int GetCount()
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

        public void Handle(int value)
        {
            Parallel.For(0, value, i =>
            {
                if (i % 2 == 0)
                    AddToCount(2);
                else
                    Debug.WriteLine(GetCount());
            });
        }
    }
}
