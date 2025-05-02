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
    Tools _currentTool;

    void Awake() {
        _currentTool = None;
        EquipTool();
    }
    private void Harvest() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            IInteractable interactableObject = hit.transform.gameObject.GetComponent<IInteractable>();
            if (interactableObject == null)
                return;
            if (_currentTool?.ToolType == interactableObject.ToolType)
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
        _currentTool.GetComponent<Rigidbody>().isKinematic = true;
        StartCoroutine(PickToolAnimation(_currentTool.gameObject, _equipTransform.gameObject));

    }

    private void ResetTool(Tools newTool = null) {
        _currentTool.GetComponent<Rigidbody>().isKinematic = false;
        _currentTool.Deequip();
        if (newTool == null) {
            _currentTool = None;
            EquipTool();
            return;
        }
        _currentTool = newTool;
    }

    void OnDisable() {
        InputManager.OnInteract -= Harvest;
        Tools.OnToolEquip -= GetToolInfo;

    }

    void OnEnable() {
        InputManager.OnInteract += Harvest;
        Tools.OnToolEquip += GetToolInfo;
    }

    IEnumerator PickToolAnimation(GameObject Tool, GameObject Target) {
        Tool.transform.DOMove(Target.transform.position, _animationTimer, true).SetEase(Ease.InOutBounce);
        yield return new WaitForSeconds(_animationTimer);
        if (!(Vector3.Distance(Tool.transform.position, Target.transform.position) < 0.1f)) {
            Tool.transform.position = Target.transform.position;
        }
        Tool.transform.parent = Target.transform;
        yield return null;
    }
}
