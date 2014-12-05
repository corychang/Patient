using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Utilities {

	public static void Assert(bool condition, string messageIfFalse) {
		if (!condition) {
			Debug.LogError(messageIfFalse);
		}
	}
	
//	public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
//	{
//		source.ThrowIfNull("source");
//		action.ThrowIfNull("action");
//		foreach (T element in source)
//		{
//			action(element);
//		}
//	}
}
