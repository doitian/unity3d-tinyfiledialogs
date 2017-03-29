using UnityEngine;
using System.Collections;

public class EventLogger : MonoBehaviour
{
	public void LogMessageBoxEvent(bool ok) {
		Debug.Log("Clicked " + (ok ? "OK/Yes" : "Cancel/No") + " button.");
	}

	public void LogInputBoxEvent(string input) {
		Debug.Log("Entered " + input + ".");
	}
}

