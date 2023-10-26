using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    // Start is called before the first frame update
    public float speed = 10;
    
    public float throwDistance = 5.0f;
    void Start()
    {
        MaxHP = 100;
        CurrentHP = 100;
        currentFruit = FruitID.APPLE;
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
