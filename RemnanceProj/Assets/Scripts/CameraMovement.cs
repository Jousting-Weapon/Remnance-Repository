using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraMovement : MonoBehaviour
{
    public TextMeshPro startText;

    public Transform startMarker;
    public Transform endMarker;
    public float speed = 10.0F;
    private float startTime;
    private float journeyLength;
    public bool isMoving;

    private void Awake()
    {
        isMoving = true;
    }

    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }
    void Update()
    {
        if(isMoving)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
            transform.rotation = Quaternion.Lerp(startMarker.rotation, endMarker.rotation, fracJourney);
            if(Vector3.Distance(transform.position, endMarker.position) < 5)
            {
                isMoving = false;
            }
        }
    }
}
