using TMPro;
using UnityEngine;

namespace RedRats.UI.AppInformation
{
    /// <summary>
    /// Updates a label's value to the current version set in the Project Settings -> Player -> Version.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class AppVersionLabel : MonoBehaviour
    {
        private void Awake() => GetComponent<TextMeshProUGUI>().text = Application.version;
    }
}