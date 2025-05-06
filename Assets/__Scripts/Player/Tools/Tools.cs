using System;
using System.Collections;
using UnityEngine;

public abstract class Tools : MonoBehaviour, IPickable {
    [SerializeField] public ToolType _toolType;
    [SerializeField] private float _animationDistanceOfZ;

    public ToolType ToolType { get => _toolType; set => _toolType = value; }
    public IPlaceable PlacedObject { get; set; }
    public GeneralState State { get; set; }

    public Coroutine ToolAnimation;

    public static event Action<Tools> OnToolEquip;

    public void Equip() {
        OnToolEquip?.Invoke(this);
    }
    public void Deequip() {
        transform.parent = null;
    }

    public abstract void Use(IInteractable interactedObject, GameObject TransformBack);

    public GameObject Carry() {
        return null;

    }

    public GameObject ObjectReferance() {
        return this.gameObject;
    }

    public IEnumerator UseAnimation(GameObject targetPosition) {
        Vector3 startPosition = transform.localPosition + new Vector3(0, 0, _animationDistanceOfZ);
        Vector3 forwardTarget = transform.parent.TransformPoint(startPosition);
        while (Vector3.Distance(transform.position, forwardTarget) > 0.1f) {
            transform.position = Vector3.MoveTowards(transform.position, forwardTarget, 0.2f);
            yield return null;

        }
        while (Vector3.Distance(transform.position, targetPosition.transform.position) > 0.1f) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.transform.position, 0.2f);
            yield return null;
        }
        ToolAnimation = null;


    }


}
