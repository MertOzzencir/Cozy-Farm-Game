using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static event Action OnPick;
    public static event Action OnGather;
    public static event Action OnInteract;
    public static event Action OnInventoryOpen;
    public static event Action OnJump;
    private PlayerInput _input;

    public Vector2 GetMovementVectorNormalized() {

        Vector2 playerMovement = _input.Player.Move.ReadValue<Vector2>();
        return playerMovement;
    }

    private void PickObject(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        OnPick?.Invoke();
    }
    private void GatherObject(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        OnGather?.Invoke();
    }

    private void Interact(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        OnInteract?.Invoke();
    }
    private void OpenInventory(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        OnInventoryOpen?.Invoke();
    }
    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        OnJump?.Invoke();
    }

    void OnEnable() {
        _input = new PlayerInput();
        _input.Enable();
        _input.Player.Pick.performed += PickObject;
        _input.Player.Gather.performed += GatherObject;
        _input.Player.Interact.performed += Interact;
        _input.Player.Inventory.performed += OpenInventory;
        _input.Player.Jump.performed += Jump;
    }



    void OnDisable() {
        _input.Disable();
        _input.Player.Pick.performed -= PickObject;
        _input.Player.Gather.performed -= GatherObject;
        _input.Player.Interact.performed -= Interact;
        _input.Player.Inventory.performed -= OpenInventory;
        _input.Player.Jump.performed -= Jump;

    }





}
