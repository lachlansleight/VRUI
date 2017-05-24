using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRUI {
	/// <summary>
	/// VRUI Canvas used for managing LookTargets on a world-space canvas
	/// </summary>
	public class VRUI_Canvas : MonoBehaviour {

		[Header("Read readme for position and scale")]

		[Tooltip("Transform used to perform raycasts. This should generally be the main camera")]
		public Transform Raycaster;
		[Tooltip("Optional reference to a VRUI_Reticle object")]
		public VRUI_Reticle Reticle;
		[Tooltip("Optional reference to a collider on which the reticle should be visible. Note - if you have a Reticle you need this!")]
		public Collider CanvasCollider;

		VRUI_LookButton[] LookButtons;

		void Awake() {
			UpdateTargets();
		}

		void Update () {
			//cast ray onto look buttons
			Ray ray = new Ray(Raycaster.position, Raycaster.forward);
			for(int i = 0; i < LookButtons.Length; i++) {
				//but only if they're active and still around
				if(LookButtons[i] == null) continue;
				if(!LookButtons[i].gameObject.activeInHierarchy) continue;
				LookButtons[i].Raycast(ray);
			}

			//display and move reticle if assigned
			if(Reticle != null && CanvasCollider != null) {
				RaycastHit hit;
				if(CanvasCollider.Raycast(new Ray(Raycaster.position, Raycaster.forward), out hit, float.MaxValue)) {
					Reticle.SetReticleVisible(true);
					Reticle.SetPosition(hit.point, hit.normal);
				} else {
					Reticle.SetReticleVisible(false);
				}
			}
		}

		/// <summary>
		/// Updates look targets - called on Awake(), but can be called externally if, for example, we instantiate or destroy some look targets
		/// </summary>
		public void UpdateTargets() {
			List<VRUI_LookButton> buttonList = new List<VRUI_LookButton>();
			ScanTransformForTargets(ref buttonList, transform);
			LookButtons = buttonList.ToArray();
		}

		//recursively look through all child objects
		void ScanTransformForTargets(ref List<VRUI_LookButton> buttonList, Transform currentTransform) {
			if(currentTransform.GetComponent<VRUI_LookButton>() != null) buttonList.Add(currentTransform.GetComponent<VRUI_LookButton>());
			for(int i = 0; i < currentTransform.childCount; i++) {
				ScanTransformForTargets(ref buttonList, currentTransform.GetChild(i));
			}
		}
	}
}