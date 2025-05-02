
public interface IInteractable :IPickable
{

    void Interact();
    ToolType ToolType{get;set;}
}

public enum ToolType{

    None,
    Hands,
    WateringCans

}
