using UnityEngine;

[CreateAssetMenu(fileName = "CoinsCollected", menuName = "Coins Collected")]
public class CoinsCollected : ScriptableObject
{
    [SerializeField]
    private int _coins;  // Number of coins collected by the player

    // Event to notify subscribers when coins change
    public delegate void CoinsChanged();
    public event CoinsChanged OnCoinsChanged;

    public int coins
    {
        get => _coins;
        set
        {
            _coins = value;
            NotifyCoinsChanged();
        }
    }

    // This method should be called at the start of the game or when the scene resets
    public void InitializeCoins()
    {
        ResetCoins();  // Reset coins collected to 0
    }

    public void ResetCoins()
    {
        coins = 0;  // Reset coins collected to 0
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    private void NotifyCoinsChanged()
    {
        OnCoinsChanged?.Invoke();
    }
}
