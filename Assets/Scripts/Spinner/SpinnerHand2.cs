using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SpinnerHand2 : MonoBehaviour {

    public string buttonName = "Fire1";
	public float spinBaseSpeed = 60;
	public float spinSpeedModifier = 1;
	private bool deactivated = false;
	public float deactivationDuration = 1;

    public UnityEvent PulseEvent;

	void Update() {

		if (deactivated == false) {

			float spinSpeed = spinBaseSpeed * spinSpeedModifier;

			transform.rotation = Quaternion.AngleAxis(Time.time * spinSpeed, new Vector3(0, 0, 1));

			float angle = transform.rotation.eulerAngles.z;

            //print(transform.rotation.eulerAngles.z.ToString("#.00"));

			if (Input.GetButtonDown(buttonName)) {
				if (deactivated == false) {
					deactivated = true;

                    if (angle >= 0 && angle < 90)
                    {
                        PulseEvent.Invoke();
                    }
                    else if (angle >= 90 && angle < 180)
                    {
                        PulseEvent.Invoke();
                    }
                    else if (angle >= 180 && angle < 270)
                    {
                        PulseEvent.Invoke();
                    }
                    else if (angle >= 270)
                    {
                        PulseEvent.Invoke();
                    }

                    StartCoroutine(deactivationTimer ());
				}
			}

		}

		//Debug.Log (deactivated);

	}

	private IEnumerator deactivationTimer() {
		yield return new WaitForSeconds (deactivationDuration);
		deactivated = false;
	}
}


