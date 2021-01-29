using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
	public Collider fakeCollider;
	private ContactTracker contacts;
	public LightCheckerScript[] lightCheckers;

	// Start is called before the first frame update
    void Start()
    {
		contacts = fakeCollider.GetComponent<ContactTracker>();
	}

    // Update is called once per frame
    void Update()
    {
		bool revealed = true;
        foreach (LightCheckerScript checkerScript in lightCheckers) {
			if (!checkerScript.revealed) {
				revealed = false;
			}
		}

		if (revealed) {
			if (contacts.numberOfContacts == 0) {
				fakeCollider.isTrigger = false;
			}
		} else {
			fakeCollider.isTrigger = true;
		}
    }
}
