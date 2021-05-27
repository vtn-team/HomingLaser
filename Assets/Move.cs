using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移動処理
/// NOTE: わりとてきとう
/// </summary>
public class Move : MonoBehaviour
{
    [SerializeField] float camSpeed = 10.0f;    // 回転速度
    [SerializeField] float mvSpeed = 0.1f;      // 移動速度

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //入力拾ってくる
        float mx = Input.GetAxis("MoveX");
        float my = Input.GetAxis("MoveY");

        float cx = Input.GetAxis("CameraX");
        float cy = Input.GetAxis("CameraY");

        //入力反映
        if (Mathf.Abs(cy) > 0.01f)
        {
            this.transform.Rotate(cy * camSpeed, 0, 0);
        }
        if (Mathf.Abs(cx) > 0.01f)
        {
            this.transform.Rotate(0, cx * camSpeed, 0);
        }
        if (Mathf.Abs(my) > 0.01f)
        {
            this.transform.Translate(0,0,my * mvSpeed);
        }
        if (Mathf.Abs(mx) > 0.01f)
        {
            this.transform.Translate(mx * mvSpeed, 0,0);
        }
    }
}
