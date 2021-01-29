using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    public bool Heavy = false;

    public IEnumerator MoveToPos(Vector3 _destination)
    {
        while (transform.position != _destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, _destination, Time.deltaTime*10);
            yield return null;
        }
    }
}
