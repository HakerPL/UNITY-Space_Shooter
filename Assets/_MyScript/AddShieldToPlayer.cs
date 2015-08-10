using UnityEngine;
using System.Collections;

public class AddShieldToPlayer : MonoBehaviour 
{

	//OBIEKT TARCZA
	public GameObject shield ;

	// Update is called once per frame
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			//TWORZYMY OBIEKT TARCZY
			Instantiate(shield , other.transform.position , Quaternion.identity ) ;

			//Debug.Log ( "COLIDER WHIT PLAYER" ) ;

			Destroy( gameObject ) ;
		}
		else
			return ;
	}
}
