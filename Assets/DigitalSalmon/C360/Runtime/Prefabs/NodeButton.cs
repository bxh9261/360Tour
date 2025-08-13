using UnityEngine;

namespace DigitalSalmon.C360 {
	public class NodeButton : Button {

		[Header("Node Button")]
		[SerializeField]
		protected Node node;

		public void AssignNode(Node node) {
			this.node = node;
			SetLabelText(node.Name);
		}

		protected override void OnSubmitted() {
			base.OnSubmitted();
			Complete360Tour.GoToMedia(node);
		}

	}
}