using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerFlipping : PlayerAbstract
{
    [SerializeField] protected Vector3 originScale;
    [SerializeField] private int direction;
    public int Direction => direction;
    protected override void SetDefaultVar()
    {
        base.SetDefaultVar();
        direction = 1;
    }

    protected override void SetVarComponentsToDefault()
    {
        base.SetVarComponentsToDefault();
        originScale = Player.PlayerModel.transform.localScale;
    }

    private void Update()
    {
        Flipping();
    }

    [Button]
    private void ToLeft()
    {
        direction = -1;
        Player.PlayerModel.transform.localScale = Vector3.Scale(new Vector3(-1, 1, 1), originScale);
    }

    [Button]
    private void ToRight()
    {
        direction = 1;
        Player.PlayerModel.transform.localScale = Vector3.Scale(new Vector3(1, 1, 1), originScale);
    }

    public void Flip(float input)
    {
        if (input == 0) return;
        if (input < 0)
        {
            ToLeft();
            return;
        }

        ToRight();
    }

    public void Flipping()
    {
        var input = Input.GetAxis("Horizontal");
        Flip(input);
    }
}