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
	private float _turnRate = 5;
	private ColliderScript colliderS;

	public Transform model;

	// Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		cCollider = GetComponent<CapsuleCollider>();
		colliderS = GetComponent<ColliderScript>();

		radius = cCollider.radius;
	}

    // Update is called once per frame
    void Update()
    {
        center = transform.position;
	}

	private bool wasGroundMerged = false;
	public void Move(Vector3 direction)
	{
		if (colliderS.groundmerged) {
			isGrounded = true;
			if (colliderS.lightCheckers[0].revealed) {
				direction.z = Mathf.Min(direction.z, 0);
			}
			if (colliderS.lightCheckers[1].revealed)
			{
				direction.x = Mathf.Max(direction.x, 0);
			}
			if (colliderS.lightCheckers[2].revealed){
				direction.z = Mathf.Max(direction.z, 0);
			}
			if (colliderS.lightCheckers[3].revealed)
			{
				direction.x = Mathf.Min(direction.x, 0);
			}
			wasGroundMerged = true;
		} else if (wasGroundMerged) {
			isGrounded = false;
		}
		rb.position += direction;
		//rb.MovePosition(transform.position + direction);
	}

	public void Rotate(Quaternion targetAngle)
	{
		model.rotation = Quaternion.Slerp(model.rotation, targetAngle, _turnRate * Time.deltaTime * 2.0f);
	}
	private void OnCollisionStay(Collision collision)
	{
		if (collision.GetContact(0).point.y <= transform.position.y - 0.4) {
			isGrounded = true;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		isGrounded = false;
	}
}
