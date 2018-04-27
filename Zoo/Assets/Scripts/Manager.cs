using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {


    public float money = 0;
    public List<GameObject> allAnimals = new List<GameObject>();
	// Use this for initialization
	void Start () {
        money += 200000;//starting funds
	}
	
	// Update is called once per frame
	void Update () {
        AnimalBehavior();
	}

    void NextMonth()//when they click the "next month" button,
    {
        //make sure they're ready to start the next month
        //calculate events
        //show visitors walking around
        //show animals doing cute stuff
        //end all the action
        //give them new decisions to make
    }

    void AnimalBehavior()
    {
        if(allAnimals.Count == 0) { return; }
        foreach (GameObject animal in allAnimals)
        {
            Rigidbody2D rb = animal.GetComponent<Rigidbody2D>();
            if(Random.value > .995f)
            {
                rb.velocity = new Vector2(0, 0);
                Vector2 moveDirection = new Vector2(Random.Range(-1,2), Random.Range(-1,2));
                rb.AddForce(moveDirection * (Random.Range(1,50)));
            }
        }
    }
}
