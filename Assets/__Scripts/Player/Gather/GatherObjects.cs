using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherObjects : MonoBehaviour {

    [SerializeField] private InventoryController _inventoryController;
    void Gather() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            IGatherable farmProduct = hit.transform.gameObject.GetComponent<IGatherable>();
            if (farmProduct == null)
                return;
            if (farmProduct.State.HasFlag(GeneralState.CanGather)) {
                foreach (InventorySlot a in _inventoryController.InventoryList) {
                    if (a.CanPlaceOnIt) {
                        a.PlaceOnPlaceable(farmProduct);
                        break;
                    }
                }
            }
        }
    }
    void OnEnable() {
        InputManager.OnGather += Gather;
    }

    void OnDisable() {
        InputManager.OnGather -= Gather;
    }

}
