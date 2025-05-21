using System.Collections.Generic;
using System.Linq;
using RedRats.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RedRats.ShortcutSystem.Linking
{
    /// <summary>
    /// Stores a set of actions that can be triggered via button shortcuts.
    /// </summary>
    public class ShortcutProfile : MonoBehaviour
    {
        [SerializeField] private ShortcutData[] shortcuts;
        [SerializeField] private bool overrideAll;

        private List<ShortcutProfile> lastProfiles;

        private void Awake()
        {
            //Check duplicate triggers in shortcuts
            ISet<InputActionReference> triggers = new HashSet<InputActionReference>();
            foreach (ShortcutData shortcut in shortcuts)
            {
                if (triggers.Add(shortcut.trigger)) continue;
                throw new FoundDuplicationException($"Duplicate trigger found: '{shortcut.trigger.action.name}' in '{gameObject.name}'");
            }
        }

        private void OnEnable()
        {
            RefreshAllInput();
            FindAndDisableProfiles();
            LinkAll();
        }

        private void OnDisable()
        {
            EnableLastProfiles();
            UnlinkAll();
        }

        public void Set(ShortcutData[] newShortcuts)
        {
            UnlinkAll();
            shortcuts = newShortcuts.AsCopy();
            LinkAll();
        }

        public void SetAsOverrideAll(bool value)
        {
            overrideAll = value;
            if (!gameObject.activeSelf) return;
            FindAndDisableProfiles();
        }

        private void RefreshAllInput()
        {
            if (shortcuts == null || shortcuts.Length == 0) return;
            foreach (ShortcutData shortcut in shortcuts)
            {
                shortcut.RefreshInput();
            }
        }
        
        private void LinkAll()
        {
            if (shortcuts == null || shortcuts.Length == 0) return;
            foreach (ShortcutData shortcut in shortcuts)
            {
                shortcut.Link();
            }
        }

        private void UnlinkAll()
        {
            if (shortcuts == null || shortcuts.Length == 0) return;
            foreach (ShortcutData shortcut in shortcuts)
            {
                shortcut.Unlink();
            }
        }

        private void FindAndDisableProfiles()
        {
            if (!overrideAll) return;
            if (lastProfiles != null && lastProfiles.Count > 0) return;
            lastProfiles = FindObjectsByType<ShortcutProfile>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).Where(p => p != this && p.enabled).ToList();
            lastProfiles.ForEach(profile => profile.enabled = false);
        }

        private void EnableLastProfiles()
        {
            if (!overrideAll) return;
            if (lastProfiles == null || lastProfiles.Count == 0) return;
            ShortcutProfile[] profiles = FindObjectsByType<ShortcutProfile>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            if (profiles.Any(p => p != this && (p.enabled && p.OverridesAll))) return;
            lastProfiles.ForEach(profile =>
            {
                if (profile != null) profile.enabled = true;
            });
            lastProfiles = null;
        }
        
        public bool OverridesAll { get => overrideAll; }
    }
}