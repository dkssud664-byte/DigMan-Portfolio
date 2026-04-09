using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Mining/Mineral Depth Config")]
public class MineralDepthConfig : ScriptableObject
{
    [SerializeField] private List<DepthRule> depthRules;

    public IReadOnlyList<DepthRule> DepthRules => depthRules;
}

[System.Serializable]
public class DepthRule
{
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private List<MineralRatio> minerals;
    public IReadOnlyList<MineralRatio> Minerals => minerals;
    public float MinY => minY;
    public float MaxY => maxY;

    public bool IsValid()
    {
        float sum = 0f;

        foreach(MineralRatio mineral in  minerals)
        {
            sum += mineral.Ratio;
        }

        return Mathf.Abs(sum - 1f) < 0.01f;
    }
}

[System.Serializable]
public class MineralRatio
{
    [SerializeField] private MineralType type;
    [Range(0f, 1f)]
    [SerializeField] private float ratio;

    public MineralType Type => type;
    public float Ratio => ratio;
}