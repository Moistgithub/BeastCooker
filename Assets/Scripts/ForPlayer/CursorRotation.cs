using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorRotation : MonoBehaviour
{
    public Transform player;
    public float radius;
    public float rotationSpeed;
    private float currentAngle;

    // Update is called once per frame
    void Update()
    {
        //get position of the player
        Vector3 playerVector = Camera.main.WorldToScreenPoint(player.position);

        //get difference between the mouse position and the player position
        Vector3 mousePosition = Input.mousePosition - playerVector;

        //calculate the angle between the player and the mouse position
        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        //update angle of the triangle circular motion to match mouse
        currentAngle = angle;

        //calculate the new position of the triangle using polar coordinates (for future mustafa coordinates using distance and angles rather than cartesian)
        Vector3 offset = new Vector3(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad), 0) * radius;

        //update the position of the triangle based on the calculated offset
        transform.position = player.position + offset;

        //rotate the triangle so the tip points towards the mouse
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }
}
