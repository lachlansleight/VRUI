using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRUI {
	/// <summary>
	/// Reticle for use with a VRUI_Canvas object
	/// </summary>
	public class VRUI_Reticle : MonoBehaviour {

		[Tooltip("Reticle size in dmm")] [Range(0f, 200f)] 
		public float ReticleSize = 25f;
		[Tooltip("How long it takes for the reticle to lerp up or down in size")] [Range(0.01f, 2f)] 
		public float ReticleSizeLerpTime = 1f;

		//calculated from ReticleSizeLerpTime using my magic formula!
		private float ReticleSizeLerpFactor;

		//these are used to actually perform the size lerp
		private float CurrentTargetReticleSize = 0f;
		private float CurrentReticleSize = 0f;

		/// <summary>
		/// Sets the reticle position from a raycast
		/// </summary>
		/// <param name="Position">New position</param>
		/// <param name="Normal">New normal used for orienting with a canvas</param>
		public void SetPosition(Vector3 Position, Vector3 Normal) {
			transform.position = Position;
			transform.rotation = Quaternion.LookRotation(-Normal);
		}


		void Awake () {
			//set scale to zero
			transform.localScale = Vector3.zero;

			//calculate size lerp factor - magic formula calculated using *****mathematics*****
			ReticleSizeLerpFactor = Mathf.Pow((0.0206f * (60f / 90f)) / ReticleSizeLerpTime, 1f / 1.055f);
		}

		/// <summary>
		/// Sets target size for reticle
		/// </summary>
		/// <param name="visible">If set to <c>true</c> visible.</param>
		public void SetReticleVisible(bool visible) {
			CurrentTargetReticleSize = visible ? ReticleSize : 0f;
		}

		void Update () {
			//just update reticle size - position is calculated in SetPosition.
			//if we wanted to do damping, we'd do it here
			CurrentReticleSize = Mathf.Lerp(CurrentReticleSize, CurrentTargetReticleSize, ReticleSizeLerpFactor);
			transform.localScale = Vector3.one * CurrentReticleSize;
		}
	}
}