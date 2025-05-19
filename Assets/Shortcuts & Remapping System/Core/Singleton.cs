using System;

namespace RedRats.Core
{
    /// <summary>
    /// Turns a class that inherits this into a Singleton.
    /// <p> The inheriting class also must have a <b>private constructor</b> as well as being marked as <b>sealed</b> in order to not allow any further inheritance.</p>
    /// </summary>
    public abstract class Singleton<T> where T : Singleton<T>
    {
        private static readonly Lazy<T> Lazy = new(() => (Activator.CreateInstance(typeof(T), true) as T)!);
        public static T Instance => Lazy.Value;
    }
}