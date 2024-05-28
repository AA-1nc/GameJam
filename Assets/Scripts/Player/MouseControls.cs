using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControls : MonoBehaviour
{

    //public Vector3 worldPosition;

    public Vector3 hitPosition;


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
    // Start is called before the first frame update
    void Start()
    {
        towerTurretGhost.SetActive(false);
        tallTurretGhost.SetActive(false);
        shortTurretGhost.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        

        #region Move Mouse to Detect World Location

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        Physics.Raycast(ray, out hitData);
        hitPosition = hitData.point;

        #endregion

        #region Click Functions

        if (Input.GetMouseButtonDown(1))
        {
            DeselectTurret();
        }

        #endregion

        #region Controlling the Ghost Towers

        if (currentTurret == ghostTurrets.TOWERTURRET)
        {
            towerTurretGhost.transform.position = hitPosition;
        }
        if (currentTurret == ghostTurrets.TALLTURRET)
        {
            tallTurretGhost.transform.position = hitPosition;
        }
        if (currentTurret == ghostTurrets.SHORTTURRET)
        {
            shortTurretGhost.transform.position = hitPosition;
        }

        #endregion
    }

    public void SelectTowerTurret()
    {
        Debug.Log("Will build tower turret.");
        currentTurret = ghostTurrets.TOWERTURRET;
        towerTurretGhost.SetActive(true);
        tallTurretGhost.SetActive(false);
        shortTurretGhost.SetActive(false);
    }

    public void SelectTallTurret()
    {
        Debug.Log("Will build tall turret.");
        currentTurret = ghostTurrets.TALLTURRET;
        towerTurretGhost.SetActive(false);
        tallTurretGhost.SetActive(true);
        shortTurretGhost.SetActive(false);
    }

    public void SelectShortTurret()
    {
        Debug.Log("Will build short turret");
        currentTurret = ghostTurrets.SHORTTURRET;
        towerTurretGhost.SetActive(false);
        tallTurretGhost.SetActive(false);
        shortTurretGhost.SetActive(true);
    }

    public void DeselectTurret()
    {
        Debug.Log("Deselected turret");
        currentTurret = ghostTurrets.NONE;
        towerTurretGhost.SetActive(false);
        tallTurretGhost.SetActive(false);
        shortTurretGhost.SetActive(false);
    }
}
