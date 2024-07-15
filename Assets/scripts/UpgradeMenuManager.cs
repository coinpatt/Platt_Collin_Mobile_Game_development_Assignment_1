using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenuManager : MonoBehaviour
{
    public EnemyDatabase enemyDatabase;
    public string targetEnemyName = "Square Enemy"; // The enemy type to filter abilities
    public Button[] upgradeButtons;
    public TMP_Text[] upgradeTexts; // Array to hold TMP_Text components for each button

    private bool panelActive = false;
    private GameObject upgradePanel;

    void Start()
    {
        // Debug to ensure Start method is called
        Debug.Log("UpgradeMenuManager Start method called.");
    }

    public void SetUpgradePanel(GameObject panel)
    {
        upgradePanel = panel;
    }

    public void ShowUpgradePanel()
    {
        // Set the panel active only if it's not already active and upgradePanel is assigned
        if (!panelActive && upgradePanel != null)
        {
            upgradePanel.SetActive(true);
            panelActive = true;

            // Debug to ensure ShowUpgradePanel method is called
            Debug.Log("ShowUpgradePanel method called.");

            // Find the target enemy dynamically
            EnemyDataData targetEnemy = enemyDatabase.enemies.Find(enemy => enemy.enemyName == targetEnemyName);

            if (targetEnemy == null)
            {
                Debug.LogError($"Enemy with name {targetEnemyName} not found in the database.");
                return;
            }

            // Log details about the target enemy and its abilities
            Debug.Log($"Found enemy: {targetEnemy.enemyName}");
            Debug.Log($"Enemy abilities count: {targetEnemy.abilities.Count}");

            // Shuffle and select 3 random abilities (if more than 3 are available)
            List<EnemyAbility> abilities = new List<EnemyAbility>(targetEnemy.abilities);
            ShuffleAbilities(abilities);

            // Ensure we have at least 3 abilities to display
            for (int i = 0; i < upgradeButtons.Length; i++)
            {
                if (i < abilities.Count)
                {
                    upgradeButtons[i].gameObject.SetActive(true);
                    // Assuming upgradeTexts[i] corresponds to the TMP_Text component for each button
                    if (upgradeTexts.Length > i && upgradeTexts[i] != null)
                    {
                        upgradeTexts[i].text = $"{abilities[i].abilityName}: {abilities[i].abilityValue}";
                    }
                    else
                    {
                        Debug.LogError($"TMP_Text component not assigned for button {i + 1}.");
                    }
                }
                else
                {
                    upgradeButtons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    void ShuffleAbilities(List<EnemyAbility> abilities)
    {
        // Shuffle abilities using Fisher-Yates shuffle algorithm
        for (int i = 0; i < abilities.Count; i++)
        {
            EnemyAbility temp = abilities[i];
            int randomIndex = Random.Range(i, abilities.Count);
            abilities[i] = abilities[randomIndex];
            abilities[randomIndex] = temp;
        }
    }

    public void OnUpgradeButtonClicked(int buttonIndex)
    {
        // Example: Handle upgrade logic based on buttonIndex
        Debug.Log($"Upgrade button {buttonIndex + 1} clicked!");

        // Hide the panel after upgrade button is clicked
        HideUpgradePanel();
    }

    public void HideUpgradePanel()
    {
        // Set the panel inactive
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
            panelActive = false;
        }
    }
}
