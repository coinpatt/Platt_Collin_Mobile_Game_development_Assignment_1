//JOSÉ PABLO PEÑALOZA
//Simple object Pooler
//cui honorem, honorem honorem -> https://www.youtube.com/watch?v=tdSmKaJvCoA
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePooler : MonoBehaviour
{
    [System.Serializable] //Available class in the editor
    public class Pool //pool class
    {
        public string tag;
        public GameObject prefab;
        public int size;

        public Pool(string tag, GameObject prefab, int size)
        {
            this.tag = tag;
            this.prefab = prefab;
            this.size = size;
        }
    }

    public static TilePooler Instance; //An instance of itself

    public List<Pool> pools; //List of the pools in the scene
    public Dictionary<string, Queue<GameObject>> poolDictionary; //Dictionary
    public GameObject Tile;
    [HideInInspector]
    public int nOfTiles;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Tile.name = "Tile";
        pools.Add(new Pool("layer01", Tile, nOfTiles)); //Creates a pool of one layer of tiles

        poolDictionary = new Dictionary<string, Queue<GameObject>>(); //Dictionary initialized

        foreach (Pool pool in pools)//Loops threw the list to add it the pools to the dictionary
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)//Loops threw every object in each pool
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool); //The queue is added to the pool. 
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {//Method called each time an object is called from the pool.
        if (!poolDictionary.ContainsKey(tag)) //Checks that the object is in the pool.
        {
            Debug.LogWarning("The tag " + tag + "does not exist in the pool");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue(); //Dequeues the wanted object of one list of the dictionary.

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.parent = gameObject.transform;
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation; //Locates the object on the scene. 

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();//Calls the interface that is called each time an object is needed.
        if(pooledObj != null)
        {
            pooledObj.onObjectSpawn(); //Se mete a la interfaz del objeto
        }

        poolDictionary[tag].Enqueue(objectToSpawn); //Regresa a otro objeto a la lista 
        return objectToSpawn;
    }
    
}
