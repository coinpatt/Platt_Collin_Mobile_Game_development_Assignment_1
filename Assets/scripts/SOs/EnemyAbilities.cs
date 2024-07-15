using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "EnemyAbilities", menuName = "EnemyAbilities", order = 1)]
public class EnemyAbilities : ScriptableObject
{
    public float spawnrate1;
    public float spawnrate2;
    public float spawnrate3;
   
   public float speed1;
    public float speed2;
    public float speed3;

    public float size1;
    public float size2;

    public float size3;
}