using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameManager : MonoBehaviour
{
    public FruitScriptable[] fruits;
    
    [Header("Statistics")]
    public int currentScore;
    public int currentWave;
    public int enemiesKilled;

    [Header("FX")]
    public GameObject effectDisplayPrefab;
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

    public IEnumerator DisplayEffect(Vector3 position, string text, Color color)
    {
        //randomize the y position
        Vector3 randomPosition = new Vector3(position.x + Random.Range(-0.5f, 1.0f), position.y, position.z);
        GameObject effect = Instantiate(effectDisplayPrefab, randomPosition, Quaternion.identity);
        effect.GetComponent<TextMeshPro>().text = text;
        effect.GetComponent<TextMeshPro>().color = color;

        effect.transform.DOMoveY(effect.transform.position.y + 1.0f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        Destroy(effect);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //pointUI.text = string.Format("Score: {0}\nCurrent Wave: {1}", currentScore.ToString("D8"), currentWave.ToString("D2"));
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnFruit();
        }
    }

    public void SpawnFruit()
    {
        int random = Random.Range(1, fruits.Length);
        FruitScriptable data = fruits[random];
        GameObject fruit = Instantiate(data.prefab, PlayerController.Instance.transform.position, Quaternion.identity);
        fruit.GetComponent<Fruit>().ID = (FruitID)random;
    }
}
