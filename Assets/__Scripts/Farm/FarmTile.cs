
using UnityEngine;

public class FarmTile : MonoBehaviour, IPlaceable {
    private ISeeds _currentSeed;


    public bool CanPlaceOnIt { get; set; }
    public IPickable CurrentPlacedObject { get; set; }

    void Awake() {
        CanPlaceOnIt = true;
    }
    public void PlaceOnPlaceable(IPickable objectToPlace) {
        _currentSeed = objectToPlace.ObjectReferance().GetComponent<ISeeds>();
        if (_currentSeed == null)
            return;

        _currentSeed.State = GeneralState.CanInteract;
        _currentSeed.PlacedObject = this;
        CanPlaceOnIt = false;
        objectToPlace.ObjectReferance().transform.position = transform.position + new Vector3(0, 0.2f, 0);
        _currentSeed.SetGrow(transform.position, this);

    }

    public GameObject GMReference() {
        return this.gameObject;
    }
}
