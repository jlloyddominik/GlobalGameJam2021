using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    public bool Heavy = false;

    public IEnumerator MoveToPos(Vector3 _destination)
    {
        while (transform.localPosition != _destination)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _destination, Time.deltaTime*10);
            yield return null;
        }
    }
}
