using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bazooka : MonoBehaviour
{

    public GameObject boulet;
    public GameObject ProjectileBazookaSpawner;
    
    public float FireRate = 0.5f;
    public GameObject TrajectoryPointPrefeb;
    private float power = 1;
    private int numOfTrajectoryPoints = 30;
    private List<GameObject> trajectoryPoints;

    private bool fire;
    void Start()
    {
        fire = true;
        trajectoryPoints = new List<GameObject>();
        
        // TrajectoryPoints are instatiated
        for (int i = 0; i < numOfTrajectoryPoints; i++)
        {
            GameObject dot = Instantiate(TrajectoryPointPrefeb);
            trajectoryPoints.Insert(i, dot);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = (Input.mousePosition - sp).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        Vector3 vel = GetForceFrom(ProjectileBazookaSpawner.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

        setTrajectoryPoints(ProjectileBazookaSpawner.transform.position, vel);

        if (Input.GetMouseButtonDown(0) && fire)
        {
            fire = false;
            ResetFire();
            GameObject projectile = Instantiate(boulet) as GameObject;
            projectile.transform.position = transform.position;
            projectile.AddComponent<BoxCollider2D>();
            Rigidbody2D rg = projectile.AddComponent<Rigidbody2D>();
            transform.eulerAngles = new Vector3(0, 0, angle);
            //projectile.GetComponent<Rigidbody2D>().useGravity = true;
            rg.AddForce(GetForceFrom(projectile.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)), ForceMode2D.Impulse);

        }

    }

    // Following method returns force by calculating distance between given two points
    private Vector2 GetForceFrom(Vector3 fromPos, Vector3 toPos)
    {
        return (new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y)) * power;
    }

    // Displays projectile trajectory path. It takes two arguments, start position of object and initial velocity of object.
    private void setTrajectoryPoints(Vector3 pStartPosition, Vector3 pVelocity)
    {
        float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));
        float fTime = 0;
        fTime += 0.1f;
        for (int i = 0; i < numOfTrajectoryPoints; i++)
        {
            float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
            Vector3 pos = new Vector3(pStartPosition.x + dx, pStartPosition.y + dy, 2);
            trajectoryPoints[i].transform.position = Vector3.Scale(pos, new Vector3(1,1,0));
            //trajectoryPoints[i].renderer.enabled = true;
            trajectoryPoints[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude) * fTime, pVelocity.x) * Mathf.Rad2Deg);
            fTime += 0.1f;
        }
    }

    private void ResetFire()
    {
        StartCoroutine(ResetFireCo());
    }

    private IEnumerator ResetFireCo()
    {
        yield return new WaitForSeconds(FireRate);
        fire = true;
    }

}
