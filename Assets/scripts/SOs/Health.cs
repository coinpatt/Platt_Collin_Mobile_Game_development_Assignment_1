using UnityEngine;

[CreateAssetMenu(fileName = "New Health", menuName = "Health")]
public class Health : ScriptableObject
{
    public float maxHealth;
    public float currentScale;

    // Method to update maxHealth based on scale
    public void UpdateMaxHealthBasedOnScale(float scale)
    {
        currentScale = scale;
        maxHealth = currentScale * 100; // Adjust this calculation as needed
    }
}
