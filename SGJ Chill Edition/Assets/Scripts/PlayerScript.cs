using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject currTurret;

    public void TeleportHere(Transform here)
    {
        transform.SetPositionAndRotation(here.position, transform.rotation);
    }
}