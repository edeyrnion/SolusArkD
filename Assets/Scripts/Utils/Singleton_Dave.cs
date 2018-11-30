using UnityEngine;
public class Singleton_Dave<T> : MonoBehaviour where T : Singleton_Dave<T>
{
	static T instance;
	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				instance = (T)FindObjectOfType(typeof(T));
				if (instance == null)
				{
					GameObject singletonGO = new GameObject();
					instance = singletonGO.AddComponent<T>();
					singletonGO.name = "Singleton " + typeof(T).ToString();
				}
				DontDestroyOnLoad(instance.gameObject);
			}
			return instance;
		}
	}
	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
		}
		else if (instance == null)
		{
			DontDestroyOnLoad(gameObject);
			instance = (T)this;
		}
	}
}