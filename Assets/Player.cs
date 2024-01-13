using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

    public int current_turn = 0;
    public float max_ap = 3;
    public float current_ap = 3;

    public bool player_turn;

    public Camera main_cam;

    public Unit selected_unit;

    public Vector3 targeted_square;

    public bool show_descriptor;

    public bool in_dialouge;

    public float material_R = 0f; // A resource mostly for moving parts, Such as bots
    public float material_G = 0f; // A resource mostly for the maintenance of things
    public float material_B = 0f; // A resource for building things
    public float material_Y = 0f; // A multipurpose resource

    public string role;
    // Roles and their benefits

    // Builder - Has a chance to not consume an action point when building
    // Inventor - Increases the speed at which you research by 50%
    // Warrior - Units deal 1 extra damage when they are created
    // Salvager - Units that die grant a random resource

    public TextMeshProUGUI current_ap_title;
    public TextMeshProUGUI title_turn;

    public GameObject select_panel;
    public TextMeshProUGUI select_name;
    public TextMeshProUGUI select_atk;
    public TextMeshProUGUI select_hp;
    public TextMeshProUGUI select_ac;

    public DialougeScript starter_dialouge;
    public DialougeManager dialouge_manager;

    public GameObject material_view;
    public TextMeshProUGUI R_text;
    public TextMeshProUGUI G_text;
    public TextMeshProUGUI B_text;
    public TextMeshProUGUI Y_text;

    public InfectedPlayer infection_player;

    // Start is called before the first frame update
    void Start()
    {
        selected_unit = null;
        select_panel.SetActive(false);
        player_turn = false; 

        dialouge_manager.BeginDialouge(starter_dialouge);

        BeginPlayerTurn();
    }

    public void SelectUnit(Unit selected)
    {
        if (player_turn && !in_dialouge)
        {
            selected_unit = selected;
            show_descriptor = true;
            select_name.text = selected.unit_name;
            select_atk.text = "ATK: " + selected.attack_damage.ToString();
            select_hp.text = "HP: " + selected.health.ToString();
            select_ac.text = "AC: " + selected.action_cost.ToString();
        }
    }

    public void BeginPlayerTurn()
    {
        max_ap += 1f;
        current_ap = max_ap;
        current_turn += 1;
        title_turn.text = "Turn : " + current_turn.ToString();
        player_turn = true;
    }

    public void EndPlayerTurn()
    {
        if (player_turn)
        {
            Debug.Log("Turn Ended");
            selected_unit = null;
            show_descriptor = false;
            player_turn = false;
            infection_player.InfectionTurnStart(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        R_text.text = material_R.ToString();
        G_text.text = material_G.ToString();
        B_text.text = material_B.ToString();
        Y_text.text = material_Y.ToString();

        select_panel.SetActive(show_descriptor && !in_dialouge);
        material_view.SetActive(!in_dialouge);

        targeted_square = main_cam.ScreenToWorldPoint(Input.mousePosition);
        targeted_square = new Vector3 (Mathf.Round(targeted_square.x), Mathf.Round(targeted_square.y), 0f);

        current_ap_title.text = "AP : " + current_ap.ToString(); 

        if (selected_unit != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                selected_unit.RequestMove(targeted_square);
            }
        }
    }
}
