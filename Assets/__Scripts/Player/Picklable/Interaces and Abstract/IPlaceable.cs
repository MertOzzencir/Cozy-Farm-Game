using UnityEngine;

public interface IPlaceable 
{
    public void PlaceOnPlaceable(IPickable objectToPlace);
    public bool CanPlaceOnIt{get;set;}
    public GameObject GMReference();
    
}
