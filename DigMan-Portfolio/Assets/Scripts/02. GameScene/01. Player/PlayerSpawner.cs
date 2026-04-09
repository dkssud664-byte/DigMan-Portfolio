using UnityEngine;

[System.Serializable]
public class PlayerSpawner : MonoBehaviour
{
    #region 플레이어 생성, 초기화
    public GameObject CreatePlayer(GameObject playerPrefab)
    {
        GameObject player = Object.Instantiate(playerPrefab);
        PlayerInfo playerInfo = player.GetComponent<PlayerInfo>();
        return player;
    }

    public void InitializePlayer(GameObject player, GameSaveData saveData)
    {
        GameSaveData gameSaveData = saveData;
        
        PlayerInfo playerInfo = player.GetComponent<PlayerInfo>();
        PlayerController playerController = player.GetComponent<PlayerController>();
        //플레이어 데이터 초기화
        playerInfo.InitializePlayer(gameSaveData);
        player.GetComponent<PlayerController>().SetPlayerInfo(playerInfo);
        playerController.SetPlayerInfo(playerInfo);
        Facade.Instance.PlayerManager.SetPlayer(player);
        //해금 장비 슬롯 추가
        Facade.Instance.PlayerManager.PlayerCanvas.OnExpandEquipArea(playerInfo.PlayerUnlockEquipData);
    }
    #endregion

    public GameObject CreatePlayerCanvas(GameObject canvasPrefab)
    {
        GameObject player = Object.Instantiate(canvasPrefab);
       
        return player;
    }

}
