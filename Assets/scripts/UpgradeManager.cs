using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradePanel;    // Reference to the upgrade panel UI
    public TextMeshProUGUI coinCountText; // Text displaying current coin count
    public int coinsThreshold = 10;    // Threshold of coins needed to show upgrade panel

    public CoinsCollected coinsCollected;  // Reference to the CoinsCollected ScriptableObject

    private void Start()
    {
        // Initialize UI
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);  // Hide upgrade panel initially
        }
    }

    private void Update()
    {
        // Check if coins collected exceed threshold
        if (coinsCollected.coins >= coinsThreshold)
        {
            ShowUpgradePanel();
            coinsCollected.ResetCoins();  // Reset coins collected after showing upgrade panel
        }
    }

    private void UpdateCoinCount()
    {
        coinCountText.text = "Coins: " + coinsCollected.coins.ToString();
    }

    public void ShowUpgradePanel()
    {
        // Pause game and show upgrade panel
        Time.timeScale = 0f;  // Pause game time
        upgradePanel.SetActive(true);
    }

    public void PurchaseUpgrade(int upgradeCost)
    {
        // Deduct coins based on upgrade cost
        coinsCollected.coins -= upgradeCost;

        // Update UI and other game aspects as needed
        UpdateCoinCount();
    }
}
