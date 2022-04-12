using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    #region Values: Player Jobs

    [Header(" *-_Player Values_-*")]

    public GameObject PlayerObj;
    public float PlayerForwardSpeed = 2;
    public float PlayerMaxHealth = 200f;
    public float PlayerDamage = 100f;

    #endregion

    #region Values: Cops Jobs

    public float MaxCopsHealth = 100;

    #endregion


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayerAttackDamage(GameObject _triggerObj)
    {
        if(PlayerObj == null)
            PlayerObj = GameObject.FindGameObjectWithTag("Player");

        EnemyController _enemyController = _triggerObj.GetComponentInParent<EnemyController>();
        Player_OnTriggerController _pTriggerController = PlayerObj.GetComponentInChildren<Player_OnTriggerController>();


        _enemyController.EnemyCurrHealth -= PlayerDamage;
        _pTriggerController._currPresidentHealth -= PlayerDamage;

        #region Player & Enemy Health Check Jobs

        if (_enemyController.EnemyCurrHealth <= 0)
        {
            //enemy death
            _triggerObj.SetActive(false);
            _enemyController.enabled = false;
        }
        if (_pTriggerController._currPresidentHealth <= 0)
        {
            //fail
            PlayerObj.SetActive(false);
            GameManager.instance.OnLevelFailed();
        }

        #endregion

    }

    public void CopsAttackDamage(GameObject _triggerObj,GameObject _copsObj)
    {
        if (PlayerObj == null)
            PlayerObj = GameObject.FindGameObjectWithTag("Player");

        EnemyController _enemyController = _triggerObj.GetComponentInParent<EnemyController>();
        Cops_OnTriggerController _cTriggerController = PlayerObj.GetComponentInChildren<Cops_OnTriggerController>();
        Collider col = _triggerObj.GetComponent<Collider>();

        _enemyController.EnemyCurrHealth -= PlayerDamage;
        _cTriggerController._currCopsHealth -= PlayerDamage;

        #region Cops & Enemy Health Check Jobs

        if (_enemyController.EnemyCurrHealth <= 0)
        {
            //death
            col.enabled = false;
            _triggerObj.SetActive(false);
            _enemyController.enabled = false;
        }
        if (_cTriggerController._currCopsHealth <= 0)
        {
            col.enabled = false;
            _copsObj.SetActive(false);
        }

        #endregion

    }
}
