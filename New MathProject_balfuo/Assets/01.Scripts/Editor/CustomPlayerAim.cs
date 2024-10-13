using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerAim))]
public class CustomPlayerAim : Editor
{
    private void OnSceneGUI()
    {
        PlayerAim aim = target as PlayerAim;
        Vector3 center = aim.HoldPositon;

        Handles.color = Color.white;
        Handles.DrawWireArc(center, Vector3.forward, Vector3.right, 360f, aim.viewRaidus);

        Vector3 viewAngleA = aim.DirectionFromAngle(-aim.viewAngle * 0.5f, false);
        Vector3 viewAngleB = aim.DirectionFromAngle(aim.viewAngle * 0.5f, false);

        Handles.DrawLine(center, center + viewAngleA * aim.viewRaidus);
        Handles.DrawLine(center, center + viewAngleB * aim.viewRaidus);

    }
}
