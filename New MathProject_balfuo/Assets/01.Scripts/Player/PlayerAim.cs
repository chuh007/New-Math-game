using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour, IPlayerComponent
{
    public Transform _weaponHolder;
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
        Vector2 lookDirection = (worldPos - _weaponHolder.position).normalized;

        _weaponHolder.right = lookDirection.normalized;
    }

    public Vector3 DirectionFromAngle(float degrre, bool isGlobalAngle)
    {
        if(!isGlobalAngle)
        {
            degrre += _weaponHolder.transform.eulerAngles.z;
            // 글로벌 엥글 아니면 자신의 회전치 추가해 글로벌 앵글로
        }
        float rad = degrre * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
    }
}
