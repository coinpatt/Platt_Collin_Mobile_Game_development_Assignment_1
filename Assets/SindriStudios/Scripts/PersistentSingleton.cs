using UnityEngine;

public class PersistentSingleton<T> : MonoBehaviour	where T : Component
{
	private static T _instance;
	
	public static T Instance 
	{
		get 
		{
			if (_instance == null) 
			{
				_instance = FindObjectOfType<T> ();
				if (_instance == null) 
				{
					GameObject obj = new GameObject ();
					_instance = obj.AddComponent<T> ();
				}
			}
			return _instance;
		}
	}
	
	public virtual void Awake ()
	{
		DontDestroyOnLoad (this.gameObject);
		if (_instance == null) 
		{
			_instance = this as T;
		} 
		else 
		{
			Destroy (gameObject);
		}
	}
}
