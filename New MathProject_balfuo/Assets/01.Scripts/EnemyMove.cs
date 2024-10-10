using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Rigidbody2D _rbCompo;
    [SerializeField] private Transform testPlayerPos;
    private void Awake()
    {
        _rbCompo = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 targetPosition = testPlayerPos.position;
        Vector2 newPosition = Vector2.MoveTowards(_rbCompo.position, targetPosition, 10f * Time.fixedDeltaTime);
        _rbCompo.MovePosition(newPosition);
    }
}
