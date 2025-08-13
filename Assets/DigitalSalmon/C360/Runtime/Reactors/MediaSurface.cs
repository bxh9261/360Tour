using UnityEngine;

namespace DigitalSalmon.C360 {
	[AddComponentMenu("Complete 360 Tour/Core/Media Surface")]
	public class MediaSurface : BaseBehaviour {

		private static readonly int TEXTURE_PROPERTY    = Shader.PropertyToID("_MainTex");
		private static readonly int PROJECTION_PROPERTY = Shader.PropertyToID("_Projection");
		private static readonly int YFLIP_PROPERTY      = Shader.PropertyToID("_YFlip");
		private static readonly int STEREO_PROPERTY     = Shader.PropertyToID("_Stereoscopic");

		private Renderer surfaceRenderer;

		protected void Awake() { surfaceRenderer = GetComponentInChildren<Renderer>(); }

		/// <summary>
		/// Sets which texture is drawn by this surface. Used by Images and Videos.
		/// </summary>
		public void SetTexture(Texture texture) { surfaceRenderer.material.SetTexture(TEXTURE_PROPERTY, texture); }

		/// <summary>
		/// Sets whether this surface is displaying stereoscopic or monoscopic media.
		/// </summary>
		public void SetStereoscopic(bool stereoscopic) { surfaceRenderer.material.SetFloat(STEREO_PROPERTY, stereoscopic ? 1 : 0); }

		/// <summary>
		/// Sets the surface projection.
		/// </summary>
		public void SetProjection(MediaProjection projection) { surfaceRenderer.material.SetFloat(PROJECTION_PROPERTY, (int)projection); }

		/// <summary>
		/// Sets whether the surface is y-flipped (Some video decoders read top->bottom, others bottom -> top).
		/// </summary>
		public void SetYFlip(bool flipped) { surfaceRenderer.material.SetFloat(YFLIP_PROPERTY, flipped ? 1 : 0); }

	}
}