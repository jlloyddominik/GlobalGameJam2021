using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
	public Collider fakeCollider;
	private ContactTracker contacts;
	public CollisionTracker realContacts;
	public LightCheckerScript[] lightCheckers;

	public bool groundmerged = false;
	public bool standingOnDarkness = false;
	public bool revealed = false;

	// Start is called before the first frame update
    void Start()
    {
		contacts = fakeCollider.GetComponent<ContactTracker>();
	}

    // Update is called once per frame
    void Update()
    {
		revealed = false;
		int nRevealed = 0;
		standingOnDarkness = false;
		foreach (LightCheckerScript checkerScript in lightCheckers) {
			if (checkerScript.revealed) {
				revealed = true;
				nRevealed += 1;
			}
			if (checkerScript.touchingDarkness) {
				standingOnDarkness = true;
			}
		}

		groundmerged = false;
		if (revealed) {
			if (nRevealed >= lightCheckers.Length - 1 || contacts.numberOfContacts == 0) {
				fakeCollider.isTrigger = false;
			} else if (realContacts == null || realContacts.numberOfContacts == 0) {
				groundmerged = true;
			}
		} else {
			fakeCollider.isTrigger = true;
		}
    }
}
