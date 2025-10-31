using System.Diagnostics;


namespace Cleverence2
{
    public class ReaderWriter
    {
        public int count;

        private ReaderWriterLock locker = new ReaderWriterLock();

        // писатели пишут последовательно
        public void AddToCount(int value)
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
            if (i % 3 == 0)
                AddToCount(3);
            else
                Debug.WriteLine(GetCount());
            });
        }
    }
}
