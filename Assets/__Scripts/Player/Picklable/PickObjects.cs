using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObjects : MonoBehaviour {


    public static event Action<IPickable> OnPlace;

    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Vector3 _offGround;


    private GameObject _currentObjectToCarry;
    IPickable _pickedObject;


    void Update() {
        if (_currentObjectToCarry != null) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _groundMask)) {

                _currentObjectToCarry.transform.position = Vector3.Lerp(_currentObjectToCarry.transform.position, hit.point
                + _offGround, 0.15f);

            }

        }
    }
    private void CarryObject() {
        if (_pickedObject != null && _currentObjectToCarry != null) {
            OnPlace?.Invoke(_pickedObject);
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            _pickedObject = hit.transform.gameObject.GetComponent<IPickable>();
            if (_pickedObject != null) {
                if (hit.transform.gameObject.GetComponent<Tools>() != null){
                    hit.transform.gameObject.GetComponent<Tools>().Equip();
                    ClearCurrentObject();
                    return;
                }

                _currentObjectToCarry = _pickedObject.Carry();

            }
        }
    }
    public void ClearCurrentObject() {
        _currentObjectToCarry = null;
        _pickedObject = null;
    }

    void OnEnable() {
        InputManager.OnPick += CarryObject;
        InputManager.OnGather += ClearCurrentObject;
    }
    void OnDisable() {
        InputManager.OnPick -= CarryObject;
        InputManager.OnGather -= ClearCurrentObject;
    }
}
