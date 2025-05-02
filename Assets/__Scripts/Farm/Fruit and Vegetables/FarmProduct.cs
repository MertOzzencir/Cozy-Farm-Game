using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmProduct : MonoBehaviour, IFarmProduct {

    [SerializeField] private FarmProductType _type;
    [SerializeField] private ToolType _interactableToolType;
    [SerializeField] private float _damageAmount;
    [Header("Animations")]
    [SerializeField] private Vector2 _animationXAmount;
    [SerializeField] private float _animationTime;
    [SerializeField] private float _forcePowerAnimationVertical;
    [SerializeField] private float _forcePowerAnimationHorizontal;



    private Rigidbody _rb;
    public FarmProductType Type { get => _type; set => _type = value; }
    public IPlaceable PlacedObject { get; set; }
    public ToolType ToolType { get => _interactableToolType; set => _interactableToolType = value; }
    public GeneralState State { get; set; }
    public float Damage { get => _damageAmount; set => _damageAmount = value; }

    void Awake() {
        _rb = GetComponent<Rigidbody>();
        transform.eulerAngles = new Vector3(0, Random.Range(0, 180), 0);
        Set();


    }
    public void Interact() {

        if (!State.HasFlag(GeneralState.CanInteract))
            return;

        State = GeneralState.CanPick | GeneralState.CanGather;
        _rb.isKinematic = false;
        StartCoroutine(Animation());
        SetState();
    }
    public GameObject Gather() {
        _rb.isKinematic = true;
        return this.gameObject;
    }
    public GameObject Carry() {
        if (!State.HasFlag(GeneralState.CanPick))
            return null;
        State = GeneralState.CanPick | GeneralState.CanGather;

        _rb.isKinematic = true;
        SetState();
        return this.gameObject;
    }

    private void SetState() {
        transform.parent = null;
        if (PlacedObject != null) {
            PlacedObject.CanPlaceOnIt = true;
            PlacedObject = null;
        }
    }

    public GameObject ObjectReferance() {
        return this.gameObject;
    }
    IEnumerator Animation() {
        _rb.isKinematic = false;
        float multiplier = Random.Range(0, 2) == 1 ? -1f : 1f;

        _rb.AddForce(Vector3.up * _forcePowerAnimationVertical, ForceMode.Impulse);
        _rb.AddForce(Vector3.right * multiplier * _forcePowerAnimationHorizontal, ForceMode.Force);

        yield return null;



    }

    public void Set() {
        State = GeneralState.CanInteract;
    }
}
