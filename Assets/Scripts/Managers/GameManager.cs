using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public FruitScriptable[] fruits;
    
    public int currentScore;
    public int currentWave;
    public int enemiesKilled;

    [Header("UI")]
    public TextMeshProUGUI pointUI;

    public static GameManager Instance { get; private set; }
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pointUI.text = string.Format("Score: {0}\nCurrent Wave: {1}", currentScore.ToString("D8"), currentWave.ToString("D2"));
    }
}
