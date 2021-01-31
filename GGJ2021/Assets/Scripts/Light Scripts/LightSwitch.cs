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

    public void Interact(PlayerMovement PlayerRef)
    {
        int _sfxIndex = Random.Range(0, _lightSwitchSFX.Count - 1);
        _audio.PlayOneShot(_lightSwitchSFX[_sfxIndex]);
        foreach (GameObject a in interactableObjs)
        {
            IInteractable interactable = a.GetComponent<IInteractable>();
            if (interactable == null) continue;
            interactable.Interact(PlayerRef);
        }
    }
}
