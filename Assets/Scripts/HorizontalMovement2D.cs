using UnityEngine;

public class HorizontalMovement2D : ActorController2D
{
    [Header("Horizontal Movement")]
    public float horizontalSpeed = 1f;

    public float ledgeTestLeft = 0.4f;
    public float ledgeTestRight = 0.4f;


    private int direction = 1;

    void Update()
    {
        grounded = IsGrounded();
        direction = GetDirection();

        var vel = rb2d.velocity;
        vel.x = direction * horizontalSpeed;

        rb2d.velocity = vel;
    }

    int GetDirection()
    {
        // freeze when in air
        if (!grounded)
            return 0;
        // continue moving if grounded again
        if (direction == 0)
            return 1;

        var ledgeRayStartLeft =
            transform.position + Vector3.up * (groundRayLength * 0.5f) + Vector3.left * ledgeTestLeft;
        if (!IsRayHit(ledgeRayStartLeft, groundRayLength, groundLayers))
            return 1;

        var ledgeRayStartRight =
            transform.position + Vector3.up * (groundRayLength * 0.5f) + Vector3.right * ledgeTestRight;
        if (!IsRayHit(ledgeRayStartRight, groundRayLength, groundLayers))
            return -1;

        // keep the current direction
        return direction;
    }

    protected override void OnImpact(Vector3 impactDirection, Controller2D actor)
    {
        if (Mathf.Abs(impactDirection.x) > Mathf.Abs(impactDirection.y))
            direction = (int)Mathf.Sign(-impactDirection.x);
    }
}