using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject statPanel;
    public TextMeshProUGUI speciesText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI healthText;
    public Slider healthSlider;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI stateText;
    public TextMeshProUGUI coinText;
    public Button HidePanelButton;

    public GameObject UnicellCardImage;

    public Sprite BlueUnicellCardImage;
    public Sprite PinkUnicellCardImage;
    public Sprite YellowUnicellCardImage;
    public Sprite GreenUnicellCardImage;
    public Sprite PurpleUnicellCardImage;
    public Sprite RedUnicellCardImage;
    public Sprite OrangeUnicellCardImage;

    [SerializeField] private GameObject CoinObject;
    [SerializeField] private Image CoinSprite;
    [SerializeField] private Sprite Coin;
    [SerializeField] private Sprite SpecialCoin;

    public Unicell StatPanelTargetUnicell;

    [SerializeField] private TMP_InputField UnicellNameInput;
    [SerializeField] private TextMeshProUGUI PlaceHolderText;

    [SerializeField] private EconomyManager economyManager;

    //private UnicellLabelManager unicellLabelManager;

    private float UIManagerTimer;
    private float UIManagerTimerThreshold;

    public bool isStatsLocked;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        statPanel.SetActive(false);

        UIManagerTimerThreshold = 0.05f;
    }

    private void Start()
    {
        CoinSprite = CoinObject.GetComponent<Image>();
    }

    private void Update()
    {
        UIManagerTimer += Time.deltaTime;
        if (UIManagerTimer > UIManagerTimerThreshold)
        {
            UIManagerTimer -= UIManagerTimerThreshold;
            LogicUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (statPanel.gameObject.activeSelf)
            {
                UnlockAndHideUnicellStats();
            }
        }

        /*if ((Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace)) && StatPanelTargetUnicell != null)
        {
            StatPanelTargetUnicell.IsDead = true;
            economyManager.HarvestUnicell(StatPanelTargetUnicell);
        }*/
    }

    void LogicUpdate()
    {
        if (StatPanelTargetUnicell != null)
        {
            UpdatePanelStats(StatPanelTargetUnicell);
        }
        else
        {
            UnlockAndHideUnicellStats();
        }
    }

    public void ShowUnicellStats(Unicell unicell)
    {
        if (!isStatsLocked)
        {
            StatPanelTargetUnicell = unicell;

            if (StatPanelTargetUnicell.isPersistent)
            {
                UnicellNameInput.interactable = true;

                PlaceHolderText.text = StatPanelTargetUnicell.name;
            }
            else
            {
                UnicellNameInput.interactable = false;

                PlaceHolderText.text = unicell.Species + " Unicell";
            }

            speciesText.text = unicell.Species + " Unicell";
            damageText.text = ((Mathf.RoundToInt(unicell.Damage * 10f)) / 10f).ToString();
            healthText.text = ((Mathf.RoundToInt(unicell.CurrentHealth * 10f)) / 10f) + "/" + ((Mathf.RoundToInt(unicell.MaxHealth * 10f)) / 10f);
            healthSlider.value = unicell.CurrentHealth / unicell.MaxHealth;
            speedText.text = "Speed: " + Mathf.CeilToInt(unicell.Speed);
            levelText.text = unicell.Level.ToString();
            if (unicell.isShinyUnicell || unicell.isElderUnicell || unicell.isKingUnicell)
            {
                CoinSprite.sprite = SpecialCoin;
            }
            else
            {
                CoinSprite.sprite = Coin;
            }
            if (unicell.Species == Unicell.UnicellSpecies.Orange)
            {
                coinText.text = ((Mathf.RoundToInt(unicell.Damage + unicell.MaxHealth)) * 10).ToString();
            }
            else
            {
                coinText.text = Mathf.RoundToInt(unicell.Damage + unicell.MaxHealth).ToString();
            }
            stateText.text = unicell.CurrentState.ToString();

            statPanel.SetActive(true); // Show the panel
            switch (unicell)
            {
                case BlueUnicell:
                    UnicellCardImage.GetComponent<Image>().sprite = BlueUnicellCardImage;
                    break;
                case PinkUnicell:
                    UnicellCardImage.GetComponent<Image>().sprite = PinkUnicellCardImage;
                    break;
                case YellowUnicell:
                    UnicellCardImage.GetComponent<Image>().sprite = YellowUnicellCardImage;
                    break;
                case GreenUnicell:
                    UnicellCardImage.GetComponent<Image>().sprite = GreenUnicellCardImage;
                    break;
                case PurpleUnicell:
                    UnicellCardImage.GetComponent<Image>().sprite = PurpleUnicellCardImage;
                    break;
                case RedUnicell:
                    UnicellCardImage.GetComponent<Image>().sprite = RedUnicellCardImage;
                    break;
                case OrangeUnicell:
                    UnicellCardImage.GetComponent<Image>().sprite = OrangeUnicellCardImage;
                    break;
            }
        }
    }

    void UpdatePanelStats(Unicell unicell)
    {
        speciesText.text = unicell.Species + " Unicell";
        damageText.text = ((Mathf.RoundToInt(unicell.Damage * 10)) / 10).ToString();
        healthText.text = ((Mathf.RoundToInt(unicell.CurrentHealth * 10)) / 10) + "/" + ((Mathf.RoundToInt(unicell.MaxHealth * 10)) / 10);
        healthSlider.value = unicell.CurrentHealth / unicell.MaxHealth;
        speedText.text = "Speed: " + Mathf.CeilToInt(unicell.Speed);
        levelText.text = unicell.Level.ToString();
        if (unicell.isShinyUnicell || unicell.isElderUnicell || unicell.isKingUnicell)
        {
            CoinSprite.sprite = SpecialCoin;
        }
        else
        {
            CoinSprite.sprite = Coin;
        }
        if (unicell.Species == Unicell.UnicellSpecies.Orange)
        {
            coinText.text = ((Mathf.RoundToInt(unicell.Damage + unicell.MaxHealth)) * 10).ToString();
        }
        else
        {
            coinText.text = Mathf.RoundToInt(unicell.Damage + unicell.MaxHealth).ToString();
        }
        stateText.text = unicell.CurrentState.ToString();
    }

    public void LockUnicellStats(Unicell unicell)
    {
        StatPanelTargetUnicell = unicell;

        if (StatPanelTargetUnicell.isPersistent)
        {
            UnicellNameInput.interactable = true;

            PlaceHolderText.text = StatPanelTargetUnicell.name;
        }
        else
        {
            UnicellNameInput.interactable = false;

            PlaceHolderText.text = unicell.Species + " Unicell";
        }

        speciesText.text = unicell.Species + " Unicell";
        damageText.text = ((Mathf.RoundToInt(unicell.Damage * 10)) / 10).ToString();
        healthText.text = ((Mathf.RoundToInt(unicell.CurrentHealth * 10)) / 10) + "/" + ((Mathf.RoundToInt(unicell.MaxHealth * 10)) / 10);
        healthSlider.value = unicell.CurrentHealth / unicell.MaxHealth;
        speedText.text = "Speed: " + Mathf.CeilToInt(unicell.Speed);
        levelText.text = unicell.Level.ToString();
        if (unicell.isShinyUnicell || unicell.isElderUnicell || unicell.isKingUnicell)
        {
            CoinSprite.sprite = SpecialCoin;
        }
        else
        {
            CoinSprite.sprite = Coin;
        }
        if (unicell.Species == Unicell.UnicellSpecies.Orange)
        {
            coinText.text = ((Mathf.RoundToInt(unicell.Damage + unicell.MaxHealth)) * 10).ToString();
        }
        else
        {
            coinText.text = Mathf.RoundToInt(unicell.Damage + unicell.MaxHealth).ToString();
        }
        stateText.text = unicell.CurrentState.ToString();

        statPanel.SetActive(true); // Show the panel
        switch (unicell)
        {
            case BlueUnicell:
                UnicellCardImage.GetComponent<Image>().sprite = BlueUnicellCardImage;
                break;
            case PinkUnicell:
                UnicellCardImage.GetComponent<Image>().sprite = PinkUnicellCardImage;
                break;
            case YellowUnicell:
                UnicellCardImage.GetComponent<Image>().sprite = YellowUnicellCardImage;
                break;
            case GreenUnicell:
                UnicellCardImage.GetComponent<Image>().sprite = GreenUnicellCardImage;
                break;
            case PurpleUnicell:
                UnicellCardImage.GetComponent<Image>().sprite = PurpleUnicellCardImage;
                break;
            case RedUnicell:
                UnicellCardImage.GetComponent<Image>().sprite = RedUnicellCardImage;
                break;
            case OrangeUnicell:
                UnicellCardImage.GetComponent<Image>().sprite = OrangeUnicellCardImage;
                break;
        }

        isStatsLocked = true;
    }

    public void OnHidePanelButtonClick()
    {
        statPanel.SetActive(false); // Hide the panel
        StatPanelTargetUnicell = null;

        isStatsLocked = false;
    }

    public void HideUnicellStats()
    {
        if (!isStatsLocked)
        {
            statPanel.SetActive(false); // Hide the panel
            StatPanelTargetUnicell = null;
        }
    }

    public void UnlockAndHideUnicellStats()
    {
        statPanel.SetActive(false); // Hide the panel
        StatPanelTargetUnicell = null;

        isStatsLocked = false;
    }

    public void OnPersistUnicellButtonClicked()
    {
        if (!StatPanelTargetUnicell.isPersistent)
        {
            StatPanelTargetUnicell.isPersistent = true;

            UnicellNameInput.interactable = true;
        }
    }

    public void OnInputFieldChanged()
    {
        PlaceHolderText.text = UnicellNameInput.text;
        StatPanelTargetUnicell.UnicellName = UnicellNameInput.text;
    }
}
