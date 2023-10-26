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
            switch (effect.type)
            {
                case EffectType.DAMAGE:
                    Debug.Log("Dealing " + effect.argument + " damage");
                    combatant.CurrentHP -= effect.argument;
                    yield return StartCoroutine(GameManager.Instance.DisplayEffect(combatant.transform.position, effect.argument.ToString(), Color.white));
                    break;
                case EffectType.HEAL:
                    
                    break;
            }
        }

        yield return null;
    }
}
