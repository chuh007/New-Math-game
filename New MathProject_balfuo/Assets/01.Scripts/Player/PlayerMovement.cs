using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour, IPlayerComponent
{
    [Header("Setting")]

    [SerializeField] private float _moveSpeed = 5f;


    public Rigidbody2D _rbCompo { get; private set; }

    private Player _player;
    private InputReader _inputReader;
    
    
    public void Initialize(Player player)
    {
        _player = player;
        _inputReader =_player.GetCompo<InputReader>();
        _rbCompo = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        Vector2 movement = _inputReader.Movement;
        _rbCompo.velocity = movement*_moveSpeed;
    }
}
