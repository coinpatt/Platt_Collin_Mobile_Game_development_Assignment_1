using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletDataUIManager : MonoBehaviour
{
    public BulletData bulletData;

    // Example TMP Text elements (assign in Inspector)
    public TMP_Text speedText;
    public TMP_Text sizeText;
    public TMP_Text damageText;
    public TMP_Text maxCollisionsText;
    public TMP_Text lifespanText;
    public TMP_Text spawnRateText;

    void Start()
    {
        // Initialize UI text with initial data
        UpdateUIText();
    }

    public void UpdateUIText()
    {
        // Update TMP Text elements with BulletData values
        speedText.text = $"Speed: {bulletData.speed}";
        sizeText.text = $"Size: {bulletData.size}";
        damageText.text = $"Damage: {bulletData.damage}";
        maxCollisionsText.text = $"Max Collisions: {bulletData.maxCollisions}";
        lifespanText.text = $"Lifespan: {bulletData.lifespan}";
        spawnRateText.text = $"Spawn Rate: {bulletData.spawnRate}";
    }
}
