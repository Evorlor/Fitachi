using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Extending this class creates a MonoBehaviour which may only have on instance and will not be destroyed between scenes.  When extending, the type of the inheriting class must be passed.
/// </summary>
public abstract class ManagerBehaviour<ManagerType> : MonoBehaviour where ManagerType : ManagerBehaviour<ManagerType>
{
    private const string ManagerName = "Manager";

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
                if (!instance)
                {
                    var instanceGameObject = GameObjectFactory.GetOrAddGameObject(ManagerName);
                    instance = instanceGameObject.AddComponent<ManagerType>();
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        RepopulateButtons();
        DestroyDuplicateManagers();
        DontDestroyOnLoad(gameObject);
    }

    private void RepopulateButtons()
    {
        //var buttons = FindObjectsOfType<Button>();
        //foreach (var button in buttons)
        //{
        //    for (int i = 0; i < button.onClick.GetPersistentEventCount(); i++)
        //    {
        //        var target = button.onClick.GetPersistentTarget(i);
        //        if (target is ManagerType)
        //        {
        //            button.onClick.
        //            var methodName = button.onClick.GetPersistentMethodName(i);
        //            Debug.Log(methodName);
        //            //Delegate method = Delegate.CreateDelegate(typeof(ManagerType), Instance, methodName);
        //            //button.onClick.AddListener(delegate { method(); });
        //            //var y = UnityA
        //            //target = Instance;
        //            //UnityEventCallState x = new UnityEventCallState
        //            //button.onClick.SetPersistentListenerState(i, UnityEventCallState);
        //            Debug.Log(button.onClick.GetPersistentTarget(i));
        //        }
        //    }
        //}
    }

    private void DestroyDuplicateManagers()
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
                bool sharesGameObjectWithManager = Instance.gameObject == manager.gameObject;
                bool hasExtraComponents = manager.GetComponents<MonoBehaviour>().Length > 1;
                bool hasChildren = transform.childCount > 0;
                bool destroyGameObject = !(sharesGameObjectWithManager || hasExtraComponents || hasChildren);
                if (destroyGameObject)
                {
                    Destroy(manager.gameObject);
                }
                else
                {
                    Destroy(manager);
                }
            }
        }
    }
}