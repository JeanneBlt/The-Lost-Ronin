using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI; 
    private bool isInventoryOpen = false;

    private void Start()
    {
        inventoryUI.SetActive(false);
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        if (isInventoryOpen ) {inventoryUI.SetActive(true);}
        else {inventoryUI.SetActive(false);}
    }
}