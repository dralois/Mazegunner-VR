using UnityEngine;

public class VRPlayerScript : MonoBehaviour
{
    // Übersichtsposition
    [SerializeField]
    private Transform godPosition;

    // Aktuell aktives Turret
    private GameObject currTurret;

    public void TurretMode(GameObject turret)
    {
        // Turret aktivieren / deaktivieren
        if(turret != null)
        {
            if(currTurret != null)
            {
                // Altes reaktivieren
                currTurret.GetComponent<Turret>().Select(false);
            }
            // Danach Tracking aus und Position wechseln
            UnityEngine.XR.InputTracking.disablePositionalTracking = true;
            transform.SetPositionAndRotation(turret.GetComponent<Turret>().playerPos.position,
                                            turret.GetComponent<Turret>().playerPos.rotation);
        }
        else
        {
            UnityEngine.XR.InputTracking.disablePositionalTracking = false;
            transform.SetPositionAndRotation(godPosition.position, godPosition.rotation);
            currTurret.GetComponentInChildren<Railgun>().isActive = false;
        }
        // Speichern
        currTurret = turret;
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
                currTurret.GetComponentInChildren<Railgun>().isShooting = true;
            }
            else if(currTurret != null)
            {
                currTurret.GetComponentInChildren<Railgun>().isShooting = false;
            }
            // Turret pos/rot updaten
            currTurret.transform.rotation = gameObject.GetComponentInChildren<Camera>().transform.rotation;
            gameObject.GetComponentInChildren<Camera>().transform.position = currTurret.GetComponent<Turret>().playerPos.position;
            //currTurret.transform.position = gameObject.GetComponentInChildren<Camera>().transform.position;
        }
    }
}