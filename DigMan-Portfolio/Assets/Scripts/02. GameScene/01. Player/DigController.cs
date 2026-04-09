using System;
using UnityEngine;

public class DigController : MonoBehaviour
{
    [SerializeField] private EquipmentDatabase equipmentDatabase;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private TerrainDigSystem digSystem;
    [SerializeField] private PlayerInfo playerInfo;
    private InputSystem input;
    public struct EquipData
    {
        public EquipStatType type;
        public float value;
    }

    [SerializeField] private float digRange;
    [SerializeField] private float damage;
    [SerializeField] private float brushSize;
    [SerializeField] private float opacity;

    private float coolTime;
    private float lastUseTime;

    private void Awake()
    {
        input = Facade.Instance.InputSystem;
    }

    private void OnEnable()
    {
        playerInfo.OnChangeCurrentEquipStat += HandleONChageEquip;
    }


    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position,
            playerCamera.transform.forward);

        if(input.LeftMouseHold && CanUse())
        {
            if (Physics.Raycast(ray, out RaycastHit hit, digRange))
            {
                Terrain terrain = hit.collider.GetComponent<Terrain>();
                if (terrain != null)
                {
                    digSystem.Dig(terrain, hit.point, brushSize, opacity);
                }
            }
        }

        if(input.RightClickHold && CanUse())
        {
            if (Physics.Raycast(ray, out RaycastHit hit, digRange))
            {
                Terrain terrain = hit.collider.GetComponent<Terrain>();
                if (terrain != null)
                {
                    digSystem.Build(terrain, hit.point, brushSize, opacity);
                }
            }
        }
    }

    private void OnDisable()
    {
        playerInfo.OnChangeCurrentEquipStat -= HandleONChageEquip;
    }

    public void HandleONChageEquip(EquipType type, PlayerUpgradeLevel playerUpgradeLevel)
    {
        if(type == EquipType.None)
        {
            digRange = 0;
            damage = 0;
            brushSize = 0;
            opacity = 0;
            return;
        }

        EquipmentData data = equipmentDatabase.Get(type);
        if (data == null)
        {
            return;
        }

        digRange = data.Range;
        coolTime = data.CoolTime;

        foreach(EquipStatType statType in Enum.GetValues(typeof(EquipStatType)))
        {
            var statData = data.GetStat(statType);
            var equipStat = playerUpgradeLevel.GetEquip(type).GetStat(statType);

            Debug.Log(
                $"[DigController] EquipType={type}, Stat={statType}, " +
                $"Level={equipStat.level}, ValuesLength={statData.values.Length}"
            );

            float result = data.GetStat(statType).
                values[playerUpgradeLevel.GetEquip(type).GetStat(statType).level];

            switch (statType)
            {
                case EquipStatType.Damage:
                    damage = result;
                    break;
                case EquipStatType.Opacity:
                    opacity = result;
                    break;
                case EquipStatType.BrushSize:
                    brushSize = result;
                    break;
                default:
                    break;
            }
        }
    }

    private bool CanUse()
    {
        return Time.time >= lastUseTime + coolTime;
    }
}
