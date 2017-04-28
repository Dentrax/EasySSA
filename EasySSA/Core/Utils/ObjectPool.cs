using System;
using System.Collections.Concurrent;

namespace EasySSA.Core.Utils {
    public sealed class ObjectPool<T> where T : IDisposable {
        private ConcurrentBag<T> m_concurrentBag;
        private Func<T> m_objectCallBack;
        private Func<bool> m_disposeCallBack;

        public int Count => m_concurrentBag.Count;

        public ObjectPool(Func<T> objectGenerator, Func<bool> disposeCondition) {
            m_concurrentBag = new ConcurrentBag<T>();
            m_objectCallBack = objectGenerator;
            m_disposeCallBack = disposeCondition;
        }

        public T GetObject() {
            T item;
            if (m_concurrentBag.TryTake(out item)) return item;
            return m_objectCallBack();
        }

        public void PutObject(T item) {
            if (m_disposeCallBack()) {
                item.Dispose();
            } else {
                m_concurrentBag.Add(item);
            }
        }

        public void Reserve(int num) {
            for (int i = 0; i < num; i++) {
                m_concurrentBag.Add(m_objectCallBack());
            }
        }

        public void Shrink(int fitSize) {
            if (m_concurrentBag.Count > fitSize) {
                for (int i = 0; i < m_concurrentBag.Count - fitSize; i++) {
                    T item;
                    if (m_concurrentBag.TryTake(out item)) {
                        item.Dispose();
                    }
                }
            }
        }
    }
}
