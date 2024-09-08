using System;
using System.IO;
using UnityEditor;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

/// <summary>
/// # SUMMARY
/// AsapTime is an update method just like Update() and FixedUpdate(),
/// it is called before every Update() or FixedUpdate().
/// This class allow you to update your code as often as possible 
/// without compromising anything thanks to it's own DeltaTime
/// 
/// # HOW TO -> https://github.com/Fiera42/AsapTime-Unity/blob/master/README.md#quick-use
/// </summary>
public static class AsapTime
{
    // --------------------------------------------------------- VALUES
    public static event Action OnAsapUpdate;
    public static float Time { get; private set; } = -UnityEngine.Time.fixedDeltaTime;
    public static float DeltaTime { get; private set; } = UnityEngine.Time.fixedDeltaTime;
    public static AsapUpdateConfigFile Config { get; private set; }
    public static bool IsInFixedUpdate { get; private set; } = false;

    private const string configFileName = "AsapUpdateConfig";
    private const string resourcesPath = "Assets/Resources/";

    // --------------------------------------------------------- Initialisation

    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {
#if ENABLE_INPUT_SYSTEM
        LoadConfigFile();

        if (Config.AllowManualInputSystemUpdate)
        {
            InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsManually;
        }
#endif
        PlayerLoopInterface.InsertSystemBefore(typeof(AsapTime), NotInFixedUpdate, typeof(UnityEngine.PlayerLoop.Update.ScriptRunBehaviourUpdate));
        PlayerLoopInterface.InsertSystemBefore(typeof(AsapTime), UpdateAsap, typeof(UnityEngine.PlayerLoop.Update.ScriptRunBehaviourUpdate));
        PlayerLoopInterface.InsertSystemBefore(typeof(AsapTime), InFixedUpdate, typeof(UnityEngine.PlayerLoop.FixedUpdate.ScriptRunBehaviourFixedUpdate));
        PlayerLoopInterface.InsertSystemBefore(typeof(AsapTime), UpdateAsap, typeof(UnityEngine.PlayerLoop.FixedUpdate.ScriptRunBehaviourFixedUpdate));
    }

    private static void LoadConfigFile()
    {
        Config = Resources.Load<AsapUpdateConfigFile>(configFileName);
        if (Config != null) return;

#if UNITY_EDITOR
        // Ensure Resources folder exists
        if (!Directory.Exists(resourcesPath))
        {
            Directory.CreateDirectory(resourcesPath);
            AssetDatabase.Refresh();
        }

        // Ensure InputSystemConfig exists
        Config = ScriptableObject.CreateInstance<AsapUpdateConfigFile>();
        AssetDatabase.CreateAsset(Config, $"{resourcesPath}{configFileName}.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
#else
        Config = ScriptableObject.CreateInstance<AsapUpdateConfigFile>();
#endif
    }

    private static void InFixedUpdate()
    {
        IsInFixedUpdate = true;
    }
    private static void NotInFixedUpdate()
    {
        IsInFixedUpdate = false;
    }

    // --------------------------------------------------------- Update logic

    private static void UpdateAsap()
    {
        if (UnityEngine.Time.time <= Time) return;

        DeltaTime = UnityEngine.Time.time - Time;
        Time = UnityEngine.Time.time;

#if ENABLE_INPUT_SYSTEM
        if(Config.AllowManualInputSystemUpdate)
        {
            InputSystem.Update();
        }
#endif

        OnAsapUpdate?.Invoke();
    }
}
