using UnityEngine;
using System.Collections;

public class LayerSorter : MonoBehaviour
{
	void Start ()
	{
		// auto sort all renderers as they were found in the hierarchy
		SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer> ();

		int order = 0;
		foreach (SpriteRenderer r in renderers)
		{
			r.sortingOrder = order++;
		}
	}
}
