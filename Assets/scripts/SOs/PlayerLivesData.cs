using UnityEngine;

[CreateAssetMenu(fileName = "New Player Lives Data", menuName = "Player Lives Data")]
public class PlayerLivesData : ScriptableObject
{
    public int currentLives = 3;  // Starting lives
    public int defaultLives = 3;  // Default lives

    public void ResetLives()
    {
        currentLives = defaultLives;
    }
}
