using System;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerManager : MonoBehaviour
{
    private GameSaveData currentSaveData;
    public GameObject Player { get; private set; }
    public PlayerInfo PlayerInfo { get; private set; }
    public PlayerController PlayerController { get; private set; }
    public PlayerInteraction PlayerInteraction { get; private set; }
    public PlayerCanvas PlayerCanvas { get; private set; }

    private void Awake()
    {
        Facade.Instance.SetPlayerManager(this);
    }

    public void SetPlayer(GameObject player)
    {
        if(player == null)
        {
            return;
        }

        this.Player = player;
        this.PlayerInfo = player.GetComponent<PlayerInfo>();
        this.PlayerController = player.GetComponent<PlayerController>();
        this.PlayerInteraction = player.GetComponent<PlayerInteraction>();
    }

    public void SetPlayerCanvas(PlayerCanvas playerCanvas)
    {
        if(playerCanvas == null)
        {
            return;
        }

        this.PlayerCanvas = playerCanvas;
    }
}
