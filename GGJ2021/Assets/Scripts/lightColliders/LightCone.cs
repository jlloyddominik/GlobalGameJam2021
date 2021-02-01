using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCone : LightCollisionScript
{
	public override bool isInside(Vector3 point)
	{
		if (checkInShadow(point)) {
			return false;
		} else {
			Vector3 toPoint = point - transform.position;
			float angle = Vector3.Angle(transform.parent.forward, toPoint);
			return angle <= pLight.spotAngle/2;
		}
	}
}
