using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlotController : MonoBehaviour
{
public int slotID { private set; get; }

[SerializeField] private TextMeshProUGUI itemName;
[SerializeField] private TextMeshProUGUI numberText; 

public void Init(int pId)
{
slotID = pId;
gameObject.name= "Slot " + (pId + 1);
}

public void UpdateDisplay(string pItemName, int pNumber)
{
    bool vEmpty = pNumber == 0;

    numberText.text = vEmpty ? "" : pNumber.ToString("00");
    itemName.text = vEmpty ? null : pItemName;
}

}
