using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI waveScoreText;
    public TextMeshProUGUI timeText;

    public int currentScore;
    public int targetScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = GameManager.Instance.currentScore;

       if(currentScore < targetScore)
       {
           currentScore++;
       }
       else if(currentScore > targetScore)
       {
           currentScore--;
       }

        waveScoreText.text = GameManager.Instance.currentWave.ToString("D2") + "\n" + currentScore.ToString("D8");
        timeText.text = WaveManager.Instance.waveTime.ToString("mm\\:ss");
    }
}
