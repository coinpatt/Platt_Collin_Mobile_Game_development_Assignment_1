using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public GameObject enemyPrefab;
    public int numberOfEnemiesToSpawn = 1;  // Default value for number of enemies to spawn
    public int speed = 5;                   // Default speed value
    public float minDistance = 2.0f;        // Default min distance value
    public float maxDistance = 5.0f;        // Default max distance value
    public string tag;                      // Tag of the enemy prefab

    // Default values
    [SerializeField]
    private int defaultNumberOfEnemiesToSpawn = 1;
    [SerializeField]
    private int defaultSpeed = 5;
    [SerializeField]
    private float defaultMinDistance = 2.0f;
    [SerializeField]
    private float defaultMaxDistance = 5.0f;

    // Event to notify changes
    public UnityEvent OnEnemyDataChanged = new UnityEvent();

    private void OnEnable()
    {
        ResetToDefaultValues();

        // Register to the scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unregister from the scene loaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Method to reset values to defaults
    public void ResetToDefaultValues()
    {
        numberOfEnemiesToSpawn = defaultNumberOfEnemiesToSpawn;
        speed = defaultSpeed;
        minDistance = defaultMinDistance;
        maxDistance = defaultMaxDistance;

        OnEnemyDataChanged.Invoke();
    }

    // Scene loaded event handler
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset to default values when scene is loaded
        ResetToDefaultValues();
    }
}
