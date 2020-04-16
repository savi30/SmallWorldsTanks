using UnityEngine;

public class TankMovement : MonoBehaviour{
    public float speed = 10f;
    public float mouseSensitivity = 10f;

    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;
    private Vector3 _rotation;

    private void Start(){
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Update(){
        _moveDirection = new Vector3(0, 0, Input.GetAxisRaw("Vertical")).normalized;
        _rotation = new Vector3(0, Input.GetAxisRaw("Mouse X"), 0) * mouseSensitivity;
    }

    public void FixedUpdate(){
        _rigidbody.MovePosition(_rigidbody.position +
                                transform.TransformDirection(_moveDirection) * (speed * Time.deltaTime));
        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(_rotation));
    }
}