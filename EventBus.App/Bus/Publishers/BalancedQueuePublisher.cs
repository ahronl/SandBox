using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventBus.App.Bus.Handlers;

namespace EventBus.App.Bus.Publishers
{
    internal class BalancedQueuePublisher : IEventPublisher
    {
        private readonly ISubscriberStore _store;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly Dictionary<int, BlockingCollection<Tuple<Type, IEventData>>> _queuePool;
        private readonly object _locker;
        private readonly int _amount;

        private Task[] _consumers;
        private int _index;
        private bool _disposing;

        public BalancedQueuePublisher(int amount, ISubscriberStore store)
        {
            _store = store;
            _index = 0;
            _amount = amount;
            _disposing = false;
            _locker = new object();
            _cancellationTokenSource = new CancellationTokenSource();
            _queuePool = new Dictionary<int, BlockingCollection<Tuple<Type, IEventData>>>(amount);
            PopulateQueuePool(amount);
        }

        private void PopulateQueuePool(int amount)
        {
            _consumers = new Task[amount];

            for (int i = 0; i < amount; i++)
            {
                var bc = new BlockingCollection<Tuple<Type, IEventData>>();

                _queuePool[i] = bc;

                _consumers[i] = Task.Factory.StartNew((x) =>
                {
                    StartConsuming(bc, _cancellationTokenSource.Token);
                }, TaskCreationOptions.LongRunning, _cancellationTokenSource.Token);
            }
        }

        private void StartConsuming(BlockingCollection<Tuple<Type, IEventData>> bc, CancellationToken token)
        {
            foreach (var item in bc.GetConsumingEnumerable())
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

        public void Post(Tuple<Type, IEventData> item)
        {
            lock (_locker)
            {
                _index = (_index + 1) % _amount;
                AddTo(_queuePool[_index], item);
            }
        }

        private void AddTo(BlockingCollection<Tuple<Type, IEventData>> bc, Tuple<Type, IEventData> item)
        {
            if (bc.IsAddingCompleted)
            {
               return;
            }

            bc.Add(item);
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
                    _queuePool.Values.ToList().ForEach(x => x.CompleteAdding());
                    _cancellationTokenSource.Cancel();
                    Task.WaitAll(_consumers);
                    _queuePool.Values.ToList().ForEach(x => x.Dispose());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
    }
}
