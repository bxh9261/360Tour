using UnityEngine;
using UnityEngine.SceneManagement;

namespace DigitalSalmon.C360 {
	public class SceneButton : Button {

		[Header("Scene Button")]
		[SerializeField]
		protected string targetScene;

		public string TargetScene => targetScene;

		public void AssignScene(string sceneName) {
			targetScene = sceneName;
			SetLabelText(sceneName);
		}

		protected override void OnSubmitted() {
			base.OnSubmitted();
			DisableInteraction();

			FadePostProcess fadePostProcess = FindObjectOfType<FadePostProcess>();
			if (fadePostProcess != null) fadePostProcess.FadeDown(onComplete: () => SceneManager.LoadScene(TargetScene));
			else SceneManager.LoadScene(TargetScene);
		}

	}
}