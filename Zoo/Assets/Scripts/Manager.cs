using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
    //playtest results:
    //100 happiness route: 12k funds left, 6.6k profit total
    //profit route (not optimal): 23k funds left, 6.3k profit, 50 happiness
    public Button nextMonthButton;
    public Text nextMonthButtonText;

    public Canvas mainCanvas;
    public UI UIScript;
    public GameObject hiderPanel;

    public GameObject endingPanel;
    public Text statsText;
    public Text analysisText;

    bool touristsVisiting = false;
    
    float baseAnimalMoveFrequency = .995f;
    float animalMoveFrequencyNightMultiplier = .8f;
    float animalMoveFrequencyDayMultiplier = 1f;
    float currentAnimalMoveFrequencyMultiplier;

    float currentAnimalMoveVelocityNightMultiplier;
    float animalMoveVelocityNightMultiplier = 3f;
    float animalMoveVelocityDayMultiplier = 3f;

    public enum animalProfits { gorilla=800, zebra=850, penguin=400, elephant=900, snake=440}
    public int gorillasOwned = 0;
    public int zebrasOwned = 0;
    public int penguinsOwned = 0;
    public int elephantsOwned = 0;
    public int snakesOwned = 0;

    public int currentMonth = 1;
    public float money = 0;
    public float happiness = 50;
    public float happinessPerMonth = -5;

    public List<GameObject> allAnimals = new List<GameObject>();

    public float animalProfit = 0;
    public float shopProfit = 0;
    public float fundingProfit = 2000;
    public float totalProfit = 0;
	// Use this for initialization
	void Start () {


        money += 2000;//starting funds
        currentAnimalMoveFrequencyMultiplier = animalMoveFrequencyDayMultiplier;
        currentAnimalMoveVelocityNightMultiplier = animalMoveVelocityDayMultiplier;

    }
	
	// Update is called once per frame
	void Update () {
        AnimalBehavior();
	}

    public void CalculateProfit()
    {
        float gorillaProfit = ((int)animalProfits.gorilla * happiness / 100 * gorillasOwned);
        float snakeProfit = ((int)animalProfits.snake * happiness / 100 * snakesOwned);
        float elephantProfit = ((int)animalProfits.elephant * happiness / 100 * elephantsOwned);
        float penguinProfit = ((int)animalProfits.penguin * happiness / 100 * penguinsOwned);
        float zebraProfit = ((int)animalProfits.zebra * happiness / 100 * zebrasOwned);
        Debug.Log("snake" + snakeProfit + "gorilla" + gorillaProfit + "elephant" + elephantProfit + "penguin" + penguinProfit + "zebra" + zebraProfit);
        animalProfit = gorillaProfit + snakeProfit + elephantProfit + penguinProfit + zebraProfit;
        if(happiness < 30) { fundingProfit = 0; }
        if(happiness > 30) { fundingProfit = 2000; }
        totalProfit = animalProfit + shopProfit + fundingProfit;
        UIScript.UpdateUI();
    }

    public void NextMonth()//when they click the "next month" button,
    {
        //make sure they're eligible to start the next month
        if(touristsVisiting)
        {
            return;
        }
        if(currentMonth >= 12)
        {
            endingPanel.SetActive(true);
            statsText.text = "You had $" + money + " of remaining funding, and a total profit of $" + totalProfit + "." +
                             "\nThe overall happiness of your animals was " + happiness + "/100.";
            if(happiness > 80)
            {
                analysisText.text = "Based on these results, it look like you tried to make your animals as happy as possible. Did you struggle with acquiring all the funds you needed?" +
                    "\n\nWere you able to buy all the animals you wanted?" + "\n\nHas your outlook on the difficulties of maintaining a successful, happy zoo changed?";
            }
            if (happiness > 50 && happiness <= 80)
            {
                analysisText.text = "Based on these results, it look like you tried to maintain a balanced approach of acquiring funding and keeping animals happy." +
                    "\n\nWere you able to buy all the animals you wanted, and make them as happy as you wanted?" + "\n\nHas your outlook on the difficulties of maintaining a successful, happy zoo changed?";
            }
            if (happiness <= 50)
            {
                analysisText.text = "Based on these results, it look like you tried to make as much money as possible, and weren't concerned about the happiness of your animals." +
                    "\n\nWere you just trying to see how much money you could possibly make? If you were running a real zoo, would you try to make your animals happier than this?" +
                    "\n\nHas your outlook on the difficulties of maintaining a successful, happy zoo changed?";
            }
        }

        money += totalProfit;//calculate events
        happiness += happinessPerMonth;
        if(happiness > 100) { happiness = 100; }
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
        yield return new WaitForSeconds(1);
        nextMonthButton.gameObject.SetActive(true);
        hiderPanel.SetActive(false);
        touristsVisiting = false;
        UIScript.UpdateChoiceButtons();
        UIScript.ToggleChoiceButtons(true);
        currentAnimalMoveFrequencyMultiplier = animalMoveFrequencyDayMultiplier;
        if(currentMonth >= 12)
        {
            nextMonthButtonText.text = "Results";
        }

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
