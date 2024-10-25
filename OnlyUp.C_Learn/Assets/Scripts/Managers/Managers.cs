using UnityEngine;

public sealed class Managers : MonoBehaviour
{
    private static Managers instance;

    #region No MonoBehaviour
    public static ResourcesManager Resource { get { return instance.resourcesManager; } }

    private ResourcesManager resourcesManager = new ResourcesManager();
    #endregion

    #region MonoBehaviour

    #endregion

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        Screen.SetResolution(1920, 1080, false);

        GameObject go = new GameObject("Managers");
        instance = go.AddComponent<Managers>();

        DontDestroyOnLoad(go);

    }

    private static T CreateManager<T>(Transform parent) where T : Component, IManager
    {
        GameObject go = new GameObject(typeof(T).Name);
        T t = go.AddComponent<T>();
        go.transform.parent = parent;

        t.Init();

        return t;
    }

    private void Start()
    {
        
    }
}
