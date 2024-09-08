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

    private void EmptyChest()
    {
        foreach (var chestItem in content)
        {
            CharacterInfos characterInfos = FindObjectOfType<CharacterInfos>();
            characterInfos.AddItemToInventory(chestItem.itemTemplate, chestItem.quantity);

            UnityEngine.Debug.Log($"Ajout de {chestItem.itemTemplate.ItemName} ({chestItem.quantity}) dans l'inventaire");
        }
    }
}
