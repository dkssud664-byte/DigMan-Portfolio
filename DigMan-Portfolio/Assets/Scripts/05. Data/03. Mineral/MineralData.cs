using UnityEngine;


[CreateAssetMenu(menuName = "Mineral/MineralData")]
public class MineralData : ScriptableObject
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Sprite[] brokenSprites;
    [SerializeField] private Material material;
    [SerializeField] private MineralType type;
    [SerializeField] private int maxhp;

    public GameObject[] Prefabs => prefabs;
    public Sprite[] BrokenSprites => brokenSprites;
    public Material Material => material;
    public MineralType Type => type;
    public int Maxhp => maxhp;
}
