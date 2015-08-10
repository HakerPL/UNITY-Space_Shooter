using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour 
{
	//SZYBKOSC
	public float speed ;
	// Use this for initialization
	void Start () 
	{
		//POCISK LECI
		rigidbody.velocity = transform.forward * speed ;
	}

}
