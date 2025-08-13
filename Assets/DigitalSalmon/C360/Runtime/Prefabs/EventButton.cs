using UnityEngine;
using UnityEngine.Events;

namespace DigitalSalmon.C360 {
public class EventButton : Button {
	[Header("Event Button")]
	[SerializeField]
	protected UnityEvent onSubmit;

	protected override void OnSubmitted() {
		base.OnSubmitted();
		onSubmit?.Invoke();
	}
}
}