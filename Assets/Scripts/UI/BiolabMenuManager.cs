using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BiolabMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject BiolabPanel;
    [SerializeField] private GameObject UpgradePanel;
    [SerializeField] private GameObject PurchasePanel;

    [SerializeField] private Button BiolabButton;
    [SerializeField] private Button PurchaseBioUpgradeButton;

    [SerializeField] private TextMeshProUGUI InfoPanelTitle;
    [SerializeField] private TextMeshProUGUI InfoPanelDescription;
    [SerializeField] private TextMeshProUGUI InfoPanelPreview;
    [SerializeField] private TextMeshProUGUI InfoPanelCost;

    [SerializeField] private GameObject BlueSoulPanel;
    [SerializeField] private GameObject PinkSoulPanel;
    [SerializeField] private GameObject YellowSoulPanel;
    [SerializeField] private GameObject GreenSoulPanel;
    [SerializeField] private GameObject PurpleSoulPanel;
    [SerializeField] private GameObject RedSoulPanel;
    [SerializeField] private GameObject OrangeSoulPanel;
    [SerializeField] private GameObject RainbowSoulPanel;

    [SerializeField] private TextMeshProUGUI BlueSoulCount;
    [SerializeField] private TextMeshProUGUI PinkSoulCount;
    [SerializeField] private TextMeshProUGUI YellowSoulCount;
    [SerializeField] private TextMeshProUGUI GreenSoulCount;
    [SerializeField] private TextMeshProUGUI PurpleSoulCount;
    [SerializeField] private TextMeshProUGUI RedSoulCount;
    [SerializeField] private TextMeshProUGUI OrangeSoulCount;
    [SerializeField] private TextMeshProUGUI RainbowSoulCount;

    [SerializeField] private GameObject BlueSoulIcon;
    [SerializeField] private GameObject PinkSoulIcon;
    [SerializeField] private GameObject YellowSoulIcon;
    [SerializeField] private GameObject GreenSoulIcon;
    [SerializeField] private GameObject PurpleSoulIcon;
    [SerializeField] private GameObject RedSoulIcon;
    [SerializeField] private GameObject OrangeSoulIcon;
    [SerializeField] private GameObject RainbowSoulIcon;

    // Bio Upgrades
    [SerializeField] private Button SelectDamageMultiplierButton;
    [SerializeField] private Button SelectMaxHealthMultiplierButton;
    [SerializeField] private Button SelectSpawnLevelIncrementerEvo;
    [SerializeField] private Button SelectElderMultiplierButton;
    [SerializeField] private Button SelectShinyMultiplierButton;
    [SerializeField] private Button SelectKingMultiplierButton;

    /*[SerializeField] private Button SelectSoulAutocollectButton;
    [SerializeField] private Button SelectElderUnicellsButton;
    [SerializeField] private Button SelectCorruptUnicellsButton;
    [SerializeField] private Button SelectKingUnicellsButton;*/

    [SerializeField] private Button SelectRainbowCellularButton;
    [SerializeField] private Button SelectMulticellularButton;

    // Bio Lab variables
    public bool isBioDamageUnlocked;
    public bool isBioMaxHealthUnlocked;
    public bool isBioSpawnLevelUnlocked;
    public bool isBioElderUnlocked;
    public bool isBioShinyUnlocked;
    public bool isBioKingUnlocked;
    //


    // Enums
    private enum BioUpgradeSelection { Default, Damage, MaxHealth, SpawnLv, Elder, Shiny, King, Rainbow, Multicellular };
    private BioUpgradeSelection SelectedBioUpgradeSelection;
    //


    // Logic Variables
    private float LogicTimer;
    private float LogicTimerThreshold;
    //


    // Class References
    [SerializeField] private EntityManager entityManager;
    [SerializeField] private EconomyManager economyManager;
    [SerializeField] private PurchasePanelManager purchasePanelManager;
    //

    void Start()
    {
        LogicTimerThreshold = 0.05f;

        isBioDamageUnlocked = false;
        isBioMaxHealthUnlocked = false;
        isBioSpawnLevelUnlocked = false;
        isBioElderUnlocked = false;
        isBioShinyUnlocked = false;
        isBioKingUnlocked = false;

        SelectedBioUpgradeSelection = BioUpgradeSelection.Default;

        BlueSoulIcon.SetActive(false);
        BlueSoulPanel.SetActive(false);

        PinkSoulIcon.SetActive(false);
        PinkSoulPanel.SetActive(false);

        YellowSoulIcon.SetActive(false);
        YellowSoulPanel.SetActive(false);

        GreenSoulIcon.SetActive(false);
        GreenSoulPanel.SetActive(false);

        PurpleSoulIcon.SetActive(false);
        PurpleSoulPanel.SetActive(false);

        RedSoulIcon.SetActive(false);
        RedSoulPanel.SetActive(false);

        OrangeSoulIcon.SetActive(false);
        OrangeSoulPanel.SetActive(false);

        RainbowSoulIcon.SetActive(false);
        RainbowSoulPanel.SetActive(false);
    }

    void Update()
    {
        LogicTimer += Time.deltaTime;
        if (LogicTimer > LogicTimerThreshold)
        {
            LogicTimer -= LogicTimerThreshold;
            LogicUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.B))
        {
            OnBioLabButtonClicked();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (BiolabPanel.gameObject.activeSelf)
            {
                BiolabPanel.SetActive(false);
            }
        }
    }

    void LogicUpdate()
    {
        if (BiolabPanel.activeSelf)
        {
            if (BlueSoulPanel.activeSelf)
            {
                BlueSoulCount.text = economyManager.BlueSouls.ToString();
            }
            if (PinkSoulPanel.activeSelf)
            {
                PinkSoulCount.text = economyManager.PinkSouls.ToString();
            }
            if (YellowSoulPanel.activeSelf)
            {
                YellowSoulCount.text = economyManager.YellowSouls.ToString();
            }
            if (GreenSoulPanel.activeSelf)
            {
                GreenSoulCount.text = economyManager.GreenSouls.ToString();
            }
            if (PurpleSoulPanel.activeSelf)
            {
                PurpleSoulCount.text = economyManager.PurpleSouls.ToString();
            }
            if (RedSoulPanel.activeSelf)
            {
                RedSoulCount.text = economyManager.RedSouls.ToString();
            }
            if (OrangeSoulPanel.activeSelf)
            {
                OrangeSoulCount.text = economyManager.OrangeSouls.ToString();
            }
            /*if (RainbowSoulPanel.activeSelf)
            {
                RainbowSoulCount.text = economyManager.RainbowSouls.ToString();
            }*/
        }

        UpdateInfoPanel();
    }

    public void OnBioLabButtonClicked()
    {
        if (!BiolabPanel.gameObject.activeSelf)
        {
            BiolabPanel.SetActive(true);
            LogicUpdate();

            //UpdateInfoPanel();

            if (UpgradePanel.gameObject.activeSelf)
            {
                UpgradePanel.SetActive(false);
            }
            if (PurchasePanel.gameObject.activeSelf)
            {
                PurchasePanel.SetActive(false);
            }
        }
        else
        {
            BiolabPanel.SetActive(false);
        }
    }

    public void OnPurchaseBioUpgradeButtonClicked()
    {
        switch (SelectedBioUpgradeSelection)
        {
            case BioUpgradeSelection.Default:
                break;
            case BioUpgradeSelection.Damage:
                if (economyManager.BioUpgradeDMGPurchaseEligible())
                {
                    economyManager.SpendDMGBioUpgrade();
                    isBioDamageUnlocked = true;
                    SelectDamageMultiplierButton.interactable = false;
                }
                break;
            case BioUpgradeSelection.MaxHealth:
                if (economyManager.BioUpgradeMaxHPPurchaseEligible())
                {
                    economyManager.SpendMaxHPBioUpgrade();
                    isBioMaxHealthUnlocked = true;
                    SelectMaxHealthMultiplierButton.interactable = false;
                }
                break;
            case BioUpgradeSelection.SpawnLv:
                if (economyManager.BioUpgradeSpawnLvPurchaseEligible())
                {
                    economyManager.SpendSpawnLvBioUpgrade();
                    isBioSpawnLevelUnlocked = true;
                    SelectSpawnLevelIncrementerEvo.interactable = false;
                }
                break;
            case BioUpgradeSelection.Elder:
                if (economyManager.BioUpgradeElderPurchaseEligible())
                {
                    economyManager.SpendElderBioUpgrade();
                    isBioElderUnlocked = true;
                    SelectElderMultiplierButton.interactable = false;
                }
                break;
            case BioUpgradeSelection.Shiny:
                if (economyManager.BioUpgradeShinyPurchaseEligible())
                {
                    economyManager.SpendShinyBioUpgrade();
                    isBioShinyUnlocked = true;
                    SelectShinyMultiplierButton.interactable = false;
                }
                break;
            case BioUpgradeSelection.King:
                if (economyManager.BioUpgradeKingPurchaseEligible())
                {
                    economyManager.SpendKingBioUpgrade();
                    isBioKingUnlocked = true;
                    SelectKingMultiplierButton.interactable = false;
                }
                break;
        }

        UpdateInfoPanel();
    }

    void UpdateInfoPanel()
    {
        switch (SelectedBioUpgradeSelection)
        {
            case BioUpgradeSelection.Default:
                InfoPanelTitle.text = "Select a Research";
                InfoPanelDescription.text = "";

                /*InfoPanelPreview.text =
                    "Levels " +
                    (BioDamageIndex + 10) +
                    " -> " +
                    (BioDamageIndex + 20) +
                    "";*/
                InfoPanelPreview.text = "";
                PurchaseBioUpgradeButton.interactable = false;

                InfoPanelCost.text = "";
                break;
            case BioUpgradeSelection.Damage:
                InfoPanelTitle.text = "Damage Upgrading";
                InfoPanelDescription.text = "Unlock the Damage Upgrade for all Unicell colours";

                /*InfoPanelPreview.text =
                    "Levels " +
                    (BioDamageIndex + 10) +
                    " -> " +
                    (BioDamageIndex + 20) +
                    "";*/
                if (!isBioDamageUnlocked)
                {
                    InfoPanelPreview.text = "Locked -> Unlocked";
                    PurchaseBioUpgradeButton.interactable = true;
                }
                else
                {
                    InfoPanelPreview.text = "Unlocked!";
                    PurchaseBioUpgradeButton.interactable = false;
                }

                InfoPanelCost.text = economyManager.BioUpgradeEvoCost.ToString();
                break;
            case BioUpgradeSelection.MaxHealth:
                InfoPanelTitle.text = "Max Health Upgrading";
                InfoPanelDescription.text = "Unlock the Max Health Upgrade for all Unicell colours";

                /*InfoPanelPreview.text =
                    "Levels " +
                    (BioMaxHealthIndex + 10) +
                    " -> " +
                    (BioMaxHealthIndex + 20) +
                    "";*/

                if (!isBioMaxHealthUnlocked)
                {
                    InfoPanelPreview.text = "Locked -> Unlocked";
                    PurchaseBioUpgradeButton.interactable = true;
                }
                else
                {
                    InfoPanelPreview.text = "Unlocked!";
                    PurchaseBioUpgradeButton.interactable = false;
                }

                InfoPanelCost.text = economyManager.BioUpgradeEvoCost.ToString();
                break;
            case BioUpgradeSelection.SpawnLv:
                InfoPanelTitle.text = "Spawn Level Upgrading";
                InfoPanelDescription.text = "Unlock the Spawn Level Upgrade for all Unicell colours";

                /*InfoPanelPreview.text =
                    "Levels " +
                    (BioSpawnLevelIndex + 10) +
                    " -> " +
                    (BioSpawnLevelIndex + 20) +
                    "";*/

                if (!isBioSpawnLevelUnlocked)
                {
                    InfoPanelPreview.text = "Locked -> Unlocked";
                    PurchaseBioUpgradeButton.interactable = true;
                }
                else
                {
                    InfoPanelPreview.text = "Unlocked!";
                    PurchaseBioUpgradeButton.interactable = false;
                }

                InfoPanelCost.text = economyManager.BioUpgradeEvoCost.ToString();
                break;
            case BioUpgradeSelection.Elder:
                InfoPanelTitle.text = "Elder Unicells";
                InfoPanelDescription.text = "Unlock Elder Unicells for all Unicell colours";

                /*InfoPanelPreview.text =
                    "Levels " +
                    (BioSpawnLevelIndex + 10) +
                    " -> " +
                    (BioSpawnLevelIndex + 20) +
                    "";*/

                if (!isBioElderUnlocked)
                {
                    InfoPanelPreview.text = "Locked -> Unlocked";
                    PurchaseBioUpgradeButton.interactable = true;
                }
                else
                {
                    InfoPanelPreview.text = "Unlocked!";
                    PurchaseBioUpgradeButton.interactable = false;
                }

                InfoPanelCost.text = economyManager.BioUpgradeEvoCost.ToString();
                break;
            case BioUpgradeSelection.Shiny:
                InfoPanelTitle.text = "Shiny Unicells";
                InfoPanelDescription.text = "Unlock Shiny Unicells for all Unicell colours";

                /*InfoPanelPreview.text =
                    "Levels " +
                    (BioSpawnLevelIndex + 10) +
                    " -> " +
                    (BioSpawnLevelIndex + 20) +
                    "";*/

                if (!isBioShinyUnlocked)
                {
                    InfoPanelPreview.text = "Locked -> Unlocked";
                    PurchaseBioUpgradeButton.interactable = true;
                }
                else
                {
                    InfoPanelPreview.text = "Unlocked!";
                    PurchaseBioUpgradeButton.interactable = false;
                }

                InfoPanelCost.text = economyManager.BioUpgradeEvoCost.ToString();
                break;
            case BioUpgradeSelection.King:
                InfoPanelTitle.text = "King Unicells";
                InfoPanelDescription.text = "Unlock King Unicells for all Unicell colours";

                /*InfoPanelPreview.text =
                    "Levels " +
                    (BioSpawnLevelIndex + 10) +
                    " -> " +
                    (BioSpawnLevelIndex + 20) +
                    "";*/

                if (!isBioKingUnlocked)
                {
                    InfoPanelPreview.text = "Locked -> Unlocked";
                    PurchaseBioUpgradeButton.interactable = true;
                }
                else
                {
                    InfoPanelPreview.text = "Unlocked!";
                    PurchaseBioUpgradeButton.interactable = false;
                }

                InfoPanelCost.text = economyManager.BioUpgradeEvoCost.ToString();
                break;
            default:
                InfoPanelTitle.text = "Select a Bio Upgrade";
                InfoPanelDescription.text = "No Bio Upgrade Selected";
                InfoPanelPreview.text = "0 -> 0";
                InfoPanelCost.text = "0";
                break;
        }
    }

    // Tier 1 Bio Upgrades
    public void OnBioUpgradeDMGButtonClicked()
    {
        SelectedBioUpgradeSelection = BioUpgradeSelection.Damage;

        BlueSoulIcon.SetActive(true);
        BlueSoulPanel.SetActive(true);

        PinkSoulIcon.SetActive(true);
        PinkSoulPanel.SetActive(true);

        YellowSoulIcon.SetActive(false);
        YellowSoulPanel.SetActive(false);

        GreenSoulIcon.SetActive(false);
        GreenSoulPanel.SetActive(false);

        PurpleSoulIcon.SetActive(false);
        PurpleSoulPanel.SetActive(false);

        RedSoulIcon.SetActive(false);
        RedSoulPanel.SetActive(false);

        OrangeSoulIcon.SetActive(false);
        OrangeSoulPanel.SetActive(false);

        RainbowSoulIcon.SetActive(false);
        RainbowSoulPanel.SetActive(false);
    }
    public void OnBioUpgradeMaxHPButtonClicked()
    {
        SelectedBioUpgradeSelection = BioUpgradeSelection.MaxHealth;

        BlueSoulIcon.SetActive(true);
        BlueSoulPanel.SetActive(true);

        PinkSoulIcon.SetActive(false);
        PinkSoulPanel.SetActive(false);

        YellowSoulIcon.SetActive(true);
        YellowSoulPanel.SetActive(true);

        GreenSoulIcon.SetActive(false);
        GreenSoulPanel.SetActive(false);

        PurpleSoulIcon.SetActive(false);
        PurpleSoulPanel.SetActive(false);

        RedSoulIcon.SetActive(false);
        RedSoulPanel.SetActive(false);

        OrangeSoulIcon.SetActive(false);
        OrangeSoulPanel.SetActive(false);

        RainbowSoulIcon.SetActive(false);
        RainbowSoulPanel.SetActive(false);
    }
    public void OnBioUpgradeSpawnLvButtonClicked()
    {
        SelectedBioUpgradeSelection = BioUpgradeSelection.SpawnLv;

        BlueSoulIcon.SetActive(false);
        BlueSoulPanel.SetActive(false);

        PinkSoulIcon.SetActive(true);
        PinkSoulPanel.SetActive(true);

        YellowSoulIcon.SetActive(true);
        YellowSoulPanel.SetActive(true);

        GreenSoulIcon.SetActive(false);
        GreenSoulPanel.SetActive(false);

        PurpleSoulIcon.SetActive(false);
        PurpleSoulPanel.SetActive(false);

        RedSoulIcon.SetActive(false);
        RedSoulPanel.SetActive(false);

        OrangeSoulIcon.SetActive(false);
        OrangeSoulPanel.SetActive(false);

        RainbowSoulIcon.SetActive(false);
        RainbowSoulPanel.SetActive(false);
    }
    public void OnBioUpgradeElderButtonClicked()
    {
        SelectedBioUpgradeSelection = BioUpgradeSelection.Elder;

        BlueSoulIcon.SetActive(true);
        BlueSoulPanel.SetActive(true);

        PinkSoulIcon.SetActive(true);
        PinkSoulPanel.SetActive(true);

        YellowSoulIcon.SetActive(true);
        YellowSoulPanel.SetActive(true);

        GreenSoulIcon.SetActive(true);
        GreenSoulPanel.SetActive(true);

        PurpleSoulIcon.SetActive(false);
        PurpleSoulPanel.SetActive(false);

        RedSoulIcon.SetActive(false);
        RedSoulPanel.SetActive(false);

        OrangeSoulIcon.SetActive(false);
        OrangeSoulPanel.SetActive(false);

        RainbowSoulIcon.SetActive(false);
        RainbowSoulPanel.SetActive(false);
    }
    public void OnBioUpgradeShinyButtonClicked()
    {
        SelectedBioUpgradeSelection = BioUpgradeSelection.Shiny;

        BlueSoulIcon.SetActive(true);
        BlueSoulPanel.SetActive(true);

        PinkSoulIcon.SetActive(true);
        PinkSoulPanel.SetActive(true);

        YellowSoulIcon.SetActive(true);
        YellowSoulPanel.SetActive(true);

        GreenSoulIcon.SetActive(false);
        GreenSoulPanel.SetActive(false);

        PurpleSoulIcon.SetActive(true);
        PurpleSoulPanel.SetActive(true);

        RedSoulIcon.SetActive(false);
        RedSoulPanel.SetActive(false);

        OrangeSoulIcon.SetActive(false);
        OrangeSoulPanel.SetActive(false);

        RainbowSoulIcon.SetActive(false);
        RainbowSoulPanel.SetActive(false);
    }
    public void OnBioUpgradeKingButtonClicked()
    {
        SelectedBioUpgradeSelection = BioUpgradeSelection.King;

        BlueSoulIcon.SetActive(true);
        BlueSoulPanel.SetActive(true);

        PinkSoulIcon.SetActive(true);
        PinkSoulPanel.SetActive(true);

        YellowSoulIcon.SetActive(true);
        YellowSoulPanel.SetActive(true);

        GreenSoulIcon.SetActive(false);
        GreenSoulPanel.SetActive(false);

        PurpleSoulIcon.SetActive(false);
        PurpleSoulPanel.SetActive(false);

        RedSoulIcon.SetActive(true);
        RedSoulPanel.SetActive(true);

        OrangeSoulIcon.SetActive(false);
        OrangeSoulPanel.SetActive(false);

        RainbowSoulIcon.SetActive(false);
        RainbowSoulPanel.SetActive(false);
    }

    // Tier 2 Bio Upgrades
    /*public void OnBioUpgradeSoulAutocollectButtonClicked()
    {
        BlueSoulIcon.SetActive(true);
        BlueSoulPanel.SetActive(true);

        PinkSoulIcon.SetActive(true);
        PinkSoulPanel.SetActive(true);

        YellowSoulIcon.SetActive(true);
        YellowSoulPanel.SetActive(true);

        GreenSoulIcon.SetActive(false);
        GreenSoulPanel.SetActive(false);

        PurpleSoulIcon.SetActive(false);
        PurpleSoulPanel.SetActive(false);

        RedSoulIcon.SetActive(false);
        RedSoulPanel.SetActive(false);

        OrangeSoulIcon.SetActive(false);
        OrangeSoulPanel.SetActive(false);

        RainbowSoulIcon.SetActive(false);
        RainbowSoulPanel.SetActive(false);
    }
    public void OnBioUpgradeElderUnicellsButtonClicked()
    {
        BlueSoulIcon.SetActive(true);
        BlueSoulPanel.SetActive(true);

        PinkSoulIcon.SetActive(true);
        PinkSoulPanel.SetActive(true);

        YellowSoulIcon.SetActive(false);
        YellowSoulPanel.SetActive(false);

        GreenSoulIcon.SetActive(true);
        GreenSoulPanel.SetActive(true);

        PurpleSoulIcon.SetActive(false);
        PurpleSoulPanel.SetActive(false);

        RedSoulIcon.SetActive(false);
        RedSoulPanel.SetActive(false);

        OrangeSoulIcon.SetActive(false);
        OrangeSoulPanel.SetActive(false);

        RainbowSoulIcon.SetActive(false);
        RainbowSoulPanel.SetActive(false);
    }
    public void OnBioUpgradeShinyUnicellsButtonClicked()
    {
        BlueSoulIcon.SetActive(true);
        BlueSoulPanel.SetActive(true);

        PinkSoulIcon.SetActive(false);
        PinkSoulPanel.SetActive(false);

        YellowSoulIcon.SetActive(true);
        YellowSoulPanel.SetActive(true);

        GreenSoulIcon.SetActive(true);
        GreenSoulPanel.SetActive(true);

        PurpleSoulIcon.SetActive(false);
        PurpleSoulPanel.SetActive(false);

        RedSoulIcon.SetActive(false);
        RedSoulPanel.SetActive(false);

        OrangeSoulIcon.SetActive(false);
        OrangeSoulPanel.SetActive(false);

        RainbowSoulIcon.SetActive(false);
        RainbowSoulPanel.SetActive(false);
    }
    public void OnBioUpgradeKingUnicellsButtonClicked()
    {
        BlueSoulIcon.SetActive(false);
        BlueSoulPanel.SetActive(false);

        PinkSoulIcon.SetActive(true);
        PinkSoulPanel.SetActive(true);

        YellowSoulIcon.SetActive(true);
        YellowSoulPanel.SetActive(true);

        GreenSoulIcon.SetActive(true);
        GreenSoulPanel.SetActive(true);

        PurpleSoulIcon.SetActive(false);
        PurpleSoulPanel.SetActive(false);

        RedSoulIcon.SetActive(false);
        RedSoulPanel.SetActive(false);

        OrangeSoulIcon.SetActive(false);
        OrangeSoulPanel.SetActive(false);

        RainbowSoulIcon.SetActive(false);
        RainbowSoulPanel.SetActive(false);
    }*/

    // Tier 3 Bio Upgrades
    public void OnBioUpgradeRainbowCellularButtonClicked()
    {
        BlueSoulIcon.SetActive(true);
        BlueSoulPanel.SetActive(true);

        PinkSoulIcon.SetActive(true);
        PinkSoulPanel.SetActive(true);

        YellowSoulIcon.SetActive(true);
        YellowSoulPanel.SetActive(true);

        GreenSoulIcon.SetActive(true);
        GreenSoulPanel.SetActive(true);

        PurpleSoulIcon.SetActive(true);
        PurpleSoulPanel.SetActive(true);

        RedSoulIcon.SetActive(true);
        RedSoulPanel.SetActive(true);

        OrangeSoulIcon.SetActive(true);
        OrangeSoulPanel.SetActive(true);

        RainbowSoulIcon.SetActive(false);
        RainbowSoulPanel.SetActive(false);
    }
    public void OnBioUpgradeMulticellularButtonClicked()
    {
        BlueSoulIcon.SetActive(true);
        BlueSoulPanel.SetActive(true);

        PinkSoulIcon.SetActive(true);
        PinkSoulPanel.SetActive(true);

        YellowSoulIcon.SetActive(true);
        YellowSoulPanel.SetActive(true);

        GreenSoulIcon.SetActive(true);
        GreenSoulPanel.SetActive(true);

        PurpleSoulIcon.SetActive(true);
        PurpleSoulPanel.SetActive(true);

        RedSoulIcon.SetActive(true);
        RedSoulPanel.SetActive(true);

        OrangeSoulIcon.SetActive(true);
        OrangeSoulPanel.SetActive(true);

        RainbowSoulIcon.SetActive(true);
        RainbowSoulPanel.SetActive(true);
    }
}
