using UnityEngine;
using UnityEngine.EventSystems;

public class Turret : MonoBehaviour, IPointerClickHandler
{
    public void Select(bool pi_fSelect)
    {
        if (pi_fSelect)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }    

    public void TurretSelect(bool pi_fSelect)
    {
        gameObject.SetActive(!pi_fSelect);
        gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        VRPlayerScript player = FindObjectOfType<VRPlayerScript>();
        if(player != null)
        {
            if(player.currTurret != null)
                player.currTurret.SetActive(true);
            // Teleport to turret
            player.currTurret = gameObject;
            player.TeleportHere(transform);
            gameObject.SetActive(false);
        }
    }

}
