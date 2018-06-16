using UnityEngine;
using UnityEngine.EventSystems;

public class Turret : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private MeshRenderer selector;
    public Transform playerPos;
    private Color oldCol;

    public void Select(bool pi_fSelect)
    {
        if (pi_fSelect)
        {
            oldCol = selector.materials[1].color;
            selector.materials[1].color = Color.yellow;
        }
        else
        {
            selector.materials[1].color = oldCol;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        VRPlayerScript player = FindObjectOfType<VRPlayerScript>();
        player.TurretMode(gameObject);
        gameObject.GetComponentInChildren<Railgun>().isActive = true;
    }

}
