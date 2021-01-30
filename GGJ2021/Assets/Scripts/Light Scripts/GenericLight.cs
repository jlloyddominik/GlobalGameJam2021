using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericLight : MonoBehaviour, IInteractable
{
    public GameObject LightSource;
    public void Interact(PlayerMovement PlayerRef)
    {
        LightSource.SetActive(!LightSource.activeSelf);
    }
}
