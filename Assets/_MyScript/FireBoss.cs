using UnityEngine;
using System.Collections;

public class FireBoss : MonoBehaviour 
{
	//CZYM STRZELAMY
	public GameObject shotLeftRight ;
	public GameObject shotCenter ;

	//START POCISKU
	public Transform LeftGun ;
	public Transform RightGun ;
	public Transform CenterGun ;

	//CZAS DO ROZPOCZECIA OSTRZALU
	public float waitTime ;

	//CZAS MIEDZY STRZELANIEM
	public float timeRateBolt ;
	public float timeRateCenter ;

	//CZAS NASTEPNEGO STRZALU
	private float timeFireBolt ;
	private float timeFireCenter ;


	private EnemyControlerBoss CanDamage ;

	void Start()
	{
		//POBIERAMY SKRYPT DLA OBECNEGO BOSA
		CanDamage = this.gameObject.GetComponent("EnemyControlerBoss") as EnemyControlerBoss ;


		//USTAWIAMY OPUZNIENIE
		waitTime = waitTime + Time.time ;
	}
	
	void Update()
	{
		if( waitTime - 0.5 < Time.time )
		{
			//POZWALAMY NA DAMAGE CHWILE PRZED 1 STRZALEM BOSA
			CanDamage.StartDamage = true ;
		}
		//Debug.Log ( waitTime + " waitTime " ) ;
		//Debug.Log ( Time.time + " Time.time " ) ;
		if( waitTime < Time.time )
		{
			if(timeFireBolt < Time.time )
			{
				//TWORZYMY STRZAL I USTAWIAMY NOWY CZAS
				Instantiate(shotLeftRight , LeftGun.position , LeftGun.rotation ) ;
				Instantiate(shotLeftRight , RightGun.position , RightGun.rotation ) ;

				timeFireBolt = Time.time + timeRateBolt ;
				//GRANIE AUDIO (TYLKO RAZ)
				audio.Play() ;
			}
			if(timeFireCenter < Time.time )
			{
				//TWORZYMY STRZAL I USTAWIAMY NOWY CZAS
				Instantiate(shotCenter , CenterGun.position , CenterGun.rotation ) ;
				
				timeFireCenter = Time.time + timeRateCenter ;
				//GRANIE AUDIO (TYLKO RAZ)
				audio.Play() ;
			}
		}
	}//InverseTransformDirection( 0 , 180 , 0 ) 


	void FixedUpdate()
	{
		//USTAWIAMY POZYCJE STRZELANIA ZAWSZE NA Y = 0
		CenterGun.position = new Vector3( CenterGun.position.x , 0f , CenterGun.position.z )  ;
		LeftGun.position = new Vector3( LeftGun.position.x , 0f , LeftGun.position.z )  ;
		RightGun.position = new Vector3( RightGun.position.x , 0f , RightGun.position.z )  ;
	}
}