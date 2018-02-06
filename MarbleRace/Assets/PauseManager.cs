using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour {
	public GameObject pauseCanvas;
	public GameObject firstSelectedButton;
	EventSystem eventSystem;

	private void Start(){
		eventSystem = FindObjectOfType<EventSystem>();
	}

	public void Unpause(){
		pauseCanvas.SetActive(false);
		Time.timeScale = 1;
	}

	public void Pause(){
		pauseCanvas.SetActive(true);
		Time.timeScale = 0;
		eventSystem.SetSelectedGameObject(firstSelectedButton);
	}

	private void Update(){
		if (Input.GetButtonDown("Pause")){
			if (pauseCanvas.activeSelf){
				Unpause();
			}else{
				Pause();				
			}
		}
	}
}
