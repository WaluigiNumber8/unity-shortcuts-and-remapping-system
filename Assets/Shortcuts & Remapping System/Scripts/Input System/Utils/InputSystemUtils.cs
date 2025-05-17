using System;
using System.Text;
using System.Text.RegularExpressions;
using Rogium.Systems.Shortcuts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;
using UnityEngine.InputSystem.Utilities;

namespace RedRats.ShortcutSystem.Input
{
    /// <summary>
    /// Utility functions for the input system.
    /// </summary>
    public static class InputSystemUtils
    {
        private const string GamepadDevicePath = "<Gamepad>/";
        private const string KeyboardDevicePath = "<Keyboard>/";
        private const string MouseDevicePath = "<Mouse>/";
        
        
        /// <summary>
        /// Gets the index of the binding for the given action and device.
        /// </summary>
        /// <param name="action">The action to get the binding of.</param>
        /// <param name="device">The input device to get the binding for.</param>
        /// <param name="getSecondary">Get the second binding found.</param>
        /// <returns>The index of the binding.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when device is unknown.</exception>
        public static int GetBindingIndexByDevice(InputAction action, InputDeviceType device, bool getSecondary = false)
        {
            return device switch
            {
                InputDeviceType.Keyboard => GetBindingIndex(new InputBinding(groups: InputSystem.Instance.KeyboardBindingGroup, path: null)),
                InputDeviceType.Gamepad => GetBindingIndex(new InputBinding(groups: InputSystem.Instance.GamepadBindingGroup, path: null)),
                _ => throw new ArgumentOutOfRangeException(nameof(device), device, null)
            };
            
            int GetBindingIndex(InputBinding group)
            {
                ReadOnlyArray<InputBinding> bindings = action.bindings;
                bool waitForComposite = false;
                for (int i = 0; i < bindings.Count; ++i)
                {
                    InputBinding b = bindings[i];
                    if (b.isComposite) waitForComposite = false;
                    if (waitForComposite) continue;
                    if (!group.Matches(b)) continue;
                    if (getSecondary)
                    {
                        getSecondary = false;
                        if (b.isPartOfComposite) waitForComposite = true;
                        continue;
                    }
                    return i;
                }
                return -1;
            }
        }

        /// <summary>
        /// Formats a <see cref="InputControl"/> path to a <see cref="InputBinding"/> path style.
        /// </summary>
        /// <param name="path">The path to format</param>
        public static string FormatForBindingPath(this string path)
        {
            string formatForBindingPath = Regex.Replace(path, @"^/(\w+)", "<$1>");
            formatForBindingPath = Regex.Replace(formatForBindingPath, "^/", "");
            return formatForBindingPath;
        }

        /// <summary>
        /// Get a human-readable path for a specific action.
        /// </summary>
        /// <param name="action">The action to get the binding path from.</param>
        /// <param name="device">For which device to take the path for</param>
        /// <param name="useAlt">Use the main or alt binding?</param>
        /// <param name="indexOverride">Use custom index instead of detecting by device.</param>
        /// <returns>A human-readable path.</returns>
        public static string GetPath(InputAction action, InputDeviceType device, bool useAlt = false, int indexOverride = -1)
        {
            int index = (indexOverride > -1) ? indexOverride : GetBindingIndexByDevice(action, device, useAlt);
            if (PreviousIsOptionalModifiersComposite(action, index) || PreviousIsTwoModifierComposite(action, index))
            {
                StringBuilder path = new();
                path.Append(action.bindings[index].GetPathWithoutDevice(device));           //Modifier 1
                path.Append((action.bindings[index].effectivePath == "") ? "" : "+");       //Plus
                path.Append(action.bindings[index + 1].GetPathWithoutDevice(device));       //Modifier 2
                path.Append((action.bindings[index + 1].effectivePath == "") ? "" : "+");   //Plus
                path.Append(action.bindings[index + 2].GetPathWithoutDevice(device));       //Button
                return path.ToString();
            }
            if (PreviousIsOneModifierComposite(action, index))
            {
                StringBuilder path = new();
                path.Append(action.bindings[index].GetPathWithoutDevice(device));           //Modifier
                path.Append((action.bindings[index].effectivePath == "") ? "" : "+");       //Plus
                path.Append(action.bindings[index + 1].GetPathWithoutDevice(device));       //Button
                return path.ToString();
            }
            return action.bindings[index].GetPathWithoutDevice(device);
        }

        /// <summary>
        /// Converts a human-readable path and overrides a binding for a specific action.
        /// </summary>
        /// <param name="humanReadablePath">The human-readable path to use for the override.</param>
        /// <param name="action">The action to affect.</param>
        /// <param name="device">The device, the binding belongs to.</param>
        /// <param name="useAlt">Use the main or alt binding.</param>
        /// <param name="indexOverride">Use custom index instead of detecting by device.</param>
        public static void ApplyBindingOverride(string humanReadablePath, InputAction action, InputDeviceType device, bool useAlt = false, int indexOverride = -1)
        {
            int index = (indexOverride > -1) ? indexOverride : GetBindingIndexByDevice(action, device, useAlt);
            if (PreviousIsOptionalModifiersComposite(action, index))
            {
                string[] paths = humanReadablePath.Split('+');
                switch (paths.Length)
                {
                    case 1:
                        ApplyBinding(index + 0, "");
                        ApplyBinding(index + 1, "");
                        ApplyBinding(index + 2, paths[0]);
                        return;
                    case 2:
                        ApplyBinding(index + 0, paths[0]);
                        ApplyBinding(index + 1, "");
                        ApplyBinding(index + 2, paths[1]);
                        return;
                    case 3:
                        ApplyBinding(index + 0, paths[0]);
                        ApplyBinding(index + 1, paths[1]);
                        ApplyBinding(index + 2, paths[2]);
                        return;
                }
            }
            ApplyBinding(index, humanReadablePath);
            return;
            
            void ApplyBinding(int idx, string path)
            {
                string finalPath = $"{GetDevicePath(path, device)}{path}";
                if (finalPath == action.bindings[idx].effectivePath) return;
                action.ApplyBindingOverride(idx, finalPath);
            }
        }

        /// <summary>
        /// Checks if the given binding is of type <see cref="TwoOptionalModifiersComposite"/>. <br/>
        /// If the binding is part of a composite, it will check the previous binding.
        /// </summary>
        /// <param name="binding">The binding to check</param>
        /// <returns>TRUE if it is <see cref="TwoOptionalModifiersComposite"/>.</returns>
        public static bool IsTwoOptionalModifiersComposite(this InputBinding binding)
        {
            InputSystem input = InputSystem.Instance;
            while (true)
            {
                if (!binding.isPartOfComposite || binding.isComposite) return binding.path == nameof(TwoOptionalModifiersComposite).Replace("Composite", "");

                InputAction action = input.GetAction(binding);
                int index = action.GetBindingIndexWithEmptySupport(binding);
                binding = action.bindings[index - 1];
            }
        }

        /// <summary>
        /// Returns the index of the binding in the action's bindings list. <br/>
        /// Supports empty bindings.
        /// </summary>
        /// <param name="action">The action to find the binding in.</param>
        /// <param name="binding">Which binding's index to find.</param>
        /// <returns>The index of binding in the given action.</returns>
        public static int GetBindingIndexWithEmptySupport(this InputAction action, InputBinding binding)
        {
            for (int i = 0; i < action.bindings.Count; i++)
            {
                if (action.bindings[i].id == binding.id) return i;
            }
            return -1;
        }

        private static string GetDevicePath(string path, InputDeviceType device)
        {
            if (string.IsNullOrEmpty(path)) return "";
            if (device == InputDeviceType.Keyboard && IsForMouse()) return MouseDevicePath;
            return (device == InputDeviceType.Gamepad) ? GamepadDevicePath : KeyboardDevicePath; 
            
            bool IsForMouse() => path.Contains("leftButton") || path.Contains("rightButton") || path.Contains("middleButton") || path.Contains("forwardButton") || path.Contains("backButton");
        }
        
        /// <summary>
        /// Returns TRUE if the previous binding is the header of a <see cref="TwoOptionalModifiersComposite"/>.
        /// </summary>
        /// <param name="action">The action the binding belongs to.</param>
        /// <param name="index">The index of the current binding. Previous one will be checked.</param>
        public static bool PreviousIsOptionalModifiersComposite(InputAction action, int index)
        {
            int currentIndex = index;
            while (true)
            {
                InputBinding binding = action.bindings[currentIndex];
                if (!binding.isPartOfComposite && !binding.isComposite) return false;
                if (binding.isComposite) return binding.IsTwoOptionalModifiersComposite();
                currentIndex--;
            }
        }

        public static bool PreviousIsOneModifierComposite(InputAction action, int index)
        {
            int currentIndex = index;
            while (true)
            {
                InputBinding binding = action.bindings[currentIndex];
                if (!binding.isPartOfComposite && !binding.isComposite) return false;
                if (binding.isComposite) return binding.path == nameof(OneModifierComposite).Replace("Composite", "");
                currentIndex--;
            }
        }

        public static bool PreviousIsTwoModifierComposite(InputAction action, int index)
        {
            int currentIndex = index;
            while (true)
            {
                InputBinding binding = action.bindings[currentIndex];
                if (!binding.isPartOfComposite && !binding.isComposite) return false;
                if (binding.isComposite) return binding.path == nameof(TwoModifiersComposite).Replace("Composite", "");
                currentIndex--;
            }
        }

        /// <summary>
        /// Returns the effective path without the device section (e.g. "Keyboard/s -> s").
        /// </summary>
        /// <param name="binding">The binding, whose path to clean.</param>
        /// <param name="device">For which device to check.</param>
        /// <returns>The effective path without the device portion.</returns>
        private static string GetPathWithoutDevice(this InputBinding binding, InputDeviceType device)
        {
            if (string.IsNullOrEmpty(binding.effectivePath)) return "";
            string devicePath = GetDevicePath(binding.effectivePath, device);
            return binding.effectivePath.Replace(devicePath, "");
        }
    }
}