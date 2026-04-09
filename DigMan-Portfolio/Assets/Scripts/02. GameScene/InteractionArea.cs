using UnityEngine;

public class InteractionArea : MonoBehaviour
{
    [SerializeField] private GameObject interactableGO;
    private IInteractable interactable;

    private void Awake()
    {
        interactable = interactableGO.GetComponent<IInteractable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Player")
        {
            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();
            if(playerInteraction == null)
            {
                return;
            }

            playerInteraction.SetTarget(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player")
        {
            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();
            if (playerInteraction == null)
            {
                return;
            }

            playerInteraction.SetTarget(null);
        }
    }
}
