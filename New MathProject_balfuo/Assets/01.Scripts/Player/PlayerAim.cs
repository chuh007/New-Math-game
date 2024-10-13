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
            // �۷ι� ���� �ƴϸ� �ڽ��� ȸ��ġ �߰��� �۷ι� �ޱ۷�
        }
        float rad = degrre * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
    }
}
