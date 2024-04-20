using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager _manager;
    public static SaveLoadSystem _loadSystem;
    public static MenuSystem _menuSystem;
    public static DataSystem _dataSystem;
    public static UnitManager _unitManager;
    public static InputSystem _inputSystem;
    //public static InventorySystem _inventorySystem;
    //public static EquipmentSystem _equipmentSystem;

    public static List<int> IDs;

    [SerializeField] private PlayerUnit player;
    //[SerializeField] public GameObject m_inventoryUIObj;
    //[SerializeField] public GameObject m_equipmentUIObj;

    //[SerializeField] private ResourceBarScript healthBar;
    [SerializeField] private GameObject enemyDisplay;



    private void Awake()
    {
        if (_manager == null)
        {
            _manager = gameObject.GetComponent<GameManager>();
            DontDestroyOnLoad(gameObject);
        }
        if (_inputSystem == null)
        {
            _inputSystem = gameObject.GetComponentInChildren<InputSystem>();
        }
        if (_menuSystem == null)
        {
            _menuSystem = new MenuSystem();
        }
        if (_loadSystem == null)
        {
            _loadSystem = new SaveLoadSystem();
        }
        if (_dataSystem == null)
        {
            _dataSystem = new DataSystem();
        }
        if (_unitManager == null)
        {
            _unitManager = new UnitManager();
        }
        

        IDs = new List<int>();
    }

    
    void Start()
    {
        //if (healthBar != null)
        //{
        //    player.e_onHealthChanged += healthBar.OnResourceChange;
        //}

        //enemyDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayEnemy(float amount, CharacterUnit toDisplay)
    {
        StartCoroutine(EnemyDisplayer(toDisplay));
    }

    int time_displaying = 0;

    IEnumerator EnemyDisplayer(CharacterUnit toDisplay)
    {
        enemyDisplay.SetActive(true);

        if (time_displaying < 10)
            time_displaying += 10;
        enemyDisplay.GetComponentInChildren<TextMeshProUGUI>().text = toDisplay.m_data.CharacterName;
        enemyDisplay.GetComponentInChildren<ResourceBarScript>().OnResourceChange(0, toDisplay);

        for (int i = 0;i < 9;i++)
        {
            yield return new WaitForSeconds(1);

            if (time_displaying > 0) time_displaying--;
        }

        if (time_displaying == 0) enemyDisplay.SetActive(false);
    }

    public PlayerUnit GetPlayer()
    {
        return player;
    }

    public int GetNewID()
    {
        IDs.Add(IDs.Count + 1);
        return IDs.Count;
    }

    //public HashSet<Ability> GetAbilities(HashSet<string> abilitiesIDs)
    //{
    //    HashSet<Ability> result = new HashSet<Ability>();

    //    foreach(var item in abilitiesIDs)
    //    {
    //        if (Registry.ABILITIES[item] != null)
    //        {

    //        }
    //    }

    //    return result;
    //}
}
