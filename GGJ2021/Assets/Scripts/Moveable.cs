using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    public bool Heavy = false;

	Rigidbody rb;
	private void Start()
	{
		rb = GetComponent<Rigidbody>();
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
		rb.velocity = new Vector3.zero;
	}
}
