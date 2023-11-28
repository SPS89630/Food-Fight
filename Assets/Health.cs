using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public Color[] colors;


    public int currentHealth;
    public int targetHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetHealth = PlayerController.Instance.CurrentHP;

       if(currentHealth < targetHealth)
       {
           currentHealth++;
           healthText.color = colors[1];
       }
       else if(currentHealth > targetHealth)
       {
           currentHealth--;
           healthText.color = colors[2];
       }
       else
       {
         healthText.color = colors[0];
       }

        healthText.text = currentHealth.ToString();
    }
}
