using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Animator2D : MonoBehaviour
{
    public enum AnimationState
    {
        Idle,
        Walk,
        Jump
    }

    public float animationFPS = 5;

    public Sprite[] idleAnimation;
    public Sprite[] walkAnimation;
    public Sprite[] jumpAnimation;

    private Rigidbody2D rb2d;
    private SpriteRenderer sRenderer;
    private ActorController2D controller;

    private float frameTimer = 0f;
    private int frameIndex = 0;
    private AnimationState state = AnimationState.Idle;
    private Dictionary<AnimationState, Sprite[]> animationAtlas;

    void Start()
    {
        animationAtlas = new Dictionary<AnimationState, Sprite[]>();
        animationAtlas.Add(AnimationState.Idle, idleAnimation);
        animationAtlas.Add(AnimationState.Walk, walkAnimation);
        animationAtlas.Add(AnimationState.Jump, jumpAnimation);

        rb2d = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<ActorController2D>();
    }

    void Update()
    {
        var newState = GetAnimationState();
        if (state != newState)
            TransitionToState(newState);

        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0f)
        {
            sRenderer.sprite = animationAtlas[state][frameIndex];

            frameIndex++;
            frameIndex %= animationAtlas[state].Length;

            frameTimer = 1 / animationFPS;
        }

        sRenderer.flipX = rb2d.velocity.x switch
        {
            < -0.01f => true,
            > 0.01f => false,
            _ => sRenderer.flipX
        };
    }

    void TransitionToState(AnimationState newState)
    {
        frameTimer = 0f;
        frameIndex = 0;
        state = newState;
    }

    AnimationState GetAnimationState()
    {
        if (!controller.grounded)
            return AnimationState.Jump;
        if (Mathf.Abs(rb2d.velocity.x) > 0.1f)
            return AnimationState.Walk;
        return AnimationState.Idle;
    }
}