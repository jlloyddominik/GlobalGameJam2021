using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactTracker : MonoBehaviour
{
    public int numberOfContacts = 0;

    // Update is called once per frame
    void Update()
    {
		
	}

	void OnTriggerEnter(Collider other) {
		numberOfContacts = numberOfContacts + 1;
	}

	void OnTriggerExit(Collider other)
	{
		numberOfContacts = numberOfContacts - 1;
	}
}
