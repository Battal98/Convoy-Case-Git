using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cops_OnTriggerController : MonoBehaviour
{
    public float _currCopsHealth;
    private void Start()
    {
        _currCopsHealth = PlayerManager.instance.MaxCopsHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            PlayerManager.instance.CopsAttackDamage(other.gameObject, this.gameObject);
        }
    }
}
