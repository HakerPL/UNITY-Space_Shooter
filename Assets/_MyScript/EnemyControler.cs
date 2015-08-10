﻿using UnityEngine;
using System.Collections;

public class EnemyControler : MonoBehaviour 
{
	//ANIMACJA EKSPLOZJI ASTEROIDY I GRACZA
	public GameObject explosion_enemy ;
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
		//Debug.Log( other.name ) ;
		//JESLI TRIGGER REAGUJE NA BAUNDARY TO NIC NIE ROBIMY
		if(other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "EnemyBolt" || other.tag == "Bonus")
		{
			return;
		}
		if(other.tag == "Player")
		{
			//WYBUCH ENEMY
			Instantiate( explosion_enemy , transform.position , transform.rotation ) ;
			//INSTANCJA WYBUCHU GRACZA
			//Instantiate( explosion_player , other.transform.position , other.transform.rotation) ;

			//EKSPLOZJA ENEMY
			//Instantiate( explosion_enemy , transform.position , transform.rotation ) ;
			
			//DODAJEMY PUNKTY ZA ZNISZCZENIE ASTEROIDY
			gameController.AddPoint(point) ;

			Destroy(gameObject) ;

			//KONIE GRY
			//gameController.GameOverFunction() ;
			return ;
		}

		//EKSPLOZJA ENEMY
		Instantiate( explosion_enemy , transform.position , transform.rotation ) ;

		//DODAJEMY PUNKTY ZA ZNISZCZENIE ASTEROIDY
		gameController.AddPoint(point) ;

		if(other.tag == "PlayerBolt2")
			Destroy(gameObject) ;
		else
		{
			//JESLI TO COS INNEGO TO NISZCZYMY TEN INNY OBIEKT I ASTEROIDE
			Destroy(other.gameObject) ;
			Destroy(gameObject) ;
		}
	}
}
