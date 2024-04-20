using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [SerializeField]
    float x_camAngle = 60;

    // Update is called once per frame
    void Update()
    {
        float mouse_Y = Input.GetAxis("Mouse X");
        float mouse_X = Input.GetAxis("Mouse Y");

        Vector3 mouseLook = new Vector3(-mouse_X * 0.3f, mouse_Y * 0.5f);

        if (mouseLook.x < -x_camAngle)
        {
            mouseLook.x = -x_camAngle;
        }
        if (mouseLook.x > x_camAngle)
        {
            mouseLook.x = x_camAngle;
        }

        transform.eulerAngles = transform.eulerAngles + mouseLook;
    }
}
