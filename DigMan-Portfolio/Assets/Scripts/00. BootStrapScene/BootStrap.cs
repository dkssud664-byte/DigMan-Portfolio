using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Scenes startScene;

    private void Start()
    {
#if UNITY_EDITOR
        // 개발 중에는 바로 게임씬
        SceneManager.LoadScene((int)startScene);
#else
        // 빌드에서는 메인씬
        SceneManager.LoadScene((int)Scenes.Main);
#endif
    }
}