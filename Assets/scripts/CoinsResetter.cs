using UnityEngine;

public class CoinsResetter : MonoBehaviour
{
    public CoinsCollected coinsCollected; // Reference to the CoinsCollected ScriptableObject

    void Awake()
    {
        ResetCoins();
    }

    void ResetCoins()
    {
        coinsCollected.ResetCoins();
    }
}
