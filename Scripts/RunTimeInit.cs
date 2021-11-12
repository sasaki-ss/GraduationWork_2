using UnityEngine;

public class RunTimeInit
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitBeforeLoad()
    {
        Debug.LogFormat("RuntimeInitializer::InitializeBeforeSceneLoad() called.");

        var manager = GameObject.Instantiate(Resources.Load("SoundManager"));
        GameObject.DontDestroyOnLoad(manager);

        var config = GameObject.Instantiate(Resources.Load("Config"));
        GameObject.DontDestroyOnLoad(config);
    }
}
