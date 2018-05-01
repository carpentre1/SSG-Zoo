using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public GameObject Manager;
    Manager managerScript;

    public GameObject penguin;
    public GameObject snake;
    public GameObject zebra;
    public GameObject gorilla;
    public GameObject elephant;

    public Text monthText;
    public Text earningsText;
    public Text totalProfitText;
    public Text happinessText;
    public Text generalInfoText;

    public Button choice1Button;
    public Button choice2Button;
    public Button choice3Button;

    public Text choice1Text;
    public Text choice2Text;
    public Text choice3Text;

    string choice1;
    string choice2;
    string choice3;

    static string extraFood = "$800: Extra food for all the animals! (instant +5 happiness)";
    static string betterAirConditioning = "$1,200: More ideal temperatures for each animal pen (+1 happiness per month)";
    static string betterAnimalTraining = "$1,500: Hire better trainers for the animals (+1 happiness per month)";
    static string fancierGiftShop = "$700: Fancier gift shop, attracting more people to buy things from it (+$250 profit)";

    public List<string> choices = new List<string>();

    // Use this for initialization
    void Start () {
        choices.Add(extraFood); choices.Add(betterAirConditioning); choices.Add(betterAnimalTraining);
        choices.Add(fancierGiftShop);

        UpdateChoiceButtons();
	}
	
	// Update is called once per frame
	void Update () {
        managerScript = Manager.GetComponent<Manager>();
	}

    public void UpdateUI()
    {
        monthText.text = "Month: " + managerScript.currentMonth;
        happinessText.text = "Animal happiness: " + managerScript.happiness + "\nHappiness gained per month: " + managerScript.happinessPerMonth;
        earningsText.text = "Expected earnings this month:\nState funding: $" + managerScript.fundingProfit
            + "\nAnimal profits: $" + managerScript.animalProfit
            + "\nGift shop profits: $" + managerScript.shopProfit;
        totalProfitText.text = "Total profits: $" + managerScript.totalProfit;
    }

    public void ChoiceButton(int choiceNumber)
    {
        if(choiceNumber == 1)
        {
            MakeChoice(choice1);
        }
        if (choiceNumber == 2)
        {
            MakeChoice(choice2);
        }
        if (choiceNumber == 3)
        {
            MakeChoice(choice3);
        }
    }

    public void MakeChoice(string choice)
    {
        float cost;
        if(choice == extraFood)
        {
            cost = 800;
            if (CanAfford(cost))
            {
                //do visual stuff
                managerScript.happiness += 5;
                managerScript.money -= cost;
                managerScript.CalculateProfit();
                //hide choice buttons
                ToggleChoiceButtons(false);
            }
        }
        if (choice == fancierGiftShop)
        {
            cost = 700;
            if (CanAfford(cost))
            {
                //do visual stuff
                managerScript.shopProfit += 250;
                managerScript.money -= cost;
                managerScript.CalculateProfit();
                //hide choice buttons
                ToggleChoiceButtons(false);
            }
        }
        if (choice == betterAirConditioning)
        {
            cost = 800;
            if (CanAfford(cost))
            {
                //do visual stuff
                managerScript.happiness += 5;
                managerScript.money -= cost;
                managerScript.CalculateProfit();
                //hide choice buttons
                ToggleChoiceButtons(false);
            }
        }
        if (choice == betterAnimalTraining)
        {
            cost = 800;
            if (CanAfford(cost))
            {
                //do visual stuff
                managerScript.happiness += 5;
                managerScript.money -= cost;
                managerScript.CalculateProfit();
                //hide choice buttons
                ToggleChoiceButtons(false);
            }
        }
    }

    public void ToggleChoiceButtons(bool areVisible)
    {
        if(areVisible == true)
        {
            choice1Button.gameObject.SetActive(true);
            choice2Button.gameObject.SetActive(true);
            choice3Button.gameObject.SetActive(true);
        }
        if(areVisible == false)
        {
            choice1Button.gameObject.SetActive(false);
            choice2Button.gameObject.SetActive(false);
            choice3Button.gameObject.SetActive(false);
        }
    }

    bool CanAfford(float cost)
    {
        if (managerScript.money < cost) return false;
        else return true;
    }

    public void UpdateChoiceButtons()
    {
        choice1 = choices[Random.Range(0, choices.Count)];
        choice1Text.text = choice1;
        choices.Remove(choice1);

        choice2 = choices[Random.Range(0, choices.Count)];
        choice2Text.text = choice2;
        choices.Remove(choice2);

        choice3 = choices[Random.Range(0, choices.Count)];
        choice3Text.text = choice3;
        choices.Add(choice1); choices.Add(choice2);
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
            managerScript.CalculateProfit();
        }
    }
}
