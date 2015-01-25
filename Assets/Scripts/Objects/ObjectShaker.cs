using UnityEngine;
using System.Collections;

public class ObjectShaker : MonoBehaviour
{	
	private Vector3 p;
	
	void Update()
	{
		float rand = (Player.currentMadness - 2);
		rand = (rand < 0) ? 0 : rand;
		rand *= 0.01f;
		transform.position = p + new Vector3 (Random.Range(-rand,rand), Random.Range(-rand,rand), 0f);
	}
	
	protected void Awake()
	{
		p = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
}
