using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {
	public Transform block;
	Text display;
	float t;
	void Start () {
		t = 0;
		display = GetComponent<Text>();
	}
	void Update () {
		t += Time.deltaTime;
		if (t <= 1) {display.text = "3"; display.color = new Color(0.509804f, 0.3411765f, 1f, 1);}
		else 
		if (t <= 2) {display.text = "2"; display.color = new Color(0.7882353f, 0.3568628f, 0.9607843f, 1);}
		else
		if (t <= 3)	{display.text = "1"; display.color = new Color(0.6705883f, 0.9803922f, 0.9529412f, 1);}

		if (t >= 3) {
			block.gameObject.SetActive(false);			
			display.text = "ROLL"; 
			display.color = new Color(0.9843137f, 0.7411765f, 0.854902f, 1);
			}
		
		if (t >= 4){
			display.text = ""; 
			Destroy(this.gameObject);
		}
	}
}
