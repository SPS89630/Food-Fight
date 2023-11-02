using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Fruit : MonoBehaviour
{
    public FruitID ID;
    public bool isHeld = false;

    MeshRenderer renderer;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.mass = GameManager.Instance.fruits[(int)ID].mass;
        //change color
        renderer.material.color = GameManager.Instance.fruits[(int)ID].color;
    }

    public IEnumerator OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Entity>() != null && isHeld && collision.gameObject != PlayerController.Instance.gameObject)
        {
            //disable the renderer
            renderer.enabled = false;

            //create a sphere at the point of collision
            Collider[] hitColliders = Physics.OverlapSphere(collision.contacts[0].point, GameManager.Instance.fruits[(int)ID].radius);

            //use LINQ get all enemies in the radius
            var enemies = hitColliders.Where(x => x.GetComponent<Enemy>() != null).Select(x => x.GetComponent<Enemy>()).ToList();

            //run actions on all enemies in the radius
            foreach (var enemy in enemies)
            {
                yield return StartCoroutine(Action(enemy));
            }
            Destroy(gameObject);
        }
    }

    void OnPickUp()
    {
        PlayerController.Instance.currentFruit = ID;
        isHeld = true;
    }

    void OnDrop()
    {
        PlayerController.Instance.currentFruit = FruitID.NONE;
    }



    public IEnumerator Action(Entity combatant)
    {
        FruitScriptable data = GameManager.Instance.fruits[(int)ID];
        Debug.Log("Running actions for " + data.name);

        foreach (var effect in data.effects)
        {
            if(!IsStructUninitialized(effect.effectData.damage))
            {
                Debug.Log("Dealing " + effect.effectData.damage.damageAmount + " damage");
                yield return StartCoroutine(DealDamage(effect.effectData.damage.damageAmount, combatant));
            }
            
            if(!IsStructUninitialized(effect.effectData.heal))
            {
                Debug.Log("Healing " + effect.effectData.heal.healAmount + " health");
                combatant.CurrentHP += effect.effectData.heal.healAmount;
            }

            if(!IsStructUninitialized(effect.effectData.damageOverTime))
            {
                Debug.Log("Dealing " + effect.effectData.damageOverTime.damageAmount + " damage over " + effect.effectData.damageOverTime.duration + " seconds");
                float time = 0;
                while(time < effect.effectData.damageOverTime.duration)
                {
                    time += Time.deltaTime;
                    yield return StartCoroutine(DealDamage(effect.effectData.damageOverTime.damageAmount, combatant, effect.effectData.damageOverTime.canKill));
                    yield return new WaitForSeconds(effect.effectData.damageOverTime.coolDown);
                    time += effect.effectData.damageOverTime.coolDown;
                }
            }   
        }

        yield return null;
    }

    IEnumerator DealDamage(int damage, Entity combatant, bool canKill = true)
    {
        combatant.CurrentHP = Mathf.Clamp(combatant.CurrentHP - damage, canKill ? 0 : 1 , combatant.MaxHP);
        yield return StartCoroutine(GameManager.Instance.DisplayEffect(combatant.transform.position, damage.ToString(), Color.white, combatant.transform));
    }

    public static bool IsStructUninitialized<T>(T myStruct) where T : struct
{
    return EqualityComparer<T>.Default.Equals(myStruct, default(T));
}
}