using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadingManager : MonoBehaviour
{
    //씬
    public static LoadingManager Instance { get; private set; }
    [SerializeField] GameObject loadingPanel;           //로딩 패널
    [SerializeField] TextMeshProUGUI loadingTxt;        //로딩 게이지

    //싱글톤
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //로딩 코루틴 호출
    public void AsyncLoadScene(Scenes scene)
    {
        loadingPanel.gameObject.SetActive(true);
        StartCoroutine(LoadSceneAsync((int)scene));
    }

    //비동기 씬 호출
    private IEnumerator LoadSceneAsync(int index)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(index);
        op.allowSceneActivation = false;

        while(op.progress < 0.9f)
        {
            float percent = Mathf.Clamp01(op.progress / 0.9f) * 100f;
            loadingTxt.text = $"{percent:0}%";
            yield return null;
        }

        //연출
        loadingTxt.text = "100%";
        yield return new WaitForSeconds(0.3f);

        op.allowSceneActivation = true;
    }

    //동기 씬 호출
    public void LoadScene(Scenes scene)
    {
        SceneManager.LoadScene((int)scene);
    }

    public void SetLoadingUI(GameObject panel, TextMeshProUGUI text)
    {
        if(text == null || panel == null)
        {
            return;
        }

        loadingPanel = panel;
        loadingTxt = text;
    }
}
