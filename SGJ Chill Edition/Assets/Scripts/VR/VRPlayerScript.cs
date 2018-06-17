using UnityEngine;
using UnityEngine.Networking;

public class VRPlayerScript : MonoBehaviour
{
    // Übersichtsposition
    [SerializeField]
    private Transform godPosition;
    // Falle
    [SerializeField]
    private GameObject trapPrefab;
    // Armpointer
    [SerializeField]
    private GvrLaserPointer myPointer;
    // Aktuell aktives Turret
    private GameObject currTurret;
    // Aktuell zu platzierende Falle
    private GameObject currTrap;
    private int trapCount;

    public bool inTurretMode()
    {
        return (currTurret != null);
    }

    public bool inPlacementMode()
    {
        return (currTrap != null);
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
        else if (inPlacementMode())
        {
            // Falle loslassen
            if(GvrControllerInput.AppButtonUp || Input.GetMouseButtonUp(1))
            {
                currTrap = null;
            }
            // Falle bewegen
            else
            {               
                RaycastHit[] myHits = Physics.RaycastAll(myPointer.GetRayForDistance(100).ray, 100, LayerMask.GetMask("Default"));
                // Etwas getroffen
                if (myHits.Length > 0)
                {
                    // Boden getroffen
                    if (myHits[0].transform.tag.Equals("Ground"))
                    {
                        currTrap.transform.position = myHits[0].transform.position;
                    }
                }
            }
        }
        else
        {
            // Click Button wird bereits für Teleportieren genutzt
            if (GvrControllerInput.AppButtonDown || Input.GetMouseButtonDown(1))
            {
                RaycastHit[] myHits = Physics.RaycastAll(myPointer.GetRayForDistance(100).ray, 100, LayerMask.GetMask("Default"));
                // Etwas getroffen
                if (myHits.Length > 0)
                {
                    // Boden getroffen
                    if (myHits[0].transform.tag.Equals("Ground") && ((ExtendedNetworkManager) NetworkManager.singleton).gameManager.getTrapCount() < trapCount)
                    {
                        trapCount++;
                        currTrap = Instantiate(trapPrefab, myHits[0].transform.position, Quaternion.identity);
                    }
                    // Alle Fallen platziert
                    else if(((ExtendedNetworkManager)NetworkManager.singleton).gameManager.getTrapCount() == trapCount)
                    {
                        trapCount++;
                        ((ExtendedNetworkManager)NetworkManager.singleton).gameManager.trapsPlaced();
                    }
                }
            }
        }
    }

    public void OnGameStarted(int pcPlayerLives, float timeInSeconds){
 
    }

    public void OnGameFinished(PlayerStats[] pcPlayers){

    }
}