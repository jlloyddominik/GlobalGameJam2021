﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Inputs")]
    public InputAction MovementInput;
    public InputAction JumpInput;
    public InputAction LightInput;
    public InputAction GrabInput;

    [Header("Movement Variables")]
    [SerializeField] private float _playerSpeed = 5, _turnRate = 5;
    private Vector3 _playerVelocity;
    private float _gravityValue = -9.81f;
    private bool _lockRot = false;
    [SerializeField] private float _jumpHeight = 5;

    [Header("Object References")]
    [SerializeField] private GameObject _grabTarget;
    private Moveable _heldObj;
    private CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        MovementInput.Enable();
        JumpInput.Enable();
        JumpInput.performed += _ => Jump();

        GrabInput.Enable();
        GrabInput.performed += _ => Grab();
    }

    // Update is called once per frame
    void Update()
    {
        if (cc.isGrounded && _playerVelocity.y < 0)
            _playerVelocity.y = 0f;

        Vector3 _playerInput = new Vector3(MovementInput.ReadValue<Vector2>().x ,0, MovementInput.ReadValue<Vector2>().y);
        Vector3 _movementDir = new Vector3(_playerInput.x, 0, _playerInput.z) * _playerSpeed;
        _playerVelocity.y += _gravityValue * Time.deltaTime;

        _movementDir.y = _playerVelocity.y;
        cc.Move(_movementDir * Time.deltaTime);

        if (_playerInput != Vector3.zero && (!_lockRot))
        {
            Quaternion a = Quaternion.LookRotation(_movementDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, a, _turnRate * Time.deltaTime * 2.0f);
        }
    }

    void Jump()
    {
        if (cc.isGrounded || (_heldObj != null && !_heldObj.Heavy))
        {
            _playerVelocity.y += Mathf.Sqrt(-_jumpHeight * _gravityValue);

        }
    }

    void Grab()
    {
        if (_heldObj == null)
        {
            if (Physics.SphereCast(transform.position + cc.center, cc.radius, transform.forward, out RaycastHit hit, 1f))
            {
                if (hit.transform.CompareTag("grabbable") && hit.transform.GetComponent<Moveable>())
                { 
                    hit.transform.parent = transform;
                    hit.transform.GetComponent<Rigidbody>().isKinematic = true;
                    _heldObj = hit.transform.GetComponent<Moveable>();
                    if (_heldObj.Heavy) _lockRot = true;
                    else
                    {
                        Vector3 position = _grabTarget.transform.position;
                        StartCoroutine(_heldObj.MoveToPos(position));
                    }
                }
            }
        }
        else
        {
            _heldObj.transform.parent = null;
            _heldObj.GetComponent<Rigidbody>().isKinematic = false;
            _lockRot = false;
            _heldObj = null;
        }
    }

    void FlashLightToggle()
    {

    }
}