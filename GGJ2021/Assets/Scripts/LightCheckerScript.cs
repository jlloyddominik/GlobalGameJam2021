using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCheckerScript : MonoBehaviour
{
    public bool revealed = false;

	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter(Collider collider)
	{
		LightCollisionScript collisionScript = collider.GetComponent<LightCollisionScript>();
		if (collisionScript.isInside(transform.position)) {
			revealed = true;
		} else {
			revealed = false;
		}
	}

	private void OnTriggerStay(Collider collider)
	{
		LightCollisionScript collisionScript = collider.GetComponent<LightCollisionScript>();
		if (collisionScript.isInside(transform.position))
		{
			revealed = true;
		} else {
			revealed = false;
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		revealed = false;
	}
}
