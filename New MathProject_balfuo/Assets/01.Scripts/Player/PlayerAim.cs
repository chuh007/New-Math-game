using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private Transform _weaponHolder;
    private Player _player;
    private InputReader _inputReader;

    [Header("Sight info")]
    [Range(0, 360f)] public float viewAngle;
    [Range(1,12f)] public float viewRaidus;
    public Vector3 HoldPositon => _weaponHolder.position;
    public void Initialize(Player player)
    {
        _player = player;
        _inputReader = _player.GetCompo<InputReader>();
    }
    private void Update()
    {
        UpdateAim();
    }

    private void UpdateAim()
    {
        Vector3 worldPos = _inputReader.MousePositon;
        Vector3 lookDirection = (worldPos - _weaponHolder.position).normalized;

        _weaponHolder.right = lookDirection;
    }

    public Vector3 DirectionFromAngle(float degrre, bool isGlobalAngle)
    {
        if(isGlobalAngle)
        {
            degrre += transform.eulerAngles.z;
        }
        float rad = degrre * Mathf.Rad2Deg;
        return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
    }
}
