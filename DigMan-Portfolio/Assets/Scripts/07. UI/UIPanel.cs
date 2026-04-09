using UnityEngine;

public class UIPanel : MonoBehaviour
{
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnCovered() { }
    public virtual void OnRevealed() { }
}
