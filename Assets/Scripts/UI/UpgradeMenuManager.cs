using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject UpgradePanels;
    [SerializeField] private GameObject PurchasePanel;
    [SerializeField] private GameObject BioLabPanel;

    [SerializeField] private Button UpgradePanelButton;
    [SerializeField] private Button PurchaseUpgradeButton;

    [SerializeField] private TextMeshProUGUI InfoPanelTitle;
    [SerializeField] private TextMeshProUGUI InfoPanelDescription;
    [SerializeField] private TextMeshProUGUI InfoPanelPreview;
    [SerializeField] private TextMeshProUGUI InfoPanelCost;

    [SerializeField] private Color BlueUnicellColour;
    [SerializeField] private Color PinkUnicellColour;
    [SerializeField] private Color YellowUnicellColour;
    [SerializeField] private Color GreenUnicellColour;
    [SerializeField] private Color PurpleUnicellColour;
    [SerializeField] private Color RedUnicellColour;
    [SerializeField] private Color OrangeUnicellColour;

    [SerializeField] private Image InfoPanelCostIcon;

    [SerializeField] private Image WalletSoulIcon;
    [SerializeField] private TextMeshProUGUI SoulCount;

    [SerializeField] private Button UpgradeDamageButton;
    [SerializeField] private Button UpgradeMaxHPButton;
    [SerializeField] private Button UpgradeSpawnLvButton;
    [SerializeField] private Button UpgradeElderButton;
    [SerializeField] private Button UpgradeShinyButton;
    [SerializeField] private Button UpgradeKingButton;

    [SerializeField] private Button SelectBlueUnicellButton;
    [SerializeField] private Button SelectPinkUnicellButton;
    [SerializeField] private Button SelectYellowUnicellButton;
    [SerializeField] private Button SelectGreenUnicellButton;
    [SerializeField] private Button SelectPurpleUnicellButton;
    [SerializeField] private Button SelectRedUnicellButton;
    [SerializeField] private Button SelectOrangeUnicellButton;

    [SerializeField] private TextMeshProUGUI UpgradeDMGText;
    [SerializeField] private TextMeshProUGUI UpgradeMaxHPText;
    [SerializeField] private TextMeshProUGUI UpgradeSpawnLvText;
    [SerializeField] private TextMeshProUGUI UpgradeElderText;
    [SerializeField] private TextMeshProUGUI UpgradeShinyText;
    [SerializeField] private TextMeshProUGUI UpgradeKingText;

    /*[SerializeField] private Sprite BlueSoulIMG;
    [SerializeField] private Sprite PinkSoulIMG;
    [SerializeField] private Sprite YellowSoulIMG;
    [SerializeField] private Sprite GreenSoulIMG;
    [SerializeField] private Sprite PurpleSoulIMG;
    [SerializeField] private Sprite RedSoulIMG;
    [SerializeField] private Sprite OrangeSoulIMG;*/

    [SerializeField] private GameObject SoulIMG1;
    [SerializeField] private GameObject SoulIMG2;
    [SerializeField] private GameObject SoulIMG3;

    private enum UnicellSelection { Default, Blue, Pink, Yellow, Green, Purple, Red, Orange };
    private UnicellSelection SelectedUnicells;

    private enum UpgradeSelection { Default, Damage, MaxHealth, SpawnLv, Elder, Shiny, King};
    private UpgradeSelection SelectedUpgradeSelection;

    private float LogicTimer;
    private float LogicTimerThreshold;

    [SerializeField] private EntityManager entityManager;
    [SerializeField] private EconomyManager economyManager;
    [SerializeField] private PurchasePanelManager purchasePanelManager;
    [SerializeField] private BiolabMenuManager biolabMenuManager;

    void Start()
    {
        UpgradePanels.SetActive(false);
        //SelectedUnicells = UnicellSelection.Blue;
        PurchaseUpgradeButton.interactable = false;
        //UpgradePanel.GetComponent<Image>().color = Color.blue;

        SelectBlueUnicellButton.interactable = true;
        SelectPinkUnicellButton.interactable = true;
        SelectYellowUnicellButton.interactable = true;
        SelectGreenUnicellButton.interactable = true;
        SelectPurpleUnicellButton.interactable = true;
        SelectRedUnicellButton.interactable = true;
        SelectOrangeUnicellButton.interactable = true;

        SelectedUnicells = UnicellSelection.Default;
        SelectedUpgradeSelection = UpgradeSelection.Default;

        DisableUpgradeButtons();

        LogicTimerThreshold = 0.05f;
    }

    void Update()
    {
        LogicTimer += Time.deltaTime;
        if (LogicTimer > LogicTimerThreshold)
        {
            LogicTimer -= LogicTimerThreshold;
            LogicUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.U))
        {
            OnUpgradePanelsButtonClicked();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UpgradePanels.gameObject.activeSelf)
            {
                UpgradePanels.SetActive(false);
            }
        }
    }

    void LogicUpdate() 
    {
        if (UpgradePanels.gameObject.activeSelf)
        {
            switch (SelectedUnicells)
            {
                case UnicellSelection.Default:
                    SoulCount.text = "";
                    break;
                case UnicellSelection.Blue:
                    SoulCount.text = economyManager.BlueSouls.ToString();
                    break;
                case UnicellSelection.Pink:
                    SoulCount.text = economyManager.PinkSouls.ToString();
                    break;
                case UnicellSelection.Yellow:
                    SoulCount.text = economyManager.YellowSouls.ToString();
                    break;
                case UnicellSelection.Green:
                    SoulCount.text = economyManager.GreenSouls.ToString();
                    break;
                case UnicellSelection.Purple:
                    SoulCount.text = economyManager.PurpleSouls.ToString();
                    break;
                case UnicellSelection.Red:
                    SoulCount.text = economyManager.RedSouls.ToString();
                    break;
                case UnicellSelection.Orange:
                    SoulCount.text = economyManager.OrangeSouls.ToString();
                    break;
            }

            switch (SelectedUpgradeSelection)
            {
                case UpgradeSelection.Default:
                    PurchaseUpgradeButton.interactable = false;
                    break;
                case UpgradeSelection.Damage:
                    if (economyManager.UpgradeDMGPurchaseEligible(SelectedUnicells.ToString()) && SelectedUnicells != UnicellSelection.Default)
                    {
                        PurchaseUpgradeButton.interactable = true;
                    }
                    else
                    {
                        PurchaseUpgradeButton.interactable = false;
                    }
                    break;
                case UpgradeSelection.MaxHealth:
                    if (economyManager.UpgradeMaxHPPurchaseEligible(SelectedUnicells.ToString()) && SelectedUnicells != UnicellSelection.Default)
                    {
                        PurchaseUpgradeButton.interactable = true;
                    }
                    else
                    {
                        PurchaseUpgradeButton.interactable = false;
                    }
                    break;
                case UpgradeSelection.SpawnLv:
                    if (economyManager.UpgradeSpawnLvPurchaseEligible(SelectedUnicells.ToString()) && SelectedUnicells != UnicellSelection.Default)
                    {
                        PurchaseUpgradeButton.interactable = true;
                    }
                    else
                    {
                        PurchaseUpgradeButton.interactable = false;
                    }
                    break;
                case UpgradeSelection.Elder:
                    if (economyManager.UpgradeElderPurchaseEligible(SelectedUnicells.ToString()) && SelectedUnicells != UnicellSelection.Default)
                    {
                        PurchaseUpgradeButton.interactable = true;
                    }
                    else
                    {
                        PurchaseUpgradeButton.interactable = false;
                    }
                    break;
                case UpgradeSelection.Shiny:
                    if (economyManager.UpgradeShinyPurchaseEligible(SelectedUnicells.ToString()) && SelectedUnicells != UnicellSelection.Default)
                    {
                        PurchaseUpgradeButton.interactable = true;
                    }
                    else
                    {
                        PurchaseUpgradeButton.interactable = false;
                    }
                    break;
                case UpgradeSelection.King:
                    if (economyManager.UpgradeKingPurchaseEligible(SelectedUnicells.ToString()) && SelectedUnicells != UnicellSelection.Default)
                    {
                        PurchaseUpgradeButton.interactable = true;
                    }
                    else
                    {
                        PurchaseUpgradeButton.interactable = false;
                    }
                    break;
                default:
                    Debug.LogError("UpgradeMenuManager LogicUpdate() - Unknown Upgrade selection for purchase button interaction checker");
                    break;

            }

            if (SelectedUnicells == UnicellSelection.Default)
            {
                UpgradeDamageButton.interactable = false;
                UpgradeMaxHPButton.interactable = false;
                UpgradeSpawnLvButton.interactable = false;
            }

            UpdateInfoPanel();
        }
    }

    // Toggle Displays
    void DisableUpgradeButtons()
    {
        UpgradeDamageButton.interactable = false;
        UpgradeMaxHPButton.interactable = false;
        UpgradeSpawnLvButton.interactable = false;
        UpgradeElderButton.interactable = false;
        UpgradeShinyButton.interactable = false;
        UpgradeKingButton.interactable = false;
    }
    void EnableUpgradeButtons()
    {
        if (biolabMenuManager.isBioDamageUnlocked)
        {
            UpgradeDamageButton.interactable = true;
        }
        if (biolabMenuManager.isBioMaxHealthUnlocked)
        {
            UpgradeMaxHPButton.interactable = true;
        }
        if (biolabMenuManager.isBioSpawnLevelUnlocked)
        {
            UpgradeSpawnLvButton.interactable = true;
        }
        if (biolabMenuManager.isBioElderUnlocked)
        {
            UpgradeElderButton.interactable = true;
        }
        if (biolabMenuManager.isBioShinyUnlocked)
        {
            UpgradeShinyButton.interactable = true;
        }
        if (biolabMenuManager.isBioKingUnlocked)
        {
            UpgradeKingButton.interactable = true;
        }
    }

    // Text Updates
    void UpdateInfoPanel()
    {
        switch (SelectedUnicells)
        {
            case UnicellSelection.Default:
                InfoPanelCost.text = "";
                SoulCount.text = "";
                //SoulCount.color = BlueUnicellColour;
                break;
            case UnicellSelection.Blue:
                InfoPanelCost.text = economyManager.UpgradeBlueDMGCost.ToString();
                SoulCount.text = economyManager.BlueSouls.ToString();
                //SoulCount.color = BlueUnicellColour;

                if (economyManager.UpgradeDMGPurchaseEligible("Blue") || UpgradeDamageButton.interactable == false)
                {
                    UpgradeDamageButton.GetComponent<Image>().color = new Color(UpgradeDamageButton.GetComponent<Image>().color.r,
                        UpgradeDamageButton.GetComponent<Image>().color.g,
                        UpgradeDamageButton.GetComponent<Image>().color.b,
                        1.0f);
                }
                else
                {
                    UpgradeDamageButton.GetComponent<Image>().color = new Color(UpgradeDamageButton.GetComponent<Image>().color.r,
                        UpgradeDamageButton.GetComponent<Image>().color.g,
                        UpgradeDamageButton.GetComponent<Image>().color.b,
                        0.2f);
                }
                if (economyManager.UpgradeMaxHPPurchaseEligible("Blue") || UpgradeMaxHPButton.interactable == false)
                {
                    UpgradeMaxHPButton.GetComponent<Image>().color = new Color(UpgradeMaxHPButton.GetComponent<Image>().color.r,
                        UpgradeMaxHPButton.GetComponent<Image>().color.g,
                        UpgradeMaxHPButton.GetComponent<Image>().color.b,
                        1.0f);
                }
                else
                {
                    UpgradeMaxHPButton.GetComponent<Image>().color = new Color(UpgradeMaxHPButton.GetComponent<Image>().color.r,
                        UpgradeMaxHPButton.GetComponent<Image>().color.g,
                        UpgradeMaxHPButton.GetComponent<Image>().color.b,
                        0.2f);
                }
                if (economyManager.UpgradeSpawnLvPurchaseEligible("Blue") || UpgradeSpawnLvButton.interactable == false)
                {
                    UpgradeSpawnLvButton.GetComponent<Image>().color = new Color(UpgradeSpawnLvButton.GetComponent<Image>().color.r,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.g,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.b,
                        1.0f);
                }
                else
                {
                    UpgradeSpawnLvButton.GetComponent<Image>().color = new Color(UpgradeSpawnLvButton.GetComponent<Image>().color.r,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.g,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.b,
                        0.2f);
                }
                break;
            case UnicellSelection.Pink:
                InfoPanelCost.text = economyManager.UpgradePinkDMGCost.ToString();
                SoulCount.text = economyManager.PinkSouls.ToString();
                //SoulCount.color = PinkUnicellColour;

                if (economyManager.UpgradeDMGPurchaseEligible("Pink") || UpgradeDamageButton.interactable == false)
                {
                    UpgradeDamageButton.GetComponent<Image>().color = new Color(UpgradeDamageButton.GetComponent<Image>().color.r,
                        UpgradeDamageButton.GetComponent<Image>().color.g,
                        UpgradeDamageButton.GetComponent<Image>().color.b,
                        1.0f);
                }
                else
                {
                    UpgradeDamageButton.GetComponent<Image>().color = new Color(UpgradeDamageButton.GetComponent<Image>().color.r,
                        UpgradeDamageButton.GetComponent<Image>().color.g,
                        UpgradeDamageButton.GetComponent<Image>().color.b,
                        0.2f);
                }
                if (economyManager.UpgradeMaxHPPurchaseEligible("Pink") || UpgradeMaxHPButton.interactable == false)
                {
                    UpgradeMaxHPButton.GetComponent<Image>().color = new Color(UpgradeMaxHPButton.GetComponent<Image>().color.r,
                        UpgradeMaxHPButton.GetComponent<Image>().color.g,
                        UpgradeMaxHPButton.GetComponent<Image>().color.b,
                        1.0f);
                }
                else
                {
                    UpgradeMaxHPButton.GetComponent<Image>().color = new Color(UpgradeMaxHPButton.GetComponent<Image>().color.r,
                        UpgradeMaxHPButton.GetComponent<Image>().color.g,
                        UpgradeMaxHPButton.GetComponent<Image>().color.b,
                        0.2f); ;
                }
                if (economyManager.UpgradeSpawnLvPurchaseEligible("Pink") || UpgradeSpawnLvButton.interactable == false)
                {
                    UpgradeSpawnLvButton.GetComponent<Image>().color = new Color(UpgradeSpawnLvButton.GetComponent<Image>().color.r,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.g,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.b,
                        1.0f);
                }
                else
                {
                    UpgradeSpawnLvButton.GetComponent<Image>().color = new Color(UpgradeSpawnLvButton.GetComponent<Image>().color.r,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.g,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.b,
                        0.2f);
                }
                break;
            case UnicellSelection.Yellow:
                InfoPanelCost.text = economyManager.UpgradeYellowDMGCost.ToString();
                SoulCount.text = economyManager.YellowSouls.ToString();
                //SoulCount.color = YellowUnicellColour;

                if (economyManager.UpgradeDMGPurchaseEligible("Yellow") || UpgradeDamageButton.interactable == false)
                {
                    UpgradeDamageButton.GetComponent<Image>().color = new Color(UpgradeDamageButton.GetComponent<Image>().color.r,
                        UpgradeDamageButton.GetComponent<Image>().color.g,
                        UpgradeDamageButton.GetComponent<Image>().color.b,
                        1.0f);
                }
                else
                {
                    UpgradeDamageButton.GetComponent<Image>().color = new Color(UpgradeDamageButton.GetComponent<Image>().color.r,
                        UpgradeDamageButton.GetComponent<Image>().color.g,
                        UpgradeDamageButton.GetComponent<Image>().color.b,
                        0.2f);
                }
                if (economyManager.UpgradeMaxHPPurchaseEligible("Yellow") || UpgradeMaxHPButton.interactable == false)
                {
                    UpgradeMaxHPButton.GetComponent<Image>().color = new Color(UpgradeMaxHPButton.GetComponent<Image>().color.r,
                        UpgradeMaxHPButton.GetComponent<Image>().color.g,
                        UpgradeMaxHPButton.GetComponent<Image>().color.b,
                        1.0f);
                }
                else
                {
                    UpgradeMaxHPButton.GetComponent<Image>().color = new Color(UpgradeMaxHPButton.GetComponent<Image>().color.r,
                        UpgradeMaxHPButton.GetComponent<Image>().color.g,
                        UpgradeMaxHPButton.GetComponent<Image>().color.b,
                        0.2f);
                }
                if (economyManager.UpgradeSpawnLvPurchaseEligible("Yellow") || UpgradeSpawnLvButton.interactable == false)
                {
                    UpgradeSpawnLvButton.GetComponent<Image>().color = new Color(UpgradeSpawnLvButton.GetComponent<Image>().color.r,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.g,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.b,
                        1.0f);
                }
                else
                {
                    UpgradeSpawnLvButton.GetComponent<Image>().color = new Color(UpgradeSpawnLvButton.GetComponent<Image>().color.r,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.g,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.b,
                        0.2f);
                }
                break;
            case UnicellSelection.Green:
                InfoPanelCost.text = economyManager.UpgradeGreenDMGCost.ToString();
                SoulCount.text = economyManager.GreenSouls.ToString();
                //SoulCount.color = GreenUnicellColour;

                if (economyManager.UpgradeDMGPurchaseEligible("Green") || UpgradeDamageButton.interactable == false)
                {
                    UpgradeDamageButton.GetComponent<Image>().color = new Color(UpgradeDamageButton.GetComponent<Image>().color.r,
                        UpgradeDamageButton.GetComponent<Image>().color.g,
                        UpgradeDamageButton.GetComponent<Image>().color.b,
                        1.0f);
                }
                else
                {
                    UpgradeDamageButton.GetComponent<Image>().color = new Color(UpgradeDamageButton.GetComponent<Image>().color.r,
                        UpgradeDamageButton.GetComponent<Image>().color.g,
                        UpgradeDamageButton.GetComponent<Image>().color.b,
                        0.2f);
                }
                if (economyManager.UpgradeMaxHPPurchaseEligible("Green") || UpgradeMaxHPButton.interactable == false)
                {
                    UpgradeMaxHPButton.GetComponent<Image>().color = new Color(UpgradeMaxHPButton.GetComponent<Image>().color.r,
                        UpgradeMaxHPButton.GetComponent<Image>().color.g,
                        UpgradeMaxHPButton.GetComponent<Image>().color.b,
                        1.0f);
                }
                else
                {
                    UpgradeMaxHPButton.GetComponent<Image>().color = new Color(UpgradeMaxHPButton.GetComponent<Image>().color.r,
                        UpgradeMaxHPButton.GetComponent<Image>().color.g,
                        UpgradeMaxHPButton.GetComponent<Image>().color.b,
                        0.2f);
                }
                if (economyManager.UpgradeSpawnLvPurchaseEligible("Green") || UpgradeSpawnLvButton.interactable == false)
                {
                    UpgradeSpawnLvButton.GetComponent<Image>().color = new Color(UpgradeSpawnLvButton.GetComponent<Image>().color.r,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.g,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.b,
                        1.0f);
                }
                else
                {
                    UpgradeSpawnLvButton.GetComponent<Image>().color = new Color(UpgradeSpawnLvButton.GetComponent<Image>().color.r,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.g,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.b,
                        0.2f);
                }
                break;
            case UnicellSelection.Purple:
                InfoPanelCost.text = economyManager.UpgradePurpleDMGCost.ToString();
                SoulCount.text = economyManager.PurpleSouls.ToString();
                //SoulCount.color = PurpleUnicellColour;

                if (economyManager.UpgradeDMGPurchaseEligible("Purple") || UpgradeDamageButton.interactable == false)
                {
                    UpgradeDamageButton.GetComponent<Image>().color = new Color(UpgradeDamageButton.GetComponent<Image>().color.r,
                        UpgradeDamageButton.GetComponent<Image>().color.g,
                        UpgradeDamageButton.GetComponent<Image>().color.b,
                        1.0f);
                }
                else
                {
                    UpgradeDamageButton.GetComponent<Image>().color = new Color(UpgradeDamageButton.GetComponent<Image>().color.r,
                        UpgradeDamageButton.GetComponent<Image>().color.g,
                        UpgradeDamageButton.GetComponent<Image>().color.b,
                        0.2f);
                }
                if (economyManager.UpgradeMaxHPPurchaseEligible("Purple") || UpgradeMaxHPButton.interactable == false)
                {
                    UpgradeMaxHPButton.GetComponent<Image>().color = new Color(UpgradeMaxHPButton.GetComponent<Image>().color.r,
                        UpgradeMaxHPButton.GetComponent<Image>().color.g,
                        UpgradeMaxHPButton.GetComponent<Image>().color.b,
                        1.0f);
                }
                else
                {
                    UpgradeMaxHPButton.GetComponent<Image>().color = new Color(UpgradeMaxHPButton.GetComponent<Image>().color.r,
                        UpgradeMaxHPButton.GetComponent<Image>().color.g,
                        UpgradeMaxHPButton.GetComponent<Image>().color.b,
                        0.2f);
                }
                if (economyManager.UpgradeSpawnLvPurchaseEligible("Purple") || UpgradeSpawnLvButton.interactable == false)
                {
                    UpgradeSpawnLvButton.GetComponent<Image>().color = new Color(UpgradeSpawnLvButton.GetComponent<Image>().color.r,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.g,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.b,
                        1.0f);
                }
                else
                {
                    UpgradeSpawnLvButton.GetComponent<Image>().color = new Color(UpgradeSpawnLvButton.GetComponent<Image>().color.r,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.g,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.b,
                        0.2f);
                }
                break;
            case UnicellSelection.Red:
                InfoPanelCost.text = economyManager.UpgradeRedDMGCost.ToString();
                SoulCount.text = economyManager.RedSouls.ToString();
                //SoulCount.color = RedUnicellColour;
                break;
            case UnicellSelection.Orange:
                InfoPanelCost.text = economyManager.UpgradeOrangeDMGCost.ToString();
                SoulCount.text = economyManager.OrangeSouls.ToString();
                //SoulCount.color = OrangeUnicellColour;

                if (economyManager.UpgradeDMGPurchaseEligible("Orange") || UpgradeDamageButton.interactable == false)
                {
                    UpgradeDamageButton.GetComponent<Image>().color = new Color(UpgradeDamageButton.GetComponent<Image>().color.r,
                        UpgradeDamageButton.GetComponent<Image>().color.g,
                        UpgradeDamageButton.GetComponent<Image>().color.b,
                        1.0f);
                }
                else
                {
                    UpgradeDamageButton.GetComponent<Image>().color = new Color(UpgradeDamageButton.GetComponent<Image>().color.r,
                        UpgradeDamageButton.GetComponent<Image>().color.g,
                        UpgradeDamageButton.GetComponent<Image>().color.b,
                        0.2f);
                }
                if (economyManager.UpgradeMaxHPPurchaseEligible("Orange") || UpgradeMaxHPButton.interactable == false)
                {
                    UpgradeMaxHPButton.GetComponent<Image>().color = new Color(UpgradeMaxHPButton.GetComponent<Image>().color.r,
                        UpgradeMaxHPButton.GetComponent<Image>().color.g,
                        UpgradeMaxHPButton.GetComponent<Image>().color.b,
                        1.0f);
                }
                else
                {
                    UpgradeMaxHPButton.GetComponent<Image>().color = new Color(UpgradeMaxHPButton.GetComponent<Image>().color.r,
                        UpgradeMaxHPButton.GetComponent<Image>().color.g,
                        UpgradeMaxHPButton.GetComponent<Image>().color.b,
                        0.2f);
                }
                if (economyManager.UpgradeSpawnLvPurchaseEligible("Orange") || UpgradeSpawnLvButton.interactable == false)
                {
                    UpgradeSpawnLvButton.GetComponent<Image>().color = new Color(UpgradeSpawnLvButton.GetComponent<Image>().color.r,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.g,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.b,
                        1.0f);
                }
                else
                {
                    UpgradeSpawnLvButton.GetComponent<Image>().color = new Color(UpgradeSpawnLvButton.GetComponent<Image>().color.r,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.g,
                        UpgradeSpawnLvButton.GetComponent<Image>().color.b,
                        0.2f);
                }
                break;
        }

        switch (SelectedUpgradeSelection)
        {
            case UpgradeSelection.Default:
                InfoPanelTitle.text = "Select a Unicell";
                InfoPanelDescription.text = "";

                InfoPanelPreview.text = "";

                switch (SelectedUnicells)
                {
                    case UnicellSelection.Default:
                        InfoPanelCost.text = "";
                        SoulCount.text = "";
                        
                        break;
                    case UnicellSelection.Blue:
                        InfoPanelCost.text = economyManager.UpgradeBlueDMGCost.ToString();
                        SoulCount.text = economyManager.BlueSouls.ToString();

                        break;
                    case UnicellSelection.Pink:
                        InfoPanelCost.text = economyManager.UpgradePinkDMGCost.ToString();
                        SoulCount.text = economyManager.PinkSouls.ToString();
                        
                        break;
                    case UnicellSelection.Yellow:
                        InfoPanelCost.text = economyManager.UpgradeYellowDMGCost.ToString();
                        SoulCount.text = economyManager.YellowSouls.ToString();
                        
                        break;
                    case UnicellSelection.Green:
                        InfoPanelCost.text = economyManager.UpgradeGreenDMGCost.ToString();
                        SoulCount.text = economyManager.GreenSouls.ToString();
                        
                        break;
                    case UnicellSelection.Purple:
                        InfoPanelCost.text = economyManager.UpgradePurpleDMGCost.ToString();
                        SoulCount.text = economyManager.PurpleSouls.ToString();
                        
                        break;
                    case UnicellSelection.Red:
                        InfoPanelCost.text = economyManager.UpgradeRedDMGCost.ToString();
                        SoulCount.text = economyManager.RedSouls.ToString();

                        break;
                    case UnicellSelection.Orange:
                        InfoPanelCost.text = economyManager.UpgradeOrangeDMGCost.ToString();
                        SoulCount.text = economyManager.OrangeSouls.ToString();

                        break;
                }
                break;
            case UpgradeSelection.Damage:
                InfoPanelTitle.text = "Increase Damage";
                InfoPanelDescription.text = "Increase the Damage for Unicells of this type";

                InfoPanelPreview.text = 
                    "+" + 
                    ((Mathf.Round((entityManager.CurrentUpgradeValue("Damage", SelectedUnicells.ToString())) * 100)) - 100) +
                    "% -> +" +
                    ((Mathf.Round(((entityManager.CurrentUpgradeValue("Damage", SelectedUnicells.ToString())) * 100) * 1.2f)) - 100) +
                    "%";

                switch (SelectedUnicells)
                {
                    case UnicellSelection.Default:
                        UpgradeDamageButton.interactable = false;
                        break;
                    case UnicellSelection.Blue:
                        InfoPanelCost.text = economyManager.UpgradeBlueDMGCost.ToString();
                        SoulCount.text = economyManager.BlueSouls.ToString();
                        break;
                    case UnicellSelection.Pink:
                        InfoPanelCost.text = economyManager.UpgradePinkDMGCost.ToString();
                        SoulCount.text = economyManager.PinkSouls.ToString();
                        break;
                    case UnicellSelection.Yellow:
                        InfoPanelCost.text = economyManager.UpgradeYellowDMGCost.ToString();
                        SoulCount.text = economyManager.YellowSouls.ToString();
                        break;
                    case UnicellSelection.Green:
                        InfoPanelCost.text = economyManager.UpgradeGreenDMGCost.ToString();
                        SoulCount.text = economyManager.GreenSouls.ToString();
                        break;
                    case UnicellSelection.Purple:
                        InfoPanelCost.text = economyManager.UpgradePurpleDMGCost.ToString();
                        SoulCount.text = economyManager.PurpleSouls.ToString();
                        break;
                    case UnicellSelection.Red:
                        InfoPanelCost.text = economyManager.UpgradeRedDMGCost.ToString();
                        SoulCount.text = economyManager.RedSouls.ToString();
                        break;
                    case UnicellSelection.Orange:
                        InfoPanelCost.text = economyManager.UpgradeOrangeDMGCost.ToString();
                        SoulCount.text = economyManager.OrangeSouls.ToString();
                        break;
                }
                break;
            case UpgradeSelection.MaxHealth:
                InfoPanelTitle.text = "Increase Max Health";
                InfoPanelDescription.text = "Increase the Max Health for Unicells of this type";

                InfoPanelPreview.text =
                    "+" +
                    ((Mathf.Round((entityManager.CurrentUpgradeValue("MaxHealth", SelectedUnicells.ToString())) * 100)) - 100) +
                    "% -> +" +
                    ((Mathf.Round(((entityManager.CurrentUpgradeValue("MaxHealth", SelectedUnicells.ToString())) * 100) * 1.2f)) - 100) +
                    "%";

                switch (SelectedUnicells)
                {
                    case UnicellSelection.Default:
                        UpgradeMaxHPButton.interactable = false;
                        break;
                    case UnicellSelection.Blue:
                        InfoPanelCost.text = economyManager.UpgradeBlueMaxHPCost.ToString();
                        SoulCount.text = economyManager.BlueSouls.ToString();
                        break;
                    case UnicellSelection.Pink:
                        InfoPanelCost.text = economyManager.UpgradePinkMaxHPCost.ToString();
                        SoulCount.text = economyManager.PinkSouls.ToString();
                        break;
                    case UnicellSelection.Yellow:
                        InfoPanelCost.text = economyManager.UpgradeYellowMaxHPCost.ToString();
                        SoulCount.text = economyManager.YellowSouls.ToString();
                        break;
                    case UnicellSelection.Green:
                        InfoPanelCost.text = economyManager.UpgradeGreenMaxHPCost.ToString();
                        SoulCount.text = economyManager.GreenSouls.ToString();
                        break;
                    case UnicellSelection.Purple:
                        InfoPanelCost.text = economyManager.UpgradePurpleMaxHPCost.ToString();
                        SoulCount.text = economyManager.PurpleSouls.ToString();
                        break;
                    case UnicellSelection.Red:
                        InfoPanelCost.text = economyManager.UpgradeRedMaxHPCost.ToString();
                        SoulCount.text = economyManager.RedSouls.ToString();
                        break;
                    case UnicellSelection.Orange:
                        InfoPanelCost.text = economyManager.UpgradeOrangeMaxHPCost.ToString();
                        SoulCount.text = economyManager.OrangeSouls.ToString();
                        break;
                }
                break;
            case UpgradeSelection.SpawnLv:
                InfoPanelTitle.text = "Increase Spawn Level";
                InfoPanelDescription.text = "Increase the spawn level for Unicells of this type";

                InfoPanelPreview.text =
                            "Lv " + 
                            entityManager.CurrentUpgradeValue("SpawnLv", SelectedUnicells.ToString()) +
                            " -> Lv " +
                            (entityManager.CurrentUpgradeValue("SpawnLv", SelectedUnicells.ToString()) + 1);

                switch (SelectedUnicells)
                {
                    case UnicellSelection.Default:
                        UpgradeSpawnLvButton.GetComponent<Image>().color = new Color(UpgradeDamageButton.GetComponent<Image>().color.r,
                                UpgradeDamageButton.GetComponent<Image>().color.g,
                                UpgradeDamageButton.GetComponent<Image>().color.b,
                                0.2f);
                        break;
                    case UnicellSelection.Blue:
                        InfoPanelCost.text = economyManager.UpgradeBlueSpawnLvCost.ToString();
                        SoulCount.text = economyManager.BlueSouls.ToString();
                        break;
                    case UnicellSelection.Pink:
                        InfoPanelCost.text = economyManager.UpgradePinkSpawnLvCost.ToString();
                        SoulCount.text = economyManager.PinkSouls.ToString();
                        break;
                    case UnicellSelection.Yellow:
                        InfoPanelCost.text = economyManager.UpgradeYellowSpawnLvCost.ToString();
                        SoulCount.text = economyManager.YellowSouls.ToString();
                        break;
                    case UnicellSelection.Green:
                        InfoPanelCost.text = economyManager.UpgradeGreenSpawnLvCost.ToString();
                        SoulCount.text = economyManager.GreenSouls.ToString();
                        break;
                    case UnicellSelection.Purple:
                        InfoPanelCost.text = economyManager.UpgradePurpleSpawnLvCost.ToString();
                        SoulCount.text = economyManager.PurpleSouls.ToString();
                        break;
                    case UnicellSelection.Red:
                        InfoPanelCost.text = economyManager.UpgradeRedSpawnLvCost.ToString();
                        SoulCount.text = economyManager.RedSouls.ToString();
                        break;
                    case UnicellSelection.Orange:
                        InfoPanelCost.text = economyManager.UpgradeOrangeSpawnLvCost.ToString();
                        SoulCount.text = economyManager.OrangeSouls.ToString();
                        break;
                }
                break;
            case UpgradeSelection.Elder:
                InfoPanelTitle.text = "Increase Health Regen";
                InfoPanelDescription.text = "Increase the Health Regen for Unicells of this type";

                InfoPanelPreview.text =
                            "Lv " +
                            entityManager.CurrentUpgradeValue("Elder", SelectedUnicells.ToString()) +
                            " -> Lv " +
                            (entityManager.CurrentUpgradeValue("Elder", SelectedUnicells.ToString()) + 1);

                switch (SelectedUnicells)
                {
                    case UnicellSelection.Default:
                        UpgradeElderButton.interactable = false;
                        break;
                    case UnicellSelection.Blue:
                        InfoPanelCost.text = economyManager.UpgradeBlueElderCost.ToString();
                        SoulCount.text = economyManager.BlueSouls.ToString();
                        if (economyManager.UpgradeMaxHPPurchaseEligible("Blue"))
                        {
                            UpgradeElderButton.interactable = true;
                        }
                        else
                        {
                            UpgradeElderButton.interactable = false;
                        }
                        break;
                    case UnicellSelection.Pink:
                        InfoPanelCost.text = economyManager.UpgradePinkShinyCost.ToString();
                        SoulCount.text = economyManager.PinkSouls.ToString();
                        if (economyManager.UpgradeMaxHPPurchaseEligible("Pink"))
                        {
                            UpgradeElderButton.interactable = true;
                        }
                        else
                        {
                            UpgradeElderButton.interactable = false;
                        }
                        break;
                    case UnicellSelection.Yellow:
                        InfoPanelCost.text = economyManager.UpgradeYellowElderCost.ToString();
                        SoulCount.text = economyManager.YellowSouls.ToString();
                        if (economyManager.UpgradeMaxHPPurchaseEligible("Yellow"))
                        {
                            UpgradeElderButton.interactable = true;
                        }
                        else
                        {
                            UpgradeElderButton.interactable = false;
                        }
                        break;
                    case UnicellSelection.Green:
                        InfoPanelCost.text = economyManager.UpgradeGreenElderCost.ToString();
                        SoulCount.text = economyManager.GreenSouls.ToString();
                        if (economyManager.UpgradeMaxHPPurchaseEligible("Green"))
                        {
                            UpgradeElderButton.interactable = true;
                        }
                        else
                        {
                            UpgradeElderButton.interactable = false;
                        }
                        break;
                    case UnicellSelection.Purple:
                        InfoPanelCost.text = economyManager.UpgradePurpleElderCost.ToString();
                        SoulCount.text = economyManager.PurpleSouls.ToString();
                        if (economyManager.UpgradeMaxHPPurchaseEligible("Purple"))
                        {
                            UpgradeElderButton.interactable = true;
                        }
                        else
                        {
                            UpgradeElderButton.interactable = false;
                        }
                        break;
                    case UnicellSelection.Red:
                        InfoPanelCost.text = economyManager.UpgradeRedElderCost.ToString();
                        SoulCount.text = economyManager.RedSouls.ToString();
                        if (economyManager.UpgradeMaxHPPurchaseEligible("Red"))
                        {
                            UpgradeElderButton.interactable = true;
                        }
                        else
                        {
                            UpgradeElderButton.interactable = false;
                        }
                        break;
                    case UnicellSelection.Orange:
                        InfoPanelCost.text = economyManager.UpgradeOrangeElderCost.ToString();
                        SoulCount.text = economyManager.OrangeSouls.ToString();
                        if (economyManager.UpgradeMaxHPPurchaseEligible("Orange"))
                        {
                            UpgradeElderButton.interactable = true;
                        }
                        else
                        {
                            UpgradeElderButton.interactable = false;
                        }
                        break;
                }
                break;
            case UpgradeSelection.Shiny:
                InfoPanelTitle.text = "Increase Value";
                InfoPanelDescription.text = "Increase the Value of Unicells of this type";

                InfoPanelPreview.text =
                            "Lv " +
                            entityManager.CurrentUpgradeValue("Shiny", SelectedUnicells.ToString()) +
                            " -> Lv " +
                            (entityManager.CurrentUpgradeValue("Shiny", SelectedUnicells.ToString()) + 1);

                switch (SelectedUnicells)
                {
                    case UnicellSelection.Default:
                        UpgradeShinyButton.interactable = false;
                        break;
                    case UnicellSelection.Blue:
                        InfoPanelCost.text = economyManager.UpgradeBlueShinyCost.ToString();
                        SoulCount.text = economyManager.BlueSouls.ToString();
                        if (economyManager.UpgradeMaxHPPurchaseEligible("Blue"))
                        {
                            UpgradeShinyButton.interactable = true;
                        }
                        else
                        {
                            UpgradeShinyButton.interactable = false;
                        }
                        break;
                    case UnicellSelection.Pink:
                        InfoPanelCost.text = economyManager.UpgradePinkShinyCost.ToString();
                        SoulCount.text = economyManager.PinkSouls.ToString();
                        if (economyManager.UpgradeMaxHPPurchaseEligible("Pink"))
                        {
                            UpgradeShinyButton.interactable = true;
                        }
                        else
                        {
                            UpgradeShinyButton.interactable = false;
                        }
                        break;
                    case UnicellSelection.Yellow:
                        InfoPanelCost.text = economyManager.UpgradeYellowShinyCost.ToString();
                        SoulCount.text = economyManager.YellowSouls.ToString();
                        if (economyManager.UpgradeMaxHPPurchaseEligible("Yellow"))
                        {
                            UpgradeShinyButton.interactable = true;
                        }
                        else
                        {
                            UpgradeShinyButton.interactable = false;
                        }
                        break;
                    case UnicellSelection.Green:
                        InfoPanelCost.text = economyManager.UpgradeGreenShinyCost.ToString();
                        SoulCount.text = economyManager.GreenSouls.ToString();
                        if (economyManager.UpgradeMaxHPPurchaseEligible("Green"))
                        {
                            UpgradeShinyButton.interactable = true;
                        }
                        else
                        {
                            UpgradeShinyButton.interactable = false;
                        }
                        break;
                    case UnicellSelection.Purple:
                        InfoPanelCost.text = economyManager.UpgradePurpleShinyCost.ToString();
                        SoulCount.text = economyManager.PurpleSouls.ToString();
                        if (economyManager.UpgradeMaxHPPurchaseEligible("Purple"))
                        {
                            UpgradeShinyButton.interactable = true;
                        }
                        else
                        {
                            UpgradeShinyButton.interactable = false;
                        }
                        break;
                    case UnicellSelection.Red:
                        InfoPanelCost.text = economyManager.UpgradeRedShinyCost.ToString();
                        SoulCount.text = economyManager.RedSouls.ToString();
                        if (economyManager.UpgradeMaxHPPurchaseEligible("Red"))
                        {
                            UpgradeShinyButton.interactable = true;
                        }
                        else
                        {
                            UpgradeShinyButton.interactable = false;
                        }
                        break;
                    case UnicellSelection.Orange:
                        InfoPanelCost.text = economyManager.UpgradeOrangeShinyCost.ToString();
                        SoulCount.text = economyManager.OrangeSouls.ToString();
                        if (economyManager.UpgradeMaxHPPurchaseEligible("Orange"))
                        {
                            UpgradeShinyButton.interactable = true;
                        }
                        else
                        {
                            UpgradeShinyButton.interactable = false;
                        }
                        break;
                }
                break;
            case UpgradeSelection.King:
                InfoPanelTitle.text = "Increase Mutation";
                InfoPanelDescription.text = "Increase the Mutation of Unicells of this type";

                InfoPanelPreview.text =
                            "Lv " +
                            entityManager.CurrentUpgradeValue("King", SelectedUnicells.ToString()) +
                            " -> Lv " +
                            (entityManager.CurrentUpgradeValue("King", SelectedUnicells.ToString()) + 1);

                switch (SelectedUnicells)
                {
                    case UnicellSelection.Default:
                        UpgradeKingButton.interactable = false;
                        break;
                    case UnicellSelection.Blue:
                        InfoPanelCost.text = economyManager.UpgradeBlueKingCost.ToString();
                        SoulCount.text = economyManager.BlueSouls.ToString();
                        break;
                    case UnicellSelection.Pink:
                        InfoPanelCost.text = economyManager.UpgradePinkKingCost.ToString();
                        SoulCount.text = economyManager.PinkSouls.ToString();
                        break;
                    case UnicellSelection.Yellow:
                        InfoPanelCost.text = economyManager.UpgradeYellowKingCost.ToString();
                        SoulCount.text = economyManager.YellowSouls.ToString();
                        break;
                    case UnicellSelection.Green:
                        InfoPanelCost.text = economyManager.UpgradeGreenKingCost.ToString();
                        SoulCount.text = economyManager.GreenSouls.ToString();
                        break;
                    case UnicellSelection.Purple:
                        InfoPanelCost.text = economyManager.UpgradePurpleKingCost.ToString();
                        SoulCount.text = economyManager.PurpleSouls.ToString();
                        break;
                    case UnicellSelection.Red:
                        InfoPanelCost.text = economyManager.UpgradeRedKingCost.ToString();
                        SoulCount.text = economyManager.RedSouls.ToString();
                        break;
                    case UnicellSelection.Orange:
                        InfoPanelCost.text = economyManager.UpgradeOrangeKingCost.ToString();
                        SoulCount.text = economyManager.OrangeSouls.ToString();
                        break;
                }
                break;
            default:
                InfoPanelTitle.text = "Select an Upgrade";
                InfoPanelDescription.text = "No Upgrade Selected";
                InfoPanelPreview.text = "0 -> 0";
                InfoPanelCost.text = "0";
                break;
        }
    }

    // Button OnClicked() Events
    public void OnUpgradePanelsButtonClicked()
    {
        if (!UpgradePanels.gameObject.activeSelf)
        {
            UpgradePanels.SetActive(true);

            if (!SelectBlueUnicellButton.gameObject.activeInHierarchy && purchasePanelManager.isBlueUnicellsUnlocked)
            {
                SelectBlueUnicellButton.gameObject.SetActive(true);
            }
            if (!SelectPinkUnicellButton.gameObject.activeInHierarchy && purchasePanelManager.isPinkUnicellsUnlocked)
            {
                SelectPinkUnicellButton.gameObject.SetActive(true);
            }
            if (!SelectYellowUnicellButton.gameObject.activeInHierarchy && purchasePanelManager.isYellowUnicellsUnlocked)
            {
                SelectYellowUnicellButton.gameObject.SetActive(true);
            }
            if (!SelectGreenUnicellButton.gameObject.activeInHierarchy && purchasePanelManager.isGreenUnicellsUnlocked)
            {
                SelectGreenUnicellButton.gameObject.SetActive(true);
            }
            if (!SelectPurpleUnicellButton.gameObject.activeInHierarchy && purchasePanelManager.isPurpleUnicellsUnlocked)
            {
                SelectPurpleUnicellButton.gameObject.SetActive(true);
            }
            if (!SelectRedUnicellButton.gameObject.activeInHierarchy && purchasePanelManager.isRedUnicellsUnlocked)
            {
                SelectRedUnicellButton.gameObject.SetActive(true);
            }
            if (!SelectOrangeUnicellButton.gameObject.activeInHierarchy && purchasePanelManager.isOrangeUnicellsUnlocked)
            {
                SelectOrangeUnicellButton.gameObject.SetActive(true);
            }

            /*if (SelectBlueUnicellButton.interactable && SelectPinkUnicellButton.interactable)
            {
                OnSelectBlueUnicellsButtonClicked();
            }*/

            UpdateInfoPanel();

            if (BioLabPanel.gameObject.activeSelf)
            {
                BioLabPanel.SetActive(false);
            }
            if (PurchasePanel.gameObject.activeSelf)
            {
                PurchasePanel.SetActive(false);
            }

            if (biolabMenuManager.isBioDamageUnlocked && UpgradeDamageButton.interactable == false)
            {
                UpgradeDamageButton.interactable = true;
            }
            if (biolabMenuManager.isBioMaxHealthUnlocked && UpgradeMaxHPButton.interactable == false)
            {
                UpgradeMaxHPButton.interactable = true;
            }
            if (biolabMenuManager.isBioSpawnLevelUnlocked && UpgradeSpawnLvButton.interactable == false)
            {
                UpgradeSpawnLvButton.interactable = true;
            }
            if (biolabMenuManager.isBioElderUnlocked && UpgradeElderButton.interactable == false)
            {
                UpgradeElderButton.interactable = true;
            }
            if (biolabMenuManager.isBioShinyUnlocked && UpgradeShinyButton.interactable == false)
            {
                UpgradeShinyButton.interactable = true;
            }
            if (biolabMenuManager.isBioKingUnlocked && UpgradeKingButton.interactable == false)
            {
                UpgradeKingButton.interactable = true;
            }
        }
        else
        {
            UpgradePanels.SetActive(false);
        }
    }

    public void OnSelectBlueUnicellsButtonClicked()
    {
        SelectedUnicells = UnicellSelection.Blue;
        //UpgradePanel.GetComponent<Image>().color = Color.blue;

        SelectBlueUnicellButton.interactable = false;
        SelectPinkUnicellButton.interactable = true;
        SelectYellowUnicellButton.interactable = true;
        SelectGreenUnicellButton.interactable = true;
        SelectPurpleUnicellButton.interactable = true;
        SelectRedUnicellButton.interactable = true;
        SelectOrangeUnicellButton.interactable = true;

        EnableUpgradeButtons();

        InfoPanelCostIcon.color = BlueUnicellColour;
        WalletSoulIcon.color = BlueUnicellColour;

        SoulCount.text = economyManager.BlueSouls.ToString();

        //InfoPanelCost.color = BlueUnicellColour;
        InfoPanelPreview.color = BlueUnicellColour;

        UpdateInfoPanel();
    }
    public void OnSelectPinkUnicellsButtonClicked()
    {
        SelectedUnicells = UnicellSelection.Pink;
        //UpgradePanel.GetComponent<Image>().color = Color.magenta;

        SelectBlueUnicellButton.interactable = true;
        SelectPinkUnicellButton.interactable = false;
        SelectYellowUnicellButton.interactable = true;
        SelectGreenUnicellButton.interactable = true;
        SelectPurpleUnicellButton.interactable = true;
        SelectRedUnicellButton.interactable = true;
        SelectOrangeUnicellButton.interactable = true;

        EnableUpgradeButtons();
        InfoPanelCostIcon.color = PinkUnicellColour;
        WalletSoulIcon.color = PinkUnicellColour;

        SoulCount.text = economyManager.PinkSouls.ToString();

        //InfoPanelCost.color = PinkUnicellColour;
        InfoPanelPreview.color = PinkUnicellColour;
        UpdateInfoPanel();
    }
    public void OnSelectYellowUnicellsButtonClicked()
    {
        SelectedUnicells = UnicellSelection.Yellow;
        //UpgradePanel.GetComponent<Image>().color = Color.yellow;

        SelectBlueUnicellButton.interactable = true;
        SelectPinkUnicellButton.interactable = true;
        SelectYellowUnicellButton.interactable = false;
        SelectGreenUnicellButton.interactable = true;
        SelectPurpleUnicellButton.interactable = true;
        SelectRedUnicellButton.interactable = true;
        SelectOrangeUnicellButton.interactable = true;

        EnableUpgradeButtons();
        InfoPanelCostIcon.color = YellowUnicellColour;
        WalletSoulIcon.color = YellowUnicellColour;

        SoulCount.text = economyManager.YellowSouls.ToString();

        //InfoPanelCost.color = YellowUnicellColour;
        InfoPanelPreview.color = YellowUnicellColour;
        UpdateInfoPanel();
    }
    public void OnSelectGreenUnicellsButtonClicked()
    {
        SelectedUnicells = UnicellSelection.Green;
        //UpgradePanel.GetComponent<Image>().color = Color.yellow;

        SelectBlueUnicellButton.interactable = true;
        SelectPinkUnicellButton.interactable = true;
        SelectYellowUnicellButton.interactable = true;
        SelectGreenUnicellButton.interactable = false;
        SelectPurpleUnicellButton.interactable = true;
        SelectRedUnicellButton.interactable = true;
        SelectOrangeUnicellButton.interactable = true;

        EnableUpgradeButtons();
        InfoPanelCostIcon.color = GreenUnicellColour;
        WalletSoulIcon.color = GreenUnicellColour;

        SoulCount.text = economyManager.GreenSouls.ToString();

        //InfoPanelCost.color = GreenUnicellColour;
        InfoPanelPreview.color = GreenUnicellColour;
        UpdateInfoPanel();
    }
    public void OnSelectPurpleUnicellsButtonClicked()
    {
        SelectedUnicells = UnicellSelection.Purple;
        //UpgradePanel.GetComponent<Image>().color = Color.yellow;

        SelectBlueUnicellButton.interactable = true;
        SelectPinkUnicellButton.interactable = true;
        SelectYellowUnicellButton.interactable = true;
        SelectGreenUnicellButton.interactable = true;
        SelectPurpleUnicellButton.interactable = false;
        SelectRedUnicellButton.interactable = true;
        SelectOrangeUnicellButton.interactable = true;

        EnableUpgradeButtons();
        InfoPanelCostIcon.color = PurpleUnicellColour;
        WalletSoulIcon.color = PurpleUnicellColour;

        SoulCount.text = economyManager.PurpleSouls.ToString();

        //InfoPanelCost.color = PurpleUnicellColour;
        InfoPanelPreview.color = PurpleUnicellColour;
        UpdateInfoPanel();
    }
    public void OnSelectRedUnicellsButtonClicked()
    {
        SelectedUnicells = UnicellSelection.Red;
        //UpgradePanel.GetComponent<Image>().color = Color.yellow;

        SelectBlueUnicellButton.interactable = true;
        SelectPinkUnicellButton.interactable = true;
        SelectYellowUnicellButton.interactable = true;
        SelectGreenUnicellButton.interactable = true;
        SelectPurpleUnicellButton.interactable = true;
        SelectRedUnicellButton.interactable = false;
        SelectOrangeUnicellButton.interactable = true;

        EnableUpgradeButtons();
        InfoPanelCostIcon.color = RedUnicellColour;
        WalletSoulIcon.color = RedUnicellColour;

        SoulCount.text = economyManager.RedSouls.ToString();

        //InfoPanelCost.color = RedUnicellColour;
        InfoPanelPreview.color = RedUnicellColour;
        UpdateInfoPanel();
    }
    public void OnSelectOrangeUnicellsButtonClicked()
    {
        SelectedUnicells = UnicellSelection.Orange;
        //UpgradePanel.GetComponent<Image>().color = Color.yellow;

        SelectBlueUnicellButton.interactable = true;
        SelectPinkUnicellButton.interactable = true;
        SelectYellowUnicellButton.interactable = true;
        SelectGreenUnicellButton.interactable = true;
        SelectPurpleUnicellButton.interactable = true;
        SelectRedUnicellButton.interactable = true;
        SelectOrangeUnicellButton.interactable = false;

        EnableUpgradeButtons();
        InfoPanelCostIcon.color = OrangeUnicellColour;
        WalletSoulIcon.color = OrangeUnicellColour;

        SoulCount.text = economyManager.OrangeSouls.ToString();

        //InfoPanelCost.color = OrangeUnicellColour;
        InfoPanelPreview.color = OrangeUnicellColour;
        UpdateInfoPanel();
    }

    public void OnPurchaseUpgradeButtonClicked()
    {
        switch (SelectedUpgradeSelection)
        {
            case UpgradeSelection.Damage:
                if (economyManager.UpgradeDMGPurchaseEligible(SelectedUnicells.ToString()))
                {
                    economyManager.SpendDMGUpgrade(SelectedUnicells.ToString());
                }
                break;
            case UpgradeSelection.MaxHealth:
                if (economyManager.UpgradeMaxHPPurchaseEligible(SelectedUnicells.ToString()))
                {
                    economyManager.SpendMaxHPUpgrade(SelectedUnicells.ToString());
                }
                break;
            case UpgradeSelection.SpawnLv:
                if (economyManager.UpgradeSpawnLvPurchaseEligible(SelectedUnicells.ToString()))
                {
                    economyManager.SpendSpawnLvUpgrade(SelectedUnicells.ToString());
                }
                break;
            case UpgradeSelection.Elder:
                if (economyManager.UpgradeElderPurchaseEligible(SelectedUnicells.ToString()))
                {
                    economyManager.SpendElderUpgrade(SelectedUnicells.ToString());
                }
                break;
            case UpgradeSelection.Shiny:
                if (economyManager.UpgradeShinyPurchaseEligible(SelectedUnicells.ToString()))
                {
                    economyManager.SpendShinyUpgrade(SelectedUnicells.ToString());
                }
                break;
            case UpgradeSelection.King:
                if (economyManager.UpgradeKingPurchaseEligible(SelectedUnicells.ToString()))
                {
                    economyManager.SpendKingUpgrade(SelectedUnicells.ToString());
                }
                break;
        }

        UpdateInfoPanel();
    }

    public void OnUpgradeDMGButtonClicked()
    {
        SelectedUpgradeSelection = UpgradeSelection.Damage;

        if (economyManager.UpgradeDMGPurchaseEligible(SelectedUnicells.ToString()))
        {
            PurchaseUpgradeButton.interactable = true;
        }


        UpdateInfoPanel();
    }
    public void OnUpgradeMaxHPButtonClicked()
    {
        SelectedUpgradeSelection = UpgradeSelection.MaxHealth;

        if (economyManager.UpgradeMaxHPPurchaseEligible(SelectedUnicells.ToString()))
        {
            PurchaseUpgradeButton.interactable = true;
        }

        UpdateInfoPanel();
    }
    public void OnUpgradeSpawnLvButtonClicked()
    {
        SelectedUpgradeSelection = UpgradeSelection.SpawnLv;

        if (economyManager.UpgradeSpawnLvPurchaseEligible(SelectedUnicells.ToString()))
        {
            PurchaseUpgradeButton.interactable = true;
        }

        UpdateInfoPanel();
    }
    public void OnUpgradeElderButtonClicked()
    {
        SelectedUpgradeSelection = UpgradeSelection.Elder;

        if (economyManager.UpgradeElderPurchaseEligible(SelectedUnicells.ToString()))
        {
            PurchaseUpgradeButton.interactable = true;
        }

        UpdateInfoPanel();
    }
    public void OnUpgradeShinyButtonClicked()
    {
        SelectedUpgradeSelection = UpgradeSelection.Shiny;

        if (economyManager.UpgradeShinyPurchaseEligible(SelectedUnicells.ToString()))
        {
            PurchaseUpgradeButton.interactable = true;
        }

        UpdateInfoPanel();
    }
    public void OnUpgradeKingButtonClicked()
    {
        SelectedUpgradeSelection = UpgradeSelection.King;

        if (economyManager.UpgradeKingPurchaseEligible(SelectedUnicells.ToString()))
        {
            PurchaseUpgradeButton.interactable = true;
        }

        UpdateInfoPanel();
    }
}
