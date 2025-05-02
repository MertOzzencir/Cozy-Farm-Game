using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaceObjects : MonoBehaviour {

    PickObjects _pickObjects;
    void Start() {
        _pickObjects = GetComponent<PickObjects>();
    }

    void Place(IPickable placedObject) {

        GameObject referance = placedObject.ObjectReferance();
        Vector3 centerOfSphere = referance.transform.position;
        Collider[] colliders = Physics.OverlapSphere(centerOfSphere, .5f);


        foreach (Collider hit in colliders) {
            IPlaceable placeable = hit.gameObject.GetComponent<IPlaceable>();
            if (placeable != null && placeable.CanPlaceOnIt) {
                placedObject.ObjectReferance().GetComponent<Rigidbody>().isKinematic = false;
                placeable.PlaceOnPlaceable(placedObject);
                break;
            }
            else
                placedObject.ObjectReferance().GetComponent<Rigidbody>().isKinematic = false;



        }



        _pickObjects.ClearCurrentObject();
    }

    void OnEnable() {
        PickObjects.OnPlace += Place;
    }
    void OnDisable() {
        PickObjects.OnPlace -= Place;
    }


}
