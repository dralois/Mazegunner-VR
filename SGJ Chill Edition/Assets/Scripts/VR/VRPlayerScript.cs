using UnityEngine;

public class VRPlayerScript : MonoBehaviour
{
    // Übersichtsposition
    [SerializeField]
    private Transform godPosition;

    // Aktuell aktives Turret
    private GameObject currTurret;

    public void TurretMode(GameObject pi_Turret)
    {
        // Turret Mode verlassen
        if (pi_Turret == null)
        {
            UnityEngine.XR.InputTracking.disablePositionalTracking = false;
            gameObject.GetComponentInChildren<Light>().enabled = true;
            transform.SetPositionAndRotation(godPosition.position, godPosition.rotation);
        }
        // Vorheriges Turret zurücksetzen
        if (currTurret != null)
        {
            // Altes reaktivieren
            currTurret.GetComponent<Turret>().Select(false);
            currTurret.transform.SetPositionAndRotation(currTurret.transform.position,
                Quaternion.Euler(0, currTurret.transform.rotation.eulerAngles.y, 0));
            currTurret.GetComponent<Turret>().GunActive(false);
        }
        // In neues Turret wechseln
        if(pi_Turret != null)
        {
            UnityEngine.XR.InputTracking.disablePositionalTracking = true;
            gameObject.GetComponentInChildren<Light>().enabled = false;
            transform.SetPositionAndRotation(pi_Turret.GetComponent<Turret>().playerPos.position,
                                            pi_Turret.GetComponent<Turret>().playerPos.rotation);            
        }
        // Neues speichern
        currTurret = pi_Turret;
    }

    private void Update()
    {
        // Deaktiviere ggf. Turret
        if (GvrControllerInput.AppButton || Input.GetMouseButtonDown(1))
        {
            TurretMode(null);
        }
        // Aktuell im Turret
        if(currTurret != null)
        {
            // Aktiviere schießen
            if(GvrControllerInput.ClickButton || Input.GetMouseButton(0))
            {
                currTurret.GetComponent<Turret>().ShootGun(true);
            }
            else if(currTurret != null)
            {
                currTurret.GetComponent<Turret>().ShootGun(false);
            }
            // Turret pos/rot updaten
            currTurret.transform.rotation = gameObject.GetComponentInChildren<Camera>().transform.rotation;
            gameObject.GetComponentInChildren<Camera>().transform.position = currTurret.GetComponent<Turret>().playerPos.position;            
        }
    }
}