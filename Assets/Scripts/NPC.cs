using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    [SerializeField] private Dialog[] dialog;
    private int currentID = -1;

    private UIDialog uiDialog;

    void Start()
    {
        uiDialog = FindObjectOfType<UIDialog>();
        InputsManager.instance.dialogEvent.AddListener(Interact);
    }

    public void StartDialogue()
    {
        currentID = 0;
        uiDialog.SetDialog(dialog[currentID]);
    }

    public bool IsDialogueActive()
    {
        return uiDialog.IsDialogActive();
    }

    public void Interact()
    {
        Debug.Log("Interact method called");
        if (dialog != null && currentID +1 < dialog.Length)
        {
            currentID++;
            uiDialog.SetDialog(dialog[currentID]);
        }
        else
        {
            currentID=-1;
            uiDialog.CloseDialog();
            Destroy(gameObject);
        }
    }
}
