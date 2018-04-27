using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {

    public GameObject Manager;
    Manager managerScript;

    public GameObject penguin;
    public GameObject snake;
    public GameObject zebra;
    public GameObject gorilla;
    public GameObject elephant;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        managerScript = Manager.GetComponent<Manager>();
	}

    public void BuyAnimal(string animalType)
    {
        if(animalType == "Penguin")
        {
            AttemptPurchase(penguin, 1000);
        }
        if (animalType == "Snake")
        {
            AttemptPurchase(snake, 1000);
        }
        if (animalType == "Zebra")
        {
            AttemptPurchase(zebra, 1000);
        }
        if (animalType == "Elephant")
        {
            AttemptPurchase(elephant, 1000);
        }
        if (animalType == "Gorilla")
        {
            AttemptPurchase(gorilla, 1000);
        }
    }

    void AttemptPurchase(GameObject animal, float cost)
    {
        if(animal.name.Contains("Owned")) { return; }
        if(managerScript.money < cost) { return; }
        else
        {
            GameObject animal1 = Instantiate(animal, new Vector3(animal.transform.position.x + Random.value, animal.transform.position.y + Random.value, animal.transform.position.z), Quaternion.identity);
            GameObject animal2 = Instantiate(animal, new Vector3(animal.transform.position.x + Random.value, animal.transform.position.y + Random.value, animal.transform.position.z), Quaternion.identity);
            GameObject animal3 = Instantiate(animal, new Vector3(animal.transform.position.x + Random.value, animal.transform.position.y + Random.value, animal.transform.position.z), Quaternion.identity);
            GameObject animal4 = Instantiate(animal, new Vector3(animal.transform.position.x + Random.value, animal.transform.position.y + Random.value, animal.transform.position.z), Quaternion.identity);
            animal.SetActive(true); animal2.SetActive(true); animal3.SetActive(true); animal4.SetActive(true);
            managerScript.allAnimals.Add(animal1); managerScript.allAnimals.Add(animal2); managerScript.allAnimals.Add(animal3); managerScript.allAnimals.Add(animal4);
            animal.name += "Owned";
            managerScript.money -= cost;
            Debug.Log(penguin.transform.position);
        }
    }
}
