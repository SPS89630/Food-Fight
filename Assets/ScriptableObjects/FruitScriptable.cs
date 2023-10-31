using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "FruitScriptable", order = 1)]
public class FruitScriptable : ScriptableObject
{
    public GameObject prefab;
    public Color color;
    public float radius;
    public float mass;
    public string name;
    public Effect[] effects;
}

public enum FruitID
{
    NONE = 0,
    APPLE = 1,
    PEAR = 2,
}

public enum EffectType
{
    NONE = 0,
    DAMAGE = 1,
    HEAL = 2,
    DAMAGE_OVER_TIME = 3,
}

[System.Serializable]
public struct Effect
{
    public EffectType type;
    public EffectData effectData;
}

[System.Serializable]
public struct DamageData
{
    public int damageAmount;
}

[System.Serializable]
public struct HealData
{
    public int healAmount;
}

[System.Serializable]
public struct DamageOverTimeData
{
    public int damageAmount;
    public float duration;
    public float coolDown;
    public bool canKill;
}

[System.Serializable]
public struct EffectData
{
    public DamageData damage;
    public HealData heal;
    public DamageOverTimeData damageOverTime;
}