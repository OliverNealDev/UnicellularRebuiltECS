using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class CoinsPerSecondController : MonoBehaviour
{
    public TextMeshProUGUI CoinsPerSecondText;
    public float TimerThreshold = 1f; // Time interval to update display (e.g., 1 second)

    private float updateTimer;
    private Queue<long> coinQueue = new Queue<long>(); // Stores last 30 seconds of coin additions
    private long rollingSum = 0; // Sum of the queue for fast average calculation
    private int averagingWindow = 30; // Number of seconds for smoothing

    private long coinsToProcess;

    void Start()
    {
        // Initialize queue with zeros to avoid incorrect first readings
        for (int i = 0; i < averagingWindow; i++)
        {
            coinQueue.Enqueue(0);
        }
    }

    public void AddCoinsToProcess(long coins)
    {
        coinsToProcess += coins;
    }

    void Update()
    {
        updateTimer += Time.deltaTime;

        if (updateTimer >= TimerThreshold)
        {
            updateTimer = 0;
            SlowUpdate();
        }
    }

    void SlowUpdate()
    {
        // Remove the oldest entry from the queue
        rollingSum -= coinQueue.Dequeue();
        
        // Add the new value
        coinQueue.Enqueue(coinsToProcess);
        rollingSum += coinsToProcess;

        // Calculate average CPS over last 30 seconds
        float CoinsPerSecond = (float)rollingSum / averagingWindow;
        CoinsPerSecondText.text = Mathf.RoundToInt(CoinsPerSecond) + " p/s";

        // Reset temporary counter
        coinsToProcess = 0;
    }
}