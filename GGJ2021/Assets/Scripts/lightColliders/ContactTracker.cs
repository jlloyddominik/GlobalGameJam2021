using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactTracker : MonoBehaviour
{
    public int numberOfContacts = 0;

	private List<int> contacts;

	private void Start()
	{
		contacts = new List<int>();
	}

	void Update()
    {
		
	}

	void OnTriggerEnter(Collider other) {
		if (!contacts.Contains(other.GetInstanceID())) {
			contacts.Add(other.GetInstanceID());
			numberOfContacts = numberOfContacts + 1;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (contacts.Contains(other.GetInstanceID()))
		{
			contacts.Remove(other.GetInstanceID());
			numberOfContacts = numberOfContacts - 1;
		}
	}
}
