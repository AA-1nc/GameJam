using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MouseControls : MonoBehaviour
{

    

    public Vector3 hitPosition;

    #region Turret Variables
    [SerializeField] GameObject towerTurretGhost;
    [SerializeField] GameObject tallTurretGhost;
    [SerializeField] GameObject shortTurretGhost;
    public enum ghostTurrets
    {
        NONE,
        TOWERTURRET,
        TALLTURRET,
        SHORTTURRET
    }
    public ghostTurrets currentTurret; 
    public bool obstructed;

    public bool changeColor;

    public CapsuleCollider ghostTurretCollider;

    [SerializeField] GameObject towerTurretPrefab;
    [SerializeField] GameObject tallTurretPrefab;
    [SerializeField] GameObject shortTurretPrefab;
 
    #endregion

    #region Finances
    [SerializeField] int resources = 200;
    public int currentPrice;
    public TextMeshProUGUI scrapText;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        towerTurretGhost.SetActive(false);
        tallTurretGhost.SetActive(false);
        shortTurretGhost.SetActive(false);
        DisplayCurrentFunds();
    }

    // Update is called once per frame
    void Update()
    {
        

        #region Standard Mouse Functions

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        Physics.Raycast(ray, out hitData);
        hitPosition = hitData.point;

        //Credit to GameDevBeginner for the above code. https://gamedevbeginner.com/raycasts-in-unity-made-easy/

        if (Input.GetMouseButtonDown(1))
        {
            DeselectTurret();
        }

        #endregion

        #region Controlling the Ghost Towers
        if (currentTurret == ghostTurrets.TOWERTURRET)
        {
            towerTurretGhost.transform.position = new Vector3(hitPosition.x, 0.999f, hitPosition.z);
            
        //Thanks to SpeedTutor for help with this. https://youtu.be/VEAU95v5MO8?si=h7uH6CCgzVPjDU-7
        if (towerTurretGhost.GetComponent<GhostTowerCollisions>().obstructed == true || resources < currentPrice)
        {
            towerTurretGhost.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            towerTurretGhost.GetComponent<MeshRenderer>().material.color = Color.green;

            if (Input.GetMouseButtonDown(0))
            {
                resources -= currentPrice;
                DisplayCurrentFunds();
                //Debug.Log("Placed a Tower Turret");
                Instantiate(towerTurretPrefab, towerTurretGhost.transform.position, towerTurretGhost.transform.rotation);
            }
            
        }
        }
        
        
        if (currentTurret == ghostTurrets.TALLTURRET)
        {
            tallTurretGhost.transform.position = new Vector3(hitPosition.x, 0, hitPosition.z);

            //Thanks to SpeedTutor for help with this. https://youtu.be/VEAU95v5MO8?si=h7uH6CCgzVPjDU-7
            if (tallTurretGhost.GetComponent<GhostTowerCollisions>().obstructed == true || resources < currentPrice)
        {
            tallTurretGhost.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            tallTurretGhost.GetComponentInChildren<MeshRenderer>().material.color = Color.green;

             if (Input.GetMouseButtonDown(0))
            {
                resources -= currentPrice;
                DisplayCurrentFunds();
                //Debug.Log("Placed a Tall Turret");
                Instantiate(tallTurretPrefab, tallTurretGhost.transform.position, tallTurretGhost.transform.rotation);
            }
        }
        }


        if (currentTurret == ghostTurrets.SHORTTURRET)
        {
            shortTurretGhost.transform.position = new Vector3(hitPosition.x, 0, hitPosition.z);

            //Thanks to SpeedTutor for help with this. https://youtu.be/VEAU95v5MO8?si=h7uH6CCgzVPjDU-7
            if (shortTurretGhost.GetComponent<GhostTowerCollisions>().obstructed == true || resources < currentPrice)
        {
            shortTurretGhost.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            shortTurretGhost.GetComponent<MeshRenderer>().material.color = Color.green;

             if (Input.GetMouseButtonDown(0))
            {
                resources -= currentPrice;
                DisplayCurrentFunds();
                //Debug.Log("Placed a Short Turret");
                Instantiate(shortTurretPrefab, shortTurretGhost.transform.position, shortTurretGhost.transform.rotation);
            }
        }
        }

        #endregion
    }

    public void SelectTowerTurret()
    {
        //Debug.Log("Will build tower turret.");
        currentTurret = ghostTurrets.TOWERTURRET;
        towerTurretGhost.SetActive(true);
        tallTurretGhost.SetActive(false);
        shortTurretGhost.SetActive(false);
        ghostTurretCollider = towerTurretGhost.GetComponent<CapsuleCollider>();
        currentPrice = 100;
    }

    public void SelectTallTurret()
    {
        //Debug.Log("Will build tall turret.");
        currentTurret = ghostTurrets.TALLTURRET;
        towerTurretGhost.SetActive(false);
        tallTurretGhost.SetActive(true);
        shortTurretGhost.SetActive(false);
        ghostTurretCollider = tallTurretGhost.GetComponent<CapsuleCollider>();
        currentPrice = 150;
    }

    public void SelectShortTurret()
    {
        //Debug.Log("Will build short turret");
        currentTurret = ghostTurrets.SHORTTURRET;
        towerTurretGhost.SetActive(false);
        tallTurretGhost.SetActive(false);
        shortTurretGhost.SetActive(true);
        ghostTurretCollider = shortTurretGhost.GetComponent<CapsuleCollider>();
        currentPrice = 65;

    }

    public void DeselectTurret()
    {
        //Debug.Log("Deselected turret");
        currentTurret = ghostTurrets.NONE;
        towerTurretGhost.SetActive(false);
        tallTurretGhost.SetActive(false);
        shortTurretGhost.SetActive(false);
        ghostTurretCollider = null;
    }


    public void AddResources(int addition)
    {
        resources += addition;
        DisplayCurrentFunds();
    }

    public void DisplayCurrentFunds()
    {
        scrapText.text = "Scrap: " + resources;
    }

    

}
