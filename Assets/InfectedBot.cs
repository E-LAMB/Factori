using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedBot : MonoBehaviour
{

    public Unit nearest_unit;

    public Transform self;
    public Vector3 ideal_position;

    public float attack_damage;
    public float health;

    public LayerMask obstructing_layer;

    public bool is_active;

    public SpriteRenderer my_renderer;
    public Sprite bot_disabled;
    public Sprite bot_enabled;

    public bool is_moving;

    // Start is called before the first frame update
    void Start()
    {
        ideal_position = self.position;
    }

    public void WakeUp()
    {
        is_active = true;
    }

    public void TakeTurn()
    {
        if (is_active)
        {

            Unit[] all_bots = Object.FindObjectsOfType<Unit>();

            if (all_bots.Length > 0)
            {

                nearest_unit = all_bots[Random.Range(0, all_bots.Length)];

                for (int x = 0; x < all_bots.Length; x++)
                {
                    if (Vector3.Distance(self.position, nearest_unit.self.position) > Vector3.Distance(self.position, all_bots[x].self.position))
                    {
                        nearest_unit = all_bots[x];
                    }
                }

                Vector3 picked_position = self.position;

                if (nearest_unit.self.position.x > picked_position.x) {picked_position.x += 1;}
                if (nearest_unit.self.position.x < picked_position.x) {picked_position.x -= 1;}
                if (nearest_unit.self.position.y > picked_position.y) {picked_position.y += 1;}
                if (nearest_unit.self.position.y < picked_position.y) {picked_position.y -= 1;}

                if (Physics2D.OverlapCircle(picked_position, 0.4f, obstructing_layer))
                {

                    Collider2D found = Physics2D.OverlapCircle(picked_position, 0.4f, obstructing_layer);

                    if (found != null)
                    {

                        if (found.gameObject.GetComponent<Building>())
                        {
                            found.gameObject.GetComponent<Building>().health -= attack_damage;

                            if (found.gameObject.GetComponent<Building>().health < 1)
                            {
                                Destroy(found.gameObject);
                            }

                        } else if (found.gameObject.GetComponent<Unit>())
                        {
                            found.gameObject.GetComponent<Unit>().health -= attack_damage;

                            if (found.gameObject.GetComponent<Unit>().health < 1)
                            {
                                Destroy(found.gameObject);
                            }

                        } else
                        {
                            Debug.Log("Can't tell what this is?");
                        }

                        if (!Physics2D.OverlapCircle(picked_position, 0.4f, obstructing_layer))
                        {
                            ideal_position = picked_position;
                        }

                    } else
                    {
                        ideal_position = picked_position;
                    }

                } else
                {
                    ideal_position = picked_position;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        self.position = Vector3.MoveTowards(self.position, ideal_position, Time.deltaTime * 5f);

        ideal_position = new Vector3(Mathf.Round(ideal_position.x), Mathf.Round(ideal_position.y), 0f);
        is_moving = (Vector3.Distance(self.position, ideal_position) > 0.1f);

        if (is_active) {my_renderer.sprite = bot_enabled;} else {my_renderer.sprite = bot_disabled;}
    }
}
