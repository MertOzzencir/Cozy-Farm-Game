using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class InventoryController : MonoBehaviour {

    [SerializeField] private Transform _openPosition, _closePosition;
    [SerializeField] private float _inventoryMoveSpeed;

    public List<InventorySlot> InventoryList;
    bool _canOpen;

    void Awake() {
        InputManager.OnInventoryOpen += Open;
        StartCoroutine(Move(_closePosition.position));
    }
    private void Open() {
        _canOpen = !_canOpen;
        if (_canOpen)
            StartCoroutine(Move(_openPosition.position));
        else
            StartCoroutine(Move(_closePosition.position));
    }

    IEnumerator Move(Vector3 position) {
        while (Vector3.Distance(transform.position, position) > 0.1f) {
            transform.DOMove(position, _inventoryMoveSpeed);
            yield return null;
        }

    }
}
