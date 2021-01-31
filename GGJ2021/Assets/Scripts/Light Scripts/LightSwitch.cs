﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] private List<GameObject> interactableObjs;

	public Transform flickSwitch;
	public Light light;

	bool off = true;
	private Color red = new Color(1f, 0.5f, 0.5f);
	private Color green = new Color(0.5f, 1f, 0.5f);

	void Start()
	{
		light.color = red;
	}

	public void Interact(PlayerMovement PlayerRef)
    {
        foreach(GameObject a in interactableObjs)
        {
            IInteractable interactable = a.GetComponent<IInteractable>();
            if (interactable == null) continue;
            interactable.Interact(PlayerRef);

			flickSwitch.localScale = new Vector3(-flickSwitch.localScale.x, flickSwitch.localScale.y, flickSwitch.localScale.z);

			if (off) {
				off = false;
				light.color = green;
			} else {
				off = true;
				light.color = red;
			}
		}
    }
}
