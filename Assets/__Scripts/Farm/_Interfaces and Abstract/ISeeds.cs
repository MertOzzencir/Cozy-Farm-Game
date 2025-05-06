
using System.Collections;
using UnityEngine;
public abstract class ISeeds : MonoBehaviour, IGatherable, IInteractable {

    [SerializeField] FarmProductSO _farmProductOfSeed;
    [SerializeField] ToolType _interactableToolType;
    public GeneralState State { get; set; }
    public IPlaceable PlacedObject { get; set; }
    public ToolType ToolType { get => _interactableToolType; set => _interactableToolType = value; }

    public Rigidbody _rb;
    public Vector3 _growPosition;

    void Awake() {
        _rb = GetComponent<Rigidbody>();

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


    public GameObject Gather() {
        if (State != GeneralState.CanGather)
            return null;
        return this.gameObject;
    }

    public void Interact() {
        if (!State.HasFlag(GeneralState.CanInteract))
            return;
        State = GeneralState.None;
        StartGrow(_growPosition, PlacedObject);
    }
    public void StartGrow(Vector3 position, IPlaceable tiled) {
        StartCoroutine(GrowInTime(_farmProductOfSeed.GrowTimer, position, tiled));
    }
    public IEnumerator GrowInTime(float GrowTimer, Vector3 position, IPlaceable tiled) {
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



    public abstract void SetGrow(Vector3 PositionToSpawn, IPlaceable farmTiled);

    void OnEnable() {
        State = GeneralState.CanPick | GeneralState.CanGather;
    }

    public GameObject ObjectReferance() {
        return this.gameObject;
    }
}

