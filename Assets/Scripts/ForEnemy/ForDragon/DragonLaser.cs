using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonLaser : MonoBehaviour
{
    [Header("Public Variables")]
    public Transform player;
    public float aimDur;
    public float laserDur;
    public GameObject laserPrefab;

    [Header("Private Variables")]
    private LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ChargeShot());
        }
    }
    private IEnumerator ChargeShot()
    {
        Vector3 targetPos = player.position;
        lr.startColor = Color.yellow;
        lr.endColor = Color.red;

        for (float timer =0; timer < aimDur; timer += Time.deltaTime)
        {
            targetPos = player.position;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, targetPos);
            yield return null;
        }

        lr.startColor = Color.red;
        lr.endColor = Color.red;

        yield return new WaitForSeconds(0.5f);

        Debug.Log("fire");
        FireLaser(targetPos);
        lr.SetPosition(0, Vector3.zero);
        lr.SetPosition(1, Vector3.zero);
    }

    private void FireLaser(Vector3 targetPos)
    {
        Vector3 dir = (targetPos - transform.position).normalized;
        GameObject laser = Instantiate(laserPrefab, transform.position, transform.rotation);
        //laser.transform.rotation = Vector2.Angle(transform.position,new Vector2 (targetPos.x, targetPos.y))
        //Destroy(laser, laserDur);
    }
}
