public enum ItemType
{
    Seed, Commodity, Furniture,///���ӣ���Ʒ���Ҿ�///
    HoeTool, ChopTool, BreakTool, ReapTool, WaterTool, CollectTool,Buildtool,///��ͷ����������ʯͷ����ݣ���ˮ���ո�
    ReapableScenery///�Ӳ�
}
public enum SlotType
{
    Bag, Box, Shop
}

public enum InventoryLocation
{
    Player,Box
}

public enum PartType
{
    None, Carry, Hoe, Break,Chop,Water,Collect,Build
}
public enum PartName
{
    Body, Tool
}

public enum Season
{
    ����,����,����,����
}

public enum GridType
{
    diggable,Dropitem,NPCObstacle,PlaceFurniture
}

public enum GameState
{
    GamePlay, Pause
}

public enum LightShift
{
    Morning,Night
}

public enum SoundName
{
    none, FootStepSoft, FootStepHard,
    Axe, Pickaxe, Hoe, Reap, Water, Basket,
    Pickup, Plant, TreeFalling, Rustle,
    AmbientCountryside1, AmbientCountryside2, MusicCalm1, MusicCalm2, MusicCalm3, MusicCalm4, MusicCalm5, MusicCalm6, AmbientIndoor1
}