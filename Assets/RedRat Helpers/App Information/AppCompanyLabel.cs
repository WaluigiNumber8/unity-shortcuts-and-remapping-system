using TMPro;
using UnityEngine;

namespace RedRats.UI.AppInformation
{
    /// <summary>
    /// Updates a label's value to the current company set in the Project Settings -> Player -> Company.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class AppCompanyLabel : MonoBehaviour
    {
        [SerializeField] private string prefix = "@";
        
        private void Awake() => GetComponent<TextMeshProUGUI>().text = $"{prefix} {Application.companyName}";
    }
}