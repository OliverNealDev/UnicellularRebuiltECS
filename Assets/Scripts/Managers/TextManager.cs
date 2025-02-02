using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public static TextManager Instance;

    [SerializeField] private TextMeshProUGUI FPSText;

    [SerializeField] private TextMeshProUGUI UnicellPopulation;
    [SerializeField] private GameObject BlueUnicellsObject;
    [SerializeField] private GameObject PinkUnicellsObject;
    [SerializeField] private GameObject YellowUnicellsObject;
    [SerializeField] private GameObject GreenUnicellsObject;
    [SerializeField] private GameObject PurpleUnicellsObject;
    [SerializeField] private GameObject RedUnicellsObject;
    [SerializeField] private GameObject OrangeUnicellsObject;

    [SerializeField] private TextMeshProUGUI DNAText;
    [SerializeField] private TextMeshProUGUI SpecialDNAText;

    [SerializeField] private float TextTickRate;
    private float TextLogicTimerThreshold;
    private float TextLogicTimer;

    private float deltaTime = 0.0f;

    [SerializeField] private EconomyManager economyManager;

    private void Start()
    {
        Instance = this;

        TextLogicTimerThreshold = 1 / TextTickRate;
    }

    void Update()
    {
        TextLogicTimer += Time.deltaTime;
        if (TextLogicTimer > TextLogicTimerThreshold)
        {
            TextLogicUpdate();
            TextLogicTimer -= TextLogicTimerThreshold;
        }
    }

    void TextLogicUpdate()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        FPSText.text = Mathf.Ceil(fps).ToString() + " FPS";

        //long UnicellPopulationLong = BlueUnicellsObject.transform.childCount + PinkUnicellsObject.transform.childCount + YellowUnicellsObject.transform.childCount + GreenUnicellsObject.transform.childCount + PurpleUnicellsObject.transform.childCount + RedUnicellsObject.transform.childCount + OrangeUnicellsObject.transform.childCount;
        
        UnicellPopulation.text = economyManager.AbbreviateNumber(economyManager.TotaltargetUnicellPopulation());
        //PinkUnicellsPopulation.text = PinkUnicellsObject.transform.childCount.ToString();
        DNAText.text = economyManager.AbbreviateNumber(economyManager.UniCoins);
        SpecialDNAText.text = economyManager.AbbreviateNumber(economyManager.SpecialUniCoins);
    }

    public void UpdateSoulText(float BlueSouls, float PinkSouls)
    {
        //BlueSoulsText.text = AbbreviateNumber(Mathf.RoundToInt(BlueSouls)).ToString();
        //PinkSoulsText.text = AbbreviateNumber(Mathf.RoundToInt(PinkSouls)).ToString();
    }
    /*public string AbbreviateNumber(long number)
    {
        if (number >= 1_000_000_000_000_000_000) // Quintillions
        {
            return (number / 1_000_000_000_000_000_000.0).ToString("0.#") + "Qi";
        }
        else if (number >= 1_000_000_000_000_000) // Quadrillions
        {
            return (number / 1_000_000_000_000_000.0).ToString("0.#") + "Q";
        }
        else if (number >= 1_000_000_000_000) // Trillions
        {
            return (number / 1_000_000_000_000.0).ToString("0.#") + "T";
        }
        else if (number >= 1_000_000_000) // Billions
        {
            return (number / 1_000_000_000.0).ToString("0.#") + "B";
        }
        else if (number >= 1_000_000) // Millions
        {
            return (number / 1_000_000.0).ToString("0.#") + "M";
        }
        else if (number >= 1_000) // Thousands
        {
            return (number / 1_000.0).ToString("0.#") + "k";
        }
        else
        {
            return number.ToString(); // Below 1000
        }
    }*/
}
