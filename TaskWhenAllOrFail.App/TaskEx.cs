using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TaskWhenAllOrFail.App
{
    static class TaskEx
    {
        public static Task WhenAllOrFail(this IEnumerable<Action<CancellationToken>> actions)
        {
            CancellationTokenSource source = new CancellationTokenSource();

            List<Task> allTasks = actions.Select(x =>
            {
                var token = source.Token;

                Action aAction = () =>
                {
                    if (token.IsCancellationRequested) return;

                    try
                    {
                        x(token);
                    }
                    catch (Exception)
                    {
                        source.Cancel();
                        throw;
                    }
                };

                return Task.Factory.StartNew(aAction, source.Token, TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent, TaskScheduler.Default);

            }).ToList();

            if (allTasks.Count == 0)
                throw new ArgumentException("No tasks to wait on");

            var tcs = new TaskCompletionSource<bool>();

            int tasksCompletedCount = 0;

            Action<Task> completedAction = t =>
            {
                if (t.IsFaulted)
                {
                    tcs.TrySetException(t.Exception);
                    tcs.SetResult(false);
                    return;
                }
                if (t.IsCanceled)
                {
                    tcs.TrySetCanceled();
                    tcs.SetResult(false);
                    return;
                }
                if (Interlocked.Increment(ref tasksCompletedCount) == allTasks.Count)
                {
                    tcs.SetResult(true);
                }
            };

            allTasks.ForEach(t => t.ContinueWith(completedAction, TaskContinuationOptions.ExecuteSynchronously));

            return tcs.Task;
        }

        public static Task<T[]> WhenAllOrFail<T>(this IEnumerable<Func<CancellationToken, T>> funcs)
        {
            CancellationTokenSource source = new CancellationTokenSource();

            List<Task<T>> allTasks = funcs.Select(x =>
            {
                var token = source.Token;

                Func<T> aFunc = () =>
                {
                    if (token.IsCancellationRequested) return default(T);

                    try
                    {
                        return x(token);
                    }
                    catch (Exception)
                    {
                        source.Cancel();
                        throw;
                    }
                };

                return Task<T>.Factory.StartNew(aFunc, source.Token, TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent, TaskScheduler.Default);

            }).ToList();

            if (allTasks.Count == 0)
                throw new ArgumentException("No tasks to wait on");

            var tcs = new TaskCompletionSource<T[]>();

            int tasksCompletedCount = 0;

            Action<Task<T>> completedAction = t =>
            {
                if (t.IsFaulted)
                {
                    tcs.TrySetException(t.Exception);
                    return;
                }
                if (t.IsCanceled)
                {
                    tcs.TrySetCanceled();
                    return;
                }
                if (Interlocked.Increment(ref tasksCompletedCount) == allTasks.Count)
                {
                    tcs.SetResult(allTasks.Select(ct => ct.Result).ToArray());
                }
            };

            allTasks.ForEach(t => t.ContinueWith(completedAction, TaskContinuationOptions.ExecuteSynchronously));

            return tcs.Task;
        }
    }
}