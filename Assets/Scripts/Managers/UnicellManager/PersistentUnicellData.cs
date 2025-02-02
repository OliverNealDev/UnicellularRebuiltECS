using UnityEngine;

[System.Serializable]
public class PersistentUnicellData
{
    public string Species;

    public float Damage;
    public float MaxHealth;

    public int Level;
    public float XP;
    public int XP_Threshold;

    public Vector2 BaseScale;

    public PersistentUnicellData(string species, float damage, float maxHealth, int level, float xp, int xp_threshold, Vector2 basescale)
    {
        this.Species = species;

        this.Damage = damage;
        this.MaxHealth = maxHealth;

        this.Level = level;
        this.XP = xp;
        this.XP_Threshold = xp_threshold;

        this.BaseScale = basescale;
    }
}
