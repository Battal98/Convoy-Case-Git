using UnityEngine;

public class PlayerForward : MonoBehaviour
{
    public GameObject PlayerPrefab;

    private PlayerManager _playerManager;
    private void Awake()
    {
        _playerManager = PlayerManager.instance;

    }
    void Update()
    {
        if (!GameManager.isGameStarted || GameManager.isGameEnded)
        {
            return;
        }

        this.transform.Translate(Vector3.forward * Time.deltaTime * _playerManager.PlayerForwardSpeed);
    }
}
