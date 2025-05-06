using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;

public class InteractWObjects : MonoBehaviour {

    [SerializeField] private GameObject _equipTransform;
    [SerializeField] private float _animationTimer;
    [SerializeField] private Tools None;
    [SerializeField] Tools _currentTool;


    private void InteractWithObject() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            IInteractable interactableObject = hit.transform.gameObject.GetComponent<IInteractable>();
            if (interactableObject == null)
                return;
            if (!(_currentTool?.ToolType == interactableObject.ToolType))
                return;
                
            _currentTool.Use(interactableObject, _equipTransform);

        }
    }

    private void GetToolInfo(Tools currentTool) {
        if (_currentTool == null) {
            _currentTool = currentTool;
            EquipTool();
        }
        else if (_currentTool != currentTool) {
            ResetTool(currentTool);
            EquipTool();
        }
        else {
            ResetTool();
        }

    }

    private void EquipTool() {
        ChangeCurrentToolRigidBody(true);
        StartCoroutine(PickToolAnimation(_currentTool.gameObject, _equipTransform.gameObject));

    }

    private void ResetTool(Tools newTool = null) {
        ChangeCurrentToolRigidBody(false);
        _currentTool.Deequip();
        if (newTool == null) {
            _currentTool = None;
            _currentTool.transform.parent = _equipTransform.transform;
            EquipTool();
            return;

        }
        _currentTool = newTool;

    }

    void ChangeCurrentToolRigidBody(bool state) {
        Rigidbody rb = _currentTool.GetComponent<Rigidbody>();
        rb.isKinematic = state;
    }

    void OnDisable() {
        InputManager.OnInteract -= InteractWithObject;
        Tools.OnToolEquip -= GetToolInfo;

    }

    void OnEnable() {
        InputManager.OnInteract += InteractWithObject;
        Tools.OnToolEquip += GetToolInfo;
    }

    IEnumerator PickToolAnimation(GameObject Tool, GameObject Target) {
        while (Vector3.Distance(Tool.transform.position, Target.transform.position) > 0.1f) {
            Tool.transform.position = Vector3.MoveTowards(Tool.transform.position, Target.transform.position, 0.5f);
            yield return null;
        }
        if (!(Vector3.Distance(Tool.transform.position, Target.transform.position) < 0.1f)) {
            Tool.transform.position = Target.transform.position;
        }
        Tool.transform.parent = Target.transform;
        yield return null;
    }
}
