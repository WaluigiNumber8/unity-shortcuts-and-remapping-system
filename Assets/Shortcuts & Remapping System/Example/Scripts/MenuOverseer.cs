using UnityEngine;

namespace RedRats.Example.Core
{
    /// <summary>
    /// Overseer actions happening in the menu.
    /// </summary>
    public class MenuOverseer : MonoBehaviour
    {
        [SerializeField] private RectTransform propertiesContent;

        private void Start()
        {
            PropertyColumnBuilder b = new(propertiesContent);
            b.Build();       
        }
    }
}