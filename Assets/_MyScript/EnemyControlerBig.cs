using UnityEngine;
using System.Collections;

public class EnemyControlerBig : MonoBehaviour 
{
	//BONUS DO DROPA
	public GameObject[] BonusItem ;

	//ANIMACJA EKSPLOZJI ASTEROIDY I GRACZA
	public GameObject explosion_enemy ;
	public GameObject explosion_player ;

	//ZYCIE
	public int health ;
	//PASEK ZYCIA
	public GameObject healthBar ;
	
	//PUNKTY ZA ASTEROIDE
	public int point ;

	//OBECNA ILOSC ZYCIA
	private float healthCurrent ;
	//ILE ODEJMUJEMY
	private float removeHealth ;

	//GameController DLA AKTUALIZACJI PUNKTOW
	private GameControler gameController ;

	//CZY WYDROPIC ITEMA
	private bool Drop ;
	//CO WYDROPIC
	private GameObject DropItem ;

	
	void Start()
	{
		//USTAWIAMY ZYCIE ORAZ ILOSC ODEJMOWANEGO ZYCIA PO TRAFIENIU
		healthCurrent = 1.0f ;
		removeHealth = healthCurrent / health ;

		//Debug.Log ( "healthCurrent " + healthCurrent + "  removeHealth " + removeHealth ) ;
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


		if( (Random.Range(0 , 100)) % 3 == 0 )
		{
			Drop = true ;
			//LOSUJEMY ITEMA
			int numberBonus = Random.Range( 0 , BonusItem.Length ) ;
			//USTAWIAMY WYLOSOWANEGO ITEMA
			DropItem = BonusItem[numberBonus] ;
		}
		else
			Drop = false ;

		//Debug.Log( "wielkosc teblicy " + BonusItem.Length ) ;
	}
	
	
	void OnTriggerEnter(Collider other)
	{
		//ODEJMUJEMY ZYCIE
		if( other.tag == "PlayerBolt" || other.tag == "PlayerBolt2" )
		{
			healthCurrent -= removeHealth ;
			ChangeHealthBar() ;
		}
		//Debug.Log( other.name ) ;
		//JESLI TRIGGER REAGUJE NA BAUNDARY TO NIC NIE ROBIMY
		if(other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "EnemyBolt" || other.tag == "Bonus")
		{
			return;
		}
		if(other.tag == "Player")
		{
			healthCurrent -= removeHealth ;
			ChangeHealthBar() ;
			//INSTANCJA WYBUCHU GRACZA
			//Instantiate( explosion_player , other.transform.position , other.transform.rotation) ;
			
			//KONIE GRY
			//gameController.GameOverFunction() ;
			return ;
		}
		//JESLI GRACZ TRAFIL W OBIEKT
		if( healthCurrent <= 0 )
		{
			//EKSPLOZJA ENEMY
			Instantiate( explosion_enemy , transform.position , transform.rotation ) ;
			//DODAJEMY PUNKTY ZA ZNISZCZENIE STATKU
			gameController.AddPoint(point) ;

			//DROP ITEMA O ILE MA BYC
			if( Drop )
				Instantiate( DropItem , transform.position ,  transform.rotation ) ;

			//NISZCZYMY STATEK
			Destroy(gameObject) ;
		}

		//JESLI TO SPHERE TO NIE NISZCZYMY JEJ
		if( other.tag == "PlayerBolt2" )
			return ;

		//JESLI TO COS INNEGO TO NISZCZYMY TEN INNY OBIEKT I ASTEROIDE
		Destroy(other.gameObject) ;
	}

	private void ChangeHealthBar() 
	{
		healthBar.transform.localScale = new Vector3 ( healthCurrent , healthBar.transform.localScale.y , 
		                                               healthBar.transform.localScale.z ) ;
	}
}
