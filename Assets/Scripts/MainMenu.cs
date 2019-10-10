﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {
	private GameStateManager t_GameStateManager;
	public Text TopText;

	public GameObject VolumePanel;
	public GameObject SoundSlider;
	public GameObject MusicSlider;

	public bool volumePanelActive;

	// Use this for initialization
	void Start () {
		t_GameStateManager = FindObjectOfType<GameStateManager> ();
		t_GameStateManager.ConfigNewGame ();

		int currentHighScore = PlayerPrefs.GetInt ("highScore", 0);
		//TopText.text = "TOP- " + currentHighScore.ToString ("D6");

		if (!PlayerPrefs.HasKey ("soundVolume")) {
			PlayerPrefs.SetFloat ("soundVolume", 1);
		}

		if (!PlayerPrefs.HasKey ("musicVolume")) {
			PlayerPrefs.SetFloat ("musicVolume", 1);
		}

		SoundSlider.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("soundVolume");
		MusicSlider.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("musicVolume");

		Debug.Log (this.name + " Start: Volume Setting sound=" + PlayerPrefs.GetFloat ("soundVolume")
			+ "; music=" + PlayerPrefs.GetFloat ("musicVolume"));
	}

	public void OnMouseHover(Button button) {
		if (!volumePanelActive) {
			GameObject cursor = button.transform.Find ("Cursor").gameObject;
			cursor.SetActive (true);
		}
	}

	public void OnMouseHoverExit(Button button) {
		if (!volumePanelActive) {
			GameObject cursor = button.transform.Find ("Cursor").gameObject;
			cursor.SetActive (false);
		}
	}

	public void Start_Batam1() {
		if (!volumePanelActive) {
			t_GameStateManager.sceneToLoad = "City 1-1";
			SceneManager.LoadScene ("Level Start Screen");
		}
	}

	public void Start_Bandung1() {
		if (!volumePanelActive) {
			t_GameStateManager.sceneToLoad = "City 2-1";
			SceneManager.LoadScene ("Level Start Screen");
		}
	}
		
	public void Start_Surabaya1() {
		if (!volumePanelActive) {
			t_GameStateManager.sceneToLoad = "City 3-1";
			SceneManager.LoadScene ("Level Start Screen");
		}
	}


	public void Start_Jakarta2() {
		if (!volumePanelActive) {
			t_GameStateManager.sceneToLoad = "City 4-2";
			SceneManager.LoadScene ("Level Start Screen");
		}
	}

	public void QuitGame() {
		if (!volumePanelActive) {
			Application.Quit ();
		}
	}

	public void SelectVolume() {
		VolumePanel.SetActive (true);
		volumePanelActive = true;
	}

	public void SetVolume() {
		PlayerPrefs.SetFloat ("soundVolume", SoundSlider.GetComponent<Slider> ().value);
		PlayerPrefs.SetFloat ("musicVolume", MusicSlider.GetComponent<Slider> ().value);
		VolumePanel.SetActive (false);
		volumePanelActive = false;
	}

	public void CancelSelectVolume() {
		SoundSlider.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("soundVolume");
		MusicSlider.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("musicVolume");
		VolumePanel.SetActive (false);
		volumePanelActive = false;
	}

}
