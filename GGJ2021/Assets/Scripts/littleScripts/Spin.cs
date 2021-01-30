using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public Vector3 spinSpeed;
	Rigidbody rb;

	// Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
    {
		rb.MoveRotation(transform.rotation * Quaternion.Euler(Time.deltaTime * spinSpeed));
    }

	private void OnCollisionEnter(Collision collision)
	{
		collision.collider.transform.SetParent(transform);
	}
}
