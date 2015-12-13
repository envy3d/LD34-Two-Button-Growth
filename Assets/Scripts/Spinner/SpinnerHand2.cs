using UnityEngine;
using System.Collections;

public class SpinnerHand2 : MonoBehaviour {

	public float spinBaseSpeed = 60;
	public float spinSpeedModifier = 1;
	private bool deactivated = false;
	public float deactivationDuration = 1;

	void Update() {

		if (deactivated == false){
			float spinSpeed = spinBaseSpeed * spinSpeedModifier;

			transform.rotation = Quaternion.AngleAxis(Time.time * spinSpeed, new Vector3(0, 0, 1));

			float angle = transform.rotation.eulerAngles.z;

			//print(transform.rotation.eulerAngles.z.ToString("#.00"));

	/*		if (angle >= 0 && angle < 90) print ("1");

			if (angle >= 90 && angle < 180) print ("2");

			if (angle >= 180 && angle < 270) print ("3");

			if (angle >= 270) print ("4");*/

			if (Input.GetKey ("f")) {
				if (deactivated == false) {
					deactivated = true;
					StartCoroutine(deactivationTimer ());
				}
			}

		}

		Debug.Log (deactivated);

	}

	private IEnumerator deactivationTimer(){
		yield return new WaitForSeconds (deactivationDuration);
		deactivated = false;
	}
}


