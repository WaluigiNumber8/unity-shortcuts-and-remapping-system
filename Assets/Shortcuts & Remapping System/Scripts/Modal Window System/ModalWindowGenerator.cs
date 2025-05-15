using System.Collections.Generic;
using RedRats.Core;
using UnityEngine;
using UnityEngine.Pool;

namespace RedRats.UI.ModalWindows
{
    /// <summary>
    /// Generates generic, customizable Modal Windows.
    /// </summary>
    public class ModalWindowGenerator : MonoBehaviour
    {
        [SerializeField] private ModalWindow windowPrefab;
        [SerializeField] private RectTransform poolParent;

        private Dictionary<string, ModalWindow> identifiedWindows;
        private ObjectPool<ModalWindow> windowPool;

        private void Awake()
        {
            identifiedWindows = new Dictionary<string, ModalWindow>();
            windowPool = new ObjectPool<ModalWindow>(
                () =>
                {
                    ModalWindow window = Instantiate(windowPrefab, poolParent);
                    window.OnClose += () =>
                    {
                        if (identifiedWindows.ContainsValue(window)) return;
                        windowPool.Release(window);
                    };
                    return window;
                },
                w =>
                {
                    w.transform.SetAsLastSibling();
                    w.gameObject.SetActive(true);
                },
                w => w.gameObject.SetActive(false),
                Destroy,
                true, 2, 6
            );
        }

        /// <summary>
        /// Open a modal window.
        /// </summary>
        /// <param name="data">The data to prepare the window with.</param>
        /// <param name="key">The key that identifies the window. (Is used when updating the window information is needed.)</param>
        public void Open(ModalWindowData data, string key)
        {
            Preconditions.IsStringNotNullOrEmpty(key, nameof(key));

            PrepareWindowIfNotExists(key);
            ModalWindow window = identifiedWindows[key];
            window.OpenFor(data);
        }

        /// <summary>
        /// Open a modal window.
        /// </summary>
        /// <param name="data">The data to prepare the window with.</param>
        public void Open(ModalWindowData data)
        {
            ModalWindow window = windowPool.Get();
            window.OpenFor(data);
        }

        /// <summary>
        /// Returns a modal window's first column <see cref="Transform"/>.
        /// </summary>
        /// <param name="key">The ID of the window.</param>
        /// <returns><see cref="Transform"/> of the left-most column.</returns>
        public Transform GetColumn1(string key)
        {
            Preconditions.IsStringNotNullOrEmpty(key, nameof(key));

            PrepareWindowIfNotExists(key);
            return identifiedWindows[key].FirstColumnContent;
        }

        /// <summary>
        /// Returns a modal window's second column <see cref="Transform"/>.
        /// </summary>
        /// <param name="key">The ID of the window.</param>
        /// <returns><see cref="Transform"/> of the right-most column.</returns>
        public Transform GetColumn2(string key)
        {
            Preconditions.IsStringNotNullOrEmpty(key, nameof(key));

            PrepareWindowIfNotExists(key);
            return identifiedWindows[key].SecondColumnContent;
        }

        /// <summary>
        /// Makes sure that a window under a specific key is created.
        /// </summary>
        /// <param name="key">The key of the window to check.</param>
        private void PrepareWindowIfNotExists(string key)
        {
            if (!identifiedWindows.ContainsKey(key)) identifiedWindows.Add(key, windowPool.Get());
        }
        
        public int ActiveWindows { get => windowPool.CountActive; }
    }
}