using UnityEngine;

// Make a subclass of this class with T as the subclass to make a singleton
public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<T>();

                // Leaving this out because it is easy to misuse and cause bugs
                /*if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(T).Name;
                    _instance = go.AddComponent<T>();
                }*/
            }

            return _instance;
        }
    }
}