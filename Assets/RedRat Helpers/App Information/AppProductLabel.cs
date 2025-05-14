using TMPro;
using UnityEngine;

namespace RedRats.UI.AppInformation
{
    /// <summary>
    /// Updates a label's value to the current product set in the Project Settings -> Player -> Product.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class AppProductLabel : MonoBehaviour
    {
        private void Awake() => GetComponent<TextMeshProUGUI>().text = Application.productName;
    }
}