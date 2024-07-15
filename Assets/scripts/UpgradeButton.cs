using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UpgradeButton : MonoBehaviour
{
    public EnemyAbilities enemyAbilities; // Reference to the EnemyAbilities ScriptableObject
    public string poolTag = "Enemy"; // Tag used to identify the pool in ObjectPool
    public EnemyData enemyData; // Reference to the EnemyData ScriptableObject
    public TMP_Text upgradeButtonText; // Reference to TMP Text component for displaying upgrade name
    public GameObject upgradePanel; // Reference to the panel containing this upgrade button

    // Dictionary to map ability names to display strings
    private Dictionary<string, string> abilityDisplayNames = new Dictionary<string, string>()
    {
        { "spawnrate1", "Spawn {X} more enemies when pushed" },
        { "spawnrate2", "Spawn {X} more enemies when pushed" },
        { "spawnrate3", "Spawn {X} more enemies when pushed" },
        { "speed1", "Increase speed by {X}" },
        { "speed2", "Increase speed by {X}" },
        { "speed3", "Increase speed by {X}" },
        { "size1", "Increase size by {X}" },
        { "size2", "Increase size by {X}" },
        { "size3", "Increase size by {X}" }
    };

    private string currentUpgradeAbility; // Track the currently displayed upgrade ability
    private bool isUpgradeApplied = false; // Flag to track if an upgrade has been applied

    private bool isPanelActive = false; // Flag to track if the panel is active

    void Start()
    {
        if (upgradeButtonText == null)
        {
            Debug.LogError("UpgradeButtonText is not assigned. Please assign the TMP Text component.");
        }
    }

    void OnEnable()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(true); // Ensure the panel is active when this script is enabled
            isPanelActive = true;
            DisplayRandomUpgrade(); // Display random upgrade when the panel is activated
        }

        // Register ApplyUpgrade method to the button's onClick event
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.RemoveAllListeners(); // Ensure only one listener is registered
            button.onClick.AddListener(ApplyUpgrade); // Add ApplyUpgrade as listener
        }
    }

    void OnDisable()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false); // Deactivate the panel when this script is disabled
            isPanelActive = false;
        }

        // Remove listener when script is disabled
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    public void DisplayRandomUpgrade()
    {
        if (isPanelActive)
        {
            // Randomly select an ability
            string abilityName = GetRandomAbility();

            // Display the selected ability name in the TMP text
            DisplayUpgradeText(abilityName);

            // Track the current upgrade ability
            currentUpgradeAbility = abilityName;

            // Reset upgrade applied flag
            isUpgradeApplied = false;
        }
    }

    public void ApplyUpgrade()
    {
        if (!isUpgradeApplied)
        {
            // Get the currently displayed ability name
            string abilityName = currentUpgradeAbility;

            // Apply the ability
            if (abilityName.StartsWith("spawnrate"))
            {
                ApplySpawnRateUpgrade(abilityName);
            }
            else
            {
                ApplyToAllEnemies(abilityName);
            }

            // Update the TMP text to reflect the applied upgrade
            DisplayUpgradeText(abilityName);

            // Mark upgrade as applied
            isUpgradeApplied = true;
        }
    }

    void DisplayUpgradeText(string abilityName)
    {
        // Display the selected ability name in the TMP text
        if (upgradeButtonText != null)
        {
            // Check if the ability has a custom display name in the dictionary
            if (abilityDisplayNames.ContainsKey(abilityName))
            {
                // Fetch the ability value from EnemyAbilities
                float abilityValue = GetAbilityValue(abilityName);

                // Replace {X} placeholder with the ability value
                string displayText = abilityDisplayNames[abilityName].Replace("{X}", abilityValue.ToString());
                upgradeButtonText.text = displayText;
            }
            else
            {
                upgradeButtonText.text = abilityName; // Default to ability name if no custom display name is set
            }
        }
    }

    string GetRandomAbility()
    {
        // Array of ability names in the EnemyAbilities SO
        string[] abilityNames = { "spawnrate1", "spawnrate2", "spawnrate3",
                                  "speed1", "speed2", "speed3",
                                  "size1", "size2", "size3" };

        // Select a random ability name
        int randomIndex = Random.Range(0, abilityNames.Length);
        return abilityNames[randomIndex];
    }

    float GetAbilityValue(string abilityName)
    {
        // Access the ability value from EnemyAbilities based on abilityName
        switch (abilityName)
        {
            case "spawnrate1":
                return enemyAbilities.spawnrate1;
            case "spawnrate2":
                return enemyAbilities.spawnrate2;
            case "spawnrate3":
                return enemyAbilities.spawnrate3;
            case "speed1":
                return enemyAbilities.speed1;
            case "speed2":
                return enemyAbilities.speed2;
            case "speed3":
                return enemyAbilities.speed3;
            case "size1":
                return enemyAbilities.size1;
            case "size2":
                return enemyAbilities.size2;
            case "size3":
                return enemyAbilities.size3;
            default:
                return 0f; // Default case, should not occur if abilityNames array is correct
        }
    }

    void ApplyToAllEnemies(string abilityName)
    {
        // Apply the ability to all instances in the object pool
        var objectPool = ObjectPool.Instance;
        if (objectPool != null)
        {
            foreach (var instance in objectPool.poolDictionary[poolTag])
            {
                ApplyUpgradeToEnemy(instance, abilityName);
            }
        }
    }

    void ApplyUpgradeToEnemy(GameObject enemyInstance, string abilityName)
    {
        // Apply the ability to the enemy instance
        float abilityValue = GetAbilityValue(abilityName);

        AIChase aiChase = enemyInstance.GetComponent<AIChase>();
        if (aiChase != null)
        {
            // Apply speed upgrade to AIChase script
            if (abilityName.StartsWith("speed"))
            {
                aiChase.speed += abilityValue; // Add to the existing speed value
            }
        }

        // Apply size upgrade to the prefab's scale
        if (abilityName.StartsWith("size"))
        {
            Vector3 currentScale = enemyInstance.transform.localScale;
            float newSize = currentScale.x + abilityValue; // Increase size by ability value
            enemyInstance.transform.localScale = Vector3.one * newSize;
        }
    }

    void ApplySpawnRateUpgrade(string abilityName)
    {
        // Apply spawn rate upgrade to EnemyData ScriptableObject
        if (enemyData != null)
        {
            float abilityValue = GetAbilityValue(abilityName);
            Debug.Log($"Adding {abilityValue} to numberOfEnemiesToSpawn"); // Debug to check the value
            enemyData.numberOfEnemiesToSpawn += Mathf.RoundToInt(abilityValue); // Add to the existing spawn rate value
        }
    }
}
