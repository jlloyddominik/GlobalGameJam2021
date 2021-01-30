using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCheckerScript : MonoBehaviour
{
    public bool revealed = false;
	public bool touchingDarkness = false;
	public ContactTracker collisions;

	public int nLightSources = 0;

	private List<int> lightIDsEntered;

	// Start is called before the first frame update
    void Start()
    {
		lightIDsEntered = new List<int>();
	}

    // Update is called once per frame
    void Update()
    {
        touchingDarkness = collisions.numberOfContacts > 0;
		revealed = nLightSources > 0;
	}

	void OnTriggerEnter(Collider collider)
	{
		LightCollisionScript collisionScript = collider.GetComponent<LightCollisionScript>();
		if (collisionScript.shining && collisionScript.isInside(transform.position) && !lightIDsEntered.Contains(collisionScript.GetInstanceID())) {
			nLightSources = nLightSources + 1;
			lightIDsEntered.Add(collisionScript.GetInstanceID());
		}
	}

	private void OnTriggerStay(Collider collider)
	{
		LightCollisionScript collisionScript = collider.GetComponent<LightCollisionScript>();
		if (collisionScript.shining && collisionScript.isInside(transform.position))
		{
			if (!lightIDsEntered.Contains(collisionScript.GetInstanceID())) {
				nLightSources = nLightSources + 1;
				lightIDsEntered.Add(collisionScript.GetInstanceID());
			}
		} else if (lightIDsEntered.Contains(collisionScript.GetInstanceID())) {
			nLightSources = nLightSources - 1;
			lightIDsEntered.Remove(collisionScript.GetInstanceID());
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		LightCollisionScript collisionScript = collider.GetComponent<LightCollisionScript>();
		if (lightIDsEntered.Contains(collisionScript.GetInstanceID())) {
			nLightSources = nLightSources - 1;
			lightIDsEntered.Remove(collisionScript.GetInstanceID());
		}
	}
}
