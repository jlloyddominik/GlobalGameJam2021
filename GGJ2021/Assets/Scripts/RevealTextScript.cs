using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RevealTextScript : MonoBehaviour
{
    public TMP_Text text;
	
    void Start()
    {
        text.color = new Color(1, 1, 1, 0);
	}

	bool playerIn = false;
    void Update()
    {
        if (playerIn) {
			text.color = new Color(1, 1, 1, Mathf.Min(text.color[3] + Time.deltaTime, 1));
		}
		else
		{
			text.color = new Color(1, 1, 1, Mathf.Max(text.color[3] - Time.deltaTime, 0));
		}
    }

	private void OnTriggerEnter(Collider other)
	{
		playerIn = true;
	}

	private void OnTriggerExit(Collider other)
	{
		playerIn = false;
	}
}
