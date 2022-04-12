using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public float ENEMY_MAXHealth = 100;
    public GameObject ENEMY_Type;
}
