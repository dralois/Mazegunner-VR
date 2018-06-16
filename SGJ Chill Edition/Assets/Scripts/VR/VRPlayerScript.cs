using UnityEngine;



public class VRPlayerScript : MonoBehaviour
{
    private bool isInTrapPlacementMode = true;

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
            gameObject.GetComponentInChildren<Light>().enabled = false;
            transform.SetPositionAndRotation(turret.GetComponent<Turret>().playerPos.position,
                                            turret.GetComponent<Turret>().playerPos.rotation);
        }
        else
        {
            UnityEngine.XR.InputTracking.disablePositionalTracking = false;
            gameObject.GetComponentInChildren<Light>().enabled = true;
            transform.SetPositionAndRotation(godPosition.position, godPosition.rotation);
            currTurret.transform.SetPositionAndRotation(currTurret.transform.position, Quaternion.identity);
            currTurret.GetComponent<Turret>().GunActive(false);
        }
        // Speichern
        currTurret = turret;
    }

    private void Update()
    {
        //TODO: handle isInTrapPlacementMode!!!!

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

    public void OnGameStarted(int pcPlayerLives, float timeInSeconds){
        isInTrapPlacementMode = false;
    }

    public void OnGameFinished(PlayerStats[] pcPlayers){
        // TODO: get Scores from all other players and display them.
    }


}