using System;
using System.Collections.Generic;

namespace RedRats.Core
{
    /// <summary>
    /// A <see cref="Stack{T}"/> that can be observed for changes.
    /// </summary>
    public class ObservableStack<T> : Stack<T>
    {
        public event Action OnChange;
        public event Action<T> OnPush;
        public event Action<T> OnPop;
        public event Action OnClear;

        public new void Push(T item)
        {
            base.Push(item);
            OnPush?.Invoke(item);
            OnChange?.Invoke();
        }

        public new T Pop()
        {
            T item = base.Pop();
            OnPop?.Invoke(item);
            OnChange?.Invoke();
            return item;
        }

        public new void Clear()
        {
            base.Clear();
            OnClear?.Invoke();
            OnChange?.Invoke();
        }
    }
}