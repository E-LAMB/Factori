using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public Player player;

    public Transform self;
    public Vector3 ideal_position;

    public string unit_name;
    public float action_cost;
    public float attack_damage;
    public float health;

    public LayerMask blockable;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>();
        if (player.role == "Warrior") {attack_damage += 1;}
        ideal_position = self.position;
    }

    public void PlayerTurnEnded()
    {
        
    }

    void OnMouseDown()
    {
        player.SelectUnit(this);
    }

    public void RequestMove(Vector3 target)
    {

        if (unit_name == "Explorer")
        {
            if (Vector3.Distance(self.transform.position, target) < 5f && Vector3.Distance(self.transform.position, target) > 0.4f)
            {
                if (player.current_ap >= action_cost)
                {
                    player.current_ap -= action_cost;
                    ideal_position = target;
                }
            }

        } else
        {
            if (Vector3.Distance(self.transform.position, target) < 1.5f && Vector3.Distance(self.transform.position, target) > 0.4f)
            {
                if (Physics2D.OverlapCircle(target, 0.4f, blockable))
                {
                    Collider2D found = Physics2D.OverlapCircle(target, 0.4f, blockable);

                    if (found.gameObject.GetComponent<InfectedBot>())
                    {
                        if (player.current_ap >= action_cost)
                        {
                            found.gameObject.GetComponent<InfectedBot>().health -= attack_damage;
                            player.current_ap -= action_cost;

                            if (found.gameObject.GetComponent<InfectedBot>().health < 1)
                            {
                                Destroy(found.gameObject);
                            }

                            if (!Physics2D.OverlapCircle(target, 0.4f, blockable)) {ideal_position = target;}
                        }
                    }

                } else
                {
                    if (player.current_ap >= action_cost)
                    {
                        player.current_ap -= action_cost;
                        ideal_position = target;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        self.position = Vector3.MoveTowards(self.position, ideal_position, Time.deltaTime * 5f);
    }
}
