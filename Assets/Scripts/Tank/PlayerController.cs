using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 10f;
    public float mouseSensitivity = 10f;

    private Rigidbody _rigidbody;
    private Vector3 moveDirection;
    private Vector3 rotation;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveDirection = new Vector3(0, 0, Input.GetAxisRaw("Vertical")).normalized;
        rotation = new Vector3(0, Input.GetAxisRaw("Mouse X"), 0) * mouseSensitivity;
    }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(rotation));
    }
}