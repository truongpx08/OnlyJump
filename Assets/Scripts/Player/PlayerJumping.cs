using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerJumping : PlayerAbstract
{
    [SerializeField] public float curForce;
    [SerializeField] protected float minForce;
    [SerializeField] protected float maxForce;

    [SerializeField] public float curMoveSpeed;
    [SerializeField] public float minMoveSpeed;
    [SerializeField] public float maxMoveSpeed;

    protected override void SetDefaultVar()
    {
        base.SetDefaultVar();
        curForce = 0;
        minForce = 1;
        maxForce = 8;

        curMoveSpeed = 0;
        minMoveSpeed = 1;
        maxMoveSpeed = 5;
    }

    public void Jumping()
    {
        if (!Player.PlayerCollision.OnGround) return;
        Jump();
    }


    [Button]
    private void Jump()
    {
        // Apply upward jump force to the Rigidbody
        Player.PlayerRigidbody2D.AddForce(Vector3.up * curForce, (ForceMode2D)ForceMode.VelocityChange);

        // Apply rightward movement force to the Rigidbody
        Player.PlayerRigidbody2D.AddForce(Vector3.right * curMoveSpeed * Player.PlayerFlipping.Direction,
            (ForceMode2D)ForceMode.VelocityChange);
    }

    public void CalculateCurForce(float curTime, float maxTime)
    {
        curForce = ((curTime / maxTime) * (maxForce - minForce)) + minForce;
    }

    public void CalculateCurMoveSpeed(float curTime, float maxTime)
    {
        curMoveSpeed = ((curTime / maxTime) * (maxMoveSpeed - minMoveSpeed)) + minMoveSpeed;
    }
}