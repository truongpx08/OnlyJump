using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbstract : TruongMonoBehaviour
{
    [SerializeField] protected Player player;
    public Player Player => player;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayer();
    }

    private void LoadPlayer()
    {
        player = GetParentComponent<Player>();
    }
}