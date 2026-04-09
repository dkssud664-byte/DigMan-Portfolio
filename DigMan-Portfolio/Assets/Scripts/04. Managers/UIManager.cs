using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Stack<UIPanel> uiStack = new Stack<UIPanel>();
    [SerializeField] private InputSystem inputSystem;
    private UIPanel pauseMenuCanvas;
    public CursorPolicy CursorPolicy { get; set; }

    private void Awake()
    {
        CursorPolicy = CursorPolicy.UnlockedByDefault;
    }

    private void Start()
    {
        ApplyBaseCursorPolicy();
    }

    private void Update()
    {
        if(inputSystem.ESCDown)
        {
            CloseTop();
        }
    }

    public void SetPauseMenuCanvas(UIPanel paseMenu)
    {
        if (paseMenu == null)
        {
            return;
        }

        pauseMenuCanvas = paseMenu;
    }

    public void Open(UIPanel panel)
    {
        if (uiStack.Count > 0)
        {
            uiStack.Peek().OnCovered();
        }

        uiStack.Push(panel);
        panel.Open();

        UpdateCursorState();
    }

    public void CloseTop()
    {
        if (uiStack.Count == 0 && pauseMenuCanvas != null)
        {
            Open(pauseMenuCanvas);
            return;
        }

        if (uiStack.Count == 0)
        {
            return;
        }

        UIPanel top = uiStack.Pop();
        top.Close();

        if (uiStack.Count > 0)
        {
            uiStack.Peek().OnRevealed();
        }

        UpdateCursorState();
    }

    public void CloseAll()
    {
        while (uiStack.Count > 0)
        {
            uiStack.Pop().Close();
        }

        UpdateCursorState();
    }

    public void ClearStack()
    {
        uiStack.Clear();

        UpdateCursorState();
    }

    private void UpdateCursorState()
    {
        if (uiStack.Count > 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (Facade.Instance.GameManager != null)
            {
                Facade.Instance.GameManager.SetState(GamePlayState.Paused);
            }
            return;
        }

        ApplyBaseCursorPolicy();
    }

    public void ApplyBaseCursorPolicy()
    {
        switch (CursorPolicy)
        {
            case CursorPolicy.LockedByDefault:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                if(Facade.Instance.GameManager != null)
                {
                    Facade.Instance.GameManager.SetState(GamePlayState.Playing);
                    Debug.Log("Playing");
                }
                break;

            case CursorPolicy.UnlockedByDefault:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }
}
