
using DigitalSalmon.C360.AnimatedComponents;

namespace DigitalSalmon.C360 {
	public class AnimatedComponentAnimator : ComponentAnimator {

		private   IAnimatedComponent[] _animatedComponents;
		protected IAnimatedComponent[] AnimatedComponents => _animatedComponents ?? (_animatedComponents = GetComponentsInChildren<IAnimatedComponent>());

		public override void SetDelta(float delta) {
			foreach (IAnimatedComponent component in AnimatedComponents) {
				component.SetDelta(delta);
			}
		}

	}
}
