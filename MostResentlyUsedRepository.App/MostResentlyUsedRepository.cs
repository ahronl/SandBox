using System;
using System.Collections.Generic;
using System.Linq;

namespace MostResentlyUsedRepository.App
{
    internal class MostResentlyUsedRepository<TKey, TValue>
    {
        private readonly int _capacity;
        private readonly List<TKey> _accessSequence;
        private readonly Dictionary<TKey, TValue> _dictionary;

        public MostResentlyUsedRepository(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(capacity)} must be greater then Zero");
            }
            _capacity = capacity;
            _accessSequence = new List<TKey>(_capacity);
            _dictionary = new Dictionary<TKey, TValue>(_capacity);
        }

        internal IEnumerable<Tuple<TKey,TValue>> All()
        {
            return _dictionary.Select(x => new Tuple<TKey, TValue>(x.Key, x.Value));
        }

        public void Put(TKey key, TValue val)
        {
            if (_dictionary.ContainsKey(key))
            {
                _dictionary[key] = val;
                UpdateAccessSequence(key);
            }
            else
            {
                if (_accessSequence.Count == _capacity)
                {
                    RemoveOldest();
                }

                _dictionary[key] = val;

                AddAccessSequence(key);
            }
        }

        private void AddAccessSequence(TKey key)
        {
            _accessSequence.Add(key);
        }

        private void RemoveOldest()
        {
            TKey key = _accessSequence.First();
            _accessSequence.Remove(key);
            _dictionary.Remove(key);
        }

        public TValue Get(TKey key)
        {
            UpdateAccessSequence(key);
            return _dictionary[key];
        }

        private void UpdateAccessSequence(TKey key)
        {
            _accessSequence.Remove(key);
            _accessSequence.Add(key);
        }
    }
}
