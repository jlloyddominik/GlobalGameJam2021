using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform trackingTarget;

	public Vector2 xBounds;
	public Vector2 yBounds;

	private float yOffset;
	private void Start()
	{
		yOffset = transform.position.y - trackingTarget.position.y;
	}

	void FixedUpdate()
    {
		float newX = Mathf.Min(Mathf.Max(trackingTarget.position.x, xBounds[0]), xBounds[1]);
		float newY = Mathf.Min(Mathf.Max(trackingTarget.position.y, yBounds[0]), yBounds[1]) + yOffset;
		transform.position = new Vector3(transform.position.x + 10*Time.deltaTime*(newX - transform.position.x), transform.position.y + 10*Time.deltaTime * (newY - transform.position.y), transform.position.z);
    }
}
