using Xunit;
using System.Collections.Concurrent;

namespace Cleverence2.Tests
{
    public class ThreadSafeCounterTests
    {
        public ThreadSafeCounterTests()
        {
            ThreadSafeCounter.Reset();
        }

        [Fact]
        public void GetCount_Initially_ReturnsZero()
        {
            Assert.Equal(0, ThreadSafeCounter.GetCount());
        }

        [Fact]
        public void AddToCount_SingleThread_AddsCorrectly()
        {
            ThreadSafeCounter.AddToCount(5);
            Assert.Equal(5, ThreadSafeCounter.GetCount());

            ThreadSafeCounter.AddToCount(3);
            Assert.Equal(8, ThreadSafeCounter.GetCount());
        }

        [Fact]
        public void AddToCount_NegativeValue_DecreasesCount()
        {
            ThreadSafeCounter.AddToCount(10);
            ThreadSafeCounter.AddToCount(-3);
            Assert.Equal(7, ThreadSafeCounter.GetCount());
        }

        [Fact]
        public void ParallelReaders_DoNotBlockEachOther()
        {
            ThreadSafeCounter.AddToCount(100);

            var results = new ConcurrentBag<int>();

            Parallel.For(0, 50, _ =>
            {
                results.Add(ThreadSafeCounter.GetCount());
            });

            Assert.All(results, value => Assert.Equal(100, value));
        }

        [Fact]
        public void ParallelWriters_WriteSequentially_CorrectSum()
        {
            const int writersCount = 100;
            const int addValue = 3;
            int expected = writersCount * addValue;

            Parallel.For(0, writersCount, _ =>
            {
                ThreadSafeCounter.AddToCount(addValue);
            });

            Assert.Equal(expected, ThreadSafeCounter.GetCount());
        }

        [Fact]
        public async Task ReadersWaitDuringWrite_ReturnsConsistentValues()
        {
            var readValues = new ConcurrentBag<int>();

            var writeTask = Task.Run(() =>
            {
                ThreadSafeCounter.AddToCount(100);
            });

            var readTasks = new List<Task>();
            for (int i = 0; i < 20; i++)
            {
                readTasks.Add(Task.Run(() =>
                {
                    readValues.Add(ThreadSafeCounter.GetCount());
                }));
            }

            await Task.WhenAll(readTasks);
            await writeTask;

            Assert.All(readValues, value => Assert.True(value == 0 || value == 100));
        }

        [Fact]
        public async Task MixedReadersAndWriters_NoRaceConditions()
        {
            const int writersCount = 50;
            const int readersCount = 200;
            const int addValue = 2;
            int expected = writersCount * addValue;

            var tasks = new List<Task>();

            for (int i = 0; i < writersCount; i++)
                tasks.Add(Task.Run(() => ThreadSafeCounter.AddToCount(addValue)));

            for (int i = 0; i < readersCount; i++)
                tasks.Add(Task.Run(() => { var _ = ThreadSafeCounter.GetCount(); }));

            await Task.WhenAll(tasks);

            Assert.Equal(expected, ThreadSafeCounter.GetCount());
        }

        [Fact]
        public void MultipleTestRuns_ConsistentResults()
        {
            for (int run = 0; run < 10; run++)
            {
                ThreadSafeCounter.Reset();
                const int writers = 30;
                const int value = 3;
                int expected = writers * value;

                Parallel.For(0, writers, _ => ThreadSafeCounter.AddToCount(value));

                Assert.Equal(expected, ThreadSafeCounter.GetCount());
            }
        }
    }
}