using UnityEngine;
using System.Collections;

public class Utilities {

	public static void Assert(bool condition, string messageIfFalse) {
		if (!condition) {
			Debug.LogError(messageIfFalse);
		}
	}
}
