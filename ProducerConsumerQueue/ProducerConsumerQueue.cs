using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerQueue
{
    public class ProducerConsumerQueue : IDisposable
    {
        public static ProducerConsumerQueue Create(int workerCount, string name)
        {
            return new ProducerConsumerQueue(workerCount, name);
        }

        public static ProducerConsumerQueue CreateLongRunning(int workerCount, string name)
        {
            return new ProducerConsumerQueue(workerCount, name, TaskCreationOptions.LongRunning);
        }

        private readonly BlockingCollection<Task> _taskQ;
       
        private readonly TaskCreationOptions _taskCreationOptions;
        private readonly Task[] _consumers;

        private bool _disposed;

        public string QName { get; }
        public Guid QId { get; }

        public int Count => _taskQ.Count;

        internal ProducerConsumerQueue(int workerCount, string name, 
            TaskCreationOptions taskCreationOptions = TaskCreationOptions.None)
        {
            QId = Guid.NewGuid();
            QName = name;
            _disposed = false;
            _taskCreationOptions = taskCreationOptions;
            _taskQ = new BlockingCollection<Task>();
            _consumers = new Task[workerCount];

            for (int i = 0; i < workerCount; i++)
            {
                _consumers[i] = Task.Factory.StartNew(Consume, taskCreationOptions);
            }
        }

        public Task Enqueue(Action action, CancellationToken cToken = default(CancellationToken))
        {
            ThrowIfDisposed();

            Task consumer = new Task(action, cToken, _taskCreationOptions);
            _taskQ.Add(consumer);
            return consumer;
        }

        public Task<TResult> Enqueue<TResult>(Func<TResult> func, CancellationToken cToken = default(CancellationToken))
        {
            ThrowIfDisposed();

            Task<TResult> consumer = new Task<TResult>(func, cToken, _taskCreationOptions);
            _taskQ.Add(consumer);
            return consumer;
        }
        private void Consume()
        {
            foreach (Task consumer in _taskQ.GetConsumingEnumerable())
            {
                try
                {
                    if (consumer.IsCanceled == false)
                    {
                        consumer.RunSynchronously(); //runs on the consumer thread
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }
        }

        private static void CancelPotentialThreadInterruptedException()
        {
            try
            {
                Thread.Sleep(1);
            }
            catch (ThreadInterruptedException)
            {
            }
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException($"{nameof(ProducerConsumerQueue)} instance :${QName} is disposed");
            }
        }

        public void Dispose()
        {
            if (_disposed) return;

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _disposed = true;

            if (disposing)
            {
                _taskQ.CompleteAdding();

                try
                {
                    Task.WaitAll(_consumers);
                }
                catch
                {
                    // ignored
                }
                _taskQ.Dispose();
            }
        }
    }
}