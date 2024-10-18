public enum ItemCode : byte
{
    Misc = 0,
    Ruby,
    Emerald,
    Sapphire, 
    CopperCoin, 
    SilverCoin,
    GoldCoin,
}

public enum ItemSortCriteria : byte
{
    Code,   // 코드 기준
    Name,   // 이름 기준
    Price   // 가격 기준
}