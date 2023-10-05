using System.Collections;
using System.Collections.Generic;
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
    public static Item[] inventory;
    private GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.GetInstance();
        inventory = new Item[1];
        inventory[0]=new Item();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void AddItem(ItemID _id, int _number)
    {
        inventory[((int )_id)].number += _number;

        Debug.Log("Inventory:");
        foreach (Item item in inventory)
        {
            Debug.Log($"Item ID: {item.id}, Number: {item.number}");
        }
    }
}
