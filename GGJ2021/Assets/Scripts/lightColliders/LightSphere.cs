using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSphere : LightCollisionScript
{
	public override bool isInside(Vector3 point)
	{
		return !checkInShadow(point);
	}
}
