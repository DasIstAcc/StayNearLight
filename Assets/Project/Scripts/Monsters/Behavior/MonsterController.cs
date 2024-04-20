using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    //[SerializeField]
    //float time_remains = 0;
    //[SerializeField]
    //float time_max = 3;

    public void PeriodicAction()
    {
        //if (time_remains > 0)
        //{
        //    time_remains -= Time.deltaTime;
        //}
        //else
        //{
        //    time_remains = time_max;
        //    if (gameObject.GetComponent<ClawAttack>() != null)
        //    {
        //        gameObject.GetComponent<ClawAttack>().OnUse(gameObject.GetComponent<CharacterObject>().m_data, gameObject.GetComponent<ClawAttack>(), gameObject.transform);
        //    } 
        //}
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PeriodicAction();
    }
}

