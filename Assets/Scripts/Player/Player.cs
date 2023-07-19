using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : TruongMonoBehaviour
{
    [SerializeField] protected PlayerModel playerModel;
    public PlayerModel PlayerModel => playerModel;
    [SerializeField] protected PlayerJumping playerJumping;
    public PlayerJumping PlayerJumping => playerJumping;
    [SerializeField] protected PlayerCollision playerCollision;
    public PlayerCollision PlayerCollision => playerCollision;
    [SerializeField] protected PlayerFlipping playerFlipping;
    public PlayerFlipping PlayerFlipping => playerFlipping;
    [SerializeField] private Rigidbody2D playerRigidbody2D;
    public Rigidbody2D PlayerRigidbody2D => playerRigidbody2D;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerModel();
        LoadPlayerJumping();
        LoadPlayerCollision();
        LoadPlayerFlipping();
        LoadPlayerRb();
    }

    private void LoadPlayerRb()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }


    private void LoadPlayerFlipping()
    {
        playerFlipping = GetComponentInChildren<PlayerFlipping>();
    }

    private void LoadPlayerCollision()
    {
        playerCollision = GetComponentInChildren<PlayerCollision>();
    }

    private void LoadPlayerModel()
    {
        playerModel = GetComponentInChildren<PlayerModel>();
    }

    private void LoadPlayerJumping()
    {
        playerJumping = GetComponentInChildren<PlayerJumping>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag($"Ground")) return;
        PlayerCollision.SetOnGround(true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag($"Ground")) return;
        PlayerCollision.SetOnGround(false);
    }
}