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
        [SerializeField] private ParticleSystem affectedParticle;
        [SerializeField] private ParticleSystem burstParticle;
        [SerializeField] private Image background;
        [Space] 
        [SerializeField] private Color[] backgroundColors;
        [SerializeField] private Color[] particleColors;
        
        private int currentBackgroundColorIndex = 0;
        private int currentParticleColorIndex = 0;

        public void SaveChanges() => InputBindingEditor.Instance.SaveChanges();
        public void StartEditing() => InputBindingEditor.Instance.StartEditing();

        public void ChangeBackground()
        {
            currentBackgroundColorIndex++;
            currentBackgroundColorIndex %= backgroundColors.Length;
            Color newColor = backgroundColors[currentBackgroundColorIndex];
            background.color = newColor;
        } 
        
        public void ChangeParticleColor()
        {
            currentParticleColorIndex++;
            currentParticleColorIndex %= particleColors.Length;
            Color newColor = particleColors[currentParticleColorIndex];
            ParticleSystem.MainModule main = affectedParticle.main;
            main.startColor = newColor;
            ParticleSystem.MainModule burstMain = burstParticle.main;
            burstMain.startColor = newColor;
        }
        
        public void TriggerBurst()
        {
            burstParticle.Play();
        }
    }
}