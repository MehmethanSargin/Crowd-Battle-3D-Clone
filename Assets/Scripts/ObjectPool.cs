using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class ObjectPool<T> where T: MonoBehaviour
    {
        private Queue<T> _queue;

        public ObjectPool(T prefab, int count, Transform parent = null)
        {
            _queue = new Queue<T>();
            for (int i = 0; i < count; i++)
            {
                T t = Object.Instantiate(prefab, parent);
                t.gameObject.SetActive(false);
                _queue.Enqueue(t);
            }
        }

        public T GetObject()
        {
            T t = _queue.Dequeue();
            t.gameObject.SetActive(true);
            _queue.Enqueue(t);
            return t;
        }
    }
}