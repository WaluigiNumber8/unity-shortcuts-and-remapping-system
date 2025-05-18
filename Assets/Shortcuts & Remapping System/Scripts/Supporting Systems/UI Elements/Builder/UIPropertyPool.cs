using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.Object;

namespace RedRats.UI.Properties
{
    /// <summary>
    /// Contains a pool of <see cref="IPBase"/>s.
    /// </summary>
    public class UIPropertyPool<T> where T : IPBase
    {
        private readonly ObjectPool<T> propertyPool;

        public UIPropertyPool(T prefab, Transform poolParent, int defaultSize = 10, int maxSize = 10000)
        {
            propertyPool = new ObjectPool<T>(
                () =>
                {
                    T property = Instantiate(prefab, poolParent);
                    property.OnReleaseToPool += () => propertyPool?.Release(property);
                    return property;
                },
                p => p.gameObject.SetActive(true),
                p =>
                {
                    p.transform.SetParent(poolParent, false);
                    p.gameObject.SetActive(false);
                },
                p => Destroy(p.gameObject),
                true, defaultSize, maxSize);
        }
        
        public T Get(Transform newParent = null)
        {
            T property = propertyPool.Get();
            if (newParent != null) property.transform.SetParent(newParent, false);
            return property;
        }
    }
}