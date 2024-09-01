using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotController : MonoBehaviour
{
public int slotID { private set; get; }

public void Init(int pId)
{
slotID = pId;
gameObject.name= "Slot " + (pId + 1);
}
}
