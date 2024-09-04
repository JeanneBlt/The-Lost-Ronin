using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]

public class ItemTemplate : ScriptableObject
{
 
 [Header("Main Infos")]
 [SerializeField] private string itemName;
 [SerializeField] private int id;

 [Header("Item Infos")]
 [SerializeField] private int stack;

 public string ItemName => itemName;
 public int ItemId => id;

 public int Stack => stack;

}
