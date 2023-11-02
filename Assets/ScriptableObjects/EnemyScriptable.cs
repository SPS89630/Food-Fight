using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "EnemyScriptable", order = 2)]
public class EnemyScriptable : ScriptableObject
{
    public string name;
    public EnemyID id;
    public GameObject prefab;
    public int health;
    public float speed;
    public float throwDistance;
    public float attackSpeed;
    public EnemyAIArguments[] ai;
}

public enum EnemyID
{
    NONE = 0,
    SLINGER = 1,
    KAMIKAZE = 2,
    TANK = 3,
}

public enum EnemyAIArguments
{
    NONE = 0,
    THROW = 1,
    EXPLODE = 2,
}