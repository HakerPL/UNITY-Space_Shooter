using UnityEngine;
using System.Collections;

public class MoveBackground : MonoBehaviour 
{
	//SZYBKOSC PORUSZANIA
	public float scrollSpeed;
	//??????????????
	public float tileSizeZ;

	//MIEJSCE OD KTOREGO ZACZYNAMY
	private Vector3 startPosition;
	
	void Start ()
	{
		startPosition = transform.position;
	}
	
	void Update ()
	{
		//Repeat  DZIALA JAK MODULO (5 , 2.5 = 0) NIGDY NIE MA UJEMNEGO WYNIKU
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition + Vector3.forward * newPosition;
	}
}
