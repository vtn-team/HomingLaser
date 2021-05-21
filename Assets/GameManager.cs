using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _player;
    static GameManager _instance = null;

    void Awake()
    {
        _instance = this;
    }

    static public GameObject Player => _instance._player;
}
