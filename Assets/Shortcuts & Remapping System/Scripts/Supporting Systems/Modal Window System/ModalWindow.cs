using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RedRats.UI.ModalWindows
{
    /// <summary>
    /// Draws a modal window on the screen.
    /// </summary>
    public class ModalWindow : ModalWindowBase
    {
        [SerializeField] private UIInfo ui;

        protected override void Awake()
        {
            base.Awake();
            generalUI.backgroundArea.onClick.AddListener(OnDeny);
        }

        public void OpenFor(ModalWindowData data)
        {
            Init();
            DrawHeader(data);
            switch (data.Layout)
            {
                case ModalWindowLayoutType.Message:
                    ui.layout.area.gameObject.SetActive(true);
                    ui.layout.message.area.gameObject.SetActive(true);
                    ui.layout.message.text.gameObject.SetActive(true);
                    ui.layout.message.text.text = data.Message;
                    break;
                case ModalWindowLayoutType.Columns1:
                    ui.layout.properties.area.gameObject.SetActive(true);
                    ui.layout.properties.firstColumn.gameObject.SetActive(true);
                    ui.layout.properties.secondColumn.gameObject.SetActive(false);
                    ui.layout.properties.firstColumnContent.gameObject.SetActive(true);
                    ui.layout.properties.secondColumnContent.gameObject.SetActive(false);
                    ui.layout.area.gameObject.SetActive(true);
                    break;
                case ModalWindowLayoutType.Columns2:
                    ui.layout.properties.area.gameObject.SetActive(true);
                    ui.layout.properties.firstColumn.gameObject.SetActive(true);
                    ui.layout.properties.secondColumn.gameObject.SetActive(true);
                    ui.layout.properties.firstColumnContent.gameObject.SetActive(true);
                    ui.layout.properties.secondColumnContent.gameObject.SetActive(true);
                    ui.layout.area.gameObject.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            DrawFooter(data);
            UpdateRect();
            Open();
        }
        
        /// <summary>
        /// Update the message of the window.
        /// </summary>
        /// <param name="newMessage">The new text to use.</param>
        public void UpdateMessageText(string newMessage) => ui.layout.message.text.text = newMessage;

        /// <summary>
        /// Stores actions, that will take place once the Accept button is clicked.
        /// </summary>
        public void OnAccept()
        {
            ui.footer.OnAcceptButtonClick?.Invoke();
            CloseWindow();
        }

        /// <summary>
        /// Stores actions, that will take place once the Deny button is clicked.
        /// </summary>
        public void OnDeny()
        {
            ui.footer.OnDenyButtonClick?.Invoke();
            CloseWindow();
        }

        /// <summary>
        /// Stores actions, that will take place once the Special button is clicked.
        /// </summary>
        public void OnSpecial()
        {
            ui.footer.OnSpecialButtonClick?.Invoke();
            CloseWindow();
        }

        /// <summary>
        /// Closes the Window.
        /// </summary>
        private void CloseWindow()
        {
            Close();
            ui.layout.message.area.gameObject.SetActive(false);
            ui.layout.properties.area.gameObject.SetActive(false);
        }

        #region Window Part Drawing

        /// <summary>
        /// Draws the Header part of the Window.
        /// </summary>
        private void DrawHeader(ModalWindowData data)
        {
            bool usesHeader = !string.IsNullOrEmpty(data.HeaderText);
            ui.header.text.text = data.HeaderText;
            ui.header.text.gameObject.SetActive(usesHeader);
            ui.header.area.gameObject.SetActive(usesHeader);
        }

        /// <summary>
        /// Draws the Footer of the Window.
        /// </summary>
        private void DrawFooter(ModalWindowData data)
        {
            ui.footer.acceptButtonText.text = data.AcceptButtonText;
            ui.footer.OnAcceptButtonClick = data.OnAcceptAction;

            bool usesDeny = !string.IsNullOrEmpty(data.DenyButtonText);
            ui.footer.denyButton.gameObject.SetActive(usesDeny);
            ui.footer.denyButtonText.text = data.DenyButtonText;
            ui.footer.OnDenyButtonClick = data.OnDenyAction;

            bool usesSpecial = !string.IsNullOrEmpty(data.SpecialButtonText);
            ui.footer.specialButton.gameObject.SetActive(usesSpecial);
            ui.footer.specialButtonText.text = data.SpecialButtonText;
            ui.footer.OnSpecialButtonClick = data.OnSpecialAction;
            
            ui.footer.area.gameObject.SetActive(true);
        }
        #endregion

        /// <summary>
        /// Prepares the window.
        /// </summary>
        private void Init()
        {
            ui.header.area.gameObject.SetActive(false);
            ui.layout.area.gameObject.SetActive(false);
            ui.layout.message.area.gameObject.SetActive(false);
            ui.layout.properties.area.gameObject.SetActive(false);
            ui.footer.area.gameObject.SetActive(false);
        }
        
        private void UpdateRect()
        {
            generalUI.windowArea.GetComponent<RectTransform>().ForceUpdateRectTransforms();
            Canvas.ForceUpdateCanvases();
        }

        /// <summary>
        /// Get the message text.
        /// </summary>
        public string GetMessageText => ui.layout.message.text.text;
        public Transform FirstColumnContent { get =>ui.layout.properties.firstColumnContent; }
        public Transform SecondColumnContent {get => ui.layout.properties.secondColumnContent; }

        [Serializable]
        public struct UIInfo
        {
            public HeaderInfo header;
            public LayoutInfo layout;
            public FooterInfo footer;
        }
        
        [Serializable]
        public struct HeaderInfo
        {
            public Transform area;
            public Image headerImage;
            public TextMeshProUGUI text;
        }

        [Serializable]
        public struct LayoutInfo
        {
            public Transform area;
            public MessageLayoutInfo message;
            public PropertiesLayoutInfo properties;
        }

        [Serializable]
        public struct MessageLayoutInfo
        {
            public Transform area;
            public TextMeshProUGUI text;
        }

        [Serializable]
        public struct PropertiesLayoutInfo
        {
            public Transform area;
            public Transform firstColumn;
            public Transform secondColumn;
            public Transform firstColumnContent;
            public Transform secondColumnContent;
        }

        [Serializable]
        public struct FooterInfo
        {
            public Transform area;
            public TextMeshProUGUI acceptButtonText;
            public TextMeshProUGUI denyButtonText;
            public TextMeshProUGUI specialButtonText;
            public Image acceptButtonImage;
            public Image denyButtonImage;
            public Image specialButtonImage;
            public Button acceptButton;
            public Button denyButton;
            public Button specialButton;

            public Action OnAcceptButtonClick;
            public Action OnDenyButtonClick;
            public Action OnSpecialButtonClick;
        }
    }
}
