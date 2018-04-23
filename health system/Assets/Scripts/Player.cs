﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    public int _MaxHealth;

    //Exlain why we cannot use range attribute witha variable
    public int _CurHealth;

    [ExecuteInEditMode]
    void OnValidate()
    {
        _CurHealth = Mathf.Clamp(_CurHealth, 0, _MaxHealth);
    }
}