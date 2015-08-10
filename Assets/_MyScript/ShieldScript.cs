using UnityEngine;
using System.Collections;

public class ShieldScript : MonoBehaviour 
{
	//ANIMACJA ASTEROIDY
	public GameObject explosion_asteroid ;

	//CZAS DZIALANIA
	public float TimeLive ;

	//OBIEKT GRACZ
	private GameObject player ;


	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");

		if(player == null)
		{
			Debug.Log( "can't find Player" ) ;
		}

		transform.Rotate( new Vector3 ( 90 , transform.rotation.y , transform.rotation.z ) ) ;

		Destroy ( gameObject , TimeLive ) ;
	}


	void FixedUpdate()
	{
		transform.position = player.transform.position ;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if( other.tag == "Enemy" || other.tag == "EnemyBolt" )
		{
			Instantiate(explosion_asteroid , other.transform.position , other.transform.rotation) ;
			Destroy( other.gameObject ) ;
		}
	}
}
