using System;

namespace RedRats.UI.ModalWindows
{
    /// <summary>
    /// Contains data needed for opening a modal window.
    /// </summary>
    public class ModalWindowData
    {
        private string acceptButtonText = string.Empty;
        private string denyButtonText = string.Empty;
        private string specialButtonText = string.Empty;

        private Action onAcceptAction;
        private Action onDenyAction;
        private Action onSpecialAction;
        
        private string message = string.Empty;
        private string headerText = string.Empty;
        private ModalWindowLayoutType layout = ModalWindowLayoutType.Message;

        private ModalWindowData() { }
        
        public void UpdateLayout(ModalWindowLayoutType newLayout) => layout = newLayout;

        public string AcceptButtonText { get => acceptButtonText; }
        public string DenyButtonText { get => denyButtonText; }
        public string SpecialButtonText { get => specialButtonText; }
        public Action OnAcceptAction { get => onAcceptAction; }
        public Action OnDenyAction { get => onDenyAction; }
        public Action OnSpecialAction { get => onSpecialAction; }
        public string Message { get => message; }
        public string HeaderText { get => headerText; }
        public ModalWindowLayoutType Layout { get =>layout; }

        public class Builder
        {
            private readonly ModalWindowData data = new();
            
            public Builder WithAcceptButton(string acceptButtonText, Action onAcceptAction = null)
            {
                data.acceptButtonText = acceptButtonText;
                data.onAcceptAction = onAcceptAction;
                return this;
            }
            
            public Builder WithDenyButton(string denyButtonText, Action onDenyAction = null)
            {
                data.denyButtonText = denyButtonText;
                data.onDenyAction = onDenyAction;
                return this;
            }
            
            public Builder WithSpecialButton(string specialButtonText, Action onSpecialAction = null)
            {
                data.specialButtonText = specialButtonText;
                data.onSpecialAction = onSpecialAction;
                return this;
            }

            public Builder WithMessage(string message)
            {
                data.message = message;
                return this;
            }
            
            public Builder WithHeaderText(string headerText)
            {
                data.headerText = headerText;
                return this;
            }
            
            public Builder WithLayout(ModalWindowLayoutType layout)
            {
                data.layout = layout;
                return this;
            }
            
            public ModalWindowData Build() => data;
        }
    }
}