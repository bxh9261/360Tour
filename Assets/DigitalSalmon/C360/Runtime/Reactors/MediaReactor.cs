using UnityEngine;

namespace DigitalSalmon.C360 {
	[AddComponentMenu("Complete 360 Tour/Core/Media Reactor")]
	public abstract class MediaReactor : BaseBehaviour {

		protected MediaSurface Surface { get; private set; }

		protected virtual void Awake() { Surface = GetComponent<MediaSurface>(); }

		protected virtual void OnEnable() { Complete360Tour.MediaSwitch += C360_MediaSwitch; }

		protected virtual void OnDisable() { Complete360Tour.MediaSwitch -= C360_MediaSwitch; }

		protected abstract void C360_MediaSwitch(TransitionState state, Node node);

	}
}