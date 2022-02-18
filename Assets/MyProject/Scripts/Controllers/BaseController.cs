using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController<T> : MonoBehaviour where T : BaseController<T>
{
    static T instance = null;

    [SerializeField] bool dontDestoy = false;

    bool alive = true;

    public static T Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                
                T[] managers = GameObject.FindObjectsOfType<T>();
                if (managers != null)
                {
                    if (managers.Length == 1)
                    {
                        instance = managers[0];
                        Debug.Log(instance.name);
                        if (instance.dontDestoy)
                            DontDestroyOnLoad(instance);
                        return instance;
                    }
                    else
                    {
                        if (managers.Length > 1)
                        {
                            for (int i = 0; i < managers.Length; ++i)
                            {
                                T manager = managers[i];
                                Destroy(manager.gameObject);
                            }
                        }
                    }
                }
                
                GameObject go = new GameObject(typeof(T).Name, typeof(T));
                instance = go.GetComponent<T>();
                instance.Initialization();

                if (instance.dontDestoy)
                    DontDestroyOnLoad(instance.gameObject);

                return instance;
            }
        }

        set
        {
            instance = value as T;
        }
    }

    /// <summary>
    /// Проверка для OnDestroy или OnApplicationExit
    /// </summary>
    public static bool IsAlive
    {
        get
        {
            if (instance == null)
                return false;
            return instance.alive;
        }
    }

    protected void Awake()
    {
        if (instance == null)
        {
            if (dontDestoy)
                DontDestroyOnLoad(gameObject);
            
            instance = this as T;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    private void Start()
    {
        Initialization();
    }

    protected void OnDestroy() { alive = false; }

    protected void OnApplicationQuit() { alive = false; }

    protected virtual void Initialization() { }
}
