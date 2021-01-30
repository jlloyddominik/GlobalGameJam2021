using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    public bool Heavy = false;

	Rigidbody rb;
	float mass;
	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		mass = rb.mass;
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

	public void grab() {
		rb.mass = 0;
	}

	public void drop() {
		transform.parent = null;
		rb.mass = mass;
	}
}
