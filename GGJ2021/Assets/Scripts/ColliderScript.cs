using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
	public Collider fakeCollider;
	private ContactTracker contacts;
	public LightCheckerScript[] lightCheckers;

	public bool groundmerged = false;

	// Start is called before the first frame update
    void Start()
    {
		contacts = fakeCollider.GetComponent<ContactTracker>();
	}

    // Update is called once per frame
    void Update()
    {
		bool revealed = false;
		int nRevealed = 0;
        foreach (LightCheckerScript checkerScript in lightCheckers) {
			if (checkerScript.revealed) {
				revealed = true;
				nRevealed += 1;
			}
		}

		groundmerged = false;
		if (revealed) {
			if (nRevealed >= 3 || contacts.numberOfContacts == 0) {
				fakeCollider.isTrigger = false;
			} else {
				groundmerged = true;
			}
		} else {
			fakeCollider.isTrigger = true;
		}
    }
}
