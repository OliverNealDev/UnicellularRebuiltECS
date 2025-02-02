using UnityEngine;

public class TargetEnvironmentSizeManager : MonoBehaviour
{
    [SerializeField] private Transform BlueUnicells;
    [SerializeField] private Transform PinkUnicells;
    [SerializeField] private Transform YellowUnicells;

    private float EnvironmentTimer;
    [SerializeField] private float EnvironmentTimerThreshold;

    [SerializeField] private int UnicellPopulation;
    [SerializeField] private int UnitCount;
    [SerializeField] private float UnitsSquaredPerUnicell;

    [SerializeField] private float GrowthRate;

    void Start()
    {
        EnvironmentTimer = 0;
    }

    private void Update()
    {
        EnvironmentTimer += Time.deltaTime;
        if (EnvironmentTimer > EnvironmentTimerThreshold)
        {
            EnvironmentTimer -= EnvironmentTimerThreshold;
            EnvironmentLogicUpdate();
        }
    }

    private void FixedUpdate()
    {
        transform.localScale += new Vector3(GrowthRate / 100, GrowthRate / 100, 0);
    }

    void EnvironmentLogicUpdate()
    {
        UnicellPopulation = BlueUnicells.childCount + PinkUnicells.childCount + YellowUnicells.childCount;
        UnitCount = Mathf.FloorToInt(((16 * transform.localScale.x) * (9 * transform.localScale.y)) / UnitsSquaredPerUnicell);

        if (UnitCount < UnicellPopulation)
        {
            if (UnicellPopulation - UnitCount > UnitCount / 5)
            GrowthRate = 1;
        }
        else if (UnitCount > UnicellPopulation)
        {
            if (UnitCount - UnicellPopulation > UnitCount / 5)
                GrowthRate = -1;
        }
        else
        {
            GrowthRate = 0;
        }
    }
}
