using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    // Start is called before the first frame update
    public EnemyID ID;
    public float speed = 10;
    
    public float throwDistance = 5.0f;
    void Start()
    {
        currentFruit = FruitID.APPLE;
    }

    public void Initialize(EnemyID id)
    {
        ID = id;
        CurrentHP = GameManager.Instance.enemies[(int)ID].health;
        MaxHP = GameManager.Instance.enemies[(int)ID].health;
        speed = GameManager.Instance.enemies[(int)ID].speed;
        throwDistance = GameManager.Instance.enemies[(int)ID].throwDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentHP <= 0)
        {
            GameManager.Instance.enemiesKilled++;
            Destroy(gameObject);
        }
        
        if(ID != EnemyID.NONE)
        {
            foreach(var ai in GameManager.Instance.enemies[(int)ID].ai)
            {
                ProcessAI(ai);
            }
        }
    }

    void ProcessAI(EnemyAIArguments enemyAI)
    {
        switch (enemyAI)
        {
            case EnemyAIArguments.THROW:
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
            break;
            case EnemyAIArguments.EXPLODE:
                //Move until a distance of 1 from the player
                if(Vector3.Distance(transform.position, PlayerController.Instance.transform.position) > 1)
                {
                    transform.LookAt(PlayerController.Instance.transform);
                    transform.position = Vector3.MoveTowards(transform.position, PlayerController.Instance.transform.position, speed * Time.deltaTime);
                }
                else
                {
                    //explode!!!
                }
            break;
        }
    }
}
