using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(menuName ="SO/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions, IPlayerComponent
{

    public Vector2 Movement { get; private set; }
    public Vector3 MousePositon { get; private set; }

    private Controls _controls;
    private Player _Player;

    private void OnEnable()
    {
        if (_controls == null)
        {   
            _controls = new Controls();
        }
        _controls.Player.Enable();
        _controls.Player.SetCallbacks(this);
    }
    public void OnAim(InputAction.CallbackContext context)
    {
        Vector2 screenPos = context.ReadValue<Vector2>();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        worldPos.z = 0;
        MousePositon = worldPos;
        Debug.Log(MousePositon);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    public void Initialize(Player player)
    {
        _Player = player;
    }
}
