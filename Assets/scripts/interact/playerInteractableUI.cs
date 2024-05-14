using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerInteractableUI : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private PlayerIneract playerInteract;
    [SerializeField] private TextMeshProUGUI interactTextMeshProUGUI;
    

    private void Update()
    {
        if(playerInteract.GetInteractableObject() !=null)
        {
            show(playerInteract.GetInteractableObject());
        }
        else
        {
            Hide();
        }
        if (playerInteract.GetInteractableObject2() != null)
        {
            show2(playerInteract.GetInteractableObject2());
        }
        else
        {
            Hide();
        }

    }
    private void show(NPCInteractable npcInteractable)
    {
        containerGameObject.SetActive(true);
        interactTextMeshProUGUI.text = npcInteractable.GetInteractText();
    }

    private void show2(NPCInteractable2 npcInteractable2)
    {
        containerGameObject.SetActive(true);
        interactTextMeshProUGUI.text = npcInteractable2.GetInteractText();
    }

    private void Hide()
    {
        containerGameObject.SetActive(false);
    }

    public void decline()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
