using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
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

    Mesh CreateFanMesh(float radius, int segmentCount, float angle)
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[segmentCount + 2];
        vertices[0] = Vector3.zero;

        float radianAngle = Mathf.Deg2Rad * angle;
        float angleStep = radianAngle / segmentCount;

        for (int i = 0; i <= segmentCount; i++)
        {
            float currentAngle = i * angleStep;
            float x = Mathf.Cos(currentAngle) * radius;
            float y = Mathf.Sin(currentAngle) * radius;
            vertices[i + 1] = new Vector3(x, y, 0);  
        }
        int[] triangles = new int[segmentCount * 3];  

        for (int i = 0; i < segmentCount; i++)
        {
            triangles[i * 3] = 0; 
            triangles[i * 3 + 1] = i + 1; 
            triangles[i * 3 + 2] = i + 2;  
        }

        Vector2[] uv = new Vector2[vertices.Length];
        uv[0] = new Vector2(0.5f, 0.5f); 

        for (int i = 1; i < uv.Length; i++)
        {
            float u = (vertices[i].x / radius) * 0.5f + 0.5f;
            float v = (vertices[i].y / radius) * 0.5f + 0.5f;
            uv[i] = new Vector2(u, v);
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();

        return mesh;
    }
    private void OnDrawGizmos()
    {
        Mesh mesh = CreateFanMesh(viewRaidus, 20, viewAngle);
        Gizmos.color = Color.white;
    }
}
