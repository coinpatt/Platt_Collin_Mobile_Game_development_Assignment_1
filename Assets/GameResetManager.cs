using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameResetManager", menuName = "ScriptableObjects/GameResetManager", order = 1)]
public class GameResetManager : ScriptableObject
{
    [Serializable]
    public class SOData
    {
        public string name;            // Name of the ScriptableObject
        public ScriptableObject so;    // Reference to the ScriptableObject
        public List<NamedInt> values;  // List of named integer values for the ScriptableObject

        public SOData(string name, ScriptableObject so)
        {
            this.name = name;
            this.so = so;
            values = new List<NamedInt>();
        }
    }

    [Serializable]
    public class NamedInt
    {
        public string name;  // Name of the integer value
        public int value;    // Actual integer value
    }

    public List<SOData> scriptableObjects = new List<SOData>();  // List of ScriptableObjects and their associated data

    public void ResetAllSOValues()
    {
        foreach (var data in scriptableObjects)
        {
            if (data.so != null)
            {
                ApplyValuesToSO(data.so, data.values);
            }
            else
            {
                Debug.LogWarning($"ScriptableObject '{data.name}' is null.");
            }
        }
    }

    private void ApplyValuesToSO(ScriptableObject so, List<NamedInt> values)
    {
        Type type = so.GetType();
        var fields = type.GetFields();

        foreach (var namedInt in values)
        {
            var field = Array.Find(fields, f => f.Name == namedInt.name);
            if (field != null)
            {
                field.SetValue(so, namedInt.value);
            }
            else
            {
                Debug.LogWarning($"Field '{namedInt.name}' not found in ScriptableObject '{so.name}'.");
            }
        }
    }
}
