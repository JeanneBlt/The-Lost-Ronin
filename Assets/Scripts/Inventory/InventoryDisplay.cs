using System.Diagnostics;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    private SlotController[] slots;
    [SerializeField] private Transform slotPrefab;
    [SerializeField] private Canvas slotCanvas;
    private InventoryController controller;
    [SerializeField] private GameObject inventoryUI;  
    private bool isInventoryOpen = false;

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

    public void UpdateDisplay(SlotsInfos[] pSlotInfos)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < pSlotInfos.Length && slots[i] != null)
            {
                slots[i].UpdateDisplay(pSlotInfos[i].ItemName, pSlotInfos[i].Number);
            }
            else if (slots[i] != null) // If there are more slots than items
            {
                slots[i].UpdateDisplay(null, 0);
            }
        }
    }
}
