using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class WaveManager : MonoBehaviour
{
    public List<Wave> waves;
    public List<Enemy> enemies;
    public bool fightingBoss = false;
    public int multiplier;
    public int timeCheck = 0;
    float elaspedTime;
    public TimeSpan waveTime = new TimeSpan();
    float maxTime = Calculation.SecondsToFrames(60);
    bool firstSpawn = false;
    public static WaveManager Instance { get; private set; }
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
        waveTime = TimeSpan.FromMinutes(1);
        InvokeRepeating("SpawnWave", 0, 3f);
    }

    void SpawnWave()
    {
        //spawn enemies
        if(!fightingBoss)
        {
            if(Calculation.Chance(GetSpawnChance(GameManager.Instance.currentWave)) || firstSpawn)
            {
                //spawn enemy in a position around a circle
                Debug.Log("Spawned enemy!");

                firstSpawn = false;

                Vector3 position = Calculation.RandomPointOnCircle(new Vector3(0, 0.75f, 0f), 50.0f);
                int enemyIndex = UnityEngine.Random.Range(0, waves[GameManager.Instance.currentWave].enemies.Count);
                EnemyID chosenEnemy = waves[GameManager.Instance.currentWave].enemies[enemyIndex];
                
                GameObject enemy = Instantiate(GameManager.Instance.enemies[(int)chosenEnemy].prefab, position, Quaternion.identity);
                Enemy enemyScript = enemy.GetComponent<Enemy>();

                enemyScript.Initialize(chosenEnemy);
                enemies.Add(enemyScript);
            }
            else
            {
                //spawn nothing
            }
        }

        //if time is up, spawn boss
        if(waveTime.TotalSeconds <= 0)
        {
            //spawn boss
            fightingBoss = true;
            Debug.Log("Spawned boss!");
            Vector3 position = Calculation.RandomPointOnCircle(new Vector3(0, 0.75f, 0f), 50.0f);
            GameObject enemy = Instantiate(GameManager.Instance.enemies[(int)waves[GameManager.Instance.currentWave].boss].prefab, position, Quaternion.identity);
            Enemy enemyScript = enemy.GetComponent<Enemy>();

            enemyScript.Initialize(waves[GameManager.Instance.currentWave].boss);
            enemies.Add(enemyScript);
        }

        //update wave
        //spawn next wave
    }

    int GetSpawnChance(int wave)
    {
        float timeBonus = Calculation.PercentXOfY(elaspedTime, maxTime) * 0.1f;
        float waveBonus = wave * 0.1f;
        float scoreBonus = GameManager.Instance.currentScore + 1 * 0.1f;
        float killBonus = GameManager.Instance.enemiesKilled + 1 * 0.1f;

        float totalBonus = timeBonus + waveBonus + scoreBonus + killBonus;

        Debug.Log("Total bonus: " + totalBonus);

        return Mathf.RoundToInt(totalBonus);    
    }

    // Update is called once per frame
    void Update()
    {
        ElapseTime();
    }

    void ElapseTime()
    {
        elaspedTime += Time.deltaTime;
        waveTime = waveTime.Subtract(TimeSpan.FromSeconds(Time.deltaTime));

        if(enemies.Any())
        {
            multiplier = 1;
        }
        else
        {
            multiplier = 10;
        }

        //every second add 1 score
        if(timeCheck != Mathf.RoundToInt(elaspedTime))
        {
            timeCheck = Mathf.RoundToInt(elaspedTime);
            GameManager.Instance.currentScore += multiplier;
        }
    }

    [System.Serializable]
    public struct Wave
    {
        public List<EnemyID> enemies;
        public EnemyID boss;
        public float spawnRate;
    }
}
