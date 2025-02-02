using UnityEngine;

public class Food : MonoBehaviour
{
    public bool isExperience;
    public enum FoodSpecies { Blue, Pink , Yellow}
    public float Age { get; set; }
    public Rigidbody2D Rigidbody { get; set; }
    public float RandomAngularVelocity { get; set; }
    public Vector2 RandomLinearVelocity { get; set; }
    public bool isDead { get; set; }
    public float ExperienceValue { get; set; }

    /*private void InitialiseFood()
    {
        Age = 0;
    }*/
}
