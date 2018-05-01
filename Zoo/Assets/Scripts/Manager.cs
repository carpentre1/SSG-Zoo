using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public Button nextMonthButton;
    public UI UIScript;
    public GameObject hiderPanel;

    bool touristsVisiting = false;
    
    float baseAnimalMoveFrequency = .995f;
    float animalMoveFrequencyNightMultiplier = .8f;
    float animalMoveFrequencyDayMultiplier = 1f;
    float currentAnimalMoveFrequencyMultiplier;

    float currentAnimalMoveVelocityNightMultiplier;
    float animalMoveVelocityNightMultiplier = 3f;
    float animalMoveVelocityDayMultiplier = 3f;

    public int currentMonth = 1;
    public float money = 0;
    public float happiness = 50;
    public float happinessPerMonth = 0;

    public List<GameObject> allAnimals = new List<GameObject>();

    public float animalProfit = 0;
    public float shopProfit = 0;
    public float fundingProfit = 2000;
    public float totalProfit = 0;
	// Use this for initialization
	void Start () {
        money += 200000;//starting funds
        currentAnimalMoveFrequencyMultiplier = animalMoveFrequencyDayMultiplier;
        currentAnimalMoveVelocityNightMultiplier = animalMoveVelocityDayMultiplier;
        UIScript.UpdateUI();

    }
	
	// Update is called once per frame
	void Update () {
        AnimalBehavior();
	}

    public void CalculateProfit()
    {
        totalProfit = (animalProfit * (happiness / 100)) + shopProfit + fundingProfit;
        UIScript.UpdateUI();
    }

    public void NextMonth()//when they click the "next month" button,
    {
        //make sure they're eligible to start the next month
        if(touristsVisiting || currentMonth >= 12)
        {
            return;
        }

        money += totalProfit;//calculate events
        happiness += happinessPerMonth;
        currentMonth += 1;
        CalculateProfit();
        StartCoroutine(TouristDelay());
        if(currentMonth >= 12)
        {
            Debug.Log("Final month!");
            //hide next month button, show assessment button.
        }

        //show visitors walking around
        //show animals doing cute stuff
        //end all the action
        //give them new decisions to make
    }
    IEnumerator TouristDelay()
    {
        //spawn tourists and make them run the travelToPens function
        nextMonthButton.gameObject.SetActive(false);
        hiderPanel.SetActive(true);
        touristsVisiting = true;
        currentAnimalMoveFrequencyMultiplier = animalMoveFrequencyNightMultiplier;
        yield return new WaitForSeconds(5);
        nextMonthButton.gameObject.SetActive(true);
        hiderPanel.SetActive(false);
        touristsVisiting = false;
        UIScript.UpdateChoiceButtons();
        UIScript.ToggleChoiceButtons(true);
        currentAnimalMoveFrequencyMultiplier = animalMoveFrequencyDayMultiplier;

    }

    void AnimalBehavior()
    {
        if(allAnimals.Count == 0) { return; }
        foreach (GameObject animal in allAnimals)
        {
            Rigidbody2D rb = animal.GetComponent<Rigidbody2D>();
            if(Random.value > baseAnimalMoveFrequency * currentAnimalMoveFrequencyMultiplier)
            {
                rb.velocity = new Vector2(0, 0);
                Vector2 moveDirection = new Vector2(Random.Range(-1,2), Random.Range(-1,2));
                rb.AddForce(moveDirection * (Random.Range(1,50) * currentAnimalMoveVelocityNightMultiplier));
            }
        }
    }
}
