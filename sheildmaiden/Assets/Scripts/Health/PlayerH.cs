﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerH : MonoBehaviour
{
    public int _MaxHealth;

    public int _CurHealth;

    [ExecuteInEditMode]
    void OnValidate()
    {
        _CurHealth = Mathf.Clamp(_CurHealth, 0, _MaxHealth);
    }
}