using UnityEngine;
using System.Collections;

public class EnemyFire : MonoBehaviour 
{
	//CZYM STRZELAMY
	public GameObject shot ;
	//START POCISKU
	public Transform LeftGun ;
	//START POCISKU
	public Transform RightGun ;
	//START POCISKU
	public Transform CenterGun ;

	//STRZELAMY TYLKO ZE SRODKOWEJ BRONI
	public bool ShotCenterGun ;

	//CZAS DO ROZPOCZECIA OSTRZALU
	public float waitTime ;
	//CZAS MIEDZY STRZELANIEM
	public float timeRate ;

	//CZAS NASTEPNEGO STRZALU
	private float timeFire ;


	void Start()
	{
		//USTAWIAMY OPUZNIENIE
		waitTime = waitTime + Time.time ;
	}


	void Update()
	{
		if( waitTime < Time.time )
		{
			if(timeFire < Time.time )
			{
				if( !ShotCenterGun )
				{
					//TWORZYMY STRZAL I USTAWIAMY NOWY CZAS
					Instantiate(shot , LeftGun.position , LeftGun.rotation ) ;
					Instantiate(shot , RightGun.position , RightGun.rotation ) ;
				}
				else
				{
					Instantiate(shot , CenterGun.position , CenterGun.rotation ) ;
				}
				timeFire = Time.time + timeRate ;
				//GRANIE AUDIO (TYLKO RAZ)
				audio.Play() ;
			}
		}
	}//InverseTransformDirection( 0 , 180 , 0 ) 

	void FixedUpdate()
	{
		//USTAWIAMY POZYCJE STRZELANIA ZAWSZE NA Y = 0
		LeftGun.position = new Vector3( LeftGun.position.x , 0f , LeftGun.position.z )  ;
		RightGun.position = new Vector3( RightGun.position.x , 0f , RightGun.position.z )  ;
	}
}
