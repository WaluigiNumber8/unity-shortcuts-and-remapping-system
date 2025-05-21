using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace Rogium.Systems.Shortcuts
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    [DisplayStringFormat("{modifier1}+{modifier2}+{button}")]
    public class TwoOptionalModifiersComposite : InputBindingComposite<float>
    {
        [InputControl(layout = "Button")] public int modifier1;
        [InputControl(layout = "Button")] public int modifier2;
        [InputControl(layout = "Button")] public int button;
        
        public enum ModifiersOrder
        {
            Default = 0,
            Ordered = 1,
            Unordered = 2
        }
        
        [Tooltip("By default it follows the Input Consumption setting to determine if the modifers keys need to be pressed first.")]
        public ModifiersOrder modifiersOrder = ModifiersOrder.Default;
        
        public override float ReadValue(ref InputBindingCompositeContext context)
        {
            return ModifiersArePressed(ref context) ? context.ReadValue<float>(button) : default;
        }

        private bool ModifiersArePressed(ref InputBindingCompositeContext context)
        {
            bool modifier1Down = modifier1 == 0 || context.ReadValueAsButton(modifier1);
            bool modifier2Down = modifier2 == 0 || context.ReadValueAsButton(modifier2);
            bool modifiersDown = modifier1Down && modifier2Down;

            if (modifiersDown && modifiersOrder == ModifiersOrder.Ordered)
            {
                double timestamp = context.GetPressTime(button);
                double timestamp1 = (modifier1 == 0) ? timestamp : context.GetPressTime(modifier1);
                double timestamp2 = (modifier1 == 0) ? timestamp : context.GetPressTime(modifier2);

                return timestamp1 <= timestamp && timestamp2 <= timestamp;
            }

            return modifiersDown;
        }
        
        public override float EvaluateMagnitude(ref InputBindingCompositeContext context) => ReadValue(ref context);

        static TwoOptionalModifiersComposite() => InputSystem.RegisterBindingComposite<TwoOptionalModifiersComposite>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
        } 
        
        protected override void FinishSetup(ref InputBindingCompositeContext context)
        {
            if (modifiersOrder == ModifiersOrder.Default) modifiersOrder = ModifiersOrder.Ordered;
            else modifiersOrder = InputSystem.settings.shortcutKeysConsumeInput ? ModifiersOrder.Ordered : ModifiersOrder.Unordered;
        }
    }
}