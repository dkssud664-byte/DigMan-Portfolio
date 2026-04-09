using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerWrapper : MonoBehaviour
{
    public static SceneManagerWrapper Instance { get; private set; }

    void Awake()
    {
        //ΩÃ±€≈Ê
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
  

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int index = scene.buildIndex;
        switch(index)
        {
            case 1:
                Facade.Instance.UIManager.ClearStack();
                Facade.Instance.UIManager.CursorPolicy = CursorPolicy.UnlockedByDefault;
                Facade.Instance.UIManager.ApplyBaseCursorPolicy();
                SoundManager.Instance.PlayBGM(Scenes.Main);
                break;
            case 2:
                Facade.Instance.UIManager.ClearStack();
                Facade.Instance.UIManager.CursorPolicy = CursorPolicy.LockedByDefault;
                Facade.Instance.UIManager.ApplyBaseCursorPolicy();
                SoundManager.Instance.PlayBGM(Scenes.Game);
                break;
            case 3:
                SoundManager.Instance.PlayBGM(Scenes.Ending);
                break;
        }
    }

    private void SceneChange()
    {

    }
}
