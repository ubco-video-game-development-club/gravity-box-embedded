using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScoreSystem))]
public class GameManager : MonoBehaviour
{
    public static ScoreSystem ScoreSystem { get { return scoreSystem; } }
    private static ScoreSystem scoreSystem;
    public static WaveSystem WaveSystem { get { return waveSystem; } }
    private static WaveSystem waveSystem;

    private static GameManager singleton;

    void Awake() 
    {
        if(singleton != null) 
        {
            Destroy(gameObject);
            return;
        }
        singleton = this;

        scoreSystem = GetComponent<ScoreSystem>();
        waveSystem = GetComponent<WaveSystem>();
    }
}
