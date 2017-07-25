using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using EventBus.App.Handlers;

namespace EventBus.App.Publishers
{
    internal class ProducerConsumerPublisher: IEventPublisher
    {
        private readonly BlockingCollection<Tuple<Type, IEventData>> _blockingCollection;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly Task[] _consumers;
        private readonly ISubscriberStore _store;

        private bool _disposing;

        public ProducerConsumerPublisher(int consumerAmount, ISubscriberStore store)
        {
            _disposing = false;
            _store = store;
            _cancellationTokenSource = new CancellationTokenSource();
            _blockingCollection = new BlockingCollection<Tuple<Type, IEventData>>();
            _consumers = new Task[consumerAmount];
            StartConsumers(consumerAmount);
        }

        private void StartConsumers(int consumerAmount)
        {
            for (int i = 0; i < consumerAmount; i++)
            {
                _consumers[i] = Task.Factory.StartNew(() =>
                {
                    PostEvents(_cancellationTokenSource.Token);
                }, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }
        }

        public void Post(Tuple<Type, IEventData> item)
        {
            if (_blockingCollection.IsAddingCompleted)
            {
                return;
            }

            _blockingCollection.Add(item);
        }

        private void PostEvents(CancellationToken token)
        {
            foreach (Tuple<Type, IEventData> item in _blockingCollection.GetConsumingEnumerable())
            {
                if (token.IsCancellationRequested)
                {
                   return;
                }

                foreach (IEventHandler handler in _store.GetHandlers(item.Item1))
                {
                    handler.Invoke(item.Item2, item.Item1);
                }
            }
        }

        public void Dispose()
        {
            if (_disposing == true) return;

            _disposing = true;

            Dispose(_disposing);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                try
                {
                    _blockingCollection.CompleteAdding();
                    _cancellationTokenSource.Cancel();
                    Task.WaitAll(_consumers);
                    _blockingCollection.Dispose();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
    }
}