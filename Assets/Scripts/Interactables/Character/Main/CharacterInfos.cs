using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public ItemTemplate itemTemplate;
    public int quantity;

    public InventoryItem(ItemTemplate template, int qty)
    {
        itemTemplate = template;
        quantity = qty;
    }
}

public class CharacterInfos : MonoBehaviour
{
    [SerializeField] public List<InventoryItem> inventory = new List<InventoryItem>();
    private GameManager manager;

    void Start()
    {
        manager = GameManager.GetInstance();
    }

    public void AddItemToInventory(ItemTemplate item, int quantity)
    {
        InventoryItem existingItem = inventory.Find(i => i.itemTemplate == item);

        if (existingItem != null)
        {
            existingItem.quantity += quantity;
        }
        else
        {
            inventory.Add(new InventoryItem(item, quantity));
        }

        UnityEngine.Debug.Log($"Ajouté : {item.ItemName} ({quantity}) dans l'inventaire");
    }

    public List<InventoryItem> GetInventory()
    {
        return inventory;
    }

    public void ShowInventory()
    {
        UnityEngine.Debug.Log("Inventaire:");
        foreach (var item in inventory)
        {
            UnityEngine.Debug.Log($"{item.itemTemplate.ItemName} - Quantité : {item.quantity}");
        }
    }
}