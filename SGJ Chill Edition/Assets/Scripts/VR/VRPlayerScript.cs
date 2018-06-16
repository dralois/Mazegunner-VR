using UnityEngine;

public class VRPlayerScript : MonoBehaviour
{
    [SerializeField]
    private Transform godPosition; 

    public GameObject currTurret{ get; set; }

    public void TeleportHere(Transform here)
    {
        transform.SetPositionAndRotation(here.position, transform.rotation);
    }

    private void Update()
    {
        if (GvrControllerInput.AppButtonDown)
        {
            currTurret.SetActive(true);
            currTurret = null;
            transform.SetPositionAndRotation(godPosition.position, transform.rotation);
        }
    }
}