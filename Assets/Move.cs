using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float camSpeed = 10.0f;
    [SerializeField] float mvSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mx = Input.GetAxis("MoveX");
        float my = Input.GetAxis("MoveY");

        float cx = Input.GetAxis("CameraX");
        float cy = Input.GetAxis("CameraY");

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
