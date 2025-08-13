using System.Collections.Generic;
using System.Linq;
using DigitalSalmon.ImGui;
using DigitalSalmon.Lite.Core;
using UnityEngine;

namespace DigitalSalmon.C360 {
	public class Time : Singleton<Time> {

		private static readonly HashSet<ITimeController> controllers = new();

		[SerializeField]
		protected bool showTime;

		public static double          TimeMs     => Controller?.Time ?? 0;
		public static ITimeController Controller { get; private set; }

		private static Sequence Sequence { get; set; }

		protected void Awake() { Sequence = new Sequence(this); }

		protected void OnGUI() {
			if (!showTime) return;
			Style.GUI.Box(new Rect(0, 0, 500, 30), DigitalSalmon.Colours.BLACK_60);
			string label = $"[Controller: {Controller}] Seconds:{TimeMs.ToString("0000")}";
			if (Complete360Tour.ActiveNode is MediaNode timelineNode) { label += $" / {timelineNode.DurationMs:0000}"; }

			GUILayout.Label(label);
		}

		/// <summary>
		/// Time controllers, such as video players or the image clock, can register as the current controller.
		/// In theory there is only one controller at a time, but just in case we need a prefab or something to 'pause' the clock,
		/// we allow multiple controllers.
		/// </summary>
		public static void RegisterController(ITimeController controller) {
			if (controllers.Contains(controller)) return;
			controllers.Add(controller);
			if (Controller == null || controller.Priority >= Controller.Priority) Controller = controller;
		}

		public static void UnregisterController(ITimeController controller) {
			if (!controllers.Contains(controller)) return;
			controllers.Remove(controller);
			Controller = null;
			if (controllers.Any()) {
				Controller = controllers.OrderBy(c => c.Priority).LastOrDefault();
			}
		}

	}
}