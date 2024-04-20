using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveLoadSystem
{
    private GameState currentState;
    private List<UnitState> currentUnits = new List<UnitState>();


    public void SaveState()
    {
        if (currentState == null)
        {
            currentState = new GameState();
            currentState.Name = "New Game";
            //TO_REMOVE
            currentUnits.Add(new UnitState() { unitPosition = new float[] { 1 , 2, 3}, unitDirection = new float[] { 0, 20, 0 }, unitData = new CharacterData("Dummy") });
            //
            currentState.units = currentUnits.ToArray();
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + currentState.Name + ".dat");
        bf.Serialize(file, currentState);
        file.Close();
    }

    public void LoadState()
    {
        if (File.Exists(Application.persistentDataPath + "/" + currentState.Name + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + currentState.Name + ".dat", FileMode.Open);
            currentState = (GameState)bf.Deserialize(file);
            file.Close();

            Debug.Log("Current state name is " + currentState.Name);
        }
    }

    public void DeleteState(string Name)
    {
        if (File.Exists(Application.persistentDataPath + "/" + Name + ".dat"))
        {
            File.Delete(Application.persistentDataPath + "/" + Name + ".dat");

            Debug.Log("Save" + Name + " was deleted");
        }
    }
}
