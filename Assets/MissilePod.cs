using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePod : MonoBehaviour
{
    [SerializeField] GameObject _bullet;
    [SerializeField] float _fireTime = 0.5f;

    Quaternion _rot = Quaternion.EulerAngles(0, 0, 0);
    float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > _fireTime)
        {
            timer -= _fireTime;
            Fire();
        }
    }

    void Fire()
    {
        GameObject blt = GameObject.Instantiate(_bullet);
        _rot *= Quaternion.EulerAngles(Random.Range(0,360), 0, Random.Range(0, 360));
        blt.transform.rotation = _rot;
        blt.transform.position = this.transform.position + _rot * new Vector3(0, 2, 0);
    }
}
