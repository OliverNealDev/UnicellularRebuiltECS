using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private float AudioLogicTimer;
    private float AudioLogicTimerThreshold;

    [SerializeField] private AudioClip UnicellularSong;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        //AudioLogicTimer += Time.deltaTime;
        /*if (AudioLogicTimer > AudioLogicTimerThreshold)
        {
            AudioLogicUpdate();
        }*/
    }

    // Update is called once per frame
    void AudioLogicUpdate()
    {
        
    }
}
