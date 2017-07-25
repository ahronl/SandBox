using System;
using System.Threading;
using System.Threading.Tasks;
using EventBus.App.Handlers;

namespace EventBus.App.Publishers
{
    internal class TaskPublisher : IEventPublisher
    {
        private readonly ISubscriberStore _store;
        private readonly CancellationTokenSource _cancellationTokenSource;

        private Task _task;

        public TaskPublisher(ISubscriberStore store)
        {
            _store = store;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Post(Tuple<Type, IEventData> item)
        {
            _task = Task.Factory.StartNew(() =>
            {
                var token = _cancellationTokenSource.Token;

                foreach (var handler in _store.GetHandlers(item.Item1))
                {
                    if (token.IsCancellationRequested) return;

                    handler.Invoke(item.Item2, item.Item1);
                }
            }, _cancellationTokenSource.Token);
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            try
            {
                _task?.Wait();
            }
            catch (Exception)
            {
                
            }
        }
    }
}
