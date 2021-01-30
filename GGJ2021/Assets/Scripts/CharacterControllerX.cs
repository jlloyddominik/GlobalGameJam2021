using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerX : MonoBehaviour
{
    public bool isGrounded = false;
	public Vector3 center;
	public float radius;

	private Rigidbody rb;
	private BoxCollider cCollider;
	private float _turnRate = 5;
	private ColliderScript colliderS;

	public Transform model;

	// Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		cCollider = GetComponent<BoxCollider>();
		center = cCollider.center;
		colliderS = GetComponent<ColliderScript>();
		//radius = cCollider.radius;
	}

    // Update is called once per frame
    void Update()
    {
		//center = transform.position;
		//center = cCollider.center;
	}

	public float timeSinceLastGround = 0f;
	private bool wasGroundMerged = false;
	public void Move(Vector3 direction)
	{
		if (colliderS.groundmerged) {
			isGrounded = colliderS.standingOnDarkness;
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

		if (isGrounded) {
			timeSinceLastGround = 0;
		} else {
			timeSinceLastGround = timeSinceLastGround + Time.deltaTime;
		}
	}

	public void Rotate(Quaternion targetAngle)
	{
		model.rotation = Quaternion.Slerp(model.rotation, targetAngle, _turnRate * Time.deltaTime * 2.0f);
	}
	private Rigidbody floorOn;

	private float footOffset = 0.8f;
	private void OnCollisionStay(Collision collision)
	{
		bool appropriate = true;
		for (int i = 0; i<collision.contactCount; i++) {
			if (collision.GetContact(i).point.y > transform.position.y - footOffset)
			{
				appropriate = false;
				break;
			}
		}

		if (appropriate) {
			isGrounded = true;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		isGrounded = false;
	}
}
