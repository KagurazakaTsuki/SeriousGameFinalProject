using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlatformerController2D : ActorController2D
{
    [Header("Platformer Controller")]
    public float horizontalSpeed = 6;
    public float jumpForce = 15;

    public int maxHealth = 5;
    private int currentHealth;

    public float points = 0f;

    private bool invulnerable = false;

    private SpriteRenderer sRenderer;

    protected override void Start()
    {
        base.Start();
        sRenderer = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
    }

    void Update()
    {
        grounded = IsGrounded();

        // Input control
        var vel = rb2d.velocity;

        var inputHorizontal = Input.GetAxis("Horizontal");
        vel.x = inputHorizontal * horizontalSpeed;

        var inputJump = Input.GetButton("Jump");
        if (grounded && inputJump)
            vel.y = jumpForce;

        rb2d.velocity = vel;
    }

    protected override void OnImpact(Vector3 impactDirection, Controller2D actor)
    {
        if (!actor.CompareTag("Hostile")) return;

        // impact from top
        if ((Mathf.Abs(impactDirection.x) > Mathf.Abs(impactDirection.y)) ||
            (impactDirection.y > 0f))
        {
            TakeDamage();
        }
        else
        {
            var vel = rb2d.velocity;
            vel.y = jumpForce;
            rb2d.velocity = vel;
        }
    }

    void TakeDamage()
    {
        if (invulnerable)
            return;

        ChangeHealth(-1);

        if (currentHealth <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        StartCoroutine(Invulnerability(1f));
    }

    void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log($"Health {currentHealth}/{maxHealth}");
    }

    IEnumerator Invulnerability(float time)
    {
        invulnerable = true;

        for (var i = 0; i < time / 0.2f; i++)
        {
            sRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }

        invulnerable = false;
    }
}