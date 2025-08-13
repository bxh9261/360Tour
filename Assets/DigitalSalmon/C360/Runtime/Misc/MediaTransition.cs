using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DigitalSalmon.C360 {
	[AddComponentMenu("Complete 360 Tour/Transition/Media Transition")]
	public abstract class MediaTransition : BaseBehaviour {

		public event EventHandler<TransitionState, Node> MediaSwitch;

		private readonly HashSet<object> loadObjects = new();

		public    bool IsTransitioning { get; protected set; }
		protected bool IsLoading       { get; private set; }

		public abstract void StartTransition(Node node);
		public virtual  void Interrupt() { }

		public void RegisterLoadObject(object obj) {
			if (obj == null) return;
			loadObjects.Add(obj);
			IsLoading = true;
		}

		public void UnregisterLoadObject(object obj) {
			if (obj == null) return;
			loadObjects.Remove(obj);
			IsLoading = loadObjects.Any();
		}

		protected void InvokeMediaSwitch(TransitionState state, Node node) { MediaSwitch?.Invoke(state, node); }

	}
}