
using System;


public interface IFarmProduct : IGatherable, IPickable,IInteractable {

    void Set();
    FarmProductType Type { get; set; }
    float Damage{get;set;}


}
public enum FarmProductType {
    Cauliflower,
    Carrot

}
