using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Turret : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private MeshRenderer Hull;
    public Transform playerPos;
    private Color oldCol;
    [SerializeField]
    private GameObject Big;
    [SerializeField]
    private GameObject Small;
    private bool active;

    public void GunActive(bool activated)
    {
        Hull.enabled = !activated;
        Big.SetActive(activated);
        Small.SetActive(!activated);
        active = activated;
        StartCoroutine("StartGuns");
    }

    public void ShootGun(bool shooting)
    {
        Big.GetComponent<Railgun>().isShooting = shooting;
    }

    IEnumerator StartGuns()
    {
        yield return new WaitForSeconds(.2f);
        Big.GetComponent<Railgun>().isActive = active;
    }

    public void Select(bool pi_fSelect)
    {
        if (pi_fSelect)
        {
            oldCol = Hull.materials[1].color;
            Hull.materials[1].color = Color.yellow;
        }
        else
        {
            Hull.materials[1].color = oldCol;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        VRPlayerScript player = FindObjectOfType<VRPlayerScript>();
        player.TurretMode(gameObject);
        GunActive(true);
    }

}
