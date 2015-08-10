using UnityEngine;
using System.Collections;

public class RandomRotate : MonoBehaviour 
{
	//ZMIENNA DO ROTACJI
	public float rotate ;
	
	// Use this for initialization
	void Start () 
	{
		//angularVelocity SLUZY DO SZYBKOSCI ROTACJI
		rigidbody.angularVelocity = Random.insideUnitSphere * rotate ;
		//insideUnitSphere LOSOWA ZMIENNA ( 0 - 1 ) W SPHERE
	}
	
}
