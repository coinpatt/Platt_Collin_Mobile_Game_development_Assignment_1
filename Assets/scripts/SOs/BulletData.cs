using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Bullet Data", menuName = "Bullet Data", order = 1)]
public class BulletData : ScriptableObject
{
    public string tag;
    public GameObject prefab;

    // Current values
    public float speed;
    public float size;
    public float damage;
    public int maxCollisions;
    public float lifespan;
    public float spawnRate;

    // Default values
    [SerializeField]
    private float defaultSpeed;
    [SerializeField]
    private float defaultSize;
    [SerializeField]
    private float defaultDamage;
    [SerializeField]
    private int defaultMaxCollisions;
    [SerializeField]
    private float defaultLifespan;
    [SerializeField]
    private float defaultSpawnRate;

    // Event to notify changes
    public UnityEvent OnBulletDataChanged = new UnityEvent();

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
        speed = defaultSpeed;
        size = defaultSize;
        damage = defaultDamage;
        maxCollisions = defaultMaxCollisions;
        lifespan = defaultLifespan;
        spawnRate = defaultSpawnRate;

        OnBulletDataChanged.Invoke();
    }

    // Callback when scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetToDefaultValues();
    }

    // Methods to set values and notify changes
    public void SetSpeed(float newSpeed)
    {
        if (speed != newSpeed)
        {
            speed = newSpeed;
            OnBulletDataChanged.Invoke();
        }
    }

    public void SetSize(float newSize)
    {
        if (size != newSize)
        {
            size = newSize;
            OnBulletDataChanged.Invoke();
        }
    }

    public void SetDamage(float newDamage)
    {
        if (damage != newDamage)
        {
            damage = newDamage;
            OnBulletDataChanged.Invoke();
        }
    }

    public void SetMaxCollisions(int newMaxCollisions)
    {
        if (maxCollisions != newMaxCollisions)
        {
            maxCollisions = newMaxCollisions;
            OnBulletDataChanged.Invoke();
        }
    }

    public void SetLifespan(float newLifespan)
    {
        if (lifespan != newLifespan)
        {
            lifespan = newLifespan;
            OnBulletDataChanged.Invoke();
        }
    }

    public void SetSpawnRate(float newSpawnRate)
    {
        if (spawnRate != newSpawnRate)
        {
            spawnRate = newSpawnRate;
            OnBulletDataChanged.Invoke();
        }
    }
}
