using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectTooltip : MonoBehaviour {

	private Image icon;
	private Text nameText;
	private Text description;
	public static ObjectTooltip instance;

	// Use this for initialization
	void Start () {
		instance = this;
		icon = transform.Find ("Icon").GetComponent<Image>();
		nameText = transform.Find ("Name").GetComponent<Text>();
		description = transform.Find ("Description").GetComponent<Text>();
		Hide ();
	}
	
	public static void Show(Ability ability){
		instance.icon.enabled = true;
		instance.icon.sprite = Resources.Load <Sprite>("Sprites/" + ability.SpriteName ());
		instance.nameText.text = ability.Name();
		instance.description.text = ability.Description();
	}

	public static void Show(Item item){
		instance.icon.enabled = true;
		instance.icon.sprite = item.sprite;
		instance.nameText.text = item.Name();
		instance.description.text = item.Description();
	}

	public static void Hide(){
		instance.icon.enabled = false;
		instance.nameText.text = "";
		instance.description.text = "";
	}

	public static void MoveTo(Vector3 position){
		instance.transform.position = position;
	}
}
