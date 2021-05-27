using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホーミングレーザー
/// </summary>
public class HomingLaser : MonoBehaviour
{
    /// <summary>
    /// 移動時のステート
    /// </summary>
    enum MoveState
    {
        FORWARD,
        LOCK_LAST_PLAYER,
        LOCK_PLAYER
    }

    [SerializeField] float _mvSpeed = 1.0f;             // 移動速度
    [SerializeField] float _stateChangeTime = 2.0f;     // ステートが切り替わるまでの時間
    [SerializeField] float _power = 0.1f;               // 方向に回転する力

    GameObject _player = null;                  // プレイヤーの参照
    MoveState _mvState = MoveState.FORWARD;     // 移動ステート
    Vector3 _lastPlayer;                        // 最後にロックオンしたプレイヤー位置
    float _mvTimer;                             // 移動タイマー
    
    void Start()
    {
        //プレイヤーの参照を拾ってくる(保持しなくてもよかったかも)
        _player = GameManager.Player;
    }

    void Update()
    {
        //移動タイマー加算
        _mvTimer += Time.deltaTime;

        //ステートに応じた移動処理をする
        switch (_mvState)
        {

        //向いている方向に直進する
        case MoveState.FORWARD:
            this.transform.position += this.transform.forward * _mvSpeed;

            if(_mvTimer > _stateChangeTime)
            {
                _mvState = MoveState.LOCK_PLAYER;
                _mvTimer = 0;
            }
            break;

        //最後に拾ってきたプレイヤー位置に向かって進む
        case MoveState.LOCK_LAST_PLAYER:
            //向きを変える
            this.transform.LookAt(this.transform.position + this.transform.forward + (_lastPlayer - this.transform.position).normalized * _power);
            
            //プレイヤーの位置についたら残り時間は直進
            if ((_player.transform.position - this.transform.position).magnitude < _mvSpeed * _mvSpeed)
            {
                _mvState = MoveState.FORWARD;
                break;
            }

            //移動
            this.transform.position += this.transform.forward * _mvSpeed;
            
            //一定時間したらロックオンする
            if (_mvTimer > _stateChangeTime)
            {
                _mvState = MoveState.LOCK_PLAYER;
                _mvTimer = 0;
            }
            break;
          
        //プレイヤー位置に向かって進む
        case MoveState.LOCK_PLAYER:
            //最後の位置を常に更新する
            _lastPlayer = _player.transform.position;

            //向きを変える
            this.transform.LookAt(this.transform.position + this.transform.forward + (_lastPlayer - this.transform.position).normalized * _power);

            //移動
            this.transform.position += this.transform.forward * _mvSpeed;

            //一定時間したらロックオンはずす
            if (_mvTimer > _stateChangeTime)
            {
                _mvState = MoveState.LOCK_LAST_PLAYER;
                _mvTimer = 0;
            }
            break;
        }

        //プレイヤー位置まで到達したら当たったことにする
        if((_player.transform.position - this.transform.position).magnitude < _mvSpeed)
        {
            Destroy(this.gameObject);
        }
    }
}
