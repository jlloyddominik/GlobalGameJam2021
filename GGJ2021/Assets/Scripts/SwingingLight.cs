using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingLight : MonoBehaviour, IInteractable
{
    //https://www.youtube.com/watch?v=9RtR7Uf4HIQ
    [Range(-1,1)]
    public float DirX, DirZ;
    [SerializeField] float _swingTimer= 2;
    float _timer = 0f;
    [SerializeField]private float _speed = 1f;
    int _phase = 0;

    [SerializeField] private Light Lightbulb;
    private void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;
        if (_timer > _swingTimer)
        {
            _phase++;
            _phase %= 4;
            _timer = 0f;
        }
        switch(_phase)
        {
            case 0:
                transform.Rotate(DirX * (_speed * (1 - _timer)), 0, DirZ*(_speed * (1 - _timer)));
                break;
            case 1:
                transform.Rotate(DirX * (-_speed * _timer), 0, DirZ * (-_speed * _timer));
                break;
            case 2:
                transform.Rotate(DirX * (-_speed * (1 - _timer)), 0, DirZ * (-_speed * (1 - _timer)));
                break;
            case 3:
                transform.Rotate(DirX * (_speed * _timer), 0, DirZ * (_speed * _timer));
                break;
        }
    }

    public void Switch()
    {
        Lightbulb.gameObject.SetActive(!Lightbulb.gameObject.activeSelf);
    }

    public void Interact()
    {
        Switch();
    }
}
