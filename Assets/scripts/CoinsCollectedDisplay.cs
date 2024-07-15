using UnityEngine;
using TMPro;

public class CoinsCollectedDisplay : MonoBehaviour
{
    public CoinsCollected coinsCollected;  // Reference to the CoinsCollected ScriptableObject
    public TMP_Text textMeshPro;           // Reference to the TextMeshPro component
    public string coinsTextFormat = "Coins: {0} / 10";  // Format string for displaying coins

    void Start()
    {
        // Ensure a TMP_Text component is assigned in the inspector
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TMP_Text>();
            if (textMeshPro == null)
            {
                Debug.LogError("TextMeshPro component not found on GameObject.");
                return;
            }
        }

        // Ensure coinsCollected reference is set in the inspector
        if (coinsCollected == null)
        {
            Debug.LogError("CoinsCollected reference is not set in CoinsCollectedDisplay script.");
            return;
        }

        // Subscribe to the OnCoinsChanged event to update text
        coinsCollected.OnCoinsChanged += UpdateCoinsText;

        // Update the text initially
        UpdateCoinsText();
    }

    void UpdateCoinsText()
    {
        // Update the TextMeshPro text to display current coins out of 10
        textMeshPro.text = string.Format(coinsTextFormat, coinsCollected.coins);
    }

    void OnDestroy()
    {
        // Unsubscribe from the event when this object is destroyed
        coinsCollected.OnCoinsChanged -= UpdateCoinsText;
    }
}
