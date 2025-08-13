using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DigitalSalmon.C360 {
public class Button : AnimatedBehaviour {
	[SerializeField]
	protected TMP_Text label;

	protected void Start() {
		EnableInteraction();
	}
	
	public void SetLabelText(string text) {
		label.text = text;
	}
}
}