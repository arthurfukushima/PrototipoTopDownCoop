﻿using UnityEngine;
using System.Collections;

public class GameplayController : MonoBehaviour 
{
    [Header("Movement Settings")]
    [Range(1, 10)]
    public float movementSpeed ; 

    [Header("Attack Settings")]
    public float projectileSpeed = 5.0f;

    [Header("Roll Settings")]
    [Range(0.01f, 0.6f)]
    public float rollDuration = 0.2f;

    [Range(1.0f, 10.0f)]
    public float rollSpeedMultiplier = 4.0f;

    public bool rollToMouseDirection;
}
