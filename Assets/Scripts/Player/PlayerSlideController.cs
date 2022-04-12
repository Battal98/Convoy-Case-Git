using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class PlayerSlideController : MonoBehaviour
{
    private bool _isTouching = false;

    #region Values: Movement 

    [Header("Values: Movement")]

    [SerializeField]
    private Vector2 MinMaxPlayerPos;

    [SerializeField]
    private Vector2 MinMaxPlayerSensivity;

    [SerializeField]
    private float CalculatedXSens = 0.75f;

    [SerializeField]
    private int _waitTimeForSwipe = 500;

    #endregion

    private GameObject OffsetObj;

    private Vector3 _playerOldPos = Vector3.zero;

    private int _swipeCounter;

    private void Start()
    {
        Init();
    }
    void Update()
    {
        if (!GameManager.isGameStarted || GameManager.isGameEnded)
        {
            return;
        }

        Swipe();
    }

    private void Init()
    {
        _swipeCounter = 0;
        _isTouching = false;
        OffsetObj = new GameObject();
        OffsetObj.name = "OffSetObje";
        OffsetObj.transform.parent = this.transform.parent;
    }
    private void Swipe()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            OffsetObj.transform.localPosition = new Vector3(CalculateX() * 3, this.transform.position.y, 0);
            _playerOldPos = OffsetObj.transform.localPosition;

        }

        if (Input.GetMouseButton(0))
        {
            if (!_isTouching)
            {
                OffsetObj.transform.localPosition = new Vector3(CalculateX() * 3, this.transform.position.y, 0);
                DetectPlayerDirection();
            }

        }
        if (!Input.GetMouseButton(0))
        {

            OffsetObj.transform.localPosition = new Vector3(CalculateX() * 3, this.transform.position.y, 0);
        }
    }

    /// <summary>
    /// Determines the player's direction of movement
    /// </summary>
    private void DetectPlayerDirection()
    {
        if (OffsetObj.transform.localPosition.x > _playerOldPos.x && _swipeCounter < 1)
        {
            //Right
            _swipeCounter++;
            _isTouching = true;

            this.transform.DOMoveX(this.transform.position.x + MinMaxPlayerPos.y, 0.1f).OnComplete(async () =>
            {
                await Task.Delay(_waitTimeForSwipe);
                _isTouching = false;
            });
        }
        else if (OffsetObj.transform.localPosition.x < _playerOldPos.x && _swipeCounter > -1)
        {
            //Left
            _isTouching = true;
            _swipeCounter--;

            this.transform.DOMoveX(this.transform.position.x + MinMaxPlayerPos.x, 0.1f).OnComplete(async () =>
            {
                await Task.Delay(_waitTimeForSwipe);
                _isTouching = false;
            });
        }
        _playerOldPos = OffsetObj.transform.localPosition;
    }

    /// <summary>
    /// Calculate X from Screen ratio
    /// </summary>
    /// <returns></returns>
    private float CalculateX()
    {
        Vector3 location = Input.mousePosition;
        return (location.x / (Screen.width / (MinMaxPlayerSensivity.y + Mathf.Abs(MinMaxPlayerSensivity.x)))) - CalculatedXSens;

    }

}
