using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : InteractablesObjects
{

    [SerializeField] private SpriteRenderer[] graphisms;

    [SerializeField] private Item[] content;

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
        foreach (var item in content) 
        {
            CharacterInfos.AddItem(item.id, item.number);
            item.number = 0;
        }
    }
}
