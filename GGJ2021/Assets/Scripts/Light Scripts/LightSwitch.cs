using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    AudioSource _audio;
    [SerializeField] private List<GameObject> interactableObjs;
    [SerializeField] List<AudioClip> _lightSwitchSFX = new List<AudioClip>();
    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

	public Transform flickSwitch;
	public Light light;

	bool off = true;
	private Color red = new Color(1f, 0.5f, 0.5f);
	private Color green = new Color(0.5f, 1f, 0.5f);

	void Start()
	{
		light.color = red;
	}

	public void Interact(PlayerMovement PlayerRef)
    {
        int _sfxIndex = Random.Range(0, _lightSwitchSFX.Count - 1);
        _audio.PlayOneShot(_lightSwitchSFX[_sfxIndex]);
        foreach (GameObject a in interactableObjs)
        {
            IInteractable interactable = a.GetComponent<IInteractable>();
            if (interactable == null) continue;
            interactable.Interact(PlayerRef);

			flickSwitch.localScale = new Vector3(-flickSwitch.localScale.x, flickSwitch.localScale.y, flickSwitch.localScale.z);

			if (off) {
				off = false;
				light.color = green;
			} else {
				off = true;
				light.color = red;
			}
		}
    }
}
