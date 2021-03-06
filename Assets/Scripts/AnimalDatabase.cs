﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AnimalDatabase : MonoBehaviour {

    private List<Animal> database = new List<Animal>();
    private LitJson.JsonData animalData;

	// Use this for initialization
	void Start () {
        string jsonString = "";
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "Animals.json");
        if (Application.platform == RuntimePlatform.Android)
        {
            WWW reader = new WWW(filePath);
            while (!reader.isDone) { }

            jsonString = reader.text;
            animalData = LitJson.JsonMapper.ToObject(jsonString);
        }
        else
        {
            animalData = LitJson.JsonMapper.ToObject(File.ReadAllText(filePath));
        }
        
        ConstructAnimalDatabase();

        Debug.Log(FetchAnimalByID(2).name);
	}

    public Animal FetchAnimalByID(int id)
    {
        Animal animal = new Animal();
        for(int i = 0; i < database.Count; i++)
        {
            if(id == (int)database[i].id)
            {
                animal = database[i];
                break;
            }
        }
        return animal;
    }

    void ConstructAnimalDatabase() {
        for(int i = 0; i < animalData.Count; i++)
        {
            database.Add(new Animal((int)animalData[i]["id"],animalData[i]["name"].ToString(), animalData[i]["modelpath"].ToString()));
        }
    }

}

public class Animal{
    public int id { get; set; }
    public string name { get; set; }
    public string modelPath { get; set; }
    public Sprite sprite { get; set; }

    public Animal() {
        this.id = -1;
    }

    public Animal(int id, string name, string modelPath)
    {
        this.id = id;
        this.name = name;
        this.modelPath = modelPath;
        this.sprite = Resources.Load<Sprite>("Sprite/Animals/" + name);
    }
}