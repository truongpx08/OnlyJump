using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerMovement : PlayerAbstract
{
    [SerializeField] protected float speed;
    [SerializeField] private bool canMove;
    [SerializeField] private float direction;

    protected override void SetDefaultVar()
    {
        base.SetDefaultVar();
        speed = 5f;
        canMove = false;
    }

    private void Update()
    {
        Movement();
    }

    public void StartMove()
    { 
        canMove = true;
        DOTween.To(() => direction, x => direction = x, 5, 5);
    }

    protected void Movement()
    {
        if (!canMove) return;
        Move();
        Debug.Log(direction);
    }

    [Button]
    private void Move()
    {
        Player.PlayerRigidbody2D.velocity =
            new Vector2(direction * speed, Player.PlayerRigidbody2D.velocity.y);
    }
}