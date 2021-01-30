using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LightCollisionScript : MonoBehaviour
{
    public abstract bool isInside(Vector3 point);

	SphereCollider ownCollider;
	protected Light pLight;

	void Start()
	{
		ownCollider = GetComponent<SphereCollider>();
		pLight = GetComponentInParent<Light>();

		ownCollider.radius = pLight.range;
	}

	protected bool checkInShadow(Vector3 point) {
		//Vector3 lightOrigin = transform.position;
		//int layerMask = 257;
		//bool inShadow = Physics.Raycast(point, lightOrigin - point, (point - lightOrigin).magnitude, layerMask);
		//return inShadow;
		return false;
	}
}
