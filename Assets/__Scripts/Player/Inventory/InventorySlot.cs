using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour,IPlaceable {

    public bool CanPlaceOnIt { get ; set ; }

    void Awake() {
        CanPlaceOnIt = true;
    }
    public void PlaceOnPlaceable(IPickable objectToPlace) {
        if(objectToPlace.ObjectReferance().GetComponent<IGatherable>() == null)
            return;
        objectToPlace.PlacedObject = this;
        CanPlaceOnIt = false;
        objectToPlace.State = GeneralState.CanPick;
        objectToPlace.ObjectReferance().GetComponent<Rigidbody>().isKinematic = true;

        objectToPlace.ObjectReferance().transform.parent = transform;
        objectToPlace.ObjectReferance().transform.position = transform.position;
    }

    public GameObject GMReference() {
        return this.gameObject;
    }
}
