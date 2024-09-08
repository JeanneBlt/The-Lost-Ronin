using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public enum ItemID
{
    HealthPotion = 0,
}

[System.Serializable]
public class Item
{
    [SerializeField] private string name;

    public ItemID id;

    public int number = 0;
}

public class CharacterInfos : MonoBehaviour
{
    //public static Item[] inventory;
    //private GameManager manager;

    //void Start()
    //{
    //    manager = GameManager.GetInstance();
    //    inventory = new Item[1];
    //    inventory[0]=new Item();
    //}

    //public static void AddItem(ItemID _id, int _number) 
    //{
    //    inventory[((int )_id)].number += _number;

    //    Debug.Log("Inventory:");
    //    foreach (Item item in inventory)
    //    {
    //        Debug.Log($"Item ID: {item.id}, Number: {item.number}");
    //    }
    //}

    public static List<ItemTemplate> inventory = new List<ItemTemplate>();
    private GameManager manager;

    void Start()
    {
        manager = GameManager.GetInstance();
    }

    public static void AddItemToInventory(ItemTemplate item)
    {
        inventory.Add(item);
        UnityEngine.Debug.Log($"Ajouté : {item.ItemName} dans l'inventaire");
    }

    public static void ShowInventory()
    {
        UnityEngine.Debug.Log("Inventaire:");
        foreach (var item in inventory)
        {
            UnityEngine.Debug.Log(item.ItemName);
        }
    }
}
