using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float speed = 5;
    public float globalMaxX;
    public float globalMaxY;
    public float globalMinX;
    public float globalMinY;

    void Start()
    {
    }

    void Update()
    {
        var start = transform.position;
        var goal = target.position + new Vector3(0, 0, -1);
        var t = Time.deltaTime * speed;
        var newPosition = Vector3.Lerp(start, goal, t);
        var maxX = globalMaxX - Camera.main.orthographicSize * Camera.main.aspect;
        var maxY = globalMaxY - Camera.main.orthographicSize;
        var minX = globalMinX + Camera.main.orthographicSize * Camera.main.aspect;
        var minY = globalMinY + Camera.main.orthographicSize;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        transform.position = newPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(globalMinX, globalMinY), new Vector3(globalMaxX, globalMinY));
        Gizmos.DrawLine(new Vector3(globalMinX, globalMinY), new Vector3(globalMinX, globalMaxY));
        Gizmos.DrawLine(new Vector3(globalMaxX, globalMaxY), new Vector3(globalMaxX, globalMinY));
        Gizmos.DrawLine(new Vector3(globalMaxX, globalMaxY), new Vector3(globalMinX, globalMaxY));
    }
}