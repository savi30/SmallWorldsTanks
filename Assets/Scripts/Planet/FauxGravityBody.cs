using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FauxGravityBody : MonoBehaviour {

	public FauxGravityAttractor attractor;
	private Rigidbody _rigidbody;
	private Transform _transform;

	void Start ()
	{
		_transform = transform;
		_rigidbody = GetComponent<Rigidbody>();
		_rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
		_rigidbody.useGravity = false;
	}
	
	void FixedUpdate ()
	{
		attractor.Attract(_rigidbody);
	}

}
