using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treasure : MonoBehaviour
{
	void OnCollisionEnter(Collision other)
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		if (other.gameObject.tag == player.tag)
		{
			Destroy(gameObject);
			player.GetComponent<SpawnTreasure>().SpawnTreasureAtRandomPos();
		}
	}
}
