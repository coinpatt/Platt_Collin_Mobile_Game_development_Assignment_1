using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainCharUpgradeManager : MonoBehaviour
{
    public TextMeshProUGUI coinCountText; // Text displaying current coin count

    public int coinsThreshold = 10;    // Threshold of coins needed to apply an upgrade

    public CoinsCollected coinsCollected;  // Reference to the CoinsCollected ScriptableObject
    public BulletData bulletData;          // Reference to the BulletData ScriptableObject

    // Public fields to define the increments for each attribute
    public int speedIncrement = 3;
    public float sizeIncrement = 0.1f;
    public int damageIncrement = 25;
    public int maxCollisionsIncrement = 1;
    public int spawnRateIncrement = 2;

    public float moveSpeedIncrement = 0.5f; // Increment for the move speed upgrade
    public AutomatedCharacterMovement automatedCharacterMovement; // Reference to the AutomatedCharacterMovement script
    public GameObject mainCharacter; // Reference to the main character GameObject

    private int lastCoinCount = -1;

    void Start()
    {
        // Initialize UI
        UpdateCoinCount();
    }

    void Update()
    {
        // Check if coin count has changed
        if (coinsCollected.coins != lastCoinCount)
        {
            UpdateCoinCount();
        }

        // Check if coins collected exceed threshold
        if (coinsCollected.coins >= coinsThreshold)
        {
            ApplyRandomUpgrade();
            coinsCollected.ResetCoins();  // Reset coins collected after applying upgrade
        }
    }

    void UpdateCoinCount()
    {
        lastCoinCount = coinsCollected.coins;
        coinCountText.text = "Coins: " + coinsCollected.coins.ToString();
    }

    void ApplyRandomUpgrade()
    {
        // List of possible upgrades
        List<System.Action> upgrades = new List<System.Action>
        {
            () => bulletData.SetSpeed(bulletData.speed + speedIncrement),
            () => bulletData.SetSize(bulletData.size + sizeIncrement),
            () => bulletData.SetDamage(bulletData.damage + damageIncrement),
            () => bulletData.SetMaxCollisions(bulletData.maxCollisions + maxCollisionsIncrement),
            () => bulletData.SetSpawnRate(bulletData.spawnRate + spawnRateIncrement),
            () => IncreaseMoveSpeed()
        };

        // Randomly select an upgrade to apply
        int randomIndex = Random.Range(0, upgrades.Count);
        upgrades[randomIndex].Invoke();
    }

    void IncreaseMoveSpeed()
    {
        if (automatedCharacterMovement != null)
        {
            automatedCharacterMovement.moveSpeed += moveSpeedIncrement;
        }
        else
        {
            Debug.LogWarning("AutomatedCharacterMovement script is not assigned.");
        }
    }
}
