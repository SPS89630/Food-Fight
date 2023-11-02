using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    // Start is called before the first frame update
    public EnemyID ID;
    public float speed = 10;

    public GameObject target;
    
    public float throwDistance = 5.0f;
    void Start()
    {
        //currentFruit = FruitID.APPLE;
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
                if(currentFruit == FruitID.NONE)
                {
                    //Find a fruit stand
                    GameObject fruitStand = FindFruitStand();

                    if(fruitStand == null)
                    {
                        Debug.Log("No fruit stand found!");
                        return;
                    }
                    else
                    {
                        //Move to the fruit stand
                        if(Vector3.Distance(transform.position, fruitStand.transform.position) > 2)
                        {
                            transform.LookAt(fruitStand.transform);
                            transform.position = Vector3.MoveTowards(transform.position, fruitStand.transform.position, speed * Time.deltaTime);
                        }
                        else
                        {
                            //Pick up a fruit
                            int random = UnityEngine.Random.Range(0, fruitStand.GetComponent<FoodStand>().fruitList.Count);
                            FoodStand fruitStandScript = fruitStand.GetComponent<FoodStand>();
                            if(fruitStandScript.foodCount > 0)
                            {
                                fruitStandScript.foodCount--;
                                currentFruit = fruitStandScript.fruitList[random].ID;
                                fruitStandScript.fruitList.RemoveAt(random);
                                Destroy(fruitStandScript.fruitList[random].gameObject);
                            }
                        }
                    }
                    

                    return;
                }
                else
                {
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

    public GameObject FindFruitStand()
    {
        GameObject[] fruitStands = GameObject.FindGameObjectsWithTag("FoodStand");
        GameObject closestFruitStand = null;
        float closestDistance = Mathf.Infinity;

        foreach(var fruitStand in fruitStands)
        {
            float distance = Vector3.Distance(transform.position, fruitStand.transform.position);
            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestFruitStand = fruitStand;
            }
        }

        return closestFruitStand;
    }
}
