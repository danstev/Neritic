using UnityEngine;
using System.Collections;

public class PopUp : MonoBehaviour {

    bool gui;
    string text;
    GUIStyle style;
    public bool customText;
    public string customTextString;

    void Start () {
        if (customText == false)
        {
            gui = false;
            style = new GUIStyle();
            style.fontSize = 20;
            style.alignment = TextAnchor.UpperCenter;

            //Item stuff
            Item i = GetComponent<Item>();
            Equipment e = GetComponent<Equipment>();
            string name = i.name;
            string description = i.description;
            text = name;
            text += "\n";
            text += description;

            //Equip stuff
            //string level;

            if (e != null)
            {
                string slot;

                if (e.attack != 0)
                {
                    string attack = e.attack.ToString();
                    text += "\nAttack: ";
                    text += attack;
                }

                if (e.armour != 0)
                {
                    string armour = e.armour.ToString();
                    text += "\nAttack: ";
                    text += armour;
                }

                if (e.strength != 0)
                {
                    string strength = e.strength.ToString();
                    text += "\nStrength: ";
                    text += strength;
                }

                if (e.intellect != 0)
                {
                    string intellect = e.intellect.ToString();
                    text += "\nIntellect: ";
                    text += intellect;
                }

                if (e.agility != 0)
                {
                    string agility = e.agility.ToString();
                    text += "\nAgility: ";
                    text += agility;
                }

                switch (e.slot)
                {
                    case 1:
                        slot = "Off handed weapon";
                        break;
                    case 2:
                        slot = "Helmet";
                        break;
                    case 3:
                        slot = "Chestpiece";
                        break;
                    case 4:
                        slot = "Leggings";
                        break;
                    case 5:
                        slot = "Gloves";
                        break;
                    case 6:
                        slot = "Feet";
                        break;
                    case 7:
                        slot = "Necklace";
                        break;
                    case 8:
                        slot = "Ring";
                        break;
                    case 9:
                        slot = "Ring"; //wont get here though
                        break;
                    case 0:
                        slot = "Main hand weapon";
                        break;
                    default:
                        slot = "";
                        break;
                }
                text += "\n";
                text += slot;

            }
        }
        else
        {
            gui = false;
            style = new GUIStyle();
            style.fontSize = 20;
            style.alignment = TextAnchor.UpperCenter;
            text = customTextString;
        }
	}
	
	

    void OnMouseEnter()
    {
        gui = true;
    }

    void OnMouseExit()
    {
        gui = false;
    }

    void OnGUI()
    {

        if (gui)
        {
            //GUI.Box(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), " ");
            GUI.backgroundColor = Color.blue;
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), text, style);
        }
    }
}
