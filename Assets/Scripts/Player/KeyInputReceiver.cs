using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeyInputReceiver : PlayerAbstract
{
    [SerializeField] protected KeyCode key;

    protected override void Awake()
    {
        base.Awake();
        SetKey();
    }

    protected virtual void Update()
    {
        KeyDown();
        KeyUp();
    }

    protected void KeyDown()
    {
        if (!Input.GetKeyDown(key)) return;
        OnKeyDown();
    }

    protected void KeyUp()
    {
        if (!Input.GetKeyUp(key)) return;
        OnKeyUp();
    }

    protected virtual void SetKey()
    {
    }

    protected virtual void OnKeyDown()
    {
    }

    protected virtual void OnKeyUp()
    {
    }
}