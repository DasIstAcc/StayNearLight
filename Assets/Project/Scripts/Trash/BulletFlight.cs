using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFlight : MonoBehaviour
{
    float time_remains = 0;
    float timer = 10;

    // Start is called before the first frame update
    void Start()
    {
        time_remains = timer;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * 0.5f;
        time_remains -= Time.deltaTime;
        if (time_remains < 0)
        {
            time_remains = timer;
            this.gameObject.SetActive(false);
        }

    }
}
