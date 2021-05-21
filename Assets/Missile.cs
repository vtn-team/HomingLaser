using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    enum MoveState
    {
        FORWARD,
        LOCK_LAST_PLAYER,
        LOCK_PLAYER
    }

    [SerializeField] float _mvSpeed = 1.0f;
    [SerializeField] float _stateChangeTime = 2.0f;
    [SerializeField] float _power = 0.1f;

    GameObject _player = null;
    MoveState _mvState = MoveState.FORWARD;
    Vector3 _lastPlayer;
    float _mvTimer;


    void Start()
    {
        _player = GameManager.Player;
    }

    void Update()
    {
        _mvTimer += Time.deltaTime;
        switch (_mvState)
        {
        case MoveState.FORWARD:
            this.transform.position += this.transform.forward * _mvSpeed;
            if(_mvTimer > _stateChangeTime)
            {
                _mvState = MoveState.LOCK_PLAYER;
                _mvTimer = 0;
            }
            break;

        case MoveState.LOCK_LAST_PLAYER:
            this.transform.LookAt(this.transform.position + this.transform.forward + (_lastPlayer - this.transform.position).normalized * _power);
            this.transform.position += this.transform.forward * _mvSpeed;
            if (_mvTimer > _stateChangeTime)
            {
                _mvState = MoveState.LOCK_PLAYER;
                _mvTimer = 0;
            }
            break;

        case MoveState.LOCK_PLAYER:
            _lastPlayer = _player.transform.position;
            this.transform.LookAt(this.transform.position + this.transform.forward + (_lastPlayer - this.transform.position).normalized * _power);
            this.transform.position += this.transform.forward * _mvSpeed;
            if (_mvTimer > _stateChangeTime)
            {
                _mvState = MoveState.LOCK_LAST_PLAYER;
                _mvTimer = 0;
            }
            break;
        }

        if((_player.transform.position - this.transform.position).magnitude < _mvSpeed)
        {
            Destroy(this.gameObject);
        }
    }
}
