using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_OnTriggerController : MonoBehaviour
{
    public float _currPresidentHealth;

    private void Start()
    {
        _currPresidentHealth = PlayerManager.instance.PlayerMaxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {

        switch (other.gameObject.tag)
        {
            case "Enemy":
                PlayerManager.instance.PlayerAttackDamage(other.gameObject);
                break;

            case "Finish":
                GameManager.instance.OnLevelCompleted();
                break;
        }
    }
}
