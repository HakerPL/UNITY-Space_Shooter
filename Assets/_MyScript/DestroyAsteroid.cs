using UnityEngine;
using System.Collections;

public class DestroyAsteroid : MonoBehaviour 
{
	//ANIMACJA EKSPLOZJI ASTEROIDY I GRACZA
	public GameObject explosion_asteroid ;
	public GameObject explosion_player ;

	//PUNKTY ZA ASTEROIDE
	public int point ;
	//GameController DLA AKTUALIZACJI PUNKTOW
	private GameControler gameController ;

	void Start()
	{
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
	

	void OnTriggerEnter(Collider other)
	{
		//JESLI TRIGGER REAGUJE NA BAUNDARY TO NIC NIE ROBIMY
		if(other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "EnemyBolt" ||
		   other.tag == "Shield" ||  other.tag == "Bonus" )
		{
			return;
		}

		//EKSPLOZJA ASTEROIDY
		Instantiate( explosion_asteroid , transform.position , transform.rotation ) ;
		if(other.tag == "Player")
		{
			//INSTANCJA WYBUCHU GRACZA
			//Instantiate(explosion_player , other.transform.position , other.transform.rotation) ;
			//KONIE GRY
			//gameController.GameOverFunction() ;

			Destroy(gameObject) ;

			return ;
		}

		if( other.tag == "PlayerBolt2" )
		{
			//DODAJEMY PUNKTY ZA ZNISZCZENIE ASTEROIDY
			gameController.AddPoint(point) ;
			Destroy(gameObject) ;
			return ;
		}

		//DODAJEMY PUNKTY ZA ZNISZCZENIE ASTEROIDY
		gameController.AddPoint(point) ;
		//JESLI TO COS INNEGO TO NISZCZYMY TEN INNY OBIEKT I ASTEROIDE
		Destroy(other.gameObject) ;
		Destroy(gameObject) ;
	}

}
