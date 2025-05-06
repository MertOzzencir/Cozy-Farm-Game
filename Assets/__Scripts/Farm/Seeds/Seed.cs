using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : ISeeds {
    
   
   
  
    public override void SetGrow(Vector3 PositionToSpawn, IPlaceable farmTiled) {
        _rb.isKinematic = true;
        _growPosition = PositionToSpawn;
        PlacedObject = farmTiled;
    }
}
