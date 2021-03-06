﻿/// <summary>
/// Each time a sentence is completed by submitting with the green checkmark button a new completed sentence scroll view is created.
/// 
/// <author> antin006@morris.umn.edu </author>
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SubmitSentenceButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

	// Temporary colors to use for completed sentences
	static Color[] colors = { new Color(0.357f,0.608f,0.835f), new Color(0.439f,0.678f,0.278f), new Color(0.929f,0.49f,0.192f), new Color(1f,0.753f,0f) };

	// Iterate through colors
	static int currentColor = 0;

	// Resize image on mouseover
	private Vector2 defaultSize,
			    	highlightSize;

	// Completed sentences list at bottom of screen
	public Transform completedSentences;

	// Completed sentence view that will contain the submitted sentence
	public Transform completedSentenceScrollView;

	// Sentence at top of screen top pull sentence text from
	public Transform sentence;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start(){
		defaultSize = this.transform.GetComponent<Image>().rectTransform.sizeDelta;
		highlightSize = new Vector2 (defaultSize.x + 10, defaultSize.y + 10);
	}
		
	/// <summary>
	/// Raises the pointer enter event.
	/// Increases the size of the submit sentence button image.
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnPointerEnter (PointerEventData eventData) {
		transform.GetComponent<Image> ().rectTransform.sizeDelta = highlightSize;
	}

	/// <summary>
	/// Raises the pointer exit event.
	/// Decreases the size of the submit sentence button image.
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnPointerExit (PointerEventData eventData) {
		transform.GetComponent<Image> ().rectTransform.sizeDelta = defaultSize;
	}
		
	/// <summary>
	/// Raises the pointer click event.
	/// Submits the sentence to the completed sentences list.
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnPointerClick (PointerEventData eventData) {

		// If there is words in sentence
		if (sentence.childCount > 0) {


			string sentenceText = " ";

			// Grab all text from sentence
			for (int i = 0; i < sentence.childCount; i++) {

				string word = sentence.GetChild (i).GetChild (0).GetComponent<Text> ().text;

				sentenceText += word + " ";	
			}
				
			// Create a new scroll view
			completedSentenceScrollView = Instantiate (completedSentenceScrollView);

			// Give it a new color
			completedSentenceScrollView.GetComponent<Image> ().color = colors [currentColor++ % colors.Length];

			// Set the text of the scroll view
			completedSentenceScrollView.GetChild (0).GetComponent<Text> ().text = sentenceText;

			// Add teh scroll view to the completed sentences list
			completedSentenceScrollView.SetParent (completedSentences, false);

			// Clear the sentence to ready it for new sentences
			sentence.GetComponent<Sentence> ().clear ();
		}
	}

}
