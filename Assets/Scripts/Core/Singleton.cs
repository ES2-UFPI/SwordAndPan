using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Private static instance of the singleton
    private static T instance;

    // Public property to access the instance
    public static T Instance
    {
        get
        {
            // Check if the instance is null
            if (instance == null)
            {
                // Try to find an existing instance in the scene
                instance = FindObjectOfType<T>();

                // If no instance is found, create a new GameObject and attach the singleton component
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    instance = singletonObject.AddComponent<T>();
                }

                // Ensure the instance persists between scene changes
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    // Awake is called when the script instance is being loaded
    protected virtual void Awake()
    {
        // If the instance already exists and it's not this, then destroy this
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}