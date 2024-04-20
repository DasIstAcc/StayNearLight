using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBarScript : MonoBehaviour
{
    [SerializeField]
    private Image displayBarImage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnResourceChange(float amount, CharacterUnit unit)
    {
        displayBarImage.rectTransform.localScale = new Vector3((float)unit.m_data.current_health / (float)unit.GetAttributeValue(Attributes.MaxHealth), 1, 1);

        displayBarImage.rectTransform.localPosition = new Vector3(-displayBarImage.rectTransform.sizeDelta.x * (1 - (float)unit.m_data.current_health / (float)unit.GetAttributeValue(Attributes.MaxHealth)) / 2, 0, 0);
    }
}
