using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//SYSTEM.SERIALIZABLE POTRZEBNE ZEBY BYLO WIDAC W INSPECTOR
[System.Serializable]
public class Boundry
{
	//ZMIENNE KONCA EKRANU
	public float xMin , xMax , zMin , zMax ;
}

public class PlayerControler : MonoBehaviour 
{
	//CZAS DZIALANIA BRONI
	public float TimeActivateWeapon = 20f ;

	
	//RODZAJ POCISKU
	public GameObject shotBolt ;
	public GameObject shotSphere ;

	//START POCISKU
	public Transform shotSpawnCenter ;
	public Transform shotSpawnLeft ;
	public Transform shotSpawnRight ;
		

	//CZAS MIEDZY STRZELANIEM
	public float timeRate ;

	//ANIMACJA EKSPLOZJI GRACZA
	public GameObject explosion_player ;

	//SZYBKOSC
	public float speed ;
	//PRZECHYLENIE
	public float tilt ;
	//OBIEKT Z KONCEM EKRANU
	public Boundry boundry ;


	//ZYCIE
	public int health = 3;
	//PASEK ZYCIA
	public GameObject healthBar ;
	//TEKST NA SKALI
	public Text TextOnHealth ;
	//OBECNA ILOSC ZYCIA
	private float healthCurrent ;
	//ILE ODEJMUJEMY
	private float removeHealth ;
	//AKTUALNE ZYCIE NA PASKU
	private int HealtText ;


	//CZAS NASTEPNEGO STRZALU
	private float timeFire ;
	//GameController DLA AKTUALIZACJI PUNKTOW
	private GameControler gameController ;

	//CZYM OBECNIE STRZELAMY
	private GameObject shot ;


	//CZAS DZIALANIA BRONI
	private float TimeTwoBolt ;
	private float TimeThreeBolt ;
	private float TimeSphere ;

	//Z CZEGO STRZELAMY
	private bool Sphere ;
	private bool TwoBolt ;
	private bool ThreeBolt ;
	
	//CZY ZMIENILISMY BRON
	private bool WeaponChange ;

	//CZAS ZMIANY STAROWANIA (JESLI JEST WIEKSZY OD Time.time TO ZNACZY ZE MAMY ZMIENIC STEROWANIE)
	private float ChangeControlTime ;
	private float TimeChangeControl = 10f ;


	void Start()
	{
		HealtText = health ;
		UpdateText() ;

		//USTAWIAMY ZYCIE ORAZ ILOSC ODEJMOWANEGO ZYCIA PO TRAFIENIU
		healthCurrent = 1.0f ;
		removeHealth = healthCurrent / health ;


		//USTAWIAMY NABOJE
		shot = shotBolt ;

		//CZYM OBECNIE STRZELAMY
		Sphere = false ;
		TwoBolt = false ;
		ThreeBolt = false ;


		//OBIEKT DO ODSZUKANIA GameController
		GameObject gameControlerComponent = GameObject.FindWithTag("GameController") ;
		//JESLI SZUKANIE SIE POWIODLO TO PRZYPISUJEMY COMPONENT DO OBIEKTU
		if( gameControlerComponent != null )
		{
			gameController = gameControlerComponent.GetComponent<GameControler>() ;
		}
		//JESLI NIE ZNALEZLISMY LUB PRZYPISANIE SIE NIE POWIODLO WYSWIETLAMY TEKST
		if(gameController == null)
		{
			Debug.Log( "gameControler can't find Component GameControler" ) ;
		}
	}

	
	void Update()
	{
		if(Input.GetButton ("Fire1") && timeFire < Time.time )
		{
			if( Sphere && TimeSphere > Time.time )
			{
				//JESLI OBECNA AMUNICJA TO NIE SPHERE
				if(shot != shotSphere )
					shot = shotSphere ;

				//TWORZYMY STRZAL I USTAWIAMY NOWY CZAS
				Instantiate(shot , shotSpawnCenter.position , shotSpawnCenter.rotation) ;
				timeFire = Time.time + timeRate ;
				//GRANIE AUDIO (TYLKO RAZ)
				audio.Play() ;
			}

			else if( TwoBolt && TimeTwoBolt > Time.time )
			{
				//JESLI OBECNA AMUNICJA TO NIE BOLT
				if(shot != shotBolt )
					shot = shotBolt ;

				//TWORZYMY STRZAL I USTAWIAMY NOWY CZAS
				Instantiate(shot , shotSpawnLeft.position , shotSpawnLeft.rotation) ;
				Instantiate(shot , shotSpawnRight.position , shotSpawnRight.rotation) ;
				timeFire = Time.time + timeRate ;
				//GRANIE AUDIO (TYLKO RAZ)
				audio.Play() ;
			}

			else if( ThreeBolt && TimeThreeBolt > Time.time )
			{
				//JESLI OBECNA AMUNICJA TO NIE BOLT
				if(shot != shotBolt )
					shot = shotBolt ;

				//TWORZYMY STRZAL I USTAWIAMY NOWY CZAS
				Instantiate(shot , shotSpawnCenter.position , shotSpawnCenter.rotation) ;
				Instantiate(shot , shotSpawnLeft.position , shotSpawnLeft.rotation) ;
				Instantiate(shot , shotSpawnRight.position , shotSpawnRight.rotation) ;
				timeFire = Time.time + timeRate ;
				//GRANIE AUDIO (TYLKO RAZ)
				audio.Play() ;
			}

			else 
			{
				//JESLI OBECNA AMUNICJA TO NIE BOLT
				if(shot != shotBolt )
					shot = shotBolt ;

				//TWORZYMY STRZAL I USTAWIAMY NOWY CZAS
				Instantiate(shot , shotSpawnCenter.position , shotSpawnCenter.rotation) ;
				timeFire = Time.time + timeRate ;
				//GRANIE AUDIO (TYLKO RAZ)
				audio.Play() ;
			}
		}
	}


	// Update is called once per frame
	void FixedUpdate () 
	{
		float moveHorizontal ;
		float moveVertical ;

		if( ChangeControlTime > Time.time )
		{
			moveHorizontal = -Input.GetAxis("Horizontal") ;
			moveVertical = -Input.GetAxis("Vertical") ;
		}
		else
		{
			moveHorizontal = Input.GetAxis("Horizontal") ;
			moveVertical = Input.GetAxis("Vertical") ;
		}

		//PORUSZANIE STATKIEM (BEZ NIESKONCZONEGO PORUSZANIA SIE // ODRAZU SIE ZATRZYMUJE)
		rigidbody.velocity = new Vector3(moveHorizontal , 0f , moveVertical) * speed ;
		//OBLICZAMY CZY STATEK JEST W EKRANIE
		rigidbody.position = new Vector3( Mathf.Clamp (rigidbody.position.x , boundry.xMin , boundry.xMax) ,
		                                 0f , Mathf.Clamp (rigidbody.position.z , boundry.zMin , boundry.zMax) ) ;
		//PRZECHYLENIE STATKU
		rigidbody.rotation = Quaternion.Euler( 0f , 0f , rigidbody.velocity.x * -tilt) ;

		//SPRAWDZAMY CZY ZMIENILISMY BRON PO CZYM USTAWIAMY CZAS ORAZ ZMIANA BRONI = FALSE
		if( WeaponChange )
		{
			WeaponChange = false ;

			TimeTwoBolt = TimeActivateWeapon + Time.time ;
			TimeThreeBolt = TimeActivateWeapon + Time.time ;
			TimeSphere = TimeActivateWeapon + Time.time ;
		}

		//ZMIENIAMY SZYBKOSC STRZELANIA
		if( Sphere && TimeSphere > Time.time )
			timeRate = 1.0f ;
		else 
			timeRate = 0.25f ;
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "EnemyBolt")
		{

			healthCurrent -= removeHealth ;
			HealtText-- ;
			UpdateText() ;
			ChangeHealthBar() ;

			if( healthCurrent <= 0 )
			{
				//KONIE GRY
				gameController.GameOverFunction() ;
				Instantiate( explosion_player , transform.position , transform.rotation ) ;

				Destroy(other.gameObject) ;
				Destroy(gameObject) ;
			}
			else
				Destroy(other.gameObject) ;
		}
		else if( other.tag == "Boss" || other.tag == "Enemy" )
		{
			healthCurrent -= removeHealth ;
			HealtText-- ;
			UpdateText() ;
			ChangeHealthBar() ;
			
			if( healthCurrent <= 0 )
			{
				//KONIE GRY
				gameController.GameOverFunction() ;
				Instantiate( explosion_player , transform.position , transform.rotation ) ;

				Destroy(gameObject) ;
			}
		}
	}

	public void AddHealth()
	{
		healthCurrent += removeHealth ;
		HealtText++ ;

		if( healthCurrent > 1 )
			healthCurrent = 1f ;

		if( HealtText > health ) 
			HealtText = health ;

		UpdateText() ;
		ChangeHealthBar() ;
	}

	public void WeaponSphere()
	{
		TwoBolt = false ;
		ThreeBolt = false ;
		Sphere = true ;
		
		WeaponChange = true ;
	}

	public void WeaponTwoBolt()
	{
		TwoBolt = true ;
		ThreeBolt = false ;
		Sphere = false ;
		
		WeaponChange = true ;
	}

	public void WeaponThreeBolt()
	{
		TwoBolt = false ;
		ThreeBolt = true ;
		Sphere = false ;
		
		WeaponChange = true ;
	}

	public void ChangeControl()
	{
		ChangeControlTime = TimeChangeControl + Time.time ;
	}


	private void ChangeHealthBar() 
	{
		healthBar.transform.localScale = new Vector3 ( healthCurrent , healthBar.transform.localScale.y , 
		                                              healthBar.transform.localScale.z ) ;
	}
	

	private void UpdateText()
	{
		TextOnHealth.text = "Health : " + HealtText + " / " + health ;
	}
}