using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    private SlotController[] slots;
    [SerializeField] private Transform slotPrefab;
    [SerializeField] private Canvas slotCanvas;
    private InventoryController controller;
    public void Init(InventoryController pController)
    {
        controller = pController;

        slots = new SlotController[controller.SlotNumber];
        for (int i=0; i < slots.Length; i++)
        {
            slots[i] = Instantiate(slotPrefab, transform.position, Quaternion.identity, slotCanvas.transform).GetComponent<SlotController>();
            slots[i].Init(i);
        }
    }
}
