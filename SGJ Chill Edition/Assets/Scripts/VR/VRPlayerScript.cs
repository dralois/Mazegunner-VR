using UnityEngine;



public class VRPlayerScript : MonoBehaviour
{

    // Übersichtsposition
    [SerializeField]
    private Transform godPosition;
    [SerializeField]
    private GameObject trapPrefab;
    // Aktuell aktives Turret
    private GameObject currTurret;
    // Aktuell zu platzierende Falle
    private GameObject currTrap; 

    public bool inTurretMode()
    {
        return (currTurret != null);
    }

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
        // Aktuell im Turret
        if(inTurretMode())
        {
            // Deaktiviere ggf. Turret
            if (GvrControllerInput.AppButtonDown || Input.GetMouseButtonDown(1))
            {
                TurretMode(null);
            }
            // Aktiviere schießen
            if(GvrControllerInput.ClickButton || Input.GetMouseButton(0))
            {
                currTurret.GetComponent<Turret>().ShootGun(true);
            }
            else if(currTurret != null)
            {
                if(currTurret.GetComponent<Turret>().isShooting)
                    currTurret.GetComponent<Turret>().ShootGun(false);
            }
            // Turret pos/rot updaten
            currTurret.transform.rotation = gameObject.GetComponentInChildren<Camera>().transform.rotation;
            gameObject.GetComponentInChildren<Camera>().transform.position = currTurret.GetComponent<Turret>().playerPos.position;            
        }
        else
        {
            // Click Button wird bereits für Teleportieren genutzt
            if (GvrControllerInput.AppButtonDown || Input.GetMouseButtonDown(1))
            {
                GvrLaserPointer myPointer = FindObjectOfType<GvrLaserPointer>();
                GvrBasePointer.PointerRay myRay = myPointer.GetRayForDistance(100);
                RaycastHit[] myHits = Physics.RaycastAll(myRay.ray, myRay.distanceFromStart, LayerMask.GetMask("Default"));
                currTrap = Instantiate(trapPrefab, myHits[0].transform.position, Quaternion.identity);
            }
        }
    }

    public void OnGameStarted(int pcPlayerLives, float timeInSeconds){
 
    }

    public void OnGameFinished(PlayerStats[] pcPlayers){
        // TODO: get Scores from all other players and display them.
    }


}