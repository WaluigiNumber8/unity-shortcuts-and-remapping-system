using System;
using UnityEngine;
using UnityEngine.UI;

namespace RedRats.UI.ModalWindows
{
    /// <summary>
    /// A base for all modal windows.
    /// </summary>
    
    public abstract class ModalWindowBase : MonoBehaviour
    {
        public event Action OnClose;
        
        [Header("UI")]
        [SerializeField] protected GeneralUIInfo generalUI;

        private bool isOpen;
        
        protected virtual void Awake()
        {
            isOpen = generalUI.entireArea.activeSelf;
            generalUI.backgroundArea.onClick.AddListener(Close);
            if (generalUI.closeButton != null) generalUI.closeButton.onClick.AddListener(Close);
        }

        /// <summary>
        /// Opens the window.
        /// </summary>
        public void Open()
        {
            generalUI.entireArea.SetActive(true);
            isOpen = true;
        }

        /// <summary>
        /// Close the window.
        /// </summary>
        public virtual void Close()
        {
            if (!isOpen) return;
            generalUI.entireArea.SetActive(false);
            OnClose?.Invoke();
            isOpen = false;
        }

        public Button CloseButton { get => generalUI.closeButton; }
        public bool IsOpen { get => isOpen; }
        
        [Serializable]
        public struct GeneralUIInfo
        {
            public GameObject entireArea;
            public Image windowArea;
            public Button backgroundArea;
            public Button closeButton;
        }
    }
}