using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Rigidbody2D _rbCompo;
    [SerializeField] private Transform testPlayerPos;
    private bool isCanMove = true;
    private void Awake()
    {
        _rbCompo = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (!isCanMove) return;
        Vector2 targetPosition = testPlayerPos.position;
        Vector2 newPosition = Vector2.MoveTowards(_rbCompo.position, targetPosition, 7.5f * Time.fixedDeltaTime);
        _rbCompo.MovePosition(newPosition);
    }
    public void StopMove()
    {
        isCanMove = false;
    }
    public void StartMove()
    {
        isCanMove = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
    
}
