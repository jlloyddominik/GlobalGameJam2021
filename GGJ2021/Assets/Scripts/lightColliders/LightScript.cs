using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour, IInteractable
{
	Light ownLight;
	LightCollisionScript collisionScript;

	public bool startOff;
	bool on = true;
	private float onChance = 1;

	void Start()
	{
		ownLight = GetComponent<Light>();
		collisionScript = GetComponentInChildren<LightCollisionScript>();

		if (startOff) {
			turnOff();
			on = false;
			onChance = 0;
		}
	}

	void Update()
	{
		if ((onChance > 0 && !on) || (onChance < 1 && on)) {
			if (on) {
				onChance = onChance + 2*Time.deltaTime;
			} else {
				onChance = onChance - 2*Time.deltaTime;
			}

			if (Random.Range(0f, 1f) < onChance) {
				turnOn();
			} else {
				turnOff();
			}
		}
		print(onChance);
	}

	public void Interact(PlayerMovement PlayerRef)
	{
		if (on) {
			on = false;
		} else {
			on = true;
		}
	}

	private void turnOn() {
		collisionScript.shining = true;
		ownLight.enabled = true;
	}

	private void turnOff()
	{
		collisionScript.shining = false;
		ownLight.enabled = false;
	}
}
