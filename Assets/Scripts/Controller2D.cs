using UnityEngine;


public abstract class Controller2D : MonoBehaviour
{
    protected Rigidbody2D rb2d;

    protected virtual void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        var controller = col.gameObject.GetComponent<Controller2D>();
        // colliding with nothing
        if (controller == null)
            return;
        var impactDirection = col.gameObject.transform.position - transform.position;
        OnImpact(impactDirection, controller);
    }

    protected abstract void OnImpact(Vector3 impactDirection, Controller2D actor);
}