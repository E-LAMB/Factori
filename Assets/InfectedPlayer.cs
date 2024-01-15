using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedPlayer : MonoBehaviour
{

    public Player player;

    public int max_actions;
    public int actions;
    public int wakeup_cost;

    public bool infection_turn;
    public bool active_bots;

    public float turn_time;

    public int turns_until_upgrade;

    public InfectedBot[] all_infected;

    public int mourning_time;

    public void Start()
    {
        
    }

    public void InfectionTurnStart()
    {

        turns_until_upgrade -= 1;

        if (turns_until_upgrade <= 0)
        {
            turns_until_upgrade = Random.Range(9, 16);

            max_actions += 1;
        }

        actions = max_actions;

        if (mourning_time > 0)
        {
            turns_until_upgrade -= 1;
            if (actions > 3) { actions = 3; }
        }

        infection_turn = true;

        turn_time = 0f;

        all_infected = Object.FindObjectsOfType<InfectedBot>();
        active_bots = false;

        for (int x = 0; x < all_infected.Length; x++)
        {
            if (all_infected[x].is_active)
            {
                active_bots = true;
            }
        }

    }

    public void InfectionTurnEnd()
    {
        infection_turn = false;
        mourning_time -= 1;
        wakeup_cost -= 1;

        player.BeginPlayerTurn();
    }

    public void Mourn()
    {
        mourning_time = Random.Range(3, 6);
        wakeup_cost -= 1;
    }

    public void Update()
    {
        if (infection_turn)
        {
            turn_time += Time.deltaTime;
        
            all_infected = Object.FindObjectsOfType<InfectedBot>();

            if (all_infected.Length > 0)
            {

                int chosen_pawn = Random.Range(0, all_infected.Length);

                if (!all_infected[chosen_pawn].is_active)
                {
                    if (actions >= wakeup_cost)
                    {
                        actions -= wakeup_cost;
                        wakeup_cost += 1;
                        if (wakeup_cost > 15)
                        {
                            wakeup_cost = 15;
                        }
                        all_infected[chosen_pawn].WakeUp();
                    }

                } else
                {
                    if (!all_infected[chosen_pawn].is_moving && actions > 1)
                    {
                        actions -= 2;
                        all_infected[chosen_pawn].TakeTurn();
                    }
                }

                if (actions == 1)
                {
                    actions = 0;
                }
            }

            if (turn_time > 3f || actions == 0 || (!active_bots && wakeup_cost >= 0) || all_infected.Length == 0)
            {
                InfectionTurnEnd();
            }
        }

    }
}
