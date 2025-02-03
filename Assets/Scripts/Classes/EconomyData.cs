using System;

[Serializable]
public class EconomyData {
    // New save version field.
    public int saveVersion;

    public long UniCoins;
    public long SpecialUniCoins;
    
    public int TargetBlueUnicellPopulation;
    public int TargetPinkUnicellPopulation;
    public int TargetYellowUnicellPopulation;
    public int TargetGreenUnicellPopulation;
    public int TargetPurpleUnicellPopulation;
    public int TargetRedUnicellPopulation;
    public int TargetOrangeUnicellPopulation;
    
    public long BlueUnicellCost;
    public long PinkUnicellCost;
    public long YellowUnicellCost;
    public long GreenUnicellCost;
    public long PurpleUnicellCost;
    public long RedUnicellCost;
    public long OrangeUnicellCost;
    
    public long UnlockNewUnicellCost;
    public long UnlockOrangeUnicellCost;
    
    public long BioUpgradeEvoCost;
    
    public long UpgradeBlueDMGCost;
    public long UpgradeBlueMaxHPCost;
    public long UpgradeBlueSpawnLvCost;
    public long UpgradeBlueElderCost;
    public long UpgradeBlueShinyCost;
    public long UpgradeBlueKingCost;
    
    public long UpgradePinkDMGCost;
    public long UpgradePinkMaxHPCost;
    public long UpgradePinkSpawnLvCost;
    public long UpgradePinkElderCost;
    public long UpgradePinkShinyCost;
    public long UpgradePinkKingCost;
    
    public long UpgradeYellowDMGCost;
    public long UpgradeYellowMaxHPCost;
    public long UpgradeYellowSpawnLvCost;
    public long UpgradeYellowElderCost;
    public long UpgradeYellowShinyCost;
    public long UpgradeYellowKingCost;
    
    public long UpgradeGreenDMGCost;
    public long UpgradeGreenMaxHPCost;
    public long UpgradeGreenSpawnLvCost;
    public long UpgradeGreenElderCost;
    public long UpgradeGreenShinyCost;
    public long UpgradeGreenKingCost;
    
    public long UpgradePurpleDMGCost;
    public long UpgradePurpleMaxHPCost;
    public long UpgradePurpleSpawnLvCost;
    public long UpgradePurpleElderCost;
    public long UpgradePurpleShinyCost;
    public long UpgradePurpleKingCost;
    
    public long UpgradeRedDMGCost;
    public long UpgradeRedMaxHPCost;
    public long UpgradeRedSpawnLvCost;
    public long UpgradeRedElderCost;
    public long UpgradeRedShinyCost;
    public long UpgradeRedKingCost;
    
    public long UpgradeOrangeDMGCost;
    public long UpgradeOrangeMaxHPCost;
    public long UpgradeOrangeSpawnLvCost;
    public long UpgradeOrangeElderCost;
    public long UpgradeOrangeShinyCost;
    public long UpgradeOrangeKingCost;
    
    public long BlueSouls;
    public long PinkSouls;
    public long YellowSouls;
    public long GreenSouls;
    public long PurpleSouls;
    public long RedSouls;
    public long OrangeSouls;

    // Additional flags from your other managers:
    public bool isBlueUnicellsUnlocked;
    public bool isPinkUnicellsUnlocked;
    public bool isYellowUnicellsUnlocked;
    public bool isGreenUnicellsUnlocked;
    public bool isPurpleUnicellsUnlocked;
    public bool isOrangeUnicellsUnlocked;
    
    public bool isBioDamageUnlocked;
    public bool isBioMaxHealthUnlocked;
    public bool isBioSpawnLevelUnlocked;
    public bool isBioElderUnlocked;
    public bool isBioShinyUnlocked;
    public bool isBioKingUnlocked;
    
    // Upgrade Variables
    public float BlueDamageStatMultiplier;
    public float BlueMaxHPStatMultiplier;
    public int BlueSpawnLvStat;
    public float BlueElderStatMultiplier;
    public float BlueShinyStatMultiplier;
    public float BlueKingStatMultiplier;


    public float PinkDamageStatMultiplier;
    public float PinkMaxHPStatMultiplier;
    public int PinkSpawnLvStat;
    public float PinkElderStatMultiplier;
    public float PinkShinyStatMultiplier;
    public float PinkKingStatMultiplier;

    public float YellowDamageStatMultiplier;
    public float YellowMaxHPStatMultiplier;
    public int YellowSpawnLvStat;
    public float YellowElderStatMultiplier;
    public float YellowShinyStatMultiplier;
    public float YellowKingStatMultiplier;

    public float GreenDamageStatMultiplier;
    public float GreenMaxHPStatMultiplier;
    public int GreenSpawnLvStat;
    public float GreenElderStatMultiplier;
    public float GreenShinyStatMultiplier;
    public float GreenKingStatMultiplier;

    public float PurpleDamageStatMultiplier;
    public float PurpleMaxHPStatMultiplier;
    public int PurpleSpawnLvStat;
    public float PurpleElderStatMultiplier;
    public float PurpleShinyStatMultiplier;
    public float PurpleKingStatMultiplier;

    public float RedDamageStatMultiplier;
    public float RedMaxHPStatMultiplier;
    public int RedSpawnLvStat;
    public float RedElderStatMultiplier;
    public float RedShinyStatMultiplier;
    public float RedKingStatMultiplier;

    public float OrangeDamageStatMultiplier;
    public float OrangeMaxHPStatMultiplier;
    public int OrangeSpawnLvStat;
    public float OrangeElderStatMultiplier;
    public float OrangeShinyStatMultiplier;
    public float OrangeKingStatMultiplier;
}
