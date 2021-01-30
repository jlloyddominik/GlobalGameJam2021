using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTracker : MonoBehaviour
{
    public int numberOfContacts = 0;

    // Update is called once per frame
    void Update()
    {
		
	}

	void OnCollisionEnter(Collision other) {
		numberOfContacts = numberOfContacts + 1;
	}

	void OnCollisionExit(Collision other)
	{
		numberOfContacts = numberOfContacts - 1;
	}
}
