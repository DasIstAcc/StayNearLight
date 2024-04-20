using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPit : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        CharacterUnit unit = other.GetComponent<CharacterUnit>();

        if (unit != null)
        {
            unit.Death();
        }
    }
}
