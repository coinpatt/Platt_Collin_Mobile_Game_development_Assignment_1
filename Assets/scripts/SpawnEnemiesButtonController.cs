using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnEnemiesButtonController : MonoBehaviour
{
    public TMP_Text buttonText; // Reference to the TextMeshPro text on the button
    public Button spawnButton; // Reference to the Button component

    public EnemyData enemyData; // Reference to the EnemyData ScriptableObject
    public float inactiveTime = 5f; // Duration in seconds when the button is inactive

    private float countdownTimer = 0f; // Timer for the countdown
    private bool buttonActive = true; // Flag to track if the button is active

    void Start()
    {
        // Initialize button text based on initial EnemyData settings
        UpdateButtonText();

        // Set up button click listener
        spawnButton.onClick.AddListener(OnSpawnButtonClicked);
    }

    void Update()
    {
        // Update countdown timer if button is inactive
        if (!buttonActive)
        {
            countdownTimer -= Time.deltaTime;

            if (countdownTimer <= 0f)
            {
                // Reactivate button
                buttonActive = true;
                spawnButton.interactable = true;
                UpdateButtonText();
            }
            else
            {
                // Update countdown text
                buttonText.text = $"Time Left: {Mathf.CeilToInt(countdownTimer)}";
            }
        }
    }

    void OnSpawnButtonClicked()
    {
        // Disable button and start countdown
        spawnButton.interactable = false;
        buttonActive = false;
        countdownTimer = inactiveTime;

        // Perform spawn logic here (not implemented in this example)
        Debug.Log("Spawn button clicked. Perform spawn logic here.");

        // Example: You may want to trigger enemy spawning here
    }

    void UpdateButtonText()
    {
        // Update button text based on current EnemyData numberOfEnemiesToSpawn
        buttonText.text = $"Spawn {enemyData.numberOfEnemiesToSpawn} Enemies";
    }
}
