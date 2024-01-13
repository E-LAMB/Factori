using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialougeManager : MonoBehaviour
{

    public string[] whole_script;
    public int script_position;

    public Player player;

    public GameObject dialouge_object;

    public TextMeshProUGUI main_text;
    public GameObject main_object;

    public GameObject choice_1;
    public GameObject choice_2;
    public GameObject choice_3;
    public GameObject choice_4;
    public GameObject only_1_choice;

    public GameObject choice_making_background;

    public TextMeshProUGUI only_1_choice_text;
    public TextMeshProUGUI choice_1_text;
    public TextMeshProUGUI choice_2_text;
    public TextMeshProUGUI choice_3_text;
    public TextMeshProUGUI choice_4_text;

    public bool waiting_on_key;

    public void BeginDialouge(DialougeScript to_take_from)
    {
        whole_script = to_take_from.script;
        script_position = 0;

        waiting_on_key = true;

        player.in_dialouge = true;

        dialouge_object.SetActive(true);

        main_object.SetActive(true);
        main_text.text = "";

        NextDialouge();

    }

    public void MadeChoice(int choice)
    {
        script_position += choice;

        choice_1.SetActive(false);
        choice_2.SetActive(false);
        choice_3.SetActive(false);
        choice_4.SetActive(false);
        only_1_choice.SetActive(false);
        choice_making_background.SetActive(false);

        waiting_on_key = true;

        dialouge_object.SetActive(true);

        NextDialouge();
    }

    public void NextDialouge()
    {
        
        if (whole_script[script_position] == "")
        {
            EndDialouge();
            return;

        } else if (whole_script[script_position].Contains("&-"))
        {
            /*

            All Commands: All commands have the &- prefix

            &-DebugCommand // Makes a debug.log appear

            &-Give-R // Gives the player R material indicated by the next line of the script
            &-Give-G // Gives the player G material indicated by the next line of the script
            &-Give-B // Gives the player B material indicated by the next line of the script
            &-Give-Y // Gives the player B material indicated by the next line of the script

            &-Choice-1 // Creates a choice for the player with one option
            &-Choice-2 // Creates a choice for the player with two options
            &-Choice-4 // Creates a choice for the player with four options

            &-Warp // Teleports to the line indicated by the next line of the script

            &-Warp-IfWarrior // Teleports to the line indicated by the next line of the script if Warrior, Otherwise skips the next line
            &-Warp-IfInventor // Teleports to the line indicated by the next line of the script if Inventor, Otherwise skips the next line
            &-Warp-IfBuilder // Teleports to the line indicated by the next line of the script if Builder, Otherwise skips the next line
            &-Warp-IfSalvager // Teleports to the line indicated by the next line of the script if Salvager, Otherwise skips the next line

            &-SetRole-Warrior // Makes the player's role Warrior
            &-SetRole-Inventor // Makes the player's role Inventor
            &-SetRole-Builder // Makes the player's role Builder
            &-SetRole-Salvager // Makes the player's role Salvager

            */

            bool do_next = true;

            if (whole_script[script_position].Contains("&-SetRole-Warrior")) {player.role = "Warrior"; script_position += 1;}
            else if (whole_script[script_position].Contains("&-SetRole-Inventor")) {player.role = "Inventor"; script_position += 1;}
            else if (whole_script[script_position].Contains("&-SetRole-Builder")) {player.role = "Builder"; script_position += 1;}
            else if (whole_script[script_position].Contains("&-SetRole-Salvager")) {player.role = "Salvager"; script_position += 1;}

            else if (whole_script[script_position].Contains("&-Warp"))
            {
                script_position += 1;
                script_position = int.Parse(whole_script[script_position]);
            }

            else if (whole_script[script_position].Contains("&-Warp-Warrior"))
            {
                if (player.role == "Warrior")
                {
                    script_position += 1;
                    script_position = int.Parse(whole_script[script_position]);
                } else
                {
                    script_position += 2;
                }
            }
            else if (whole_script[script_position].Contains("&-Warp-Inventor"))
            {
                if (player.role == "Inventor")
                {
                    script_position += 1;
                    script_position = int.Parse(whole_script[script_position]);
                } else
                {
                    script_position += 2;
                }
            }
            else if (whole_script[script_position].Contains("&-Warp-Builder"))
            {
                if (player.role == "Builder")
                {
                    script_position += 1;
                    script_position = int.Parse(whole_script[script_position]);
                } else
                {
                    script_position += 2;
                }
            }
            else if (whole_script[script_position].Contains("&-Warp-Salvager"))
            {
                if (player.role == "Salvager")
                {
                    script_position += 1;
                    script_position = int.Parse(whole_script[script_position]);
                } else
                {
                    script_position += 2;
                }
            }

            else if (whole_script[script_position].Contains("&-Choice-1"))
            {
                waiting_on_key = false;
                do_next = false;

                only_1_choice.SetActive(true);
                script_position += 1;
                only_1_choice_text.text = whole_script[script_position];

                main_object.SetActive(false);
                main_text.text = "";

                choice_making_background.SetActive(true);

            } else if (whole_script[script_position].Contains("&-Choice-2"))
            {
                waiting_on_key = false;
                do_next = false;

                choice_1.SetActive(true);
                script_position += 1;
                choice_1_text.text = whole_script[script_position];

                choice_1.SetActive(true);
                script_position += 1;
                choice_1_text.text = whole_script[script_position];

                main_object.SetActive(false);
                main_text.text = "";

                choice_making_background.SetActive(true);

            } else if (whole_script[script_position].Contains("&-Choice-4"))
            {
                waiting_on_key = false;
                do_next = false;

                choice_1.SetActive(true);
                script_position += 1;
                choice_1_text.text = whole_script[script_position];

                choice_2.SetActive(true);
                script_position += 1;
                choice_2_text.text = whole_script[script_position];

                choice_3.SetActive(true);
                script_position += 1;
                choice_3_text.text = whole_script[script_position];

                choice_4.SetActive(true);
                script_position += 1;
                choice_4_text.text = whole_script[script_position];

                main_object.SetActive(false);
                main_text.text = "";

                choice_making_background.SetActive(true);

            }

            else if (whole_script[script_position].Contains("&-DebugCommand"))
            {
                script_position += 1;
                Debug.Log(whole_script[script_position]);
                script_position += 1;
            }

            else if (whole_script[script_position].Contains("&-Give-R"))
            {
                script_position += 1;
                player.material_R += int.Parse(whole_script[script_position]);
                script_position += 1;
            }
            else if (whole_script[script_position].Contains("&-Give-G"))
            {
                script_position += 1;
                player.material_G += int.Parse(whole_script[script_position]);
                script_position += 1;
            }
            else if (whole_script[script_position].Contains("&-Give-B"))
            {
                script_position += 1;
                player.material_B += int.Parse(whole_script[script_position]);
                script_position += 1;
            }
            else if (whole_script[script_position].Contains("&-Give-Y"))
            {
                script_position += 1;
                player.material_Y += int.Parse(whole_script[script_position]);
                script_position += 1;
            }

            if (do_next) {NextDialouge();}

        } else
        {
            main_text.text = whole_script[script_position];
            script_position += 1;
        }

    }   

    public void EndDialouge()
    {
        dialouge_object.SetActive(false);
        main_object.SetActive(false);
        main_text.text = "";
        player.in_dialouge = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.in_dialouge && waiting_on_key && Input.GetKeyDown(KeyCode.E))
        {
            NextDialouge();
        }
    }
}
