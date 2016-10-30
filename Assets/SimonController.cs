using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SimonController : MonoBehaviour {

	public bool enableInput = false;

	public float stepPlaybackTime = .5f;

	public enum SimonButton{
		Undecided,
		Green,
		Red,
		Yellow,
		Blue
	}

	public int currentStep = 0;
	public int highestStep = 1;
	public Text levelCounter;

	public List<SimonButton> HitPattern = new List<SimonButton>();

	public Button[] InputButtons;
	public AudioSource[] buttonAudioSources;
	public UnityEngine.UI.Image[] buttonBaseSprites;
	public UnityEngine.UI.Image[] buttonHighlightSprites;

	public List<Color> colorPallets;

	public AudioSource WrongBuzzer;

	// Use this for initialization
	void Start () {
		BeginNextPatternStep ();

	}
	
	// Update is called once per frame
	void Update () {
		//Lazily keep the levelCounter updated
		levelCounter.text = "STAGE " + highestStep.ToString();


		if (enableInput) {
			for(int i = 0; i < InputButtons.Length; i++){
				InputButtons[i].interactable = true;
			}
		}
		else {
			for(int i = 0; i < InputButtons.Length; i++){
				InputButtons[i].interactable = false;
			}
		}
	}

	public void CompareInput(int playerInput){
		// int is used instead of enum because Unity Button Events can't use enums as parameters
		if ((SimonButton) playerInput == HitPattern [currentStep]) {
			// input matches, move to next step in pattern
			currentStep++;
			if (currentStep >= HitPattern.Count) {
				//Add to pattern and start again
				highestStep++;
				enableInput = false;
				BeginNextPatternStep();
			}
		}
		else { //player input fails to match the current step
			enableInput = false;
			Debug.Log("YOU LOSE");
			WrongBuzzer.Play ();
			StartCoroutine ("RestartGame");
			highestStep = 1;
		}
	}

	public void ClearPattern(){
		//Should only be used when beginning a new game
		HitPattern.Clear ();
	}

	public void BeginNextPatternStep(){
		//Add a new instruction to the list, increasing difficulty of the pattern.
		SimonButton newButton;
		newButton = (SimonButton) Random.Range (1, System.Enum.GetValues (typeof(SimonButton)).Length);
		HitPattern.Add (newButton);
		currentStep = 0; //Set currentstep back to 0, player has to start from beginning.
		StartCoroutine("PlayPattern");
	}

	IEnumerator RestartGame(){
		yield return new WaitForSeconds (1.5f);
		ClearPattern();
		BeginNextPatternStep ();
	}

	IEnumerator PlayPattern(){
		yield return new WaitForSeconds (1f);
		foreach (SimonButton step in HitPattern) {
			if (step == SimonButton.Green) {
				buttonAudioSources [0].Play ();
				buttonBaseSprites [0].color = colorPallets [1];
				buttonHighlightSprites [0].color = colorPallets [3];

				yield return new WaitForSeconds (stepPlaybackTime);

				buttonBaseSprites [0].color = colorPallets [0];
				buttonHighlightSprites [0].color = colorPallets [2];
			}

			else if (step == SimonButton.Red) {
				buttonAudioSources [1].Play ();
				buttonBaseSprites [1].color = colorPallets [5];
				buttonHighlightSprites [1].color = colorPallets [7];

				yield return new WaitForSeconds (stepPlaybackTime);

				buttonBaseSprites [1].color = colorPallets [4];
				buttonHighlightSprites [1].color = colorPallets [6];
			}

			else if (step == SimonButton.Yellow) {
				buttonAudioSources [2].Play ();
				buttonBaseSprites [2].color = colorPallets [9];
				buttonHighlightSprites [2].color = colorPallets [11];

				yield return new WaitForSeconds (stepPlaybackTime);

				buttonBaseSprites [2].color = colorPallets [8];
				buttonHighlightSprites [2].color = colorPallets [10];
			}

			else if (step == SimonButton.Blue) {
				buttonAudioSources [3].Play ();
				buttonBaseSprites [3].color = colorPallets [13];
				buttonHighlightSprites [3].color = colorPallets [15];

				yield return new WaitForSeconds (stepPlaybackTime);

				buttonBaseSprites [3].color = colorPallets [12];
				buttonHighlightSprites [3].color =colorPallets [14];
			}
		}
		enableInput = true; //enable input after the machine is done playing the pattern
	}
}
