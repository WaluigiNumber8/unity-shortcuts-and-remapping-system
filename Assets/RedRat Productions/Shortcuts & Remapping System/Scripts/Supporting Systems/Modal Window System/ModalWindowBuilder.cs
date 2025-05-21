using RedRats.Core;
using UnityEngine;

namespace RedRats.UI.ModalWindows
{
    /// <summary>
    /// Builds and opens modal windows.
    /// </summary>
    public class ModalWindowBuilder : MonoSingleton<ModalWindowBuilder>
    {
        [SerializeField] private ModalWindowGenerator windowGenerator;
        
        /// <summary>
        /// Opens a new generic modal window.
        /// </summary>
        /// <param name="data">Data to load the window with.</param>
        public void OpenWindow(ModalWindowData data)
        {
            data.UpdateLayout(ModalWindowLayoutType.Message);
            windowGenerator.Open(data);
        }
        
        /// <summary>
        /// Opens a new generic window.
        /// </summary>
        /// <param name="data">The data to load the window with.</param>
        /// <param name="key">Unique key that will identify the window.</param>
        /// <param name="column1">Transform of the property column.</param>
        public void OpenWindow(ModalWindowData data, string key, out Transform column1)
        {
            data.UpdateLayout(ModalWindowLayoutType.Columns1);
            windowGenerator.Open(data, key);
            column1 = windowGenerator.GetColumn1(key);
        }
        
        /// <summary>
        /// Opens a new generic window.
        /// </summary>
        /// <param name="data">The data to load the window with.</param>
        /// <param name="key">Unique key that will identify the window.</param>
        /// <param name="column1">Transform of the 1st column.</param>
        /// <param name="column2">Transform of the 2nd column.</param>
        public void OpenWindow(ModalWindowData data, string key, out Transform column1, out Transform column2)
        {
            data.UpdateLayout(ModalWindowLayoutType.Columns2);
            windowGenerator.Open(data, key);
            column1 = windowGenerator.GetColumn1(key);
            column2 = windowGenerator.GetColumn2(key);
        }
        
        private void SendToFront(ModalWindowBase window) => window.transform.SetAsLastSibling();

        public int GenericActiveWindows => windowGenerator.ActiveWindows;
    }
}