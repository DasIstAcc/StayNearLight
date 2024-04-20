using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] Item item;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterUnit player = other.GetComponent<CharacterUnit>();
        if (player != null && other.tag == "Player")
        {
            if (player.m_data.Inventory.GetEmptySlotsCount() != 0)
            {
                player.m_data.Inventory.AddItem(item);
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Not enough inventory space");
            }
        }
    }
}
