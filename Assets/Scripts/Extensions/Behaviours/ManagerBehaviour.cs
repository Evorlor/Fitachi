using UnityEngine;

/// <summary>
/// Extending this class creates a MonoBehaviour which may only have on instance and will not be destroyed between scenes.  When extending, the type of the inheriting class must be passed.
/// </summary>
public abstract class ManagerBehaviour<ManagerType> : MonoBehaviour where ManagerType : Component
{
    private static ManagerType instance;

    /// <summary>
    /// Gets the singleton instance of the Manager
    /// </summary>
    public static ManagerType Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<ManagerType>();
            }
            return instance;
        }
    }

    void Awake()
    {
        var managers = FindObjectsOfType<ManagerType>();
        foreach (var manager in managers)
        {
            if (!manager)
            {
                continue;
            }
            if (Instance != manager)
            {
                if (Instance.gameObject == manager.gameObject)
                {
                    Destroy(manager);
                }
                else
                {
                    Destroy(manager.gameObject);
                }
            }
        }
        DontDestroyOnLoad(gameObject);
    }
}