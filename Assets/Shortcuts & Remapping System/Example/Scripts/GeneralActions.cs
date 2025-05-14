using RedRats.ActionHistory;
using RedRats.Core;
using UnityEngine;
using UnityEngine.UI;

namespace RedRats.Example.Core
{
    /// <summary>
    /// Contains general actions, triggered by interactive properties.
    /// </summary>
    public class GeneralActions : MonoSingleton<GeneralActions>
    {
        [SerializeField] private UIGridRenderer uiGridRenderer;
        [SerializeField] private Image gridBorderImage;
        [Space] 
        [SerializeField] private Sprite borderSprite1;
        [SerializeField] private Sprite borderSprite2;


        public void ChangeGridColor(int value)
        {
            uiGridRenderer.color = Color.HSVToRGB(value / 360f, 0.25f, 0.56f);
        }
        
        public void ToggleGridVisibility(bool value)
        {
            uiGridRenderer.gameObject.SetActive(value);
        }

        public void ChangeBorder(int value)
        {
            Sprite borderSprite = value switch
            {
                0 => borderSprite1,
                1 => borderSprite2,
                _ => new SpriteBuilder().WithSingleColorTexture(Color.clear, 1, 1).Build()
            };
            gridBorderImage.sprite = borderSprite;
        }
        
        public void Undo() => ActionHistorySystem.Undo();
        public void Redo() => ActionHistorySystem.Redo();
    }
}