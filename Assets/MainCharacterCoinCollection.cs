using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterCoinCollection : MonoBehaviour
{
    public CoinsCollected coinsCollectedSO;  // Reference to the CoinsCollected ScriptableObject
    public string coinTag = "Coin";  // Tag of the coin objects to collect

    private void Awake()
    {
        if (coinsCollectedSO != null)
        {
            coinsCollectedSO.ResetCoins();  // Reset coins collected to 0 on Awake
        }
        else
        {
            Debug.LogError("CoinsCollected ScriptableObject reference is not set.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(coinTag))
        {
            CollectCoin();
        }
    }

    public void CollectCoin()
    {
        if (coinsCollectedSO != null)
        {
            coinsCollectedSO.AddCoins(1);  // Add 1 coin to the CoinsCollected SO
        }
        else
        {
            Debug.LogError("CoinsCollected ScriptableObject reference is not set.");
        }
    }
}
