#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class PlayModeBootstrap
{
    static PlayModeBootstrap()
    {
        EditorSceneManager.playModeStartScene =
            AssetDatabase.LoadAssetAtPath<SceneAsset>(
                EditorBuildSettings.scenes[0].path
            );
    }
}
#endif