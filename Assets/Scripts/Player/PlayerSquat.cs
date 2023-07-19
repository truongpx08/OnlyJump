using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Squat to gain momentum for the jump
/// </summary>
public class PlayerSquat : KeyInputReceiver
{
    [SerializeField] protected float maxTime;
    [SerializeField] protected float curTime;
    [SerializeField] protected bool canCalculatedTime;

    protected override void SetDefaultVar()
    {
        base.SetDefaultVar();
        curTime = 0;
        maxTime = 1;
        canCalculatedTime = false;
    }

    protected override void Update()
    {
        base.Update();
        CalculateJumpTime();
    }

    protected override void SetKey()
    {
        base.SetKey();
        key = KeyCode.DownArrow;
    }

    protected override void OnKeyDown()
    {
        base.OnKeyDown();
        StartCalculateJumpTime();
    }

    protected override void OnKeyUp()
    {
        base.OnKeyUp();
        StopCalculateJumpTime();
    }

    protected void StartCalculateJumpTime()
    {
        canCalculatedTime = true;
        curTime = 0;
    }

    protected void CalculateJumpTime()
    {
        if (!canCalculatedTime) return;
        if (curTime > maxTime)
        {
            StopCalculateJumpTime();
            return;
        }

        curTime += Time.deltaTime;
    }

    protected void StopCalculateJumpTime()
    {
        canCalculatedTime = false;
        Player.PlayerJumping.CalculateCurForce(curTime, maxTime);
        Player.PlayerJumping.CalculateCurMoveSpeed(curTime, maxTime);
        Player.PlayerJumping.Jumping();
    }
}