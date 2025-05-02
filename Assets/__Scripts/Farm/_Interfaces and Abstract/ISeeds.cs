
using UnityEngine;
public interface ISeeds : IPickable, IGatherable, IInteractable {

    public void SetGrow(Vector3 PositionToSpawn,IPlaceable farmTiled);

    
}
