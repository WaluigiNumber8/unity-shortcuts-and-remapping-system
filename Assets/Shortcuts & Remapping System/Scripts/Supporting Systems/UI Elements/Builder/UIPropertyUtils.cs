using UnityEngine;

namespace RedRats.UI.Properties
{
    /// <summary>
    /// Helper methods for UI Properties.
    /// </summary>
    public static class UIPropertyUtils
    {
        public static void ReleaseAllProperties(this Transform content, bool includeSelf = false)
        {
            IPBase[] p = content.GetComponentsInChildren<IPBase>();
            if (p.Length == 0) return;
            foreach (IPBase property in p)
            {
                if (includeSelf == false && property.transform == content) continue;
                property.ReleaseToPool();
            }
        }
    }
}