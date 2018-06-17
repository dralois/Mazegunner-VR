using UnityEngine;
using UnityEngine.EventSystems;

public class VRPlayerTeleport : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {

    [SerializeField]
    private GameObject myPlayer;
    [SerializeField]
    private GameObject teleportPoint;
    private GameObject myTeleportPoint;

    public void OnPointerDown(PointerEventData ped)
    {
        if (GvrControllerInput.ClickButton && 
            !myPlayer.GetComponent<VRPlayerScript>().inTurretMode() && 
            !myPlayer.GetComponent<VRPlayerScript>().inPlacementMode())
        {
            myTeleportPoint = Instantiate(teleportPoint, ped.pointerCurrentRaycast.worldPosition, teleportPoint.transform.rotation);
        }
    }

    public void OnDrag(PointerEventData ped)
    {
        if(myTeleportPoint != null)
        {
            myTeleportPoint.transform.position = ped.pointerCurrentRaycast.worldPosition;
        }
    }

    public void OnPointerUp(PointerEventData ped)
    {
        if (myTeleportPoint != null)
        {
            myPlayer.transform.position = new Vector3(myTeleportPoint.transform.position.x,
                                                    myPlayer.transform.position.y,
                                                    myTeleportPoint.transform.position.z);
            Destroy(myTeleportPoint);        
        }
    }    
}
