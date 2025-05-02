
using UnityEngine;

public interface IPickable 
{
    
    public GameObject Carry();
    public GeneralState State{set;get;}
    public GameObject ObjectReferance();
    public IPlaceable PlacedObject{get; set;}
    
}
[System.Flags]
public enum GeneralState{

     None        = 0,
    CanPick     = 1 << 0,   // 1
    CanGather   = 1 << 1,   // 2
    CanInteract = 1 << 2,    // 4
    CanAttack = 1 << 3
    

}
