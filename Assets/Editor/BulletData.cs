using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet Data", menuName = "Bullet Data", order = 1)]
public class BulletData : ScriptableObject
{
    public string tag;
    public GameObject prefab;
    public float speed;
    public float size;
    public float damage;
    public int maxCollisions;
    public float lifespan;
    public float spawnRate;

    // Default values
    public float defaultSpeed;
    public float defaultSize;
    public float defaultDamage;
    public int defaultMaxCollisions;
    public float defaultLifespan;
    public float defaultSpawnRate;
}
