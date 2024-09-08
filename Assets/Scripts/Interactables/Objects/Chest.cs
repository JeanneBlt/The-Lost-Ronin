using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : InteractableObjects
{

    [SerializeField] private SpriteRenderer[] graphisms;

    [System.Serializable]
    public class ChestItem
    {
        public ItemTemplate itemTemplate;
        public int quantity;          
    }

    [SerializeField] private ChestItem[] content;

    public override void Interact()
    {
        if (isReach)
        {
            open = true;
            EmptyChest();
        }

        else
        {
            open = false;
        }
    }

    //private void EmptyChest()
    //{
    //    foreach (var item in content) 
    //    {
    //        CharacterInfos.AddItem(item.id, item.number);
    //        item.number = 0;
    //    }
    //}

    private void EmptyChest()
    {
        foreach (var chestItem in content)
        {
            CharacterInfos.AddItemToInventory(chestItem.itemTemplate);

            UnityEngine.Debug.Log($"Ajout de {chestItem.itemTemplate.ItemName} dans l'inventaire");
        }
    }
}
