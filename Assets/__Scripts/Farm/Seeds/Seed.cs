using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour, ISeeds {
    [SerializeField] FarmProductSO _farmProductOfSeed;
    [SerializeField] ToolType _interactableToolType;

    public IPlaceable PlacedObject { get; set; }
    public ToolType ToolType { get => _interactableToolType; set => _interactableToolType = value; }
    public GeneralState State { get; set; }

    private Vector3 _growPosition;
    private Rigidbody _rb;
    void Awake() {
        _rb = GetComponent<Rigidbody>();

    }
    void OnEnable() {
        State = GeneralState.CanPick | GeneralState.CanGather;
    }
    public GameObject Carry() {
        if (!State.HasFlag(GeneralState.CanPick))
            return null;
        State = GeneralState.CanPick | GeneralState.CanGather;
        _rb.isKinematic = true;
        transform.parent = null;
        if (PlacedObject != null) {
            PlacedObject.CanPlaceOnIt = true;
            PlacedObject = null;
        }
        return this.gameObject;
    }
    public void Interact() {
        if (!State.HasFlag(GeneralState.CanInteract))
            return;
        State = GeneralState.None;
        StartGrow(_growPosition, PlacedObject);
    }

    public void SetGrow(Vector3 position, IPlaceable tiled) {
        _rb.isKinematic = true;
        _growPosition = position;
        PlacedObject = tiled;
    }
    private void StartGrow(Vector3 position, IPlaceable tiled) {
        StartCoroutine(GrowInTime(_farmProductOfSeed.GrowTimer, position, tiled));
    }
    IEnumerator GrowInTime(float GrowTimer, Vector3 position, IPlaceable tiled) {
        float TimeNow = Time.time;
        while (true) {
            if (Time.time > TimeNow + GrowTimer) {
                GameObject a = Instantiate(_farmProductOfSeed.PreFab, position, Quaternion.identity);
                a.GetComponent<IFarmProduct>().PlacedObject = tiled;
                Destroy(gameObject);
                break;
            }
            yield return null;
        }
    }

    public GameObject ObjectReferance() {
        return this.gameObject;
    }

    public GameObject Gather() {
        if (State != GeneralState.CanGather)
            return null;
        return this.gameObject;
    }


}
