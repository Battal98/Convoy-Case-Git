using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForwardController : MonoBehaviour
{

    [SerializeField]
    private Vector2 _enemyMinMaxForwardSpeed;

    private float _enemyForwardSpeed;

    private void Start()
    {
        _enemyForwardSpeed = Random.Range(_enemyMinMaxForwardSpeed.x, _enemyMinMaxForwardSpeed.y);
    }

    void Update()
    {
        if (!GameManager.isGameStarted || GameManager.isGameEnded)
        {
            return;
        }

        this.transform.Translate(Vector3.back * Time.deltaTime * _enemyForwardSpeed);
    }
}
