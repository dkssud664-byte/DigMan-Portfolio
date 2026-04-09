public enum EquipType
{
    None = -1,
    Shovel,
    Drill,
    Grenade,
    Gun,
    Launcher,
    Remote
}

public enum Scenes
{
    Bootstrap,
    Main,
    Game,
    Ending,
    Test
}

public enum PlayerStatType
{
    hp,
    stamina,
    weight,
    speed,
    jumpPower,
    jetpackPower
}

public enum EquipStatType
{
    Damage,
    Opacity,
    BrushSize
}

public enum CursorPolicy
{
    LockedByDefault,  
    UnlockedByDefault 
}

public enum GamePlayState
{
    Playing,
    Paused
}

public enum MineralType
{
    None = 0,
    Stone,          //µπ
    Coal,           //ºÆ≈∫
    Hematite,       //√∂±§ºÆ
    Chalcopyrite,   //±∏∏Æ±§ºÆ
    Silver,         //¿∫±§ºÆ
    Gold,           //±›±§ºÆ
    Diamond         //¥Ÿ¿Ãæ∆∏ÛµÂ
}
