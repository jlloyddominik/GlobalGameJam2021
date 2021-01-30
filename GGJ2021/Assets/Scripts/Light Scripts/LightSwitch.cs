using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] private List<GameObject> interactableObjs;

    public void Interact(PlayerMovement PlayerRef)
    {
        foreach(GameObject a in interactableObjs)
        {
            IInteractable interactable = a.GetComponent<IInteractable>();
            if (interactable == null) continue;
            interactable.Interact(PlayerRef);
        }
    }
}
