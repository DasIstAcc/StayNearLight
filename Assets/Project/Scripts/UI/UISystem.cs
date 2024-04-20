using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;


namespace UI
{
    public class UISystem : MonoBehaviour
    {
        public static UISystem Instance { get; private set; }

        [Header("Player")]
        public PlayerUnit PlayerCharacter;
        public Slider PlayerHealthSlider;
        //public TextMeshProUGUI MaxHealth;
        //public TextMeshProUGUI CurrentHealth;
        //public Text StatsText;

        //[Header("Enemy")]
        //public Slider EnemyHealthSlider;
        //public Text EnemyName;
        
        [Header("Inventory")]
        public InventoryUI InventoryWindow;
        //public Button OpenInventoryButton;


        void Awake()
        {
            Instance = this;

            InventoryWindow.Init();
        }

        void Start()
        {
            //m_ClosedInventorySprite = ((Image)OpenInventoryButton.targetGraphic).sprite;
            //m_OpenInventorySprite = OpenInventoryButton.spriteState.pressedSprite;
        }

        // Update is called once per frame
        void Update()
        {
            UpdatePlayerUI();
        }

        void UpdatePlayerUI()
        {
            //CharacterData data = PlayerCharacter.m_data;

            PlayerHealthSlider.value = (float)PlayerCharacter.m_data.current_health / (float)PlayerCharacter.m_data.attributes.GetAttribute(Attributes.MaxHealth).GetValue();
            //MaxHealth.text = PlayerCharacter.m_data.attributes.GetAttribute(Attributes.MaxHealth).GetValue().ToString();
            //CurrentHealth.text = PlayerCharacter.m_data.current_health.ToString();

            //if (PlayerCharacter.CurrentTarget != null)
            //{
            //    UpdateEnemyUI(PlayerCharacter.CurrentTarget);
            //}
            //else
            //{
            //    EnemyHealthSlider.gameObject.SetActive(false);
            //}

            //StatsText.text = $"Str : {data.attributes.GetAttribute(Attributes.Strength).GetValue()}  Agi : {data.attributes.GetAttribute(Attributes.Agility).GetValue()} Int : {data.attributes.GetAttribute(Attributes.Intelligence).GetValue()} Armor : {data.attributes.GetAttribute(Attributes.Armor).GetValue()}";
        }

        //void UpdateEnemyUI(CharacterData enemy)
        //{
        //    EnemyHealthSlider.gameObject.SetActive(true);
        //    EnemyHealthSlider.value = (float)enemy.current_health / (float)enemy.attributes.GetAttribute(Attributes.MaxHealth).GetValue();
        //    EnemyName.text = enemy.CharacterName;
        //}

        public void ToggleInventory()
        {
            if (InventoryWindow.gameObject.activeSelf)
            {
                //((Image)OpenInventoryButton.targetGraphic).sprite = m_ClosedInventorySprite;
                InventoryWindow.gameObject.SetActive(false);
            }
            else
            {
                //((Image)OpenInventoryButton.targetGraphic).sprite = m_OpenInventorySprite;
                InventoryWindow.gameObject.SetActive(true);
                InventoryWindow.Load(PlayerCharacter.m_data);
            }
        }
    }
}
