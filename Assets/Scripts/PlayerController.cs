using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float groundDistance = .2f;

    public bool isGrounded = false;
    public LayerMask groundLayerMask;

    Transform _groundChecker;

    Rigidbody _body;

    Vector3 _movement;
    Vector3 _rotation;

    void Start () {
        _body = GetComponent<Rigidbody> ();
        _groundChecker = transform.Find ("GroundChecker");
    }

    // Update is called once per frame
    void Update () {
        isGrounded = Physics.CheckSphere (_groundChecker.position, groundDistance, groundLayerMask, QueryTriggerInteraction.Ignore);

        _movement = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
        if (_movement != Vector3.zero) {
            transform.forward = _movement;
        }

        _movement *= moveSpeed;

        if (Input.GetButtonDown ("Jump") && isGrounded) {
            _body.AddForce (Vector3.up * Mathf.Sqrt (jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
    }

    void FixedUpdate () {
        _body.MovePosition (_body.position + _movement * Time.fixedDeltaTime);
    }
}