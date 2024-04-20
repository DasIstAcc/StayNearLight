using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UnitManager
{
    private List<CharacterUnit> activeUnits = new List<CharacterUnit>();
    private List<CharacterUnit> disabledUnits = new List<CharacterUnit>();

    private List<CharacterUnit> allUnits = new List<CharacterUnit>();

    public UnitManager()
    {
        allUnits = GameObject.FindObjectsByType<CharacterUnit>(FindObjectsSortMode.None).ToList();
        foreach (var unit in allUnits)
        {
            if (unit.GetType() != typeof(PlayerUnit))
                unit.e_onHealthChanged += GameManager._manager.DisplayEnemy;
        }

    }

    
    public List<CharacterUnit> GetAllUnits()
    {
        List<CharacterUnit> units = new List<CharacterUnit>();
        units.AddRange(allUnits);
        return units;
    }
}