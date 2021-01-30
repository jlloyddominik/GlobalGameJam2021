using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    Image fadeImage;

	public bool fadeOut = false;
    void Start()
    {
        fadeImage = GetComponentInChildren<Image>();
    }

    void Update()
    {
		if (fadeOut) {
			fadeImage.color = fadeImage.color + new Color(0, 0, 0, Time.deltaTime);
		} else {
			fadeImage.color = new Color(0, 0, 0, Mathf.Max(fadeImage.color[3] - Time.deltaTime, 0));
		}
	}
}
