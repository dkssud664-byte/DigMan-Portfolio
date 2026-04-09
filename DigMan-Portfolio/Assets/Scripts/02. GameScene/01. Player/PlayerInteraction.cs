using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable currentTarget;
    private GameObject interactionUI;
    private InputSystem inputSystem;

    public void Awake()
    {
        inputSystem = Facade.Instance.InputSystem;
    }
   
    private void Update()
    {
        if (currentTarget == null)
        {
            return;
        }

        if (inputSystem.FDown)
        {
            currentTarget.Interact();
        }
    }

    public void SetTarget(IInteractable target)
    {
        currentTarget = target;

        if (target == null)
        {
            HideInteractionUI();
        }
        else
        {
            ShowInterctionUI();
        }
    }
 

    public void SetInteractionUI(GameObject ui)
    {
        if(ui == null)
        {
            return;
        }

        interactionUI = ui;
    }

    public void ShowInterctionUI()
    {
        interactionUI.SetActive(true);
    }

    public void HideInteractionUI()
    {
        interactionUI.SetActive(false);
    }
}
