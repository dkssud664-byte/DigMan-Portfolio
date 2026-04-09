using UnityEngine;

public class ManagersRoot : MonoBehaviour
{
    //하이러키에서 매니저들을 하나로 관리
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
