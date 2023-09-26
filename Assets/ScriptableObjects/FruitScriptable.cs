using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "FruitScriptable", order = 1)]
public class FruitScriptable : ScriptableObject
{
    public GameObject prefab;
    public string name;
    public Effect[] effects;
}

public enum FruitID
{
    NONE = 0,
    APPLE = 1,
}

public enum EffectType
{
    NONE = 0,
    DAMAGE = 1,
}

[System.Serializable]
public struct Effect
{
    public EffectType type;
    public int argument;
}
