using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour, IInteractable
{
    public bool Heavy = false;
	public bool visible = true;

	public bool hideInDarkness = false;
	private ColliderScript colliderScript;

	Rigidbody rb;
	float mass;
	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		mass = rb.mass;

		if (hideInDarkness) {
			colliderScript = GetComponent<ColliderScript>();
		}
	}

	private void Update()
	{
		if (hideInDarkness) {
			visible = colliderScript.revealed;
		}
	}

	public IEnumerator MoveToPos(Vector3 _destination)
    {
        while (transform.position != _destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, _destination, Time.deltaTime*10);
            yield return null;
        }
    }

	public void AndrewsMoveToPos(Vector3 _destination)
	{
		rb.MovePosition(_destination);
		rb.velocity = Vector3.zero;
	}
	public void Interact(PlayerMovement PlayerRef)
	{
		if (CompareTag("grabbable") && visible)
		{
			PlayerRef.HeldObj = this.GetComponent<Moveable>();
		}
		grab();
	}
	public void grab() {
		rb.mass = 0;
	}

	public void drop() {
		transform.parent = null;
		rb.mass = mass;
	}
}
