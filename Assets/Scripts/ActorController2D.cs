using System;
using UnityEngine;

// Include grounding detection logics used by actors
public abstract class ActorController2D : Controller2D

{
    [Header("Grounding Detection")]
    public LayerMask groundLayers;
    public float groundRayLength = 0.1f;
    public float groundRaySpread = 0.35f;
    public bool grounded = false;

    private static RaycastHit2D RaycastDown(Vector3 origin, float distance, LayerMask targetedLayers)
    {
        var hit = Physics2D.Raycast(origin, Vector2.down, distance, targetedLayers);
        Debug.DrawLine(origin, origin + Vector3.down * distance, Color.red);
        return hit;
    }

    protected bool IsGrounded()
    {
        var rayStartCenter = transform.position + Vector3.up * (groundRayLength * 0.5f);
        var rayStartLeft = rayStartCenter + Vector3.left * groundRaySpread;
        var rayStartRight = rayStartCenter + Vector3.right * groundRaySpread;

        var hitCenter = IsRayHit(rayStartCenter, groundRayLength, groundLayers);
        var hitLeft = IsRayHit(rayStartLeft, groundRayLength, groundLayers);
        var hitRight = IsRayHit(rayStartRight, groundRayLength, groundLayers);

        // true if any of the three rays hit the ground
        return hitCenter || hitLeft || hitRight;
    }

    protected static bool IsRayHit(Vector3 rayStart, float rayLength, LayerMask targetedLayers)
    {
        var hit = RaycastDown(rayStart, rayLength, targetedLayers);
        return hit.collider != null;
    }
}