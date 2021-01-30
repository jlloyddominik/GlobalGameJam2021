using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    [Header("Player Inputs")]
    public InputAction MovementInput;
    public InputAction LockRotation;
    public InputAction JumpInput;
    public InputAction LightInput;
    public InputAction GrabInput;

    [Header("Movement Variables")]
    [SerializeField] private float _playerSpeed = 8;
    private Vector3 _playerVelocity;
    private float _gravityValue = -9.81f;
    [SerializeField] private float _jumpHeight = 5;
    Vector3 _movementDir = new Vector3(0, 0, 0);
    [Header("Object References")]
    [SerializeField] private GameObject _grabTarget;
    [SerializeField] private Light _flashlight;
    private Moveable _heldObj;

    private bool _lockRot => LockRotation.ReadValue<float>() >0;
    private bool _freezeMovements => (this.animator.GetCurrentAnimatorStateInfo(0).IsName("PickUp"));
    public Moveable HeldObj { get => _heldObj; set => _heldObj = value; }

    private Rigidbody _heldRB;
    private CharacterControllerX cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterControllerX>();
        animator = GetComponent<Animator>();
        MovementInput.Enable();
        JumpInput.Enable();
        JumpInput.performed += _ => Jump();

        GrabInput.Enable();
        GrabInput.performed += _ => Grab();

        LightInput.Enable();
        LightInput.performed += _ => FlashLightToggle();

        LockRotation.Enable();
        //LockRotation.started += _ => _lockRot = true;
        //LockRotation.performed += _ => _lockRot = false;
        //LockRotation.canceled += _ => _lockRot = false;
    }

	private void OnDestroy()
	{
		MovementInput.Disable();
		JumpInput.Disable();
		GrabInput.Disable();
		LightInput.Disable();
		LockRotation.Disable();
	}

	// Update is called once per frame
	void Update()
    {
        Debug.Log(_freezeMovements);
        float _lockBtn = LockRotation.ReadValue<float>();
        _playerVelocity.y += _gravityValue * Time.deltaTime;
		if (cc.isGrounded && _playerVelocity.y < 0)
            _playerVelocity.y = 0f;

        Vector3 _playerInput = new Vector3(MovementInput.ReadValue<Vector2>().x ,0, MovementInput.ReadValue<Vector2>().y);

        if (!_freezeMovements)
            _movementDir = new Vector3(_playerInput.x, 0, _playerInput.z) * _playerSpeed;
        else
            _movementDir = new Vector3(0,0,0);
        Animation(_playerInput);


        _movementDir.y = _playerVelocity.y;
        cc.Move(_movementDir * Time.deltaTime);

        if (_playerInput != Vector3.zero && (!_lockRot  || _lockBtn <1) && !_freezeMovements)
        { 
			Vector3 _rotationDir = _movementDir;
			_rotationDir[1] = 0;
			Quaternion a = Quaternion.LookRotation(_rotationDir, Vector3.up);
			cc.Rotate(a);
        }

		if (_heldObj != null) {
            //StartCoroutine(_heldObj.MoveToPos(_grabTarget.transform.position));
            _heldObj.AndrewsMoveToPos(_grabTarget.transform.position);
			_heldObj.rotate(cc.model.rotation);
		}
	}
    void Animation(Vector3 _playerInput)
    {
        animator.SetBool("isGrounded", cc.isGrounded);
        animator.SetBool("IsMoving", _playerInput != Vector3.zero);
        animator.SetBool("Holding", _heldObj != null);
        
    }
	private float coyoteTime = 0.05f;
    void Jump()
    {
		print(cc.isGrounded);
        if (cc.isGrounded || cc.timeSinceLastGround <= coyoteTime)// || (_heldObj != null && !_heldObj.Heavy))
        {
            animator.SetTrigger("Jumped");
            _playerVelocity.y += Mathf.Sqrt(-_jumpHeight * _gravityValue);
			cc.timeSinceLastGround = 2* coyoteTime;

		}
    }

    void Grab()
    {
        if (_heldObj == null)
        {
            if (Physics.SphereCast(transform.position , 0.5f, _grabTarget.transform.forward, out RaycastHit hit, 1.5f))
            {
                var interactable = hit.transform.GetComponent<IInteractable>();
                if (interactable == null) return;
                interactable.Interact(this);
                animator.SetTrigger("Grab");
            }
        }
        else
        {
            _heldObj.drop();
            _heldObj = null;
        }
    }

    void FlashLightToggle()
    {
        _flashlight.gameObject.SetActive(!_flashlight.gameObject.activeSelf);
    }
}
