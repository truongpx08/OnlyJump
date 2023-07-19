using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : TruongMonoBehaviour
{
    [SerializeField] protected bool onGround;
    public bool OnGround => onGround;

    protected override void SetDefaultVar()
    {
        base.SetDefaultVar();
        onGround = false;
    }

    public void SetOnGround(bool value)
    {
        onGround = value;
    }
}