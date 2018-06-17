using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Turret : MonoBehaviour, IPointerClickHandler
{
    // Hülle
    [SerializeField]
    private MeshRenderer Hull;
    private Color oldCol;
    // Kanonen
    [SerializeField]
    private GameObject Big;
    [SerializeField]
    private GameObject Small;
    public Transform playerPos;
    private bool active;
    // Schiessen
    [SerializeField]
    private Transform smallAnchorLeft;
    [SerializeField]
    private Transform smallAnchorRight;
    [SerializeField]
    private Transform bigAnchorLeft;
    [SerializeField]
    private Transform bigAnchorRight;
    private bool rightShooting;
    private bool leftShooting;
    public bool isShooting { get; private set; }

    [SerializeField]
    private float LaserDuration = 0.25f;
    [SerializeField]
    private string[] ShootableLayers;


    public void GunActive(bool activated)
    {
        Hull.enabled = !activated;
        Big.SetActive(activated);
        Small.SetActive(!activated);
        active = activated;
        StartCoroutine("StartGuns");
    }

    public void ShootGun(bool pi_fEnabled)
    {
        isShooting = pi_fEnabled;
        Big.GetComponent<Railgun>().isShooting = pi_fEnabled;
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

    public IEnumerator Shootright()
    {
        RaycastHit[] hits = Physics.RaycastAll(new Ray(smallAnchorRight.position, transform.forward), 200, LayerMask.GetMask(ShootableLayers));

        gameObject.GetComponent<LineRenderer>().SetPosition(0, hits[0].point);
        gameObject.GetComponent<LineRenderer>().SetPosition(1, bigAnchorRight.position);

        rightShooting = true;
        yield return new WaitForSeconds(LaserDuration);
        rightShooting = false;

        gameObject.GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
        gameObject.GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);
    }

    public IEnumerator Shootleft()
    {
        RaycastHit[] hits = Physics.RaycastAll(new Ray(smallAnchorLeft.position, transform.forward), 200, LayerMask.GetMask(ShootableLayers));

        gameObject.GetComponent<LineRenderer>().SetPosition(0, hits[0].point);
        gameObject.GetComponent<LineRenderer>().SetPosition(1, bigAnchorLeft.position);

        leftShooting = true;
        yield return new WaitForSeconds(LaserDuration);
        leftShooting = false;

        gameObject.GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
        gameObject.GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);
    }

    private void Update()
    {
        if (rightShooting)
        {
            gameObject.GetComponent<LineRenderer>().SetPosition(1, bigAnchorRight.position);
        }
        else if (leftShooting)
        {
            gameObject.GetComponent<LineRenderer>().SetPosition(1, bigAnchorLeft.position);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        VRPlayerScript player = FindObjectOfType<VRPlayerScript>();
        player.TurretMode(gameObject);
        GunActive(true);
    }

}
