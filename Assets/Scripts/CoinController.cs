using UnityEngine;

public class CoinController : Controller2D
{
    protected override void OnImpact(Vector3 impactDirection, Controller2D actor)
    {
        if (!actor.CompareTag("Player")
            || actor is not PlatformerController2D platformer) return;

        platformer.points++;
        Debug.Log($"Points: {platformer.points}");
        Destroy(gameObject);
    }
}