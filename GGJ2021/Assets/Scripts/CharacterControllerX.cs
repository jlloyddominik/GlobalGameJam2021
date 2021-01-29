using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerX : MonoBehaviour
{
    public bool isGrounded = false;
	public Vector3 center;
	public float radius;

	private Rigidbody rb;
	private CapsuleCollider cCollider;

	// Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		cCollider = GetComponent<CapsuleCollider>();

		radius = cCollider.radius;
	}

    // Update is called once per frame
    void Update()
    {
        center = transform.position;
    }

	public void Move(Vector3 direction)
	{
		rb.position += direction;
		//rb.MovePosition(transform.position + direction);
	}

	public void Rotate(Quaternion rotate)
	{
		rb.MoveRotation(rotate);
	}

	private Rigidbody floorOn;
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.GetContact(0).point.y <= transform.position.y - 0.4) {
			isGrounded = true;
			floorOn = collision.rigidbody;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.rigidbody == floorOn) {
			isGrounded = false;
		}
	}
}
