using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    // Start is called before the first frame update
    public EnemyID ID;
    public float speed = 10;
    public float throwAccuracy = 70.0f;

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
                        if(!MoveTowardsTarget(fruitStand.transform.position, 5))
                        {
                            //Pick up a fruit
                            int random = UnityEngine.Random.Range(0, fruitStand.GetComponent<FoodStand>().fruitList.Count);
                            FoodStand fruitStandScript = fruitStand.GetComponent<FoodStand>();
                            currentFruit = fruitStandScript.TakeRandomFruit();
                        }
                    }
                    

                    return;
                }
                else
                {
                    //Move until a distance of 1 from the player
                    if(!MoveTowardsTarget(PlayerController.Instance.transform.position, throwDistance))
                    {
                        //Throw the fruit
                        // Calculate the direction to the player
                        Vector3 playerPosition = PlayerController.Instance.transform.position;
                        Vector3 enemyPosition = transform.position;
                        Vector3 throwDirection = (playerPosition - enemyPosition).normalized;

                        // Calculate the initial velocity to achieve the desired throw distance
                        float gravity = 9.81f; // Acceleration due to gravity (adjust as needed)
                        float timeToTarget = Mathf.Sqrt((2 * throwDistance) / gravity);
                        Vector3 initialVelocity = (throwDirection * throwDistance) / timeToTarget;

                        // Create and throw the fruit
                        GameObject fruit = Instantiate(GameManager.Instance.fruits[(int)currentFruit].prefab, transform.position, Quaternion.identity);
                        fruit.GetComponent<Fruit>().isEnemyTeam = true;
                        fruit.GetComponent<Fruit>().ID = currentFruit;
                        fruit.GetComponent<Fruit>().isHeld = true;
                        Rigidbody fruitRigidbody = fruit.GetComponent<Rigidbody>();
                        fruitRigidbody.velocity = initialVelocity;

                        currentFruit = FruitID.NONE;
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

    bool MoveTowardsTarget(Vector3 targetPosition, float distance)
    {
        if (Vector3.Distance(transform.position, targetPosition) > distance)
        {
            transform.LookAt(targetPosition);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            return true;
        }
        else
        {
            return false;
        }
    }
}
