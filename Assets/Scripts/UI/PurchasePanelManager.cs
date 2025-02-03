using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PurchasePanelManager : MonoBehaviour
{
    [SerializeField] private GameObject BiolabPanel;
    [SerializeField] private GameObject PurchasePanel;
    [SerializeField] private GameObject UpgradePanels;

    [SerializeField] private Button PurchasePanelButton;
    [SerializeField] private Sprite Padlock;

    [SerializeField] private Button BlueUnicellPurchaseButton;
    [SerializeField] private TextMeshProUGUI BlueUnicellPurchaseCost;
    [SerializeField] private Sprite BlueUnicellTexture;
    [SerializeField] private Image BlueUnicell;
    [SerializeField] private Image BlueUnicellBackground;

    [SerializeField] private Button PinkUnicellPurchaseButton;
    [SerializeField] private TextMeshProUGUI PinkUnicellPurchaseCost;
    [SerializeField] private Sprite PinkUnicellTexture;
    [SerializeField] private Image PinkUnicell;
    [SerializeField] private Image PinkUnicellBackground;

    [SerializeField] private Button YellowUnicellPurchaseButton;
    [SerializeField] private TextMeshProUGUI YellowUnicellPurchaseCost;
    [SerializeField] private Sprite YellowUnicellTexture;
    [SerializeField] private Image YellowUnicell;
    [SerializeField] private Image YellowUnicellBackground;

    [SerializeField] private Button GreenUnicellPurchaseButton;
    [SerializeField] private TextMeshProUGUI GreenUnicellPurchaseCost;
    [SerializeField] private Sprite GreenUnicellTexture;
    [SerializeField] private Image GreenUnicell;
    [SerializeField] private Image GreenUnicellBackground;

    [SerializeField] private Button PurpleUnicellPurchaseButton;
    [SerializeField] private TextMeshProUGUI PurpleUnicellPurchaseCost;
    [SerializeField] private Sprite PurpleUnicellTexture;
    [SerializeField] private Image PurpleUnicell;
    [SerializeField] private Image PurpleUnicellBackground;

    [SerializeField] private Button RedUnicellPurchaseButton;
    [SerializeField] private TextMeshProUGUI RedUnicellPurchaseCost;
    [SerializeField] private Sprite RedUnicellTexture;
    [SerializeField] private Image RedUnicell;
    [SerializeField] private Image RedUnicellBackground;

    [SerializeField] private Button OrangeUnicellPurchaseButton;
    [SerializeField] private TextMeshProUGUI OrangeUnicellPurchaseCost;
    [SerializeField] private Sprite OrangeUnicellTexture;
    [SerializeField] private Image OrangeUnicell;
    [SerializeField] private Image OrangeUnicellBackground;

    [SerializeField] private EntityManager entityMananger;
    [SerializeField] private EconomyManager economyManager;

    public bool isBlueUnicellsUnlocked {  get; private set; }
    public bool isPinkUnicellsUnlocked { get; private set; }
    public bool isYellowUnicellsUnlocked { get; private set; }
    public bool isGreenUnicellsUnlocked { get; private set; }
    public bool isPurpleUnicellsUnlocked { get; private set; }
    public bool isRedUnicellsUnlocked { get; private set; }
    public bool isOrangeUnicellsUnlocked { get; private set; }

    private float LogicTimer;
    private float LogicTimerThreshold;

    private float PurchaseAllTimer;
    private float PurchaseAllTimerThreshold;
    private bool isPurchaseTrigger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        LogicTimer = 0;
        LogicTimerThreshold = 0.05f;
        PurchaseAllTimer = 0;
        PurchaseAllTimerThreshold = 0.2f;

        PurchasePanel.SetActive(false);

        BlueUnicell.sprite = Padlock;
        BlueUnicellBackground.sprite = Padlock;

        PinkUnicell.sprite = Padlock;
        PinkUnicellBackground.sprite = Padlock;

        YellowUnicell.sprite = Padlock;
        YellowUnicellBackground.sprite = Padlock;

        GreenUnicell.sprite = Padlock;
        GreenUnicellBackground.sprite = Padlock;

        PurpleUnicell.sprite = Padlock;
        PurpleUnicellBackground.sprite = Padlock;

        RedUnicell.sprite = Padlock;
        RedUnicellBackground.sprite = Padlock;

        OrangeUnicell.sprite = Padlock;
        OrangeUnicellBackground.sprite = Padlock;

        //entityMananger = FindFirstObjectByType<EntityManager>();

        //EconomyManager economyManager;
    }

    public bool isBlueUnicellsUnlockedCheck()
    {
        if (isBlueUnicellsUnlocked) return true;
        return false;
    }
    public bool isPinkUnicellsUnlockedCheck()
    {
        if (isPinkUnicellsUnlocked) return true;
        return false;
    }
    public bool isYellowUnicellsUnlockedCheck()
    {
        if (isYellowUnicellsUnlocked) return true;
        return false;
    }
    public bool isGreenUnicellsUnlockedCheck()
    {
        if (isGreenUnicellsUnlocked) return true;
        return false;
    }
    public bool isPurpleUnicellsUnlockedCheck()
    {
        if (isPurpleUnicellsUnlocked) return true;
        return false;
    }
    public bool isOrangeUnicellsUnlockedCheck()
    {
        if (isOrangeUnicellsUnlocked) return true;
        return false;
    }

    public void AssignIsUnlockedBools(bool blue, bool pink, bool yellow, bool green, bool purple, bool orange)
    {
        if (blue)
        {
            isBlueUnicellsUnlocked = true;
            BlueUnicell.sprite = BlueUnicellTexture;
            BlueUnicellBackground.sprite = BlueUnicellTexture;
        }
        else
        {
            /*BlueUnicell.sprite = Padlock;
            BlueUnicellBackground.sprite = Padlock;*/
        }

        if (pink)
        {
            isPinkUnicellsUnlocked = true;
            PinkUnicell.sprite = PinkUnicellTexture;
            PinkUnicellBackground.sprite = PinkUnicellTexture;

        }
        else
        {
            //PinkUnicell.sprite = Padlock;
            //PinkUnicellBackground.sprite = Padlock;
        }

        if (yellow)
        {
            isYellowUnicellsUnlocked = true;
            YellowUnicell.sprite = YellowUnicellTexture;
            YellowUnicellBackground.sprite = YellowUnicellTexture;
        }
        else
        {
            //YellowUnicell.sprite = Padlock;
            //YellowUnicellBackground.sprite = Padlock;
        }

        if (green)
        {
            isGreenUnicellsUnlocked = true;
            GreenUnicell.sprite = GreenUnicellTexture;
            GreenUnicellBackground.sprite = GreenUnicellTexture;
        }
        else
        {
            //GreenUnicell.sprite = Padlock;
            //GreenUnicellBackground.sprite = Padlock;
        }

        if (purple)
        {
            isPurpleUnicellsUnlocked = true;
            PurpleUnicell.sprite = PurpleUnicellTexture;
            PurpleUnicellBackground.sprite = PurpleUnicellTexture;
        }
        else
        {
            //PurpleUnicell.sprite = Padlock;
            //PurpleUnicellBackground.sprite = Padlock;
        }

        if (orange)
        {
            isOrangeUnicellsUnlocked = true;
            OrangeUnicell.sprite = OrangeUnicellTexture;
            OrangeUnicellBackground.sprite = OrangeUnicellTexture;
        }
        else
        {
            //OrangeUnicell.sprite = Padlock;
            //OrangeUnicellBackground.sprite = Padlock;
        }
    }

    public void ResetUnlocks()
    {
        isBlueUnicellsUnlocked = false;
        isPinkUnicellsUnlocked = false;
        isYellowUnicellsUnlocked = false;
        isGreenUnicellsUnlocked = false;
        isOrangeUnicellsUnlocked = false;
        isPurpleUnicellsUnlocked = false;
    }
    

    private void Update()
    {
        LogicTimer += Time.deltaTime;
        if (LogicTimer > LogicTimerThreshold)
        {
            LogicTimer -= LogicTimerThreshold;
            LogicUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.P))
        {
            OnPurchasePanelButtonClicked();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PurchasePanel.gameObject.activeSelf)
            {
                PurchasePanel.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            PurchaseAllTimer = -0.5f;

            PurchaseAllTimer -= PurchaseAllTimerThreshold;
            OnBlueUnicellPurchaseButtonClicked();
            OnPinkUnicellPurchaseButtonClicked();
            OnYellowUnicellPurchaseButtonClicked();
            OnGreenUnicellPurchaseButtonClicked();
            OnPurpleUnicellPurchaseButtonClicked();
            OnOrangeUnicellPurchaseButtonClicked();
        }
        if (Input.GetKey(KeyCode.A))
        {
            PurchaseAllTimer += Time.deltaTime;
            if (PurchaseAllTimer > PurchaseAllTimerThreshold)
            {
                PurchaseAllTimer -= PurchaseAllTimerThreshold;
                OnBlueUnicellPurchaseButtonClicked();
                OnPinkUnicellPurchaseButtonClicked();
                OnYellowUnicellPurchaseButtonClicked();
                OnGreenUnicellPurchaseButtonClicked();
                OnPurpleUnicellPurchaseButtonClicked();
                OnOrangeUnicellPurchaseButtonClicked();
            }
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            PurchaseAllTimer = 0;
        }
    }

    private void LogicUpdate()
    {
        if (PurchasePanel.gameObject.activeSelf)
        {
            UpdatePurchasePanelButtons();
        }
    }

    public void OnPurchasePanelButtonClicked()
    {
        if (!PurchasePanel.gameObject.activeSelf)
        {
            PurchasePanel.SetActive(true);

            UpdatePurchasePanelButtons();

            if (BiolabPanel.gameObject.activeSelf)
            {
                BiolabPanel.SetActive(false);
            }
            if (UpgradePanels.gameObject.activeSelf)
            {
                UpgradePanels.SetActive(false);
            }
        }
        else
        {
            PurchasePanel.SetActive(false);
        }
    }

    public void OnBlueUnicellPurchaseButtonClicked()
    {
        if (isBlueUnicellsUnlocked)
        {
            if (economyManager.UnicellPurchaseEligible("Blue"))
            {
                economyManager.SpendUnicell("Blue");
                UpdatePurchasePanelButtons();

                economyManager.IncrementUnicellPopulation("Blue");
            }
        }
        else
        {
            if (economyManager.UniCoins >= economyManager.UnlockNewUnicellCost)
            {
                economyManager.SpendNewUnicellUnlock();
                isBlueUnicellsUnlocked = true;
                BlueUnicell.sprite = BlueUnicellTexture;
                BlueUnicellBackground.sprite = BlueUnicellTexture;
                UpdatePurchasePanelButtons();
            }
        }
    }
    public void OnPinkUnicellPurchaseButtonClicked()
    {
        if (isPinkUnicellsUnlocked)
        {
            if (economyManager.UnicellPurchaseEligible("Pink"))
            {
                economyManager.SpendUnicell("Pink");
                UpdatePurchasePanelButtons();

                economyManager.IncrementUnicellPopulation("Pink");
            }
        }
        else
        {
            if (economyManager.UniCoins >= economyManager.UnlockNewUnicellCost)
            {
                economyManager.SpendNewUnicellUnlock();
                isPinkUnicellsUnlocked = true;
                PinkUnicell.sprite = PinkUnicellTexture;
                PinkUnicellBackground.sprite = PinkUnicellTexture;
                UpdatePurchasePanelButtons();
            }
        }
    }
    public void OnYellowUnicellPurchaseButtonClicked()
    {
        if (isYellowUnicellsUnlocked)
        {
            if (economyManager.UnicellPurchaseEligible("Yellow"))
            {
                economyManager.SpendUnicell("Yellow");
                UpdatePurchasePanelButtons();

                economyManager.IncrementUnicellPopulation("Yellow");
            }
        }
        else
        {
            if (economyManager.UniCoins >= economyManager.UnlockNewUnicellCost)
            {
                economyManager.SpendNewUnicellUnlock();
                isYellowUnicellsUnlocked = true;
                YellowUnicell.sprite = YellowUnicellTexture;
                YellowUnicellBackground.sprite = YellowUnicellTexture;
                UpdatePurchasePanelButtons();
            }
        }
    }
    public void OnGreenUnicellPurchaseButtonClicked()
    {
        if (isGreenUnicellsUnlocked)
        {
            if (economyManager.UnicellPurchaseEligible("Green"))
            {
                economyManager.SpendUnicell("Green");
                UpdatePurchasePanelButtons();

                economyManager.IncrementUnicellPopulation("Green");
            }
        }
        else
        {
            if (economyManager.UniCoins >= economyManager.UnlockNewUnicellCost)
            {
                economyManager.SpendNewUnicellUnlock();
                isGreenUnicellsUnlocked = true;
                GreenUnicell.sprite = GreenUnicellTexture;
                GreenUnicellBackground.sprite = GreenUnicellTexture;
                UpdatePurchasePanelButtons();
            }
        }
    }
    public void OnPurpleUnicellPurchaseButtonClicked()
    {
        if (isPurpleUnicellsUnlocked)
        {
            if (economyManager.UnicellPurchaseEligible("Purple"))
            {
                economyManager.SpendUnicell("Purple");
                UpdatePurchasePanelButtons();

                economyManager.IncrementUnicellPopulation("Purple");
            }
        }
        else
        {
            if (economyManager.UniCoins >= economyManager.UnlockNewUnicellCost)
            {
                economyManager.SpendNewUnicellUnlock();
                isPurpleUnicellsUnlocked = true;
                PurpleUnicell.sprite = PurpleUnicellTexture;
                PurpleUnicellBackground.sprite = PurpleUnicellTexture;
                UpdatePurchasePanelButtons();
            }
        }
    }
    public void OnRedUnicellPurchaseButtonClicked()
    {
        if (isRedUnicellsUnlocked)
        {
            if (economyManager.UnicellPurchaseEligible("Red"))
            {
                economyManager.SpendUnicell("Red");
                UpdatePurchasePanelButtons();

                economyManager.IncrementUnicellPopulation("Red");
            }
        }
        else
        {
            if (economyManager.UniCoins >= economyManager.UnlockNewUnicellCost)
            {
                economyManager.SpendNewUnicellUnlock();
                isRedUnicellsUnlocked = true;
                RedUnicell.sprite = RedUnicellTexture;
                RedUnicellBackground.sprite = RedUnicellTexture;
                UpdatePurchasePanelButtons();
            }
        }
    }
    public void OnOrangeUnicellPurchaseButtonClicked()
    {
        if (isOrangeUnicellsUnlocked)
        {
            if (economyManager.UnicellPurchaseEligible("Orange"))
            {
                economyManager.SpendUnicell("Orange");
                UpdatePurchasePanelButtons();

                economyManager.IncrementUnicellPopulation("Orange");
            }
        }
        else
        {
            if (economyManager.SpecialUniCoins >= economyManager.UnlockOrangeUnicellCost)
            {
                economyManager.SpendOrangeUnicellUnlock();
                isOrangeUnicellsUnlocked = true;
                OrangeUnicell.sprite = OrangeUnicellTexture;
                OrangeUnicellBackground.sprite = OrangeUnicellTexture;
                UpdatePurchasePanelButtons();
            }
        }
    }

    private void UpdatePurchasePanelButtons()
    {
        if (isBlueUnicellsUnlocked)
        {
            BlueUnicellPurchaseCost.text = economyManager.AbbreviateNumber(economyManager.UnicellCost("Blue"));
            if (economyManager.UnicellPurchaseEligible("Blue"))
            {
                BlueUnicellPurchaseButton.interactable = true;
            }
            else
            {
                BlueUnicellPurchaseButton.interactable = false;
            }
        }
        else
        {
            BlueUnicellPurchaseCost.text = economyManager.AbbreviateNumber(economyManager.UnlockNewUnicellCost);
            if (economyManager.UniCoins >= economyManager.UnlockNewUnicellCost)
            {
                BlueUnicellPurchaseButton.interactable = true;
            }
            else
            {
                BlueUnicellPurchaseButton.interactable = false;
            }
        }

        if (isPinkUnicellsUnlocked)
        {
            PinkUnicellPurchaseCost.text = economyManager.AbbreviateNumber(economyManager.UnicellCost("Pink"));
            if (economyManager.UnicellPurchaseEligible("Pink"))
            {
                PinkUnicellPurchaseButton.interactable = true;
            }
            else
            {
                PinkUnicellPurchaseButton.interactable = false;
            }
        }
        else
        {
            PinkUnicellPurchaseCost.text = economyManager.AbbreviateNumber(economyManager.UnlockNewUnicellCost);
            if (economyManager.UniCoins >= economyManager.UnlockNewUnicellCost)
            {
                PinkUnicellPurchaseButton.interactable = true;
            }
            else
            {
                PinkUnicellPurchaseButton.interactable = false;
            }
        }

        if (isYellowUnicellsUnlocked)
        {
            YellowUnicellPurchaseCost.text = economyManager.AbbreviateNumber(economyManager.UnicellCost("Yellow"));
            if (economyManager.UnicellPurchaseEligible("Yellow"))
            {
                YellowUnicellPurchaseButton.interactable = true;
            }
            else
            {
                YellowUnicellPurchaseButton.interactable = false;
            }
        }
        else
        {
            YellowUnicellPurchaseCost.text = economyManager.AbbreviateNumber(economyManager.UnlockNewUnicellCost);
            if (economyManager.UniCoins >= economyManager.UnlockNewUnicellCost)
            {
                YellowUnicellPurchaseButton.interactable = true;
            }
            else
            {
                YellowUnicellPurchaseButton.interactable = false;
            }
        }
        
        if (isGreenUnicellsUnlocked)
        {
            GreenUnicellPurchaseCost.text = economyManager.AbbreviateNumber(economyManager.UnicellCost("Green"));
            if (economyManager.UnicellPurchaseEligible("Green"))
            {
                GreenUnicellPurchaseButton.interactable = true;
            }
            else
            {
                GreenUnicellPurchaseButton.interactable = false;
            }
        }
        else
        {
            GreenUnicellPurchaseCost.text = economyManager.AbbreviateNumber(economyManager.UnlockNewUnicellCost);
            if (economyManager.UniCoins >= economyManager.UnlockNewUnicellCost)
            {
                GreenUnicellPurchaseButton.interactable = true;
            }
            else
            {
                GreenUnicellPurchaseButton.interactable = false;
            }
        }

        if (isPurpleUnicellsUnlocked)
        {
            PurpleUnicellPurchaseCost.text = economyManager.AbbreviateNumber(economyManager.UnicellCost("Purple"));
            if (economyManager.UnicellPurchaseEligible("Purple"))
            {
                PurpleUnicellPurchaseButton.interactable = true;
            }
            else
            {
                PurpleUnicellPurchaseButton.interactable = false;
            }
        }
        else
        {
            PurpleUnicellPurchaseCost.text = economyManager.AbbreviateNumber(economyManager.UnlockNewUnicellCost).ToString();
            if (economyManager.UniCoins >= economyManager.UnlockNewUnicellCost)
            {
                PurpleUnicellPurchaseButton.interactable = true;
            }
            else
            {
                PurpleUnicellPurchaseButton.interactable = false;
            }
        }

        if (isRedUnicellsUnlocked)
        {
            RedUnicellPurchaseCost.text = economyManager.AbbreviateNumber(economyManager.UnicellCost("Red")).ToString();
            if (economyManager.UnicellPurchaseEligible("Red"))
            {
                RedUnicellPurchaseButton.interactable = true;
            }
            else
            {
                RedUnicellPurchaseButton.interactable = false;
            }
        }
        else
        {
            RedUnicellPurchaseCost.text = economyManager.AbbreviateNumber(economyManager.UnlockNewUnicellCost).ToString();
            if (economyManager.UniCoins >= economyManager.UnlockNewUnicellCost)
            {
                RedUnicellPurchaseButton.interactable = true;
            }
            else
            {
                RedUnicellPurchaseButton.interactable = false;
            }
        }

        if (isOrangeUnicellsUnlocked)
        {
            OrangeUnicellPurchaseCost.text = economyManager.AbbreviateNumber(economyManager.UnicellCost("Orange")).ToString();
            if (economyManager.UnicellPurchaseEligible("Orange"))
            {
                OrangeUnicellPurchaseButton.interactable = true;
            }
            else
            {
                OrangeUnicellPurchaseButton.interactable = false;
            }
        }
        else
        {
            OrangeUnicellPurchaseCost.text = economyManager.AbbreviateNumber(economyManager.UnlockOrangeUnicellCost).ToString();
            if (economyManager.SpecialUniCoins >= economyManager.UnlockOrangeUnicellCost)
            {
                OrangeUnicellPurchaseButton.interactable = true;
            }
            else
            {
                OrangeUnicellPurchaseButton.interactable = false;
            }
        }

        if ((isBlueUnicellsUnlocked && isPinkUnicellsUnlocked && isYellowUnicellsUnlocked) && GreenUnicellPurchaseButton.gameObject.activeSelf == false)
        {
            GreenUnicellPurchaseButton.gameObject.SetActive(true);
            PurpleUnicellPurchaseButton.gameObject.SetActive(true);
            //RedUnicellPurchaseButton.gameObject.SetActive(true);
            OrangeUnicellPurchaseButton.gameObject.SetActive(true);
        }

        /*if (isGreenUnicellsUnlocked && !GreenUnicellPurchaseButton.gameObject.activeSelf)
        {
            GreenUnicellPurchaseButton.gameObject.SetActive(true);
        }
        if (isPurpleUnicellsUnlocked && !PurpleUnicellPurchaseButton.gameObject.activeSelf)
        {
            PurpleUnicellPurchaseButton.gameObject.SetActive(true);
        }
        if (isOrangeUnicellsUnlocked && !OrangeUnicellPurchaseButton.gameObject.activeSelf)
        {
            OrangeUnicellPurchaseButton.gameObject.SetActive(true);
        }*/
    }
}
