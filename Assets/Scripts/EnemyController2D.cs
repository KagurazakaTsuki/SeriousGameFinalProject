using UnityEngine;

public class EnemyController2D : ActorController2D
{
    protected override void OnImpact(Vector3 impactDirection, Controller2D actor)
    {
        if (!actor.CompareTag("Player")
            || actor is not PlatformerController2D platformer) return;

        if (impactDirection.y > 0f)
        {
            Destroy(gameObject);
            platformer.points += 0.5f;
        }
    }
}