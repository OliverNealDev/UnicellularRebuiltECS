using System.Net;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;

public class Unicell : MonoBehaviour
{
    // Species
    public enum UnicellSpecies { Blue, Pink, Yellow, Green, Purple, Red, Orange }
    public UnicellSpecies Species { get; set; }

    // Transforms & Components
    public Vector3 Position { get; set; }
    public Rigidbody2D Rigidbody { get; set; }
    public float RandomAngularVelocity { get; set; }
    public Vector2 RandomLinearVelocity { get; set; }
    public Vector2 SpawnLocation { get; set; } 

    // Levelling Variables
    public int Level { get; set; }
    public float XP { get; set; }
    public int XP_Threshold { get; set; }

    // Statistic Variables
    public bool isPersistent;
    public Unicell ParentUnicell { get; set; }
    public float Age { get; set; }
    public float Hunger { get; set; }
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    public float HealthRegen { get; set; }
    public float Damage { get; set; }
    public float Speed { get; set; }
    public float AuraPerSecond { get; set; }
    public string UnicellName { get; set; }
    public float UnicellValue { get; set; }

    // Scale Variables
    public Vector2 BaseScale { get; set; }
    public Vector2 PregnantScaleThreshold { get; set; }
    public float ProximityRadius { get; set; }

    // Mutation Values
    public float DamageMutation;
    public float MaxHealthMutation;
    public float SpeedMutation;
    public float ProximityRadiusMutation;

    // Unicell type values
    public bool isElderUnicell;
    public bool isShinyUnicell;
    public bool isKingUnicell;

    // Macro logic variables
    public enum UnicellState { Idle, Fighting, Fleeing, Hungry, Reproducing}
    public UnicellState CurrentState { get; set; }
    public bool isPregnant { get; set; }
    public float PregnantTimer;
    public float PregnantTimerThreshold;
    public bool IsDead { get; set; }

    public bool isLookingAtSomething { get; set; }
    public Vector3 LookingAtTarget {  get; set; }

    // Hungry Variables
    public bool hasFoodTarget { get; set; } // Whether the Unicell has found and picked an eligible food object
    private float findFoodTargetTimer { get; set; } // Timer to check for food
    [SerializeField] private float findFoodTargetThreshold { get; set; } // Trigger time to check for food
    public bool isAtRandomLocation;
    public Vector2 RandomLocation;
    public bool isHungry;

    // Proximity Variables
    public Food NearestFood { get; set; }
    public Unicell NearestUnicell { get; set; }

    // Collision Variables
    public bool isColliding;
    public Collision2D collidingObjectCollision;
    public Unicell collidingUnicellClass;
    public Food collidingFoodClass;
    public bool isCollisionWithUnicell;

    // UI/Mouse Variables
    public bool isClickedOn;
    public bool isHovered;

    public bool isLabelled;
    public bool labelUpdateRequested;

    // Logic (exceptions to using EntityManager)
    void OnMouseOver()
    {
        if (!isHovered)
        {
            SelectSquareManager.Instance.InstantiateSelectSquare(this);
            isHovered = true;
        }

        UIManager.Instance.ShowUnicellStats(this);

        if (Input.GetMouseButtonDown(0))
        {
            //isClickedOn = true;

            UIManager.Instance.LockUnicellStats(this);
            CameraManager.Instance.FollowUnicell(this);
        }
    }

    private void OnMouseExit()
    {
        isHovered = false;

        UIManager.Instance.HideUnicellStats();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collidingObjectCollision = collision;
        if (collidingObjectCollision.gameObject.tag == "Unicell")
        {
            isColliding = true;
            collidingUnicellClass = collision.gameObject.GetComponent<Unicell>();
            isCollisionWithUnicell = true;

            switch (this)
            {
                case BlueUnicell:
                    BlueUnicell blueUnicell = GetComponent<BlueUnicell>();
                    Vector2 directionB = (collision.transform.position - transform.position).normalized;
                    if (blueUnicell.isInflateAbilityActive || blueUnicell.InflationCooldown >= blueUnicell.InflationCooldownThreshold || collision.gameObject.GetComponent<Unicell>().Age < 1)
                    {
                        collision.rigidbody.linearVelocity += directionB * 15;
                    }
                    else
                    {
                        collision.rigidbody.linearVelocity += directionB;
                    }
                    break;
                case PinkUnicell:
                    PinkUnicell pinkUnicell = GetComponent<PinkUnicell>();
                    Vector2 directionP = (collision.transform.position - transform.position).normalized;

                    if (Rigidbody.angularVelocity >= 72)
                    {
                        collision.rigidbody.linearVelocity += (directionP * 5) * (Rigidbody.angularVelocity / 144); // Up to 5 bonus direction multiplier
                    }
                    else
                    {
                        collision.rigidbody.linearVelocity += (directionP * 5);
                    }

                    break;
                default:
                    Unicell Unicell = GetComponent<Unicell>();
                    Vector2 directionP2 = (collision.transform.position - transform.position).normalized;

                    collision.rigidbody.linearVelocity += (directionP2 * 5);

                    break;
            }
        }
        else
        {
            isColliding = true;
            collidingObjectCollision = collision;
            collidingFoodClass = collision.gameObject.GetComponent<Food>();
            isCollisionWithUnicell = false;
        }
    }

}
