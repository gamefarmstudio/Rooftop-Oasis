using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public static CSVReader instance;

    public class PlantData
    {
        public int id;
        public string type;
        public float PMabsorption;
        public float timeToNextStage;
        public int cost;
        public float timeToDie;
    }
    List<PlantData> plantDataList = new List<PlantData>();


    void ReadCSV(string filename)
    {
        string filepath = Path.Combine(Application.streamingAssetsPath, filename);
        if (File.Exists(filepath))
        {
            string[] lines = File.ReadAllLines(filepath);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');
                PlantData plantData = new PlantData
                {
                    id = int.Parse(values[0]),
                    type = values[1],
                    PMabsorption = float.Parse(values[2]),
                    timeToNextStage = float.Parse(values[3]),
                    cost = int.Parse(values[4]),
                    timeToDie = float.Parse(values[5])  
                };
                plantDataList.Add(plantData);
            }
        }
        else
        {
            Debug.LogError("File not found: " + filepath);
        }

    }

    [ContextMenu("Load Plant Data")]
    void LoadPlantData()
    {
        ReadCSV("Game_data - plants.csv");
        foreach (var plant in plantDataList)
        {
            Debug.Log($"ID: {plant.id}, Type: {plant.type}, PM Absorption: {plant.PMabsorption}, Time to Next Stage: {plant.timeToNextStage}, Cost: {plant.cost}, Time to Die: {plant.timeToDie}");
        }
    }
    void GetPlantData(int ID)
    {
        ReadCSV("Game_data - plants.csv");
        foreach (var plant in plantDataList)
        {
            if (plant.id == ID)
            {
                Debug.Log($"ID: {plant.id}, Type: {plant.type}, PM Absorption: {plant.PMabsorption}, Time to Next Stage: {plant.timeToNextStage}, Cost: {plant.cost}, Time to Die: {plant.timeToDie}");
                break;
            }
        }
    }

}
