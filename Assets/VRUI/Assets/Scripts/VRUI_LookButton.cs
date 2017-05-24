using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityEngine {
	namespace Events {
		[System.Serializable]
		public class UnityFloatEvent : UnityEngine.Events.UnityEvent<float> {}
	}
}

namespace VRUI {
	/// <summary>
	/// Look Button, handy when there are no other input devices. Also known as a Fuse Button
	/// </summary>
	[RequireComponent(typeof(Collider))]
	public class VRUI_LookButton : MonoBehaviour {

		[Header("Timings")]
		[Tooltip("How long it takes to trigger this look button in seconds")] [Range(0f, 5f)] 
		public float TimeToTrigger = 2f;
		[Tooltip("How long it takes for this look button to reset when not looked at in seconds")] [Range(0f, 5f)] 
		public float TimeToCooldown = 0f;

		[Header("Events")]
		[Tooltip("Called once when button is looked at for TimeToTrigger seconds")] 
		public UnityEvent OnTrigger;
		[Space(20)]
		[Tooltip("Called every frame that the button's LookCounter changes. Useful for animating look progress")] 
		public UnityFloatEvent OnLookValueChanged;
		[Space(20)]
		[Tooltip("Called once when the button is hovered over")] 
		public UnityEvent OnHoverDown;
		[Tooltip("Called once when the button is no longer being hovered over")] 
		public UnityEvent OnHoverUp;

		//current look value, this is output by OnLookValueChanged each frame it needs to be so shouldn't need to query it externally
		private float LookValue = 0f;

		//whether this was hit by a raycast this frame
		private bool HitThisFrame = false;

		//the collider attached to this object
		private Collider myCollider;
		private Collider MyCollider {
			get {
				if(myCollider == null) myCollider = GetComponent<Collider>();
				return myCollider;
			} set {
				myCollider = value;
			}
		}

		//whether we are currently being hovered over by the cursor
		private bool Hovering = false;

		// Use this for initialization
		void Start () {
			
		}

		public void Raycast(Ray ray) {
			RaycastHit hit;
			//If we get hit
			if(MyCollider.Raycast(ray, out hit, float.MaxValue)) {

				//calculate a new look value
				HitThisFrame = true;
				float LookValueBefore = LookValue;
				LookValue += Time.deltaTime / TimeToTrigger;
				LookValue = Mathf.Clamp01(LookValue);

				//if it changed
				if(LookValue != LookValueBefore) {

					//check if we're finished, trigger if so
					if(LookValue == 1f) {
						if(OnTrigger != null) {
							OnTrigger.Invoke();
						}
					}

					//check if we should be hovering
					if(!Hovering) {
						if(OnHoverDown != null) {
							OnHoverDown.Invoke();
						}
						Hovering = true;
					}

					//finally, output the new look value
					if(OnLookValueChanged != null) {
						OnLookValueChanged.Invoke(LookValue);
					}

				}
			}
		}
		
		//We do this on lateupdate so we can be sure that all raycasting has been completed
		void LateUpdate () {
			//if we were hit by any raycasters
			if(!HitThisFrame) {

				//calculate a new look value, or just set it to zero if TimeToCooldown is zero
				float LookValueBefore = LookValue;
				if(TimeToCooldown <= 0f) {
					LookValue = 0f;
				} else {
					LookValue -= Time.deltaTime / TimeToCooldown;
					LookValue = Mathf.Clamp01(LookValue);
				}

				//if it changed
				if(LookValue != LookValueBefore) {

					//output the new look value
					if(OnLookValueChanged != null) {
						OnLookValueChanged.Invoke(LookValue);
					}
				}

				//if we're hovering and we weren't hit, stop hovering
				if(Hovering) {
					if(OnHoverUp != null) {
						OnHoverUp.Invoke();
					}
				}
			}

			HitThisFrame = false;
		}
	}

}