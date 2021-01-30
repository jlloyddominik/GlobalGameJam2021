using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

interface ISwitchable
{
    void TurnOn();
    void TurnOff();
}

interface IInteract
{
    void Interact(GameObject source);
    void InteractStop(GameObject source);
    GameObject gameObject();
}

[RequireComponent(typeof(Collider))]
public class Switch : MonoBehaviour, IInteract
{
    public List<GameObject> affected;
    bool on;
    public void Interact(GameObject source)
    {
        if (!on)
        {
            foreach (GameObject g in affected) foreach (ISwitchable s in g.GetComponents<ISwitchable>()) s.TurnOn();
        }
        else
        {
            foreach (GameObject g in affected) foreach (ISwitchable s in g.GetComponents<ISwitchable>()) s.TurnOff();
        }
        on = !on;
    }

    public void InteractStop(GameObject source){}

    GameObject IInteract.gameObject()
    {
        return gameObject;
    }
}

public class Box : MonoBehaviour, IInteract
{
    Rigidbody body;

    public void Interact(GameObject source)
    {
        body.isKinematic = true;
        transform.SetParent(source.transform);
    }

    public void InteractStop(GameObject source)
    {
        body.isKinematic = false;
        transform.SetParent(null);
    }

    GameObject IInteract.gameObject()
    {
        return gameObject;
    }
}

public class SpiderMovement : MonoBehaviour
{
    [Header("Body Shit")]
    [SerializeField] LayerMask ground;
    [SerializeField] Transform frontPoint, backPoint;
    [SerializeField] Rigidbody body;
    [SerializeField] float surfaceAttraction;
    Vector3 upDirection;

    [Header("Control Shit")]
    [SerializeField] public InputAction movement;
    [SerializeField] InputAction rotation;
    [SerializeField] float speed;
    [SerializeField] float rot;
    [SerializeField] float cameraPitch;

    bool controlRotLock => rotation.ReadValue<bool>();

    IInteract currentInteractor;

    private void OnEnable()
    {
        movement.Enable();
        movement.started += _ => Interact();
        rotation.Enable();
    }
    private void OnDisable()
    {
        movement.Disable();
        rotation.Disable();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        FindUpDirection();
        /*TODO: for better down direction:
         *
         * Do a ray cast in current movement direction (and maybe sides) to see if there are walls.
         *
         * Limit ray cast for front and back. On fail try an angle pointing inwards (maybe always do).
         *
         * Move relative the down direction (get a forward and right in terms of the downDirection)
         */

        body.AddForce(surfaceAttraction * -upDirection, ForceMode.Force);

        Vector2 rotationInput = rotation.ReadValue<Vector2>();
        if (Mathf.Abs(rotationInput.x) > .01f) { transform.Rotate(transform.up, rotationInput.x * rot, Space.World); Debug.Log(rotationInput); }

        Vector2 moveInput = movement.ReadValue<Vector2>();
        body.MovePosition(transform.position + speed * (transform.forward * moveInput.y + transform.right * moveInput.x));
    }

    void FindUpDirection()
    {
        if (Physics.Raycast(frontPoint.position, -transform.up, out RaycastHit hit1, Mathf.Infinity, ground) &&
            Physics.Raycast(frontPoint.position, -transform.up, out RaycastHit hit2, Mathf.Infinity, ground))
        {
            upDirection = (hit1.normal + hit2.normal).normalized;
        }
    }

    private void Interact()
    {
        currentInteractor?.Interact(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteract interact = other.GetComponent<IInteract>();
        if (interact != null)
        {
            currentInteractor = interact;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentInteractor != null && currentInteractor.gameObject() == other.gameObject)
        {
            currentInteractor = null;
        }
    }
}
