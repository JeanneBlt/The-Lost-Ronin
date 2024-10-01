using System.Collections;
using System.Collections.Generic;
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

        // Appel de la fonction de synchronisation dans LevelLoader
        SyncInventoryWithLevelLoader();
    }

    // Nouvelle fonction pour synchroniser avec LevelLoader
    private void SyncInventoryWithLevelLoader()
    {
        if (LevelLoader.instance != null)
        {
            LevelLoader.instance.SyncInventoryWithCharacterInfos();
        }
        else
        {
            Debug.LogWarning("LevelLoader instance is null. Cannot sync inventory.");
        }
    }

    public List<InventoryItem> GetInventory()
    {
        return inventory;
    }
}
