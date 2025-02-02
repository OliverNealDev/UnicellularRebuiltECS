using System;
using Unity.VisualScripting;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public long UniCoins { get; private set; }
    public long SpecialUniCoins { get; private set; }

    public int TargetBlueUnicellPopulation { get; private set; }
    public int TargetPinkUnicellPopulation { get; private set; }
    public int TargetYellowUnicellPopulation { get; private set; }
    public int TargetGreenUnicellPopulation { get; private set; }
    public int TargetPurpleUnicellPopulation { get; private set; }
    public int TargetRedUnicellPopulation { get; private set; }
    public int TargetOrangeUnicellPopulation { get; private set; }


    [SerializeField] private long BlueUnicellCost;
    [SerializeField] private long PinkUnicellCost;
    [SerializeField] private long YellowUnicellCost;
    [SerializeField] private long GreenUnicellCost;
    [SerializeField] private long PurpleUnicellCost;
    [SerializeField] private long RedUnicellCost;
    [SerializeField] private long OrangeUnicellCost;

    // Purchase Panel
    public long UnlockNewUnicellCost { get; private set; }
    public long UnlockOrangeUnicellCost { get; private set; }

    // BioLab
    public long BioUpgradeEvoCost { get; private set; }

    // Evo Lab
    public long UpgradeBlueDMGCost { get; private set; }
    public long UpgradeBlueMaxHPCost { get; private set; }
    public long UpgradeBlueSpawnLvCost { get; private set; }
    public long UpgradeBlueElderCost { get; private set; }
    public long UpgradeBlueShinyCost { get; private set; }
    public long UpgradeBlueKingCost { get; private set; }

    public long UpgradePinkDMGCost { get; private set; }
    public long UpgradePinkMaxHPCost { get; private set; }
    public long UpgradePinkSpawnLvCost { get; private set; }
    public long UpgradePinkElderCost { get; private set; }
    public long UpgradePinkShinyCost { get; private set; }
    public long UpgradePinkKingCost { get; private set; }

    public long UpgradeYellowDMGCost { get; private set; }
    public long UpgradeYellowMaxHPCost { get; private set; }
    public long UpgradeYellowSpawnLvCost { get; private set; }
    public long UpgradeYellowElderCost { get; private set; }
    public long UpgradeYellowShinyCost { get; private set; }
    public long UpgradeYellowKingCost { get; private set; }

    public long UpgradeGreenDMGCost { get; private set; }
    public long UpgradeGreenMaxHPCost { get; private set; }
    public long UpgradeGreenSpawnLvCost { get; private set; }
    public long UpgradeGreenElderCost { get; private set; }
    public long UpgradeGreenShinyCost { get; private set; }
    public long UpgradeGreenKingCost { get; private set; }

    public long UpgradePurpleDMGCost { get; private set; }
    public long UpgradePurpleMaxHPCost { get; private set; }
    public long UpgradePurpleSpawnLvCost { get; private set; }
    public long UpgradePurpleElderCost { get; private set; }
    public long UpgradePurpleShinyCost { get; private set; }
    public long UpgradePurpleKingCost { get; private set; }

    public long UpgradeRedDMGCost { get; private set; }
    public long UpgradeRedMaxHPCost { get; private set; }
    public long UpgradeRedSpawnLvCost { get; private set; }
    public long UpgradeRedElderCost { get; private set; }
    public long UpgradeRedShinyCost { get; private set; }
    public long UpgradeRedKingCost { get; private set; }

    public long UpgradeOrangeDMGCost { get; private set; }
    public long UpgradeOrangeMaxHPCost { get; private set; }
    public long UpgradeOrangeSpawnLvCost { get; private set; }
    public long UpgradeOrangeElderCost { get; private set; }
    public long UpgradeOrangeShinyCost { get; private set; }
    public long UpgradeOrangeKingCost { get; private set; }

    public long BlueSouls { get; private set; }
    public long PinkSouls { get; private set; }
    public long YellowSouls { get; private set; }
    public long GreenSouls { get; private set; }
    public long PurpleSouls { get; private set; }
    public long RedSouls { get; private set; }
    public long OrangeSouls { get; private set; }

    [SerializeField] private EntityManager entityManager;

    void Start()
    {
        // Example starting values
        UniCoins = 2000000000000000000;
        BlueSouls = PinkSouls = YellowSouls = GreenSouls = PurpleSouls = RedSouls = OrangeSouls = 0;
        UnlockNewUnicellCost = 10;
        UnlockOrangeUnicellCost = 10000;
        TargetBlueUnicellPopulation = TargetPinkUnicellPopulation = TargetYellowUnicellPopulation =
            TargetGreenUnicellPopulation = TargetPurpleUnicellPopulation = TargetRedUnicellPopulation =
            TargetOrangeUnicellPopulation = 0;
        BioUpgradeEvoCost = 10;

        // Initial cost values
        UpgradeBlueDMGCost = UpgradeBlueMaxHPCost = UpgradeBlueSpawnLvCost =
        UpgradeBlueElderCost = UpgradeBlueShinyCost = UpgradeBlueKingCost = 10;
        
        UpgradePinkDMGCost = UpgradePinkMaxHPCost = UpgradePinkSpawnLvCost =
        UpgradePinkElderCost = UpgradePinkShinyCost = UpgradePinkKingCost = 10;
        
        UpgradeYellowDMGCost = UpgradeYellowMaxHPCost = UpgradeYellowSpawnLvCost =
        UpgradeYellowElderCost = UpgradeYellowShinyCost = UpgradeYellowKingCost = 10;
        
        UpgradeGreenDMGCost = UpgradeGreenMaxHPCost = UpgradeGreenSpawnLvCost =
        UpgradeGreenElderCost = UpgradeGreenShinyCost = UpgradeGreenKingCost = 10;
        
        UpgradePurpleDMGCost = UpgradePurpleMaxHPCost = UpgradePurpleSpawnLvCost =
        UpgradePurpleElderCost = UpgradePurpleShinyCost = UpgradePurpleKingCost = 10;
        
        UpgradeRedDMGCost = UpgradeRedMaxHPCost = UpgradeRedSpawnLvCost =
        UpgradeRedElderCost = UpgradeRedShinyCost = UpgradeRedKingCost = 10;
        
        UpgradeOrangeDMGCost = UpgradeOrangeMaxHPCost = UpgradeOrangeSpawnLvCost =
        UpgradeOrangeElderCost = UpgradeOrangeShinyCost = UpgradeOrangeKingCost = 10;

        Invoke("UpdateSoulTextCall", 0.25f);
    }

    void UpdateSoulTextCall()
    {
        TextManager.Instance.UpdateSoulText(BlueSouls, PinkSouls);
    }

    public int TotaltargetUnicellPopulation()
    {
        return TargetBlueUnicellPopulation
             + TargetPinkUnicellPopulation
             + TargetYellowUnicellPopulation
             + TargetGreenUnicellPopulation
             + TargetPurpleUnicellPopulation
             + TargetRedUnicellPopulation
             + TargetOrangeUnicellPopulation;
    }

    public void UpdateSouls(int blueSouls, int pinkSouls)
    {
        // Using System.Math.Floor ensures the value remains a double before converting to long.
        BlueSouls += (long)Math.Floor((double)blueSouls);
        PinkSouls += (long)Math.Floor((double)pinkSouls);
        TextManager.Instance.UpdateSoulText(BlueSouls, PinkSouls);
    }

    public void SpendUnicell(string UnicellType)
    {
        switch (UnicellType)
        {
            case "Blue":
                UniCoins -= BlueUnicellCost;
                // Use System.Math.Ceiling to avoid converting to int
                BlueUnicellCost = (long)Math.Ceiling(BlueUnicellCost * 1.05);
                break;
            case "Pink":
                UniCoins -= PinkUnicellCost;
                PinkUnicellCost = (long)Math.Ceiling(PinkUnicellCost * 1.05);
                break;
            case "Yellow":
                UniCoins -= YellowUnicellCost;
                YellowUnicellCost = (long)Math.Ceiling(YellowUnicellCost * 1.05);
                break;
            case "Green":
                UniCoins -= GreenUnicellCost;
                GreenUnicellCost = (long)Math.Ceiling(GreenUnicellCost * 1.05);
                break;
            case "Purple":
                UniCoins -= PurpleUnicellCost;
                PurpleUnicellCost = (long)Math.Ceiling(PurpleUnicellCost * 1.05);
                break;
            case "Red":
                UniCoins -= RedUnicellCost;
                RedUnicellCost = (long)Math.Ceiling(RedUnicellCost * 1.05);
                break;
            case "Orange":
                SpecialUniCoins -= OrangeUnicellCost;
                OrangeUnicellCost = (long)Math.Ceiling(OrangeUnicellCost * 1.05);
                break;
        }
    }

    public void SpendNewUnicellUnlock()
    {
        UniCoins -= UnlockNewUnicellCost;
        UnlockNewUnicellCost *= 10;
    }
    public void SpendOrangeUnicellUnlock()
    {
        SpecialUniCoins -= UnlockOrangeUnicellCost;
    }

    public void SpendDMGBioUpgrade()
    {
        BlueSouls -= BioUpgradeEvoCost;
        PinkSouls -= BioUpgradeEvoCost;
        BioUpgradeEvoCost *= 3;
    }
    public void SpendMaxHPBioUpgrade()
    {
        BlueSouls -= BioUpgradeEvoCost;
        YellowSouls -= BioUpgradeEvoCost;
        BioUpgradeEvoCost *= 3;
    }
    public void SpendSpawnLvBioUpgrade()
    {
        PinkSouls -= BioUpgradeEvoCost;
        YellowSouls -= BioUpgradeEvoCost;
        BioUpgradeEvoCost *= 3;
    }
    public void SpendElderBioUpgrade()
    {
        BlueSouls -= BioUpgradeEvoCost;
        PinkSouls -= BioUpgradeEvoCost;
        YellowSouls -= BioUpgradeEvoCost;
        GreenSouls -= BioUpgradeEvoCost;
        BioUpgradeEvoCost *= 3;
    }
    public void SpendShinyBioUpgrade()
    {
        BlueSouls -= BioUpgradeEvoCost;
        PinkSouls -= BioUpgradeEvoCost;
        YellowSouls -= BioUpgradeEvoCost;
        PurpleSouls -= BioUpgradeEvoCost;
        BioUpgradeEvoCost *= 3;
    }
    public void SpendKingBioUpgrade()
    {
        BlueSouls -= BioUpgradeEvoCost;
        PinkSouls -= BioUpgradeEvoCost;
        YellowSouls -= BioUpgradeEvoCost;
        RedSouls -= BioUpgradeEvoCost;
        BioUpgradeEvoCost *= 3;
    }

    public void SpendDMGUpgrade(string UnicellType)
    {
        switch (UnicellType)
        {
            case "Blue":
                BlueSouls -= UpgradeBlueDMGCost;
                UpgradeBlueDMGCost = (long)Math.Ceiling(UpgradeBlueDMGCost * 1.3);
                break;
            case "Pink":
                PinkSouls -= UpgradePinkDMGCost;
                UpgradePinkDMGCost = (long)Math.Ceiling(UpgradePinkDMGCost * 1.3);
                break;
            case "Yellow":
                YellowSouls -= UpgradeYellowDMGCost;
                UpgradeYellowDMGCost = (long)Math.Ceiling(UpgradeYellowDMGCost * 1.3);
                break;
            case "Green":
                GreenSouls -= UpgradeGreenDMGCost;
                UpgradeGreenDMGCost = (long)Math.Ceiling(UpgradeGreenDMGCost * 1.3);
                break;
            case "Purple":
                PurpleSouls -= UpgradePurpleDMGCost;
                UpgradePurpleDMGCost = (long)Math.Ceiling(UpgradePurpleDMGCost * 1.3);
                break;
            case "Red":
                RedSouls -= UpgradeRedDMGCost;
                UpgradeRedDMGCost = (long)Math.Ceiling(UpgradeRedDMGCost * 1.3);
                break;
            case "Orange":
                OrangeSouls -= UpgradeOrangeDMGCost;
                UpgradeOrangeDMGCost = (long)Math.Ceiling(UpgradeOrangeDMGCost * 1.3);
                break;
        }

        entityManager.UpgradeUnicellStats(UnicellType, "Damage");
        TextManager.Instance.UpdateSoulText(BlueSouls, PinkSouls);
    }
    public void SpendMaxHPUpgrade(string UnicellType)
    {
        switch (UnicellType)
        {
            case "Blue":
                BlueSouls -= UpgradeBlueMaxHPCost;
                UpgradeBlueMaxHPCost = (long)Math.Ceiling(UpgradeBlueMaxHPCost * 1.3);
                break;
            case "Pink":
                PinkSouls -= UpgradePinkMaxHPCost;
                UpgradePinkMaxHPCost = (long)Math.Ceiling(UpgradePinkMaxHPCost * 1.3);
                break;
            case "Yellow":
                YellowSouls -= UpgradeYellowMaxHPCost;
                UpgradeYellowMaxHPCost = (long)Math.Ceiling(UpgradeYellowMaxHPCost * 1.3);
                break;
            case "Green":
                GreenSouls -= UpgradeGreenMaxHPCost;
                UpgradeGreenMaxHPCost = (long)Math.Ceiling(UpgradeGreenMaxHPCost * 1.3);
                break;
            case "Purple":
                PurpleSouls -= UpgradePurpleMaxHPCost;
                UpgradePurpleMaxHPCost = (long)Math.Ceiling(UpgradePurpleMaxHPCost * 1.3);
                break;
            case "Red":
                RedSouls -= UpgradeRedMaxHPCost;
                UpgradeRedMaxHPCost = (long)Math.Ceiling(UpgradeRedMaxHPCost * 1.3);
                break;
            case "Orange":
                OrangeSouls -= UpgradeOrangeMaxHPCost;
                UpgradeOrangeMaxHPCost = (long)Math.Ceiling(UpgradeOrangeMaxHPCost * 1.3);
                break;
        }

        entityManager.UpgradeUnicellStats(UnicellType, "MaxHP");
        TextManager.Instance.UpdateSoulText(BlueSouls, PinkSouls);
    }
    public void SpendSpawnLvUpgrade(string UnicellType)
    {
        switch (UnicellType)
        {
            case "Blue":
                BlueSouls -= UpgradeBlueSpawnLvCost;
                UpgradeBlueSpawnLvCost = (long)Math.Ceiling(UpgradeBlueSpawnLvCost * 1.3);
                break;
            case "Pink":
                PinkSouls -= UpgradePinkSpawnLvCost;
                UpgradePinkSpawnLvCost = (long)Math.Ceiling(UpgradePinkSpawnLvCost * 1.3);
                break;
            case "Yellow":
                YellowSouls -= UpgradeYellowSpawnLvCost;
                UpgradeYellowSpawnLvCost = (long)Math.Ceiling(UpgradeYellowSpawnLvCost * 1.3);
                break;
            case "Green":
                GreenSouls -= UpgradeGreenSpawnLvCost;
                UpgradeGreenSpawnLvCost = (long)Math.Ceiling(UpgradeGreenSpawnLvCost * 1.3);
                break;
            case "Purple":
                PurpleSouls -= UpgradePurpleSpawnLvCost;
                UpgradePurpleSpawnLvCost = (long)Math.Ceiling(UpgradePurpleSpawnLvCost * 1.3);
                break;
            case "Red":
                RedSouls -= UpgradeRedSpawnLvCost;
                UpgradeRedSpawnLvCost = (long)Math.Ceiling(UpgradeRedSpawnLvCost * 1.3);
                break;
            case "Orange":
                OrangeSouls -= UpgradeOrangeSpawnLvCost;
                UpgradeOrangeSpawnLvCost = (long)Math.Ceiling(UpgradeOrangeSpawnLvCost * 1.3);
                break;
        }

        entityManager.UpgradeUnicellStats(UnicellType, "SpawnLv");
        TextManager.Instance.UpdateSoulText(BlueSouls, PinkSouls);
    }
    public void SpendElderUpgrade(string UnicellType)
    {
        switch (UnicellType)
        {
            case "Blue":
                BlueSouls -= UpgradeBlueElderCost;
                UpgradeBlueElderCost = (long)Math.Ceiling(UpgradeBlueElderCost * 1.3);
                break;
            case "Pink":
                PinkSouls -= UpgradePinkElderCost;
                UpgradePinkElderCost = (long)Math.Ceiling(UpgradePinkElderCost * 1.3);
                break;
            case "Yellow":
                YellowSouls -= UpgradeYellowElderCost;
                UpgradeYellowElderCost = (long)Math.Ceiling(UpgradeYellowElderCost * 1.3);
                break;
            case "Green":
                GreenSouls -= UpgradeGreenElderCost;
                UpgradeGreenElderCost = (long)Math.Ceiling(UpgradeGreenElderCost * 1.3);
                break;
            case "Purple":
                PurpleSouls -= UpgradePurpleElderCost;
                UpgradePurpleElderCost = (long)Math.Ceiling(UpgradePurpleElderCost * 1.3);
                break;
            case "Red":
                RedSouls -= UpgradeRedElderCost;
                UpgradeRedElderCost = (long)Math.Ceiling(UpgradeRedElderCost * 1.3);
                break;
            case "Orange":
                OrangeSouls -= UpgradeOrangeElderCost;
                UpgradeOrangeElderCost = (long)Math.Ceiling(UpgradeOrangeElderCost * 1.3);
                break;
        }

        entityManager.UpgradeUnicellStats(UnicellType, "Elder");
        TextManager.Instance.UpdateSoulText(PinkSouls, YellowSouls);
    }
    public void SpendShinyUpgrade(string UnicellType)
    {
        switch (UnicellType)
        {
            case "Blue":
                BlueSouls -= UpgradeBlueShinyCost;
                UpgradeBlueShinyCost = (long)Math.Ceiling(UpgradeBlueShinyCost * 1.3);
                break;
            case "Pink":
                PinkSouls -= UpgradePinkShinyCost;
                UpgradePinkShinyCost = (long)Math.Ceiling(UpgradePinkShinyCost * 1.3);
                break;
            case "Yellow":
                YellowSouls -= UpgradeYellowShinyCost;
                UpgradeYellowShinyCost = (long)Math.Ceiling(UpgradeYellowShinyCost * 1.3);
                break;
            case "Green":
                GreenSouls -= UpgradeGreenShinyCost;
                UpgradeGreenShinyCost = (long)Math.Ceiling(UpgradeGreenShinyCost * 1.3);
                break;
            case "Purple":
                PurpleSouls -= UpgradePurpleShinyCost;
                UpgradePurpleShinyCost = (long)Math.Ceiling(UpgradePurpleShinyCost * 1.3);
                break;
            case "Red":
                RedSouls -= UpgradeRedShinyCost;
                UpgradeRedShinyCost = (long)Math.Ceiling(UpgradeRedShinyCost * 1.3);
                break;
            case "Orange":
                OrangeSouls -= UpgradeOrangeShinyCost;
                UpgradeOrangeShinyCost = (long)Math.Ceiling(UpgradeOrangeShinyCost * 1.3);
                break;
        }

        entityManager.UpgradeUnicellStats(UnicellType, "Shiny");
        TextManager.Instance.UpdateSoulText(PinkSouls, GreenSouls);
    }
    public void SpendKingUpgrade(string UnicellType)
    {
        switch (UnicellType)
        {
            case "Blue":
                BlueSouls -= UpgradeBlueKingCost;
                UpgradeBlueKingCost = (long)Math.Ceiling(UpgradeBlueKingCost * 1.3);
                break;
            case "Pink":
                PinkSouls -= UpgradePinkKingCost;
                UpgradePinkKingCost = (long)Math.Ceiling(UpgradePinkKingCost * 1.3);
                break;
            case "Yellow":
                YellowSouls -= UpgradeYellowKingCost;
                UpgradeYellowKingCost = (long)Math.Ceiling(UpgradeYellowKingCost * 1.3);
                break;
            case "Green":
                GreenSouls -= UpgradeGreenKingCost;
                UpgradeGreenKingCost = (long)Math.Ceiling(UpgradeGreenKingCost * 1.3);
                break;
            case "Purple":
                PurpleSouls -= UpgradePurpleKingCost;
                UpgradePurpleKingCost = (long)Math.Ceiling(UpgradePurpleKingCost * 1.3);
                break;
            case "Red":
                RedSouls -= UpgradeRedKingCost;
                UpgradeRedKingCost = (long)Math.Ceiling(UpgradeRedKingCost * 1.3);
                break;
            case "Orange":
                OrangeSouls -= UpgradeOrangeKingCost;
                UpgradeOrangeKingCost = (long)Math.Ceiling(UpgradeOrangeKingCost * 1.3);
                break;
        }

        entityManager.UpgradeUnicellStats(UnicellType, "King");
        TextManager.Instance.UpdateSoulText(YellowSouls, GreenSouls);
    }

    public void GrantCurrenciesOnUnicellDeath(Unicell unicell)
    {
        if (unicell.isElderUnicell || unicell.isShinyUnicell || unicell.isKingUnicell)
        {
            if (unicell.Species != Unicell.UnicellSpecies.Orange)
            {
                // Use Math.Round and cast to long so the result stays in the long range
                SpecialUniCoins += (long)Math.Round(unicell.Damage) + (long)Math.Round(unicell.MaxHealth);
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    SpecialUniCoins += (long)Math.Round(unicell.Damage) + (long)Math.Round(unicell.MaxHealth);
                }
            }
        }
        else
        {
            if (unicell.Species != Unicell.UnicellSpecies.Orange)
            {
                UniCoins += (long)Math.Round(unicell.Damage) + (long)Math.Round(unicell.MaxHealth);
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    UniCoins += (long)Math.Round(unicell.Damage) + (long)Math.Round(unicell.MaxHealth);
                }
            }
        }
    }

    public void IncrementUnicellPopulation(string UnicellType)
    {
        switch (UnicellType)
        {
            case "Blue":
                TargetBlueUnicellPopulation++;
                break;
            case "Pink":
                TargetPinkUnicellPopulation++;
                break;
            case "Yellow":
                TargetYellowUnicellPopulation++;
                break;
            case "Green":
                TargetGreenUnicellPopulation++;
                break;
            case "Purple":
                TargetPurpleUnicellPopulation++;
                break;
            case "Red":
                TargetRedUnicellPopulation++;
                break;
            case "Orange":
                TargetOrangeUnicellPopulation++;
                break;
            default:
                Debug.LogError("EconomyManager IncrementUnicellPopulation() - Unknown UnicellType for target population incrementation");
                break;
        }
    }

    public void HarvestUnicell(Unicell UnicellType)
    {
        // The following cases assume that BlueUnicell, etc., are defined constants.
        switch (UnicellType)
        {
            case BlueUnicell:
                BlueSouls++;
                TargetBlueUnicellPopulation--;
                break;
            case PinkUnicell:
                PinkSouls++;
                TargetPinkUnicellPopulation--;
                break;
            case YellowUnicell:
                YellowSouls++;
                TargetYellowUnicellPopulation--;
                break;
            default:
                Debug.LogError("EconomyManager HarvestUnicell() - Unknown UnicellType for target unicell soul incrementation");
                break;
        }

        TextManager.Instance.UpdateSoulText(BlueSouls, PinkSouls);
    } // Vaulted

    public void IncrementSoul(Unicell UnicellType)
    {
        switch (UnicellType)
        {
            case BlueUnicell:
                BlueSouls++;
                break;
            case PinkUnicell:
                PinkSouls++;
                break;
            case YellowUnicell:
                YellowSouls++;
                break;
            case GreenUnicell:
                GreenSouls++;
                break;
            case PurpleUnicell:
                PurpleSouls++;
                break;
            case RedUnicell:
                RedSouls++;
                break;
            case OrangeUnicell:
                OrangeSouls++;
                break;
            default:
                Debug.LogError("EconomyManager IncrementSoul() - Unknown UnicellType for target unicell soul incrementation");
                break;
        }
    }

    public bool UnicellPurchaseEligible(string UnicellType)
    {
        switch (UnicellType)
        {
            case "Blue":
                return UniCoins >= BlueUnicellCost;
            case "Pink":
                return UniCoins >= PinkUnicellCost;
            case "Yellow":
                return UniCoins >= YellowUnicellCost;
            case "Green":
                return UniCoins >= GreenUnicellCost;
            case "Purple":
                return UniCoins >= PurpleUnicellCost;
            case "Red":
                return UniCoins >= RedUnicellCost;
            case "Orange":
                return SpecialUniCoins >= OrangeUnicellCost;
            default:
                Debug.LogError("Unknown UnicellType for UnicellPurchaseEligible");
                return false;
        }
    }

    public bool UpgradeDMGPurchaseEligible(string UnicellType)
    {
        switch (UnicellType)
        {
            case "Blue":
                return BlueSouls >= UpgradeBlueDMGCost;
            case "Pink":
                return PinkSouls >= UpgradePinkDMGCost;
            case "Yellow":
                return YellowSouls >= UpgradeYellowDMGCost;
            case "Green":
                return GreenSouls >= UpgradeGreenDMGCost;
            case "Purple":
                return PurpleSouls >= UpgradePurpleDMGCost;
            case "Red":
                return RedSouls >= UpgradeRedDMGCost;
            case "Orange":
                return OrangeSouls >= UpgradeOrangeDMGCost;
            default:
                Debug.LogError("Unknown UnicellType for UpgradeDMGPurchaseEligible");
                return false;
        }
    }
    public bool UpgradeMaxHPPurchaseEligible(string UnicellType)
    {
        switch (UnicellType)
        {
            case "Blue":
                return BlueSouls >= UpgradeBlueMaxHPCost;
            case "Pink":
                return PinkSouls >= UpgradePinkMaxHPCost;
            case "Yellow":
                return YellowSouls >= UpgradeYellowMaxHPCost;
            case "Green":
                return GreenSouls >= UpgradeGreenMaxHPCost;
            case "Purple":
                return PurpleSouls >= UpgradePurpleMaxHPCost;
            case "Red":
                return RedSouls >= UpgradeRedMaxHPCost;
            case "Orange":
                return OrangeSouls >= UpgradeOrangeMaxHPCost;
            default:
                Debug.LogError("Unknown UnicellType for UpgradeMaxHPPurchaseEligible");
                return false;
        }
    }
    public bool UpgradeSpawnLvPurchaseEligible(string UnicellType)
    {
        switch (UnicellType)
        {
            case "Blue":
                return BlueSouls >= UpgradeBlueSpawnLvCost;
            case "Pink":
                return PinkSouls >= UpgradePinkSpawnLvCost;
            case "Yellow":
                return YellowSouls >= UpgradeYellowSpawnLvCost;
            case "Green":
                return GreenSouls >= UpgradeGreenSpawnLvCost;
            case "Purple":
                return PurpleSouls >= UpgradePurpleSpawnLvCost;
            case "Red":
                return RedSouls >= UpgradeRedSpawnLvCost;
            case "Orange":
                return OrangeSouls >= UpgradeOrangeSpawnLvCost;
            default:
                Debug.LogError("Unknown UnicellType for UpgradeSpawnLvPurchaseEligible");
                return false;
        }
    }
    public bool UpgradeElderPurchaseEligible(string UnicellType)
    {
        switch (UnicellType)
        {
            case "Blue":
                return BlueSouls >= UpgradeBlueElderCost;
            case "Pink":
                return PinkSouls >= UpgradePinkElderCost;
            case "Yellow":
                return YellowSouls >= UpgradeYellowElderCost;
            case "Green":
                return GreenSouls >= UpgradeGreenElderCost;
            case "Purple":
                return PurpleSouls >= UpgradePurpleElderCost;
            case "Red":
                return RedSouls >= UpgradeRedElderCost;
            case "Orange":
                return OrangeSouls >= UpgradeOrangeElderCost;
            default:
                Debug.LogError("Unknown UnicellType for UpgradeElderPurchaseEligible");
                return false;
        }
    }
    public bool UpgradeShinyPurchaseEligible(string UnicellType)
    {
        switch (UnicellType)
        {
            case "Blue":
                return BlueSouls >= UpgradeBlueShinyCost;
            case "Pink":
                return PinkSouls >= UpgradePinkShinyCost;
            case "Yellow":
                return YellowSouls >= UpgradeYellowShinyCost;
            case "Green":
                return GreenSouls >= UpgradeGreenShinyCost;
            case "Purple":
                return PurpleSouls >= UpgradePurpleShinyCost;
            case "Red":
                return RedSouls >= UpgradeRedShinyCost;
            case "Orange":
                return OrangeSouls >= UpgradeOrangeShinyCost;
            default:
                Debug.LogError("Unknown UnicellType for UpgradeShinyPurchaseEligible");
                return false;
        }
    }
    public bool UpgradeKingPurchaseEligible(string UnicellType)
    {
        switch (UnicellType)
        {
            case "Blue":
                return BlueSouls >= UpgradeBlueKingCost;
            case "Pink":
                return PinkSouls >= UpgradePinkKingCost;
            case "Yellow":
                return YellowSouls >= UpgradeYellowKingCost;
            case "Green":
                return GreenSouls >= UpgradeGreenKingCost;
            case "Purple":
                return PurpleSouls >= UpgradePurpleKingCost;
            case "Red":
                return RedSouls >= UpgradeRedKingCost;
            case "Orange":
                return OrangeSouls >= UpgradeOrangeKingCost;
            default:
                Debug.LogError("Unknown UnicellType for UpgradeKingPurchaseEligible");
                return false;
        }
    }

    public bool BioUpgradeDMGPurchaseEligible()
    {
        return BlueSouls >= BioUpgradeEvoCost && PinkSouls >= BioUpgradeEvoCost;
    }
    public bool BioUpgradeMaxHPPurchaseEligible()
    {
        return BlueSouls >= BioUpgradeEvoCost && YellowSouls >= BioUpgradeEvoCost;
    }
    public bool BioUpgradeSpawnLvPurchaseEligible()
    {
        return PinkSouls >= BioUpgradeEvoCost && YellowSouls >= BioUpgradeEvoCost;
    }
    public bool BioUpgradeElderPurchaseEligible()
    {
        return BlueSouls >= BioUpgradeEvoCost &&
               PinkSouls >= BioUpgradeEvoCost &&
               YellowSouls >= BioUpgradeEvoCost &&
               GreenSouls >= BioUpgradeEvoCost;
    }
    public bool BioUpgradeShinyPurchaseEligible()
    {
        return BlueSouls >= BioUpgradeEvoCost &&
               PinkSouls >= BioUpgradeEvoCost &&
               YellowSouls >= BioUpgradeEvoCost &&
               PurpleSouls >= BioUpgradeEvoCost;
    }
    public bool BioUpgradeKingPurchaseEligible()
    {
        return BlueSouls >= BioUpgradeEvoCost &&
               PinkSouls >= BioUpgradeEvoCost &&
               YellowSouls >= BioUpgradeEvoCost &&
               RedSouls >= BioUpgradeEvoCost;
    }

    public string AbbreviateNumber(long number)
    {
        if (number < 10000) return number.ToString();

        // Limited to Qi due to 64-bit constraints
        string[] suffixes = { "", "k", "M", "B", "T", "Qa", "Qi" };
        int suffixIndex = 0;
        double abbreviatedNumber = number;

        while (abbreviatedNumber >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            abbreviatedNumber /= 1000;
            suffixIndex++;
        }

        // Format string depends on the value
        string format;
        if (abbreviatedNumber >= 100)
            format = "0";
        else if (abbreviatedNumber >= 10)
            format = "0.#";
        else
            format = "0.##";

        return $"{abbreviatedNumber.ToString(format)}{suffixes[suffixIndex]}";
    }

    public long UnicellCost(string UnicellType)
    {
        switch (UnicellType)
        {
            case "Blue":
                return BlueUnicellCost;
            case "Pink":
                return PinkUnicellCost;
            case "Yellow":
                return YellowUnicellCost;
            case "Green":
                return GreenUnicellCost;
            case "Purple":
                return PurpleUnicellCost;
            case "Red":
                return RedUnicellCost;
            case "Orange":
                return OrangeUnicellCost;
            default:
                Debug.LogError("Unknown UnicellType for UnicellCost");
                return 0;
        }
    }

    public void DoubleCoins()
    {
        if (UniCoins < 1000000000000000000)
            UniCoins *= 2;
        else
            UniCoins = 2000000000000000000;
    }
}
