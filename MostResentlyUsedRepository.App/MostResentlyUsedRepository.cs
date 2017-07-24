using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MostResentlyUsedRepository.App
{
    internal class MostResentlyUsedRepository<TKey, TValue>
    {
        private readonly int _capacity;
        private readonly TKey[] _keyStore;
        private readonly Dictionary<TKey, Tuple<TValue, int>> _dictionary;

        private int _index;

        public MostResentlyUsedRepository(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(capacity)} must be greater then Zero");
            }

            _index = 0;
            _capacity = capacity;
            _keyStore = new TKey[_capacity];
            _dictionary = new Dictionary<TKey, Tuple<TValue, int>>(_capacity);
        }

        public void Put(TKey key, TValue val)
        {
            if (ShouldUpdate(key))
            {
                Update(key, val);
            }
            else
            {
                CalculateNextIndex();

                if (IsOverCapacity())
                {
                    RemoveItem();
                }

                PutNewItem(key, val);
            }
        }

        private bool ShouldUpdate(TKey key)
        {
            return Contains(key);
        }

        private void Update(TKey key, TValue val)
        {
            _index = GetIndex(key);
            _dictionary[key] = new Tuple<TValue, int>(val, _index);
        }

        private int GetIndex(TKey key)
        {
            return _dictionary[key].Item2;
        }

        private bool IsOverCapacity()
        {
            return IsDefaultAt(_index) == false;
        }

        private void PutNewItem(TKey key, TValue val)
        {
            _dictionary[key] = new Tuple<TValue, int>(val, _index);
            SetKeyInStore(key);
        }

        private bool Contains(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        private void RemoveItem()
        {
            TKey toRemove = _keyStore[_index];
            _dictionary.Remove(toRemove);
        }

        private void CalculateNextIndex()
        {
            _index = (_index + 1) % _capacity;
        }

        private bool IsDefaultAt(int itemIndex)
        {
            return _keyStore[itemIndex].Equals(default(TKey));
        }

        public TValue Get(TKey key)
        {
            if (Contains(key) == false)
            {
                return default(TValue);
            }

            CalculateNextIndex();
            SetKeyInStore(key);

            return GetValue(key);
        }

        private void SetKeyInStore(TKey key)
        {
            _keyStore[_index] = key;
        }

        private TValue GetValue(TKey key)
        {
            return _dictionary[key].Item1;
        }

        public List<Tuple<TKey, TValue>> All()
        {
            return _dictionary.Select(x => new Tuple<TKey, TValue>(x.Key, x.Value.Item1)).ToList();
        }
    }
}
