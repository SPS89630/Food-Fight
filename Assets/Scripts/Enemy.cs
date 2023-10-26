using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int MaxHP = 100;
    public int CurrentHP = 100;
    public float speed = 10;
    public FruitID currentFruit;
    public float throwDistance = 5.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentHP <= 0)
        {
            GameManager.Instance.enemiesKilled++;
            Destroy(gameObject);
        }

        //Move until a distance of 1 from the player
        if(Vector3.Distance(transform.position, PlayerController.Instance.transform.position) > throwDistance)
        {
            transform.LookAt(PlayerController.Instance.transform);
            transform.position = Vector3.MoveTowards(transform.position, PlayerController.Instance.transform.position, speed * Time.deltaTime);
        }
        else
        {
            //Debug.Log("Reached player!");
        }
    }
}
