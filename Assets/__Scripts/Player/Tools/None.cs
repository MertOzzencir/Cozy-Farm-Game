
using UnityEngine;

public class None : Tools {

    public override void Use(IInteractable interactedObject, GameObject targetPositionForAnimation) {
        if (ToolAnimation != null) {
            StopCoroutine(ToolAnimation);
            ToolAnimation = null;
        }
        else {
            ToolAnimation = StartCoroutine(UseAnimation(targetPositionForAnimation));
        }
        interactedObject.Interact();
    }


}
