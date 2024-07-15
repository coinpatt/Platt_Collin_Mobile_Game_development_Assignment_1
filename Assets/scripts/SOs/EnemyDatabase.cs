using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDatabase", menuName = "ScriptableObjects/EnemyDatabase", order = 1)]
public class EnemyDatabase : ScriptableObject
{
    public List<EnemyDataData> enemies = new List<EnemyDataData>();  // List of all enemies with their abilities
}
