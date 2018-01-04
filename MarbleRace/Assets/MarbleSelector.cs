using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarbleSelector : MonoBehaviour {
	[HideInInspector]
	public bool selected = false;
	[HideInInspector]
	public bool ready = false;
	public Text nameText;
	public GameObject arrows;
	public GameObject joinText;
	public GameObject indicator;
	MarbleSkin ms;
	// Use this for initialization
	void Start () {
		ms = GetComponent<MarbleSkin>();
	}
	private void LateUpdate(){
		if (!selected){
			arrows.SetActive(false);
		}
	}
	public void Select(){
		nameText.gameObject.SetActive(true);
		arrows.SetActive(true);
		joinText.SetActive(false);
		indicator.SetActive(true);
		selected = true;
	}
	public void Deselect(){
		nameText.gameObject.SetActive(false);
		arrows.SetActive(false);
		joinText.SetActive(true);
		indicator.SetActive(false);
		selected = false;
	}
	public void SetReady(){
		arrows.SetActive(false);
		ready = true;
	}
	public void SetNotReady(){
		arrows.SetActive(true);
		ready = false;
	}
}
