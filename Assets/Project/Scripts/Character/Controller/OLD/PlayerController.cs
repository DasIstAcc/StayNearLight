using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public GameObject bulletPrefab;
    //public GameObject[] pull;

    Camera cam;
    Rigidbody rb;
    [SerializeField]
    GameObject groundCheck;
    void Start()
    {
        cam = this.GetComponentInChildren<Camera>();
        rb = this.GetComponent<Rigidbody>();

        //pull = new GameObject[10];
        //for (int i = 0; i < 10; i++)
        //{
        //    pull[i] = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), new Quaternion(0, 0, 0, 0));
        //    pull[i].SetActive(false);
        //}
    }

    [SerializeField]
    float speed = 10;
    [SerializeField]
    float jumpSpeed = 10;
    // Update is called once per frame
    void Update()
    {
        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");


        Vector3 move = new Vector3(X, 0, Z) * speed;

        transform.position = transform.position + cam.transform.forward * Z * speed * 0.01f + cam.transform.right * X * speed * 0.01f;

        transform.eulerAngles = new Vector3(0, cam.transform.localEulerAngles.y, 0);
        

        if (Input.GetKeyDown(KeyCode.Space) && groundCheck.GetComponent<GroundCheckScript>().grounded)
        {
            rb.AddForce(new Vector3(0, 100 * jumpSpeed, 0));
        }

        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    foreach(var o in pull)
        //    {
        //        if (!o.activeSelf)
        //        {
        //            o.SetActive(true);
        //            o.transform.position = gameObject.transform.position;
        //            o.transform.eulerAngles = cam.transform.eulerAngles;
        //            break;
        //        }
        //    }
        //}
    }
}
