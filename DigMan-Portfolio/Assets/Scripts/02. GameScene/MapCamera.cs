using UnityEngine;

public class MapCamera : MonoBehaviour
{
    private Transform Player;
    [SerializeField] private float cameraHeightOffset = 200f;

    private void Awake()
    {
        Facade.Instance.SetMapCamera(this);
    }


    private void LateUpdate()
    {
        FollowPlayer();
    }

    public void SetPlayer(Transform player)
    {
        if(player == null)
        {
            return;
        }

        Player = player;
    }

    private void FollowPlayer()
    {
        if (Player == null)
        {
            return;
        }

        this.transform.position = new Vector3(Player.position.x, Player.position.y + cameraHeightOffset, Player.position.z);
    }
}
