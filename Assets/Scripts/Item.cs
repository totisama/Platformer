using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName ="Scriptable object/Item")]
public class Item : ScriptableObject
{
    public Sprite image;
    public ActionType type;

    public enum ActionType
    {
        health,
        speed,
        attack
    }
}
