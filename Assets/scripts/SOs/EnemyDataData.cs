using System;
using System.Collections.Generic;

[Serializable]
public class EnemyDataData
{
    public string enemyName;  // Name of the enemy
    public List<EnemyAbility> abilities = new List<EnemyAbility>();  // List of abilities for the enemy
}
