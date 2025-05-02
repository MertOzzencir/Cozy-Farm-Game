using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
    [Header("Movement Settings")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _movementInterpolate;


    [Header("Input Settings")]
    [SerializeField] InputManager _inputManager;


    [Header("Turn Settings")]
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _turnInterpolate;

    [Header("Jump Settings")]
    [SerializeField] private float _jumpPower;
    [SerializeField] private LayerMask _groundCheck;
    [SerializeField] private bool _canJump;
    [SerializeField] private float _checkDistance;

    private float _turnAngleValue;
    private Rigidbody _rb;


    void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        PlayerMoveVertical();
        PlayerTurnHorizontal();
        JumpMechanic();
    }

    private void JumpMechanic() {
        if (Physics.Raycast(transform.position + new Vector3(0, transform.localScale.y / 2), Vector3.down, _checkDistance, _groundCheck)) {
            _canJump = true;
        }
        else {
            _canJump = false;
        }
    }

    private void PlayerMoveVertical() {
        Vector3 velocityTarget = transform.forward * _inputManager.GetMovementVectorNormalized().y * _movementSpeed;
        _rb.velocity = Vector3.Lerp(_rb.velocity, velocityTarget, _movementInterpolate);
    }

    private void PlayerTurnHorizontal() {
        _turnAngleValue += _inputManager.GetMovementVectorNormalized().x * _turnSpeed;
        Quaternion lookRotation = Quaternion.Euler(0, _turnAngleValue * Time.fixedDeltaTime, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, _turnInterpolate);
    }
    private void Jump() {
        if (!_canJump)
            return;
        _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
    }
    void OnEnable() {
        InputManager.OnJump += Jump;
    }


}
