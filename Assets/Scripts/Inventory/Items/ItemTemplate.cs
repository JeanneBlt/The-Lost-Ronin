using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]

public class ItemsTemplate : ScriptableObject
{
 
 [Header("Main Infos")]
 [SerializeField] private string itemName;
 [SerializeField] private int id;

 [Header("Item Infos")]
 [SerializeField] private int stack;

 private string getItemName => itemName;
 private int getId => id;

 private int getStack => stack;

}
