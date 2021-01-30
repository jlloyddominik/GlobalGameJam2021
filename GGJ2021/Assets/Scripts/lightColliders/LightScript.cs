using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour, IInteractable
{
	Light ownLight;
	LightCollisionScript collisionScript;

	void Start()
	{
		ownLight = GetComponent<Light>();
		collisionScript = GetComponentInChildren<LightCollisionScript>();
	}

	void Update()
	{
		if (on)
		{
			turnOff();
		}
		else
		{
			turnOn();
		}
	}

	public void Interact(PlayerMovement PlayerRef)
	{
		if (on) {
			turnOff();
		} else {
			turnOn();
		}
	}

	bool on = true;
	private void turnOn() {
		collisionScript.shining = true;
		ownLight.enabled = true;
		on = true;
	}

	private void turnOff()
	{
		collisionScript.shining = false;
		ownLight.enabled = false;
		on = false;
	}
}
