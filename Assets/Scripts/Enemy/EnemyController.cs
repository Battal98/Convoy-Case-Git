using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Enemy ScriptableEnemy;

    public float EnemyCurrHealth;

    private void Start()
    {
        Init();
    }

    private void Init()
    {

        EnemyCurrHealth = ScriptableEnemy.ENEMY_MAXHealth;

        GameObject _createdObj = Instantiate(ScriptableEnemy.ENEMY_Type);
        _createdObj.transform.parent = this.gameObject.transform;
        _createdObj.transform.localPosition = Vector3.zero;
        _createdObj.transform.rotation = Quaternion.Euler(0, 180f, 0);

    }
}
