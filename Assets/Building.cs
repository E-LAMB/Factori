using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    public float health;

    public string building_name;

    public float internal_charge;

    public Player player;

    public Transform self;

/*

How to acquire spools and their uses:

Red Spools - 
Used for Bots and other moving components,
Comes from ruins and other bots.

Green Spools -
Used for repairs and maintenance,
Comes from ruins - Begin with 5 (+2 if builder)

Blue Spools -
A resource used in building many things
Comes from ruins and can be broken down from Deposits

Yellow Spools -
A multipurpose resource, Can only be synthesised with a Synthesiser.

Bots:

Fighter Bot -
A bot that deals extra damage. 
When fighting, Has a 25% chance to deal critical damage - Dealing one extra damage point.

Explorer Bot -
A bot capable of moving a distance of 5 units. 
Ignores obstacles and anything that would be in the way. 
Cannot initiate attacks.

Wall Bot -
A bot with a lot of health. 
Takes 2 action points to use.

Research Bot -
Builds 2 charges internally at the end of each turn if near a Ruin. 
When near any infection, Build up 3 more charges as well. 
Once reaching 15 charges, Adds 1 point towards research and resets its internal charge to 0. 
If you are the Researcher, There is a 50% chance of creating a second charge at the end of each turn. 

Builder -
Gives 10 build progress to the nearest building while it is being built, Without the need for having excess action points.

Buildings:

Wall -
A solid structure with a lot of health.

Research Station -
Builds 1 charge internally at the end of each turn. 
Once reaching 15 charges, Adds 1 point towards research and reset internal charge to 0. 
There is a 50% chance of creating a second charge at the end of each turn 
as well as another 10% chance of damaging itself at the end of each turn
If you are the Researcher, Then the charges generated during the turn are multiplied by %150 (1.5x multiplier)

Inhibitor -
Prevents nearby Research Stations from inflicting damage on each other. 
Prevents damage notifications from nearby buildings. 
Prevents viruses from Research Stations from spreading to other nearby buildings.

Ruin Decomposer -
Deals 1 damage to each nearby ruin. 
For each damage dealt, A random Red, Green or Blue spool is created. 

Synthesiser -
Creates Yellow Spools from excess action points. 
10pts = 1 spool.
Takes 1 excess action point per turn.

Spool Storage -
Stores 20 spools of a certain type for the next turn. 
When destroyed, You lose half of your total resources of that type.

    */

    public void PlayerTurnEnded()
    {
        if (building_name == "Research Station")
        {
            float added_charges = 1;

            if (Random.Range(0, 2) == 1) { added_charges += 1f;}
            
            if (Random.Range(0, 11) == 1) 
            {

                bool inhibitors_nearby = false;

                Building[] all_buildings = Object.FindObjectsOfType<Building>();
                if (all_buildings.Length > 0) 
                {
                    for (int x = 0; x < all_buildings.Length; x++) 
                    {
                        if (all_buildings[x].building_name == "Inhibitor" && Vector3.Distance(self.position, all_buildings[x].self.position) < 5f)
                        {
                            inhibitors_nearby = true;
                        }
                    }
                }

                if (!inhibitors_nearby)
                {
                    health -= 1f;
                    if (health <= 0) {Destroy(gameObject);}
                }
            } 

            internal_charge += added_charges;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
