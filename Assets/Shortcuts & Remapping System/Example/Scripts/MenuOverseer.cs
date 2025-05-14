using UnityEngine;

namespace RedRats.Example.Core
{
    /// <summary>
    /// Overseer actions happening in the menu.
    /// </summary>
    public class MenuOverseer : MonoBehaviour
    {
        [SerializeField] private RectTransform propertiesContent;

        private async void Start()
        {
            PropertyColumnBuilder b = new(propertiesContent);
            await Awaitable.NextFrameAsync();
            b.Build();       
        }
    }
}