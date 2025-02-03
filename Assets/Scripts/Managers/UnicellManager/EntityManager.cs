using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using static Unicell;
using Unity.Collections;
using Unity.Jobs;
using Unity.VisualScripting;
using Unity.Entities.UniversalDelegates;

public class EntityManager : MonoBehaviour
{
    public static EntityManager Instance;
    
    // Variable Declarations
    #region VariableDeclarations
    // Unicell Prefabs
    public GameObject blueUnicellPrefab;
    public GameObject pinkUnicellPrefab;
    public GameObject yellowUnicellPrefab;
    public GameObject greenUnicellPrefab;
    public GameObject purpleUnicellPrefab;
    public GameObject redUnicellPrefab;
    public GameObject orangeUnicellPrefab;
    private GameObject unicellPrefabType;

    [SerializeField] private Sprite AwakeBlueUnicell;
    [SerializeField] private Sprite AwakePinkUnicell; // VAULTED
    [SerializeField] private Sprite AwakeYellowUnicell;

    private Unicell initialUnicell;

    // Food Prefabs
    public GameObject blueFoodPrefab;
    public GameObject pinkFoodPrefab;
    public GameObject yellowFoodPrefab;
    public GameObject greenFoodPrefab;
    public GameObject purpleFoodPrefab;
    public GameObject redFoodPrefab;
    public GameObject orangeFoodPrefab;
    private GameObject foodPrefabType;

    // Transform Related
    [SerializeField] private GameObject BlueUnicellsGameObject;
    [SerializeField] private GameObject PinkUnicellsGameObject;
    [SerializeField] private GameObject YellowUnicellsGameObject;
    [SerializeField] private GameObject GreenUnicellsGameObject;
    [SerializeField] private GameObject PurpleUnicellsGameObject;
    [SerializeField] private GameObject RedUnicellsGameObject;
    [SerializeField] private GameObject OrangeUnicellsGameObject;

    [SerializeField] private GameObject BlueFoodGameObject;
    [SerializeField] private GameObject PinkFoodGameObject;
    [SerializeField] private GameObject YellowFoodGameObject;
    [SerializeField] private GameObject GreenFoodGameObject;
    [SerializeField] private GameObject PurpleFoodGameObject;
    [SerializeField] private GameObject RedFoodGameObject;
    [SerializeField] private GameObject OrangeFoodGameObject;

    // Entity Lists
    [SerializeField] private List<Unicell> unicellList = new List<Unicell>();
    private List<Unicell> unicellsToRemove = new List<Unicell>();
    private List<Unicell> unicellsToAdd = new List<Unicell>();

    [SerializeField] private List<PersistentUnicellData> persistentBlueUnicells = new List<PersistentUnicellData>();
    [SerializeField] private List<PersistentUnicellData> persistentPinkUnicells = new List<PersistentUnicellData>();
    [SerializeField] private List<PersistentUnicellData> persistentYellowUnicells = new List<PersistentUnicellData>();
    [SerializeField] private List<PersistentUnicellData> persistentGreenUnicells = new List<PersistentUnicellData>();
    [SerializeField] private List<PersistentUnicellData> persistentPurpleUnicells = new List<PersistentUnicellData>();
    [SerializeField] private List<PersistentUnicellData> persistentRedUnicells = new List<PersistentUnicellData>();
    [SerializeField] private List<PersistentUnicellData> persistentOrangeUnicells = new List<PersistentUnicellData>();
    private bool isSpawningPersistentUnicell;
    private PersistentUnicellData stats;

    [SerializeField] private List<Food> foodList = new List<Food>();
    [SerializeField] private GameObject ExperienceSquarePrefab;

    [SerializeField] private int PregnantBlueUnicells;
    [SerializeField] private int PregnantPinkUnicells;
    [SerializeField] private int PregnantYellowUnicells;
    [SerializeField] private int PregnantGreenUnicells;
    [SerializeField] private int PregnantPurpleUnicells;
    [SerializeField] private int PregnantRedUnicells;
    [SerializeField] private int PregnantOrangeUnicells;
    #endregion

    // Logic Variables
    #region LogicVariables
    private float LogicUpdateTimer;
    float LogicUpdateTimerThreshold; // Set this in inspector
    [SerializeField] float LogicTickRate;

    private float SlowLogicUpdateTimer;
    float SlowLogicUpdateTimerThreshold;
    [SerializeField] float ProximityTickRate;

    private Vector2 Direction; // Used to calculate Unicell direction for fleeing, fighting and hunger
    [SerializeField] float HungryThreshold;

    private float BlueAuraTotal;
    private float PinkAuraTotal;
    private float YellowAuraTotal;
    [SerializeField] private int FoodEntityCountCapacity;
    #endregion

    // Proximity ints
    #region ProximityInts
    [SerializeField] private int ProximityBatchSize;
    [SerializeField] private int currentIndex = 0;
    [SerializeField] private bool isProximityUpdateComplete;
    #endregion

    // Unicellskins
    [SerializeField] private Sprite BlueShiny;
    [SerializeField] private Sprite PinkShiny;
    [SerializeField] private Sprite YellowShiny;
    [SerializeField] private Sprite GreenShiny;
    [SerializeField] private Sprite PurpleShiny;
    [SerializeField] private Sprite OrangeShiny;

    // Upgrade Variables
    [SerializeField] public float BlueDamageStatMultiplier;
    [SerializeField] public float BlueMaxHPStatMultiplier;
    [SerializeField] public int BlueSpawnLvStat;
    [SerializeField] public float BlueElderStatMultiplier;
    [SerializeField] public float BlueShinyStatMultiplier;
    [SerializeField] public float BlueKingStatMultiplier;


    [SerializeField] public float PinkDamageStatMultiplier;
    [SerializeField] public float PinkMaxHPStatMultiplier;
    [SerializeField] public int PinkSpawnLvStat;
    [SerializeField] public float PinkElderStatMultiplier;
    [SerializeField] public float PinkShinyStatMultiplier;
    [SerializeField] public float PinkKingStatMultiplier;

    [SerializeField] public float YellowDamageStatMultiplier;
    [SerializeField] public float YellowMaxHPStatMultiplier;
    [SerializeField] public int YellowSpawnLvStat;
    [SerializeField] public float YellowElderStatMultiplier;
    [SerializeField] public float YellowShinyStatMultiplier;
    [SerializeField] public float YellowKingStatMultiplier;

    [SerializeField] public float GreenDamageStatMultiplier;
    [SerializeField] public float GreenMaxHPStatMultiplier;
    [SerializeField] public int GreenSpawnLvStat;
    [SerializeField] public float GreenElderStatMultiplier;
    [SerializeField] public float GreenShinyStatMultiplier;
    [SerializeField] public float GreenKingStatMultiplier;

    [SerializeField] public float PurpleDamageStatMultiplier;
    [SerializeField] public float PurpleMaxHPStatMultiplier;
    [SerializeField] public int PurpleSpawnLvStat;
    [SerializeField] public float PurpleElderStatMultiplier;
    [SerializeField] public float PurpleShinyStatMultiplier;
    [SerializeField] public float PurpleKingStatMultiplier;

    [SerializeField] public float RedDamageStatMultiplier;
    [SerializeField] public float RedMaxHPStatMultiplier;
    [SerializeField] public int RedSpawnLvStat;
    [SerializeField] public float RedElderStatMultiplier;
    [SerializeField] public float RedShinyStatMultiplier;
    [SerializeField] public float RedKingStatMultiplier;

    [SerializeField] public float OrangeDamageStatMultiplier;
    [SerializeField] public float OrangeMaxHPStatMultiplier;
    [SerializeField] public int OrangeSpawnLvStat;
    [SerializeField] public float OrangeElderStatMultiplier;
    [SerializeField] public float OrangeShinyStatMultiplier;
    [SerializeField] public float OrangeKingStatMultiplier;

    // Debug Variables
    [SerializeField] private bool FixedUpdateScaleAnimation;

    // Class References
    [SerializeField] private EconomyManager economyManager;
    [SerializeField] private TargetEnvironmentSizeManager targetEnvironmentSizeController;
    [SerializeField] private UnicellLabelManager unicellLabelManager;
    [SerializeField] private BiolabMenuManager biolabMenuManager;

    // Label Variables
    //[SerializeField] private UnicellLabelManager UnicellLabelManager;

    // Macro Logic
    #region MacroLogic

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        LogicUpdateTimerThreshold = 1f / LogicTickRate;
        SlowLogicUpdateTimerThreshold = 1f / ProximityTickRate;
    }
    void Update()
    {
        LogicUpdateTimer += Time.deltaTime;
        if (LogicUpdateTimer > LogicUpdateTimerThreshold)
        {
            LogicUpdateTimer -= LogicUpdateTimerThreshold;
            LogicUpdate();
        }

        SlowLogicUpdateTimer += Time.deltaTime;
        if (SlowLogicUpdateTimer > SlowLogicUpdateTimerThreshold)
        {
            SlowLogicUpdateTimer -= SlowLogicUpdateTimerThreshold;
            isProximityUpdateComplete = false;
            ProximityBatchUpdate();
            SlowLogicUpdate();

            CheckUnicellsForHunger();
        }
    }
    void FixedUpdate()
    {
        if (!isProximityUpdateComplete)
        {
            ProximityBatchUpdate();
        }

        foreach (Unicell unicell in unicellList)
        {
            switch (unicell)
            {
                case BlueUnicell:
                    BlueUnicell blueUnicell = unicell.GetComponent<BlueUnicell>();
                    blueUnicell.InflationCooldown += 1 / LogicTickRate;
                    if (blueUnicell.isInflateAbilityActive)
                    {
                        InflationAbilityLogicUpdate(blueUnicell);
                    }
                    break;
                case PinkUnicell:
                    // Pink Unicell Fixed Update Logic
                    break;
                case YellowUnicell:
                    // Yellow Unicell Fixed Update Logic
                    break;
                case GreenUnicell:
                    // Yellow Unicell Fixed Update Logic
                    break;
                case PurpleUnicell:
                    // Yellow Unicell Fixed Update Logic
                    break;
                case RedUnicell:
                    // Yellow Unicell Fixed Update Logic
                    break;
                case OrangeUnicell:
                    // Yellow Unicell Fixed Update Logic
                    break;
                default:
                    Debug.LogError("EntityManager FixedUpdate() - Unknown Unicell Type");
                    break;
            }

            if (unicell.isLookingAtSomething)
            {
                switch (unicell)
                {
                    case PinkUnicell:
                        break;
                    default:
                        unicell.transform.right = unicell.LookingAtTarget;
                        break;
                }
            }
        }
    }

    /*void GrowAsBaby(Unicell unicell)
    {
        unicell.transform.localScale = new Vector2(
            unicell.transform.localScale.x + unicell.BaseScale.x / 2000f, 
            unicell.transform.localScale.y + unicell.BaseScale.y / 2000f);
        if (unicell.transform.localScale.x >= unicell.BaseScale.x)
        {
            unicell.transform.localScale = unicell.BaseScale;
            unicell.isBaby = false;
            switch (unicell)
            {
                case BlueUnicell:
                    unicell.GetComponent<SpriteRenderer>().sprite = AwakeBlueUnicell; break;
                case PinkUnicell:
                    unicell.GetComponent<SpriteRenderer>().sprite = AwakePinkUnicell; break;
                case YellowUnicell:
                    unicell.GetComponent<SpriteRenderer>().sprite = AwakeYellowUnicell; break;
            }
        }
    }*/
    void LogicUpdate()
    {
        UnicellLogicUpdate();
        FoodLogicUpdate();

        // To prevent modifying unicellList<> during the UnicellUpdate() process for all Unicells,
        // new and dead unicells must not be added or removed until AFTER processing all current unicells is complete.

        // Add new unicells
        /*foreach (Unicell newUnicell in unicellsToAdd)
        {
            unicellList.Add(newUnicell);
        }
        unicellsToAdd.Clear();

        // Remove dead unicells
        foreach (Unicell deadUnicell in unicellsToRemove)
        {
            unicellList.Remove(deadUnicell);
        }
        unicellsToRemove.Clear();*/
    }
    void ProximityBatchUpdate()
    {
        // Determines end index for this batch
        int endIndex = Mathf.Min(currentIndex + ProximityBatchSize, unicellList.Count);

       
            // Updates the batch of unicells
        for (int i = currentIndex; i < endIndex; i++)
        {
            Unicell unicell = unicellList[i];

            if (unicell.BaseScale.x < 2)
            {
                unicell.BaseScale = new Vector2(unicell.BaseScale.x + 0.001f, unicell.BaseScale.y + 0.001f);
                unicell.transform.localScale = new Vector2(unicell.transform.localScale.x + 0.001f, unicell.transform.localScale.y + 0.001f);
                unicell.PregnantScaleThreshold = unicell.BaseScale * 2; 
                if (unicell is BlueUnicell)
                {
                    BlueUnicell blueUnicell = (BlueUnicell)unicell;
                    blueUnicell.InflationScale = unicell.BaseScale * 2;
                }
            }

            if (unicell.CurrentState == UnicellState.Hungry)
            {
                Food NewNearestFood = FindNearestFood(unicell, unicell.ProximityRadius);
                if ((
                    unicellList[i].NearestFood == null && NewNearestFood != null) || (
                    NewNearestFood != null && NewNearestFood != unicell.NearestFood))
                {
                    unicell.NearestFood = NewNearestFood;
                }
            }

            Unicell NewNearestUnicell = FindNearestUnicell(unicell, unicellList[i].ProximityRadius);
            if ((
                unicell.NearestUnicell == null && NewNearestUnicell != null) || (
                NewNearestUnicell != null && NewNearestUnicell != unicell.NearestUnicell))
            {
                unicell.NearestUnicell = NewNearestUnicell;
            }

            DecideUnicellState(unicell);
        }

        // Updates current index
        currentIndex = endIndex;

        // Resets current index if it reaches the end of the list
        if (currentIndex >= unicellList.Count)
        {
            currentIndex = 0;
            isProximityUpdateComplete = true;
        }
    }
    void SlowLogicUpdate()
    {
        PregnantBlueUnicells = 0;
        PregnantPinkUnicells = 0;
        PregnantYellowUnicells = 0;
        PregnantGreenUnicells = 0;
        PregnantPurpleUnicells = 0;
        PregnantRedUnicells = 0;
        PregnantOrangeUnicells = 0;

        for (int i = 0; i < unicellList.Count; i++)
        {
            Unicell unicell = unicellList[i];

            switch (unicell)
            {
                case BlueUnicell:
                    if (unicell.isPregnant)
                    {
                        PregnantBlueUnicells++;
                    }
                    unicell.PregnantTimerThreshold = 0.1f * (BlueUnicellsGameObject.transform.childCount / 32f);
                    break;
                case PinkUnicell:
                    if (unicell.isPregnant)
                    {
                        PregnantPinkUnicells++;
                    }
                    unicell.PregnantTimerThreshold = 0.1f * (PinkUnicellsGameObject.transform.childCount / 32f);
                    break;
                case YellowUnicell:
                    if (unicell.isPregnant)
                    {
                        PregnantYellowUnicells++;
                    }
                    unicell.PregnantTimerThreshold = 0.1f * (YellowUnicellsGameObject.transform.childCount / 32f);
                    break;
                case GreenUnicell:
                    if (unicell.isPregnant)
                    {
                        PregnantGreenUnicells++;
                    }
                    unicell.PregnantTimerThreshold = 0.1f * (GreenUnicellsGameObject.transform.childCount / 32f);
                    break;
                case PurpleUnicell:
                    if (unicell.isPregnant)
                    {
                        PregnantPurpleUnicells++;
                    }
                    unicell.PregnantTimerThreshold = 0.1f * (PurpleUnicellsGameObject.transform.childCount / 32f);
                    break;
                case RedUnicell:
                    if (unicell.isPregnant)
                    {
                        PregnantRedUnicells++;
                    }
                    unicell.PregnantTimerThreshold = 0.1f * (RedUnicellsGameObject.transform.childCount / 32f);
                    break;
                case OrangeUnicell:
                    if (unicell.isPregnant)
                    {
                        PregnantOrangeUnicells++;
                    }
                    unicell.PregnantTimerThreshold = 0.1f * (OrangeUnicellsGameObject.transform.childCount / 32f);
                    break;
                default:
                    Debug.LogError("Entity Manager SlowLogicUpdate() - Unknown Unicell type for pregnancy check");
                    break;
            }


            if (unicell.PregnantTimer >= unicell.PregnantTimerThreshold && !unicell.isPregnant) // Needs changed to be pregnant X time after either birth or gave birth - ok
            {
                switch (unicell)
                {
                    case BlueUnicell:
                        if (economyManager.TargetBlueUnicellPopulation > BlueUnicellsGameObject.transform.childCount + PregnantBlueUnicells)
                        {
                            unicell.isPregnant = true;
                            PregnantBlueUnicells++;
                        }
                        break;
                    case PinkUnicell:
                        if (economyManager.TargetPinkUnicellPopulation > PinkUnicellsGameObject.transform.childCount + PregnantPinkUnicells)
                        {
                            unicell.isPregnant = true;
                            PregnantPinkUnicells++;
                        }
                        break;
                    case YellowUnicell:
                        if (economyManager.TargetYellowUnicellPopulation > YellowUnicellsGameObject.transform.childCount + PregnantYellowUnicells)
                        {
                            unicell.isPregnant = true;
                            PregnantYellowUnicells++;
                        }
                        break;
                    case GreenUnicell:
                        if (economyManager.TargetGreenUnicellPopulation > GreenUnicellsGameObject.transform.childCount + PregnantGreenUnicells)
                        {
                            unicell.isPregnant = true;
                            PregnantGreenUnicells++;
                        }
                        break;
                    case PurpleUnicell:
                        if (economyManager.TargetPurpleUnicellPopulation > PurpleUnicellsGameObject.transform.childCount + PregnantPurpleUnicells)
                        {
                            unicell.isPregnant = true;
                            PregnantPurpleUnicells++;
                        }
                        break;
                    case RedUnicell:
                        if (economyManager.TargetRedUnicellPopulation > RedUnicellsGameObject.transform.childCount + PregnantRedUnicells)
                        {
                            unicell.isPregnant = true;
                            PregnantRedUnicells++;
                        }
                        break;
                    case OrangeUnicell:
                        if (economyManager.TargetOrangeUnicellPopulation > OrangeUnicellsGameObject.transform.childCount + PregnantOrangeUnicells)
                        {
                            unicell.isPregnant = true;
                            PregnantOrangeUnicells++;
                        }
                        break;
                    default:
                        Debug.LogError("EntityManager SlowLogicUpdate() - Unknown UnicellType for Pregnent checker");
                        break;
                }
            }

            if (unicell.XP >= unicell.XP_Threshold)
            {
                LevelUp(unicell);
            }

            CheckUnicellsForPersistence(unicell);

        }

        // Other Logic

        if (BlueUnicellsGameObject.transform.childCount < 1 && economyManager.TargetBlueUnicellPopulation > 0)
        {
            if (BlueUnicellsGameObject.transform.childCount < economyManager.TargetBlueUnicellPopulation)
            {
                SpawnUnicells(UnicellSpecies.Blue, 1);
            }
        }
        if (PinkUnicellsGameObject.transform.childCount < 1 && economyManager.TargetPinkUnicellPopulation > 0)
        {
            if (PinkUnicellsGameObject.transform.childCount < economyManager.TargetPinkUnicellPopulation)
            {
                SpawnUnicells(UnicellSpecies.Pink, 1);
            }
        }
        if (YellowUnicellsGameObject.transform.childCount < 1 && economyManager.TargetYellowUnicellPopulation > 0)
        {
            if (YellowUnicellsGameObject.transform.childCount < economyManager.TargetYellowUnicellPopulation)
            {
                SpawnUnicells(UnicellSpecies.Yellow, 1);
            }
        }
        if (GreenUnicellsGameObject.transform.childCount < 1 && economyManager.TargetGreenUnicellPopulation > 0)
        {
            if (GreenUnicellsGameObject.transform.childCount < economyManager.TargetGreenUnicellPopulation)
            {
                SpawnUnicells(UnicellSpecies.Green, 1);
            }
        }
        if (PurpleUnicellsGameObject.transform.childCount < 1 && economyManager.TargetPurpleUnicellPopulation > 0)
        {
            if (PurpleUnicellsGameObject.transform.childCount < economyManager.TargetPurpleUnicellPopulation)
            {
                SpawnUnicells(UnicellSpecies.Purple, 1);
            }
        }
        if (RedUnicellsGameObject.transform.childCount < 1 && economyManager.TargetRedUnicellPopulation > 0)
        {
            if (RedUnicellsGameObject.transform.childCount < economyManager.TargetRedUnicellPopulation)
            {
                SpawnUnicells(UnicellSpecies.Red, 1);
            }
        }
        if (OrangeUnicellsGameObject.transform.childCount < 1 && economyManager.TargetOrangeUnicellPopulation > 0)
        {
            if (OrangeUnicellsGameObject.transform.childCount < economyManager.TargetOrangeUnicellPopulation)
            {
                SpawnUnicells(UnicellSpecies.Orange, 1);
            }
        }

        FoodEntityCountCapacity = unicellList.Count;

        // Removes food when food count goes above unicell population count for performance reasons. Despawns oldest food first.
        if (foodList.Count > FoodEntityCountCapacity)
        {
            for (int i = 0; i < foodList.Count - FoodEntityCountCapacity; i++)
            {
                foodList[i].isDead = true;
            }
        }
    }
    void UnicellLogicUpdate()
    {
        for (int i = unicellList.Count - 1; i >= 0; i--)
        {
            Unicell unicell = unicellList[i];
            if (unicell.Hunger <= 0 || unicell.CurrentHealth <= 0 || unicell.IsDead)
            {
                HandleUnicellDeath(unicell);
            }
            else
            {
                unicell.Age += 1 / LogicTickRate;
                StateLogicUpdate(unicell);
                if (unicell.isPregnant)
                {
                    UnicellPregnantUpdate(unicell);
                }
                if (unicell.isColliding && unicell.collidingObjectCollision != null)
                {
                    HandleUnicellCollision(unicell);
                    unicell.isColliding = false;
                    unicell.collidingObjectCollision = null;
                }
            }


            unicell.PregnantTimer += 1 / LogicTickRate;
            unicell.Hunger -= (1 / LogicTickRate) / 120; // Unicells die in 60 seconds from 1 Hunger to 0 without eating
        }
    }
    void FoodLogicUpdate()
    {
        for (int i = foodList.Count - 1; i >= 0; i--)
        {
            Food food = foodList[i];

            if (food.isDead)
            {
                UnregisterFood(food);
                Destroy (food.gameObject);
            }
            else
            {
                food.Rigidbody.angularVelocity += food.RandomAngularVelocity * (1 / LogicTickRate);
                food.Rigidbody.linearVelocity += food.RandomLinearVelocity * (1 / LogicTickRate);
            }
        }
    }
    void UnicellPregnantUpdate(Unicell unicell)
    {
        unicell.transform.localScale = new Vector2(
                    unicell.transform.localScale.x + (2f / LogicTickRate),
                    unicell.transform.localScale.y + (2f / LogicTickRate));
        if (unicell.transform.localScale.x >= unicell.PregnantScaleThreshold.x)
        {
            HandleReproduction(unicell);
            unicell.transform.localScale = unicell.BaseScale;
            unicell.PregnantTimer = 0;
            unicell.isPregnant = false;

            DecideUnicellState(unicell);
        }
    }
    #endregion


    // Species-Exclusive Functions
    #region SpeciesExclusiveFunctions
    void InflationAbilityLogicUpdate(BlueUnicell blueUnicell)
    {
        if (blueUnicell.isInflating)
        {
            blueUnicell.transform.localScale = new Vector2(blueUnicell.transform.localScale.x + ((1f / 50f) * 20f), blueUnicell.transform.localScale.y + ((1f / 50f) * 20f));
            if (blueUnicell.transform.localScale.x > blueUnicell.InflationScale.x)
            {
                blueUnicell.isInflating = false;
                blueUnicell.transform.localScale = new Vector2(blueUnicell.InflationScale.x, blueUnicell.InflationScale.y);
            }
        }
        else
        {
            blueUnicell.transform.localScale = new Vector2(blueUnicell.transform.localScale.x - ((1f / 50f) * 4f), blueUnicell.transform.localScale.y - ((1f / 50f) * 4f));
            if (blueUnicell.transform.localScale.x < blueUnicell.BaseScale.x)
            {
                blueUnicell.isInflateAbilityActive = false;
                blueUnicell.transform.localScale = new Vector2(blueUnicell.BaseScale.x, blueUnicell.BaseScale.y);
            }
        }
    }
    #endregion

    // State Enter/Change/Update
    #region StateEnterChangeUpdate
    void StateEnter(Unicell unicell)
    {
        switch (unicell.CurrentState)
        {
            case UnicellState.Idle:
                IdleStart(unicell);
                break;
            case UnicellState.Fighting:
                FightStart(unicell);
                break;
            case UnicellState.Fleeing:
                FleeStart(unicell);
                break;
            case UnicellState.Hungry:
                HungryStart(unicell);
                break;
            case UnicellState.Reproducing:
                ReproduceStart(unicell);
                break;
        }
    }
    void ChangeState(Unicell unicell, UnicellState newState)
    {
        unicell.isLookingAtSomething = false;
        unicell.CurrentState = newState; StateEnter(unicell);

        if (UIManager.Instance.StatPanelTargetUnicell == unicell)
        {
            UIManager.Instance.ShowUnicellStats(unicell);
        }
    }
    void StateLogicUpdate(Unicell unicell)
    {
        switch (unicell.CurrentState)
        {
            case UnicellState.Idle:
                unicell.Rigidbody.angularVelocity += unicell.RandomAngularVelocity * (1 / LogicTickRate);
                unicell.Rigidbody.linearVelocity += unicell.RandomLinearVelocity * (1 / LogicTickRate);
                if (unicell.CurrentHealth < unicell.MaxHealth)
                {
                    unicell.CurrentHealth += (unicell.MaxHealth / LogicTickRate) / 10;
                    if (unicell.CurrentHealth > unicell.MaxHealth)
                    {
                        unicell.CurrentHealth = unicell.MaxHealth;
                    }
                }
                break;
            case UnicellState.Fighting:

                if (unicell is PinkUnicell)
                {
                    if (unicell.Rigidbody.angularVelocity < 720)
                    {
                        unicell.Rigidbody.angularVelocity += 30;
                    }
                }

                // If nearest Unicell is null, attempt to find a new nearest Unicell to fight, otherwise change state
                if (unicell.NearestUnicell == null)
                {
                    var Unicell = FindNearestUnicell(unicell, unicell.ProximityRadius);
                    if (Unicell != null)
                    {
                        unicell.NearestUnicell = Unicell;
                    }
                    else
                    {
                        DecideUnicellState(unicell);
                        break;
                    }
                }

                // Sets target to a variable so that FixedUpdate can set unicell rotation at physics tickrate
                Vector3 CurrentLookingTarget = unicell.NearestUnicell.transform.position - unicell.transform.position;
                if (CurrentLookingTarget != unicell.LookingAtTarget || !unicell.isLookingAtSomething)
                {
                    unicell.LookingAtTarget = CurrentLookingTarget;
                    unicell.isLookingAtSomething = true;
                }

                Direction = unicell.NearestUnicell.transform.position - unicell.transform.position;
                if (Direction.x > 0)
                {
                    Direction.x = unicell.Speed;
                }
                else
                {
                    Direction.x = -unicell.Speed;
                }
                if (Direction.y > 0)
                {
                    Direction.y = unicell.Speed;
                }
                else
                {
                    Direction.y = -unicell.Speed;
                }

                unicell.Rigidbody.linearVelocity += Direction / LogicTickRate;

                break;
            case UnicellState.Fleeing:
                // If nearest Unicell is null, attempt to find a new nearest Unicell to flee from, otherwise change state
                if (unicell.NearestUnicell == null)
                {
                    var Unicell = FindNearestUnicell(unicell, unicell.ProximityRadius);
                    if (Unicell != null)
                    {
                        unicell.NearestUnicell = Unicell;
                    }
                    else
                    {
                        DecideUnicellState(unicell);
                        break;
                    }
                }

                Direction = unicell.NearestUnicell.transform.position - unicell.transform.position;
                if (Direction.x > 0)
                {
                    Direction.x = unicell.Speed;
                }
                else
                {
                    Direction.x = -unicell.Speed;
                }
                if (Direction.y > 0)
                {
                    Direction.y = unicell.Speed;
                }
                else
                {
                    Direction.y = -unicell.Speed;
                }

                unicell.Rigidbody.linearVelocity -= Direction / LogicTickRate;
                break;
            case UnicellState.Hungry:
                // If nearest Unicell is null, attempt to find a new nearest Unicell to fight, otherwise change state
                if (unicell.NearestFood == null)
                {

                    if (unicell.RandomLocation == Vector2.zero)
                    {
                        unicell.RandomLocation = new Vector2(Random.Range(-(8 * targetEnvironmentSizeController.transform.localScale.x), (8 * targetEnvironmentSizeController.transform.localScale.x)), Random.Range(-(4.5f * targetEnvironmentSizeController.transform.localScale.y), (4.5f * targetEnvironmentSizeController.transform.localScale.y)));
                        //Debug.Log(unicell.RandomLocation);

                        /*float DistanceFromSpawn = Vector2.Distance(unicell.transform.position, unicell.SpawnLocation);
                        if (DistanceFromSpawn > 30)
                        {
                            unicell.RandomLocation = new Vector2(unicell.SpawnLocation.x, unicell.SpawnLocation.y);
                        }
                        else
                        {
                            //unicell.RandomLocation = new Vector2(unicell.transform.position.x + (Random.Range(-10, 10)), unicell.transform.position.y + (Random.Range(-50, 50)));
                        }*/
                    }

                    float DistanceToRandomLocation = Vector2.Distance(unicell.transform.position, unicell.RandomLocation);
                    if (DistanceToRandomLocation < 1)
                    {
                        unicell.RandomLocation = new Vector2(Random.Range(-(8 * targetEnvironmentSizeController.transform.localScale.x), (8 * targetEnvironmentSizeController.transform.localScale.x)), Random.Range(-(4.5f * targetEnvironmentSizeController.transform.localScale.y), (4.5f * targetEnvironmentSizeController.transform.localScale.y)));
                        //Debug.Log(unicell.RandomLocation);

                        /*float DistanceFromSpawn = Vector2.Distance(unicell.transform.position, unicell.SpawnLocation);
                        if (DistanceFromSpawn > 30)
                        {
                            unicell.RandomLocation = new Vector2(unicell.SpawnLocation.x, unicell.SpawnLocation.y);
                        }
                        else
                        {
                            unicell.RandomLocation = new Vector2(unicell.transform.position.x + (Random.Range(-10, 10)), unicell.transform.position.y + (Random.Range(-50, 50)));
                        }*/
                    }
                    else
                    {
                        // Go towards random location

                        Direction = unicell.RandomLocation - new Vector2(unicell.transform.position.x, unicell.transform.position.y);
                        if (Direction.x > 0)
                        {
                            Direction.x = unicell.Speed;
                        }
                        else
                        {
                            Direction.x = -unicell.Speed;
                        }
                        if (Direction.y > 0)
                        {
                            Direction.y = unicell.Speed;
                        }
                        else
                        {
                            Direction.y = -unicell.Speed;
                        }

                        unicell.Rigidbody.linearVelocity += Direction / LogicTickRate;
                    }
                }
                else
                {
                    // Go towards food

                    // Sets target to a variable so that FixedUpdate can set unicell rotation at physics tickrate
                    Vector3 CurrentLookingTargetFood = unicell.NearestFood.transform.position - unicell.transform.position;
                    if (CurrentLookingTargetFood != unicell.LookingAtTarget || !unicell.isLookingAtSomething)
                    {
                        unicell.LookingAtTarget = CurrentLookingTargetFood;
                        unicell.isLookingAtSomething = true;
                    }

                    Direction = unicell.NearestFood.transform.position - unicell.transform.position;
                    if (Direction.x > 0)
                    {
                        Direction.x = unicell.Speed;
                    }
                    else
                    {
                        Direction.x = -unicell.Speed;
                    }
                    if (Direction.y > 0)
                    {
                        Direction.y = unicell.Speed;
                    }
                    else
                    {
                        Direction.y = -unicell.Speed;
                    }

                    unicell.Rigidbody.linearVelocity += Direction / LogicTickRate;
                }
                break;
            case UnicellState.Reproducing:
                /*unicell.transform.localScale = new Vector2(
                    unicell.transform.localScale.x + (0.5f / LogicTickRate),
                    unicell.transform.localScale.y + (0.5f / LogicTickRate));
                if (unicell.transform.localScale.x >= unicell.PregnantScaleThreshold.x)
                {
                    HandleReproduction(unicell);
                    unicell.transform.localScale = unicell.BaseScale;
                    unicell.PregnantTimer = 0;
                    unicell.isPregnant = false;

                    DecideUnicellState(unicell);
                }*/
                break;
        }
    }
    #endregion

    // State Starts
    #region StateStarts
    void FightStart(Unicell unicell)
    {
        
    }
    void IdleStart(Unicell unicell)
    {
        
    }
    void FleeStart(Unicell unicell)
    {

    }
    void HungryStart(Unicell unicell)
    {
        //var PickedFood = FindNearestFoodObject(this, BaseScale.x * 2); // Attempts to pick a food object near itself in an area the size of its scale * 2
    }
    void ReproduceStart(Unicell unicell)
    {
        unicell.isPregnant = true;
    }
    #endregion

    // Events
    #region Events
    void LevelUp(Unicell unicell)
    {
        // While the unicell has enough XP to level up:
        while (unicell.XP >= unicell.XP_Threshold)
        {
            // Subtract the threshold so that XP is "spent"
            unicell.XP -= unicell.XP_Threshold;

            // Increase the level
            unicell.Level++;

            // Increase the XP threshold (for example, double it)
            unicell.XP_Threshold = Mathf.CeilToInt(unicell.XP_Threshold * 1.1f);

            // Increase stats
            unicell.MaxHealth = Mathf.CeilToInt(unicell.MaxHealth * 1.1f);
            unicell.Damage = Mathf.CeilToInt(unicell.Damage * 1.1f);

            // Update UI if this unicell is currently being inspected
            if (UIManager.Instance.StatPanelTargetUnicell == unicell)
            {
                UIManager.Instance.ShowUnicellStats(unicell);
            }

            unicell.labelUpdateRequested = true;
        }
    }

    void HandleUnicellDeath(Unicell unicell)
    {
        Vector2 spawnPosition = unicell.transform.position;
        if (unicell is BlueUnicell)
        {
            foodPrefabType = blueFoodPrefab;
        }
        else if (unicell is PinkUnicell)
        {
            foodPrefabType = pinkFoodPrefab;
        }
        else if (unicell is YellowUnicell)
        {
            foodPrefabType = yellowFoodPrefab;
        }
        else if (unicell is GreenUnicell)
        {
            foodPrefabType = greenFoodPrefab;
        }
        else if (unicell is PurpleUnicell)
        {
            foodPrefabType = purpleFoodPrefab;
        }
        else if (unicell is RedUnicell)
        {
            foodPrefabType = redFoodPrefab;
        }
        else if (unicell is OrangeUnicell)
        {
            foodPrefabType = orangeFoodPrefab;
        }
        else
        {
            Debug.LogError("UnicellManager HandleUnicellDeath() - Unknown food type for death");
        }
        GameObject newFoodObject = Instantiate(foodPrefabType, spawnPosition, Quaternion.identity);
        Food newFood = newFoodObject.GetComponent<Food>();
        InitialiseFood(newFood);
        foodList.Add(newFood);
        RegisterFood(newFood);

        economyManager.IncrementSoul(unicell);

        // Persistent Unicell Mechanic, checks if Unicell is marked as persistent and saves it to a list of Persistent Unicells to be reborn
        if (unicell.isPersistent)
        {
            PersistentUnicellData stats = new PersistentUnicellData(
                unicell.Species.ToString(),
                unicell.Damage,
                unicell.MaxHealth,
                unicell.Level,
                unicell.XP,
                unicell.XP_Threshold,
                unicell.BaseScale);
            switch (unicell)
            {
                case BlueUnicell:
                    persistentBlueUnicells.Add(stats);
                    break;
                case PinkUnicell:
                    persistentPinkUnicells.Add(stats);
                    break;
                case YellowUnicell:
                    persistentYellowUnicells.Add(stats);
                    break;
                default:
                    Debug.LogError("EntityManager HandleUnicellDeath() - Unkown Unicell species");
                    break;
            }
        }
        else
        {
            if (unicell.XP > 0)
            {
                int ExperienceSquareSpawnCount = Mathf.CeilToInt(unicell.XP / 500);

                /*if (ExperienceSquareSpawnCount == 0)
                {
                    ExperienceSquareSpawnCount = 1;
                }
                else if (ExperienceSquareSpawnCount > 4)
                {
                    ExperienceSquareSpawnCount = 4;
                }*/

                ExperienceSquareSpawnCount = 2;

                float ExperienceSquareValue = unicell.XP / ExperienceSquareSpawnCount;
                for (int i = 0; i < ExperienceSquareSpawnCount; i++)
                {
                    GameObject newfoodObject = Instantiate(ExperienceSquarePrefab, spawnPosition, Quaternion.identity);
                    Food food = newfoodObject.GetComponent<Food>();
                    InitialiseFood(food);
                    food.ExperienceValue = ExperienceSquareValue;
                    foodList.Add(food);
                    RegisterFood(food);
                }
            }

        }

        economyManager.GrantCurrenciesOnUnicellDeath(unicell);

        if (unicell.isHovered || unicell.isClickedOn)
        {
            UIManager.Instance.UnlockAndHideUnicellStats();
        }

        //UnicellLabelManager unicellLabelManager = GetComponent<UnicellLabelManager>();
        //unicellLabelManager.RemoveUnicellLabel(unicell);
        unicellList.Remove(unicell);
        Destroy(unicell.gameObject);
    }
    void HandleReproduction(Unicell unicell)
    {
        switch (unicell.Species)
        {
            case Unicell.UnicellSpecies.Blue:
                unicellPrefabType = blueUnicellPrefab; break;
            case Unicell.UnicellSpecies.Pink:
                unicellPrefabType = pinkUnicellPrefab; break;
            case Unicell.UnicellSpecies.Yellow:
                unicellPrefabType = yellowUnicellPrefab; break;
            case Unicell.UnicellSpecies.Green:
                unicellPrefabType = greenUnicellPrefab; break;
            case Unicell.UnicellSpecies.Purple:
                unicellPrefabType = purpleUnicellPrefab; break;
            case Unicell.UnicellSpecies.Red:
                unicellPrefabType = redUnicellPrefab; break;
            case Unicell.UnicellSpecies.Orange:
                unicellPrefabType = orangeUnicellPrefab; break;
            default:
                Debug.LogError("UnicellManager HandleReproduction() - Unkown species of parent Unicell"); break;

        }

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        //Vector2 parentPositionSkewed = new Vector2(unicell.transform.position.x + Random.Range(-0.5f, 0.5f), unicell.transform.position.y + Random.Range(-0.5f, 0.5f));

        GameObject newUnicellObject = Instantiate(unicellPrefabType, new Vector2(unicell.transform.position.x + randomDirection.x, unicell.transform.position.y + randomDirection.y), Quaternion.identity); ;
        Unicell newUnicell = newUnicellObject.GetComponent<Unicell>();
        newUnicell.ParentUnicell = unicell;
        Initialise(newUnicell, unicell.Species);
        switch (newUnicell.Species)
        {
            case UnicellSpecies.Blue:
                newUnicell.transform.parent = BlueUnicellsGameObject.transform;
                break;
            case UnicellSpecies.Pink:
                newUnicell.transform.parent = PinkUnicellsGameObject.transform;
                break;
            case UnicellSpecies.Yellow:
                newUnicell.transform.parent = YellowUnicellsGameObject.transform;
                break;
            case UnicellSpecies.Green:
                newUnicell.transform.parent = GreenUnicellsGameObject.transform;
                break;
            case UnicellSpecies.Purple:
                newUnicell.transform.parent = PurpleUnicellsGameObject.transform;
                break;
            case UnicellSpecies.Red:
                newUnicell.transform.parent = RedUnicellsGameObject.transform;
                break;
            case UnicellSpecies.Orange:
                newUnicell.transform.parent = OrangeUnicellsGameObject.transform;
                break;
        }
        RegisterUnicell(newUnicell);

        unicell.Rigidbody.linearVelocity = -randomDirection * 10;
        newUnicell.Rigidbody.linearVelocity = randomDirection * 10;
    }
    void HandleUnicellCollision(Unicell unicell)
    {
        switch (unicell)
        {
            case BlueUnicell:
                BlueUnicell blueUnicell = unicell.GetComponent<BlueUnicell>();
                if (unicell.isCollisionWithUnicell)
                {
                    if (blueUnicell.isInflateAbilityActive || blueUnicell.InflationCooldown >= blueUnicell.InflationCooldownThreshold)
                    {
                        if (!blueUnicell.isInflateAbilityActive)
                        {
                            blueUnicell.isInflateAbilityActive = true;
                            blueUnicell.isInflating = true;
                            blueUnicell.InflationCooldown -= blueUnicell.InflationCooldownThreshold;
                        }

                        if (blueUnicell.collidingUnicellClass != null && blueUnicell != null && blueUnicell.collidingUnicellClass.Age > 1 && blueUnicell.Age > 1)
                        {
                            blueUnicell.collidingUnicellClass.CurrentHealth -= blueUnicell.Damage;
                            if (blueUnicell.collidingUnicellClass.CurrentHealth <= 0)
                            {
                                blueUnicell.XP += 50;
                            }
                            else
                            {
                                if (UIManager.Instance.StatPanelTargetUnicell == blueUnicell.collidingUnicellClass)
                                {
                                    UIManager.Instance.ShowUnicellStats(blueUnicell.collidingUnicellClass);
                                }
                            }
                        }
                    }
                }
                break;
            case PinkUnicell:
                PinkUnicell pinkUnicell = unicell.GetComponent<PinkUnicell>();
                if (unicell.isCollisionWithUnicell)
                {
                    if (pinkUnicell.collidingUnicellClass != null && pinkUnicell != null && pinkUnicell.collidingUnicellClass.Age > 1 && pinkUnicell.Age > 1)
                    {
                        pinkUnicell.collidingUnicellClass.CurrentHealth -= pinkUnicell.Damage;
                        if (pinkUnicell.collidingUnicellClass.CurrentHealth <= 0)
                        {
                            pinkUnicell.XP += 50;
                        }
                        else
                        {
                            if (UIManager.Instance.StatPanelTargetUnicell == pinkUnicell.collidingUnicellClass)
                            {
                                UIManager.Instance.ShowUnicellStats(pinkUnicell.collidingUnicellClass);
                            }
                        }
                    }
                }
                break;
            default:
                Unicell Unicell = unicell.GetComponent<Unicell>();
                if (unicell.isCollisionWithUnicell)
                {
                    if (Unicell.collidingUnicellClass != null && Unicell != null && Unicell.collidingUnicellClass.Age > 1 && Unicell.Age > 1)
                    {
                        Unicell.collidingUnicellClass.CurrentHealth -= Unicell.Damage;
                        if (Unicell.collidingUnicellClass.CurrentHealth <= 0)
                        {
                            Unicell.XP += 50;
                        }
                        else
                        {
                            if (UIManager.Instance.StatPanelTargetUnicell == Unicell.collidingUnicellClass)
                            {
                                UIManager.Instance.ShowUnicellStats(Unicell.collidingUnicellClass);
                            }
                        }
                    }
                }
                break;
        }

        if (!unicell.isCollisionWithUnicell)
        {
            if (unicell.collidingFoodClass.isExperience != true)
            {
                unicell.Hunger += 0.5f; // This should be moved to Food or as a serialized variable in editor
                unicell.CurrentHealth = unicell.MaxHealth;
                unicell.collidingFoodClass.isDead = true;
                if (unicell.Hunger >= HungryThreshold && unicell.CurrentState == UnicellState.Hungry)
                {
                    DecideUnicellState(unicell);
                }
            }
            else
            {
                unicell.XP += unicell.collidingFoodClass.ExperienceValue; // This should be moved to Food or as a serialized variable in editor
                unicell.collidingFoodClass.isDead = true;
                if (unicell.XP >= unicell.XP_Threshold)
                {
                    LevelUp(unicell);
                }
            }
        }
    }
    void Initialise(Unicell unicell, Unicell.UnicellSpecies species)
    {
        isSpawningPersistentUnicell = false;
        unicell.Species = species;

        unicell.CurrentState = Unicell.UnicellState.Idle;
        unicell.Rigidbody = unicell.GetComponent<Rigidbody2D>();
        unicell.RandomAngularVelocity = Random.Range(-0.025f, 0.025f);
        unicell.RandomLinearVelocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        unicell.Level = 1;
        unicell.XP = 0;
        unicell.XP_Threshold = 100;

        unicell.MaxHealth = 5;
        unicell.Damage = 1;
        unicell.PregnantScaleThreshold = new Vector2(unicell.BaseScale.x * 2, unicell.BaseScale.y * 2);
        unicell.PregnantTimerThreshold = 0.1f;

        unicell.Hunger = 1;
        unicell.Speed = 5;
        unicell.SpawnLocation = unicell.transform.position;

        unicell.BaseScale = new Vector2(1, 1);
        unicell.transform.localScale = new Vector2(unicell.BaseScale.x, unicell.BaseScale.y);
        unicell.isPregnant = false;
        unicell.ProximityRadius = 4 * unicell.BaseScale.x;

        switch (unicell)
        {
            case BlueUnicell:
                for (int i = 0; i < BlueSpawnLvStat; i++)
                {
                    unicell.XP = unicell.XP_Threshold;
                    LevelUp(unicell);
                }
                break;
            case PinkUnicell:
                for (int i = 0; i < PinkSpawnLvStat; i++)
                {
                    unicell.XP = unicell.XP_Threshold;
                    LevelUp(unicell);
                }
                break;
            case YellowUnicell:
                for (int i = 0; i < YellowSpawnLvStat; i++)
                {
                    unicell.XP = unicell.XP_Threshold;
                    LevelUp(unicell);
                }
                break;
            case GreenUnicell:
                for (int i = 0; i < GreenSpawnLvStat; i++)
                {
                    unicell.XP = unicell.XP_Threshold;
                    LevelUp(unicell);
                }
                break;
            case PurpleUnicell:
                for (int i = 0; i < PurpleSpawnLvStat; i++)
                {
                    unicell.XP = unicell.XP_Threshold;
                    LevelUp(unicell);
                }
                break;
            case RedUnicell:
                for (int i = 0; i < RedSpawnLvStat; i++)
                {
                    unicell.XP = unicell.XP_Threshold;
                    LevelUp(unicell);
                }
                break;
            case OrangeUnicell:
                for (int i = 0; i < OrangeSpawnLvStat; i++)
                {
                    unicell.XP = unicell.XP_Threshold;
                    LevelUp(unicell);
                }
                break;
        }

        if (biolabMenuManager.isBioElderUnlocked && Random.Range(1, 101) == 1)
        {
            unicell.isElderUnicell = true;
            unicell.BaseScale = new Vector2(5, 5);
            unicell.transform.localScale = new Vector2(unicell.BaseScale.x, unicell.BaseScale.y);

            unicell.Damage *= 10;
            unicell.MaxHealth *= 10;
        }
        if (biolabMenuManager.isBioShinyUnlocked && Random.Range(1, 101) == 1)
        {
            unicell.isShinyUnicell = true;
            switch (unicell)
            {
                case BlueUnicell:
                    unicell.GetComponent<SpriteRenderer>().sprite = BlueShiny;
                    break;
                case PinkUnicell:
                    unicell.GetComponent<SpriteRenderer>().sprite = PinkShiny;
                    break;
                case YellowUnicell:
                    unicell.GetComponent<SpriteRenderer>().sprite = YellowShiny;
                    break;
                case GreenUnicell:
                    unicell.GetComponent<SpriteRenderer>().sprite = GreenShiny;
                    break;
                case PurpleUnicell:
                    unicell.GetComponent<SpriteRenderer>().sprite = PurpleShiny;
                    break;
                case OrangeUnicell:
                    unicell.GetComponent<SpriteRenderer>().sprite = OrangeShiny;
                    break;
            }

            unicell.Damage *= 10;
            unicell.MaxHealth *= 10;
        }

        switch (unicell)
        {
            case BlueUnicell:
                BlueUnicell blueUnicell = unicell.GetComponent<BlueUnicell>();
                blueUnicell.InflationCooldownThreshold = 3;
                blueUnicell.InflationScale = new Vector2(unicell.BaseScale.x * 2f, unicell.BaseScale.y * 2f);
                break;
            case PinkUnicell:
                PinkUnicell pinkUnicell = unicell.GetComponent<PinkUnicell>();
                // Set Species-Exclusive Stats Here
                break;
            case YellowUnicell:
                YellowUnicell yellowUnicell = unicell.GetComponent<YellowUnicell>();
                // Set Species-Exclusive Stats Here
                break;
            case GreenUnicell:
                GreenUnicell greenUnicell = unicell.GetComponent<GreenUnicell>();
                // Set Species-Exclusive Stats Here
                break;
            case PurpleUnicell:
                PurpleUnicell purpleUnicell = unicell.GetComponent<PurpleUnicell>();
                // Set Species-Exclusive Stats Here
                break;
            case RedUnicell:
                RedUnicell redUnicell = unicell.GetComponent<RedUnicell>();
                // Set Species-Exclusive Stats Here
                break;
            case OrangeUnicell:
                OrangeUnicell orangeUnicell = unicell.GetComponent<OrangeUnicell>();
                // Set Species-Exclusive Stats Here
                break;
            default:
                Debug.LogError("EntityManager Initialise(none-persistent unicell) - Unknown unicell species");
                break;
        }

        //MutateUnicellStats(unicell);
        unicell.CurrentHealth = unicell.MaxHealth;
        unicell.isPersistent = false;
    }
    void MutateUnicellStats(Unicell unicell) // Generated Mutation Value typically ranges between -2 & 2
    {
        // Try to multiply value to the stat divided by 20

        unicell.DamageMutation = (GenerateMutationValue() * 0.05f) + unicell.Damage;
        unicell.Damage = unicell.DamageMutation;
        unicell.MaxHealthMutation = (GenerateMutationValue() * 0.25f) + unicell.MaxHealth;
        unicell.MaxHealth = unicell.MaxHealthMutation;
    }
    void InitialiseFood(Food food)
    {
        food.RandomAngularVelocity = Random.Range(-0.05f, 0.05f);
        food.RandomLinearVelocity = new Vector2(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f));
        food.Rigidbody = food.GetComponent<Rigidbody2D>();
    }
    void InitialiseExperienceSquare(Food food, float experienceSquareXPValue)
    {
        food.RandomAngularVelocity = Random.Range(-0.05f, 0.05f);
        food.RandomLinearVelocity = new Vector2(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f));
        food.Rigidbody = food.GetComponent<Rigidbody2D>();
        food.ExperienceValue = experienceSquareXPValue;
    }
    public void SpawnUnicells(UnicellSpecies species, int Amount)
    {
        for (int x = 0; x < Amount; x++)
        {
            switch (species)
            {
                case UnicellSpecies.Blue:
                    initialUnicell = Instantiate(blueUnicellPrefab).GetComponent<Unicell>();
                    initialUnicell.transform.parent = BlueUnicellsGameObject.transform;
                    Initialise(initialUnicell, Unicell.UnicellSpecies.Blue);
                    break;
                case UnicellSpecies.Pink:
                    initialUnicell = Instantiate(pinkUnicellPrefab).GetComponent<Unicell>();
                    initialUnicell.transform.parent = PinkUnicellsGameObject.transform;
                    Initialise(initialUnicell, Unicell.UnicellSpecies.Pink);
                    break;
                case UnicellSpecies.Yellow:
                    initialUnicell = Instantiate(yellowUnicellPrefab).GetComponent<Unicell>();
                    initialUnicell.transform.parent = YellowUnicellsGameObject.transform;
                    Initialise(initialUnicell, Unicell.UnicellSpecies.Yellow);
                    break;
                case UnicellSpecies.Green:
                    initialUnicell = Instantiate(greenUnicellPrefab).GetComponent<Unicell>();
                    initialUnicell.transform.parent = GreenUnicellsGameObject.transform;
                    Initialise(initialUnicell, Unicell.UnicellSpecies.Green);
                    break;
                case UnicellSpecies.Purple:
                    initialUnicell = Instantiate(purpleUnicellPrefab).GetComponent<Unicell>();
                    initialUnicell.transform.parent = PurpleUnicellsGameObject.transform;
                    Initialise(initialUnicell, Unicell.UnicellSpecies.Purple);
                    break;
                case UnicellSpecies.Red:
                    initialUnicell = Instantiate(redUnicellPrefab).GetComponent<Unicell>();
                    initialUnicell.transform.parent = RedUnicellsGameObject.transform;
                    Initialise(initialUnicell, Unicell.UnicellSpecies.Red);
                    break;
                case UnicellSpecies.Orange:
                    initialUnicell = Instantiate(orangeUnicellPrefab).GetComponent<Unicell>();
                    initialUnicell.transform.parent = OrangeUnicellsGameObject.transform;
                    Initialise(initialUnicell, Unicell.UnicellSpecies.Orange);
                    break;
            }
            initialUnicell.transform.position = new Vector2((Random.value * Amount * 10) - (Amount / 2f), (Random.value * Amount * 10) - (Amount / 2f));
            initialUnicell.CurrentState = UnicellState.Idle;
            RegisterUnicell(initialUnicell);
        }
    }
    void CheckUnicellsForHunger()
    {
        foreach (Unicell unicell in unicellList)
        {
            if (unicell.CurrentState != UnicellState.Fighting && !unicell.isHungry)
            {
                if (isHungry(unicell))
                {
                    ChangeState(unicell, UnicellState.Hungry);
                    unicell.isHungry = true;
                }
            }
        }
    }
    public void CheckUnicellsForPersistence(Unicell unicell)
    {
        if (unicell.isPersistent && !unicell.isLabelled)
        {
            /*UnicellLabelManager.Instance.InitialiseUnicellLabel(unicell);
            unicell.isLabelled = true;*/

            //Debug.Log("I am persistent, my name is " + unicell.UnicellName);
            //unicell.isLabelled = true;
        }
    }
    void DecideUnicellState(Unicell unicell)
    {
        // Fight, Idle, Flee, Hungry, Reproduce

        if (unicell.NearestUnicell != null)
        {
            if (/*unicell.CurrentHealth < unicell.MaxHealth / 5f*/ false)
            {
                ChangeState(unicell, UnicellState.Fleeing); // Shows a fear of death from Unicells, even if inevitable it would still make them feel more sentient and convincing
            }
            else
            {
                ChangeState(unicell, UnicellState.Fighting); 
            }
        }
        else
        {
            if (unicell.CurrentHealth < unicell.MaxHealth)
            {
                ChangeState(unicell, UnicellState.Idle); // Idle regenerates health
            }
            else
            {
                ChangeState(unicell, UnicellState.Hungry); // Default behaviour
            }
        }
    }
    public void UpgradeUnicellStats(string UnicellType, string UpgradeType)
    {
        for (int i = 0; i < unicellList.Count; i++)
        {
            Unicell unicell = unicellList[i];

            switch (unicell)
            {
                case BlueUnicell:
                    if (UnicellType == "Blue")
                    {
                        switch (UpgradeType)
                        {
                            case "Damage":
                                unicell.Damage *= 1.2f;
                                break;
                            case "MaxHP":
                                unicell.MaxHealth *= 1.2f;
                                break;
                        }
                    }
                    break;
                case PinkUnicell:
                    if (UnicellType == "Pink")
                    {
                        switch (UpgradeType)
                        {
                            case "Damage":
                                unicell.Damage *= 1.2f;
                                break;
                            case "MaxHP":
                                unicell.MaxHealth *= 1.2f;
                                break;
                        }
                    }
                    break;
                case YellowUnicell:
                    if (UnicellType == "Yellow")
                    {
                        switch (UpgradeType)
                        {
                            case "Damage":
                                unicell.Damage *= 1.2f;
                                break;
                            case "MaxHP":
                                unicell.MaxHealth *= 1.2f;
                                break;
                        }
                    }
                    break;
                case GreenUnicell:
                    if (UnicellType == "Green")
                    {
                        switch (UpgradeType)
                        {
                            case "Damage":
                                unicell.Damage *= 1.2f;
                                break;
                            case "MaxHP":
                                unicell.MaxHealth *= 1.2f;
                                break;
                        }
                    }
                    break;
                case PurpleUnicell:
                    if (UnicellType == "Purple")
                    {
                        switch (UpgradeType)
                        {
                            case "Damage":
                                unicell.Damage *= 1.2f;
                                break;
                            case "MaxHP":
                                unicell.MaxHealth *= 1.2f;
                                break;
                        }
                    }
                    break;
                case RedUnicell:
                    if (UnicellType == "Red")
                    {
                        switch (UpgradeType)
                        {
                            case "Damage":
                                unicell.Damage *= 1.2f;
                                break;
                            case "MaxHP":
                                unicell.MaxHealth *= 1.2f;
                                break;
                        }
                    }
                    break;
                case OrangeUnicell:
                    if (UnicellType == "Orange")
                    {
                        switch (UpgradeType)
                        {
                            case "Damage":
                                unicell.Damage *= 1.2f;
                                break;
                            case "MaxHP":
                                unicell.MaxHealth *= 1.2f;
                                break;
                        }
                    }
                    break;
                default:
                    Debug.LogError("EntityManager UpdateUnicellStats() Unknown Unicell for stat update");
                    break;
            }

            unicell.labelUpdateRequested = true;
        }

        switch (UnicellType)
        {
            case "Blue":
                switch (UpgradeType)
                {
                    case "Damage":
                        BlueDamageStatMultiplier *= 1.2f;
                        break;
                    case "MaxHP":
                        BlueMaxHPStatMultiplier *= 1.2f;
                        break;
                    case "SpawnLv":
                        BlueSpawnLvStat ++;
                        break;
                }
                break;
            case "Pink":
                switch (UpgradeType)
                {
                    case "Damage":
                        PinkDamageStatMultiplier *= 1.2f;
                        break;
                    case "MaxHP":
                        PinkMaxHPStatMultiplier *= 1.2f;
                        break;
                    case "SpawnLv":
                        PinkSpawnLvStat++;
                        break;
                }
                break;
            case "Yellow":
                switch (UpgradeType)
                {
                    case "Damage":
                        YellowDamageStatMultiplier *= 1.2f;
                        break;
                    case "MaxHP":
                        YellowMaxHPStatMultiplier *= 1.2f;
                        break;
                    case "SpawnLv":
                        YellowSpawnLvStat ++;
                        break;
                }
                break;
            case "Green":
                switch (UpgradeType)
                {
                    case "Damage":
                        GreenDamageStatMultiplier *= 1.2f;
                        break;
                    case "MaxHP":
                        GreenMaxHPStatMultiplier *= 1.2f;
                        break;
                    case "SpawnLv":
                        GreenSpawnLvStat++;
                        break;
                }
                break;
            case "Purple":
                switch (UpgradeType)
                {
                    case "Damage":
                        PurpleDamageStatMultiplier *= 1.2f;
                        break;
                    case "MaxHP":
                        PurpleMaxHPStatMultiplier *= 1.2f;
                        break;
                    case "SpawnLv":
                        PurpleSpawnLvStat++;
                        break;
                }
                break;
            case "Red":
                switch (UpgradeType)
                {
                    case "Damage":
                        RedDamageStatMultiplier *= 1.2f;
                        break;
                    case "MaxHP":
                        RedMaxHPStatMultiplier *= 1.2f;
                        break;
                    case "SpawnLv":
                        RedSpawnLvStat++;
                        break;
                }
                break;
            case "Orange":
                switch (UpgradeType)
                {
                    case "Damage":
                        OrangeDamageStatMultiplier *= 1.2f;
                        break;
                    case "MaxHP":
                        OrangeMaxHPStatMultiplier *= 1.2f;
                        break;
                    case "SpawnLv":
                        OrangeSpawnLvStat++;
                        break;
                }
                break;
        }
    }
    public void UnrenderUnicells()
    {

    }
    public void RenderUnicells()
    {

    }
    #endregion

    // Returning Functions
    #region ReturningFunctions
    Food FindNearestFood(Unicell unicell, float searchRadius)
    {
        // Perform an overlap circle query to find all colliders within the search radius
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(unicell.transform.position, searchRadius);
        Food nearestFood = null;
        float minDistance = float.MaxValue;

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Food")
            {
                Food Food = hitCollider.GetComponent<Food>();
                if (Food == null) continue;

                float distance = Vector2.Distance(unicell.transform.position, Food.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestFood = Food;
                }
            }
        }

        return nearestFood;
    }
    Unicell FindNearestUnicell(Unicell unicell, float searchRadius)
    {
        // Perform an overlap circle query to find all colliders within the search radius
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(unicell.transform.position, searchRadius);
        Unicell nearestUnicell = null;
        float minDistance = float.MaxValue;

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.tag != "Unicell") // Checks if nearby collider is NOT a unicell to which it will continue
            {
                continue;
            }
            else
            {
                Unicell hitUnicell = hitCollider.GetComponent<Unicell>();
                // Checks if they are a parent or if you are a parent of the unicell. Prevents mitosis ending badly
                if (hitUnicell == null || hitUnicell == unicell || hitUnicell.ParentUnicell == unicell || unicell.ParentUnicell == hitUnicell) continue;

                float distance = Vector2.Distance(unicell.transform.position, hitUnicell.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestUnicell = hitUnicell;
                }
            }
        }

        return nearestUnicell;
    }
    bool isHungry(Unicell unicell)
    {
        if (unicell.Hunger <= HungryThreshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    float GenerateMutationValue()
    {
        // Marsaglia polar method to generate a standard normal (Gaussian) distribution
        float u, v, s;
        do
        {
            u = 2.0f * Random.Range(0.0f, 1.0f) - 1.0f;
            v = 2.0f * Random.Range(0.0f, 1.0f) - 1.0f;
            s = u * u + v * v;
        }
        while (s >= 1.0f || s == 0.0f);

        float mul = Mathf.Sqrt(-2.0f * Mathf.Log(s) / s);
        float randStdNormal = u * mul;

        float randNormal = 0 + 1 * randStdNormal; // Mean + Standard Deviation + randStdNormal
        return randNormal;
    }
    public float CurrentUpgradeValue(string UpgradeType, string UnicellSelected)
    {
        switch (UpgradeType)
        {
            case "Damage":
                switch (UnicellSelected)
                {
                    case "Blue":
                        return BlueDamageStatMultiplier;
                    case "Pink":
                        return PinkDamageStatMultiplier;
                    case "Yellow":
                        return YellowDamageStatMultiplier;
                    case "Green":
                        return GreenDamageStatMultiplier;
                    case "Purple":
                        return PurpleDamageStatMultiplier;
                    case "Red":
                        return RedDamageStatMultiplier;
                    case "Orange":
                        return OrangeDamageStatMultiplier;
                    default:
                        Debug.LogError("EntityManager CurrentUpgradeValue() - Unknown UnicellSelected for return");
                        return 0;
                }
            case "MaxHealth":
                switch (UnicellSelected)
                {
                    case "Blue":
                        return BlueMaxHPStatMultiplier;
                    case "Pink":
                        return PinkMaxHPStatMultiplier;
                    case "Yellow":
                        return YellowMaxHPStatMultiplier;
                    case "Green":
                        return GreenMaxHPStatMultiplier;
                    case "Purple":
                        return PurpleMaxHPStatMultiplier;
                    case "Red":
                        return RedMaxHPStatMultiplier;
                    case "Orange":
                        return OrangeMaxHPStatMultiplier;
                    default:
                        Debug.LogError("EntityManager CurrentUpgradeValue() - Unknown UnicellSelected for return");
                        return 0;
                }
            case "SpawnLv":
                switch (UnicellSelected)
                {
                    case "Blue":
                        return BlueSpawnLvStat;
                    case "Pink":
                        return PinkSpawnLvStat;
                    case "Yellow":
                        return YellowSpawnLvStat;
                    case "Green":
                        return GreenSpawnLvStat;
                    case "Purple":
                        return PurpleSpawnLvStat;
                    case "Red":
                        return RedSpawnLvStat;
                    case "Orange":
                        return OrangeSpawnLvStat;
                    default:
                        Debug.LogError("EntityManager CurrentUpgradeValue() - Unknown UnicellSelected for return");
                        return 0;
                }
            case "Elder":
                switch (UnicellSelected)
                {
                    case "Blue":
                        return BlueElderStatMultiplier;
                    case "Pink":
                        return PinkElderStatMultiplier;
                    case "Yellow":
                        return YellowElderStatMultiplier;
                    case "Green":
                        return GreenElderStatMultiplier;
                    case "Purple":
                        return PurpleElderStatMultiplier;
                    case "Red":
                        return RedElderStatMultiplier;
                    case "Orange":
                        return OrangeElderStatMultiplier;
                    default:
                        Debug.LogError("EntityManager CurrentUpgradeValue() - Unknown UnicellSelected for return");
                        return 0;
                }
            case "Shiny":
                switch (UnicellSelected)
                {
                    case "Blue":
                        return BlueShinyStatMultiplier;
                    case "Pink":
                        return PinkShinyStatMultiplier;
                    case "Yellow":
                        return YellowShinyStatMultiplier;
                    case "Green":
                        return GreenShinyStatMultiplier;
                    case "Purple":
                        return PurpleShinyStatMultiplier;
                    case "Red":
                        return RedShinyStatMultiplier;
                    case "Orange":
                        return OrangeShinyStatMultiplier;
                    default:
                        Debug.LogError("EntityManager CurrentUpgradeValue() - Unknown UnicellSelected for return");
                        return 0;
                }
            case "King":
                switch (UnicellSelected)
                {
                    case "Blue":
                        return BlueKingStatMultiplier;
                    case "Pink":
                        return PinkKingStatMultiplier;
                    case "Yellow":
                        return YellowKingStatMultiplier;
                    case "Green":
                        return GreenKingStatMultiplier;
                    case "Purple":
                        return PurpleKingStatMultiplier;
                    case "Red":
                        return RedKingStatMultiplier;
                    case "Orange":
                        return OrangeKingStatMultiplier;
                    default:
                        Debug.LogError("EntityManager CurrentUpgradeValue() - Unknown UnicellSelected for return");
                        return 0;
                }
            default:
                Debug.LogError("EntityManager CurrentUpgradeValue() - Unknown UpgradeType for return");
                return 0;
        }
    }
    #endregion

    // Registration Functions
    #region RegistrationFunctions
    void RegisterUnicell(Unicell unicell)
    {
        //unicellsToAdd.Add(unicell);
        //UnicellLabelManager unicellLabelManager = GetComponent<UnicellLabelManager>();
        //UnicellLabelManager.InitialiseUnicellLabel(unicell);

        unicellList.Add(unicell);
    }
    void UnregisterUnicell(Unicell unicell)
    {
        unicellsToRemove.Add(unicell);
    }
    void RegisterFood(Food food)
    {
        foodList.Add(food);
    }
    void UnregisterFood(Food food)
    {
        foodList.Remove(food);
    }
    #endregion
}