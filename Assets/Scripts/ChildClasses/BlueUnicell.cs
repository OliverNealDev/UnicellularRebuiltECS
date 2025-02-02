using UnityEngine;

public class BlueUnicell : Unicell
{
    public bool isInflateAbilityActive { get; set; }
    public bool isInflating { get; set; }
    public float InflationCooldown { get; set; }
    public float InflationCooldownThreshold { get; set; }
    public Vector2 InflationScale { get; set; }
}

