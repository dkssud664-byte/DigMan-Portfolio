using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStat/PlayerStatLevelData")]
public class PlayerStatLevelData : ScriptableObject
{
    [SerializeField] protected PlayerStatType type;
    [SerializeField] protected string statName;
    [SerializeField] protected Sprite icon;
    [SerializeField] protected int level;
    [SerializeField] protected int[] value;
    [SerializeField] protected int[] cost;

    public PlayerStatType Type => type;
    public string StatName => statName;
    public Sprite Icon => icon;
    public int Level => level;
    public int[] Value => value;
    public int[] Cost => cost;
}