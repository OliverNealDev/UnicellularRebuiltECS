using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float GameRuntime {  get; private set; }
    [SerializeField] private List<float> fpsList = new List<float>();
    private float AverageFPSfloat;
    [SerializeField] private EntityManager entityMananger;
    [SerializeField] private EconomyManager economyManager;

    void Start()
    {
        GameRuntime = 0;
        //InvokeRepeating("AverageFPS", 12, 12);

        //entityMananger.SpawnUnicells(Unicell.UnicellSpecies.Blue, 500);
        //entityMananger.SpawnUnicells(Unicell.UnicellSpecies.Pink, 500);

        //economyManager.IncrementUnicellPopulation("Blue");

        /*for (int i = 0; i < 100; i++)
        {
            economyManager.IncrementUnicellPopulation("Blue");
        }

        for (int i = 0; i < 100; i++)
        {
            economyManager.IncrementUnicellPopulation("Pink");
        }*/
    }

    void Update()
    {
        GameRuntime += Time.deltaTime;

        //float FpsCapture = 1 / Time.deltaTime;
        //fpsList.Add(FpsCapture);
        //
        //)

        //#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            for (int i = 0; i < 100; i++)
            {
                economyManager.IncrementUnicellPopulation("Blue");
            }
            for (int i = 0; i < 100; i++)
            {
                economyManager.IncrementUnicellPopulation("Pink");
            }
            for (int i = 0; i < 100; i++)
            {
                economyManager.IncrementUnicellPopulation("Yellow");
            }
            for (int i = 0; i < 100; i++)
            {
                economyManager.IncrementUnicellPopulation("Green");
            }
            for (int i = 0; i < 100; i++)
            {
                economyManager.IncrementUnicellPopulation("Purple");
            }
            for (int i = 0; i < 100; i++)
            {
                economyManager.IncrementUnicellPopulation("Orange");
            }
        }

        if (Input.GetKeyDown(KeyCode.Plus))
        {
            for (int i = 0; i < 100; i++)
            {
                economyManager.IncrementUnicellPopulation("Green");
            }
            for (int i = 0; i < 100; i++)
            {
                economyManager.IncrementUnicellPopulation("Purple");
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            for (int i = 0; i < 100; i++)
            {
                economyManager.IncrementUnicellPopulation("Orange");
            }
        }

        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            economyManager.DoubleCoins();
        }

        if (Input.GetKeyDown(KeyCode.Comma))
        {
            Time.timeScale -= 1f;
        }

        if (Input.GetKeyDown(KeyCode.Period))
        {
            Time.timeScale += 1f;
        }

        //#endif
    }

    void AverageFPS()
    {
        foreach (float fps in fpsList)
        {
            AverageFPSfloat += fps;
        }
        AverageFPSfloat = AverageFPSfloat / fpsList.Count;
        Debug.Log("Average FPS of " + AverageFPSfloat);
        fpsList.Clear();
    }
}
