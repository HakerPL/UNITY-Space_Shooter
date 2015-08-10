using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameControler : MonoBehaviour 
{
	//LICZBA ASTEROID
	public int hazardCount ;
	//CZAS MIEDZY POSZCZEGOLNYMI OBIEKTAMI
	public float spawnWait ;
	//CZAS MIEDZY FALAMI
	public float waveWait ;


	//OBIEKT ASTEROIDY1
	public GameObject hazard1 ;
	//OBIEKT ASTEROIDY2
	public GameObject hazard2 ;
	//OBIEKT ASTEROIDY3
	public GameObject hazard3;
	//OBIEKT STATEK SMALL1
	public GameObject ShipSmall_1;
	//OBIEKT STATEK SMALL2
	public GameObject ShipSmall_2;
	//OBIEKT STATEK BIG1
	public GameObject ShipBig_1;
	//OBIEKT STATEK BIG2
	public GameObject ShipBig_2;
	//OBIEKT BOSS
	public GameObject Boss ;
	//DANE DLA POZYCJI POJAWIENIA SIE ASTEROIDY
	public Vector3 spawnValue ;


	//ILOSC STATKOW MALYCH
	public Vector2 numberShipSmall ;
	//ILOSC STATKOW DUZYCH
	public Vector2 numberShipBig ;


	//TEKST PUNKTOW
	public Text PointText ;
	//TEKST GAMEOVER
	public GameObject GameOverText ;
	//PRZYCISK START BUTTON
	public GameObject StartButton ;
	//PRZYCISK CONTROL BUTTON
	public GameObject ControlButton ;
	//PRZYCISK CONTROL SCREEN
	public GameObject ControlScreenObject ;
	//PRZYCISK RESTART BUTTON
	public GameObject RestartButton ;



	//CALKOWITA ILOSC PUNKTOW
	private int point ;
	//KONIEC GRY
	private bool GameOver ;
	//START GRY
	private bool StartGame ;
	//LICZNIK FAL
	private int wave ;


	//LICZNIK STATKOW MALYCH
	private int countShipSmall ;
	//CZY WYSWIETLIC MALE STATKI
	private int shipSpawnSmall ;
	//ILE BEDZIE STATKOW MALYCH
	private int numberShipSmallSpawn ;


	//LICZNIK STATKOW DUZYCH
	private int countShipBig ;
	//CZY WYSWIETLIC DUZE STATKI
	private int shipSpawnBig ;
	//ILE BEDZIE STATKOW DUZYCH
	private int numberShipBigSpawn ;


	//STAN FALL (JAKA OBECNIE JEST PUSZCZONA)
	private bool S_Boss ;
	private bool S_Normal ;
	private bool End_Wave ;


	// Use this for initialization
	void Start () 
	{
		//ZMIENNE DLA FALL
		S_Boss = false ;
		//S_Bonus = false ;
		S_Normal = true ;
		End_Wave = false ;

		wave = 1 ;
		//KONIEC GRY
		GameOver = false ;
		//POCZATKOWA WARTOSC GRY
		StartGame = false ;
		//POCZATKOWA WARTOSC
		point = 0 ;
		//AKTUALIZACJA TEKSTU
		UpdateText() ;
		//POWTARZAJACA SIE FUNKCJA (COS ALA PETLA)
		StartCoroutine(SpawnWaves ()) ;
	}


	IEnumerator SpawnWaves()
	{
		//WYCHODZI Z FUNKCJA NA OKRESLONY CZAS 
		//yield return new WaitForSeconds(startWait) ;
		//SPAWN W NIESKONCZONOSC
		while(true)
		{
			if(StartGame)
			{
				//Debug.Log( "Wave " + wave ) ;
				//BOSS LVL
				if( S_Boss )
				{
					StartCoroutine( "SpawnBoss" ) ;
				}
				//NORMALNY LVL
				else if( S_Normal )
				{
					StartCoroutine( "SpawnNormalWaves" ) ;
				}
			}
			yield return new WaitForSeconds (waveWait);

			if( End_Wave )
			{
				End_Wave = false ;

				if( wave % 10 == 0 )
					S_Boss = true ;
				else 
					S_Normal = true ;
			}

			//ZAKONCZENIE GRY
			if(GameOver)
			{
				RestartButton.SetActive(true) ;
				break ;
			}
		}
	}


	void UpdateText()
	{
		PointText.text = "Score: " + point ;
	}


	public void AddPoint(int addPoint)
	{
		point += addPoint ;
		UpdateText() ;
	}


	public void Start_Game()
	{
		//FUNKCJA DLA PRZYCISKU (START GRY)
		StartGame = true ;
		//WYLACZAMY PRZYCISK
		StartButton.SetActive(false) ;
		ControlButton.SetActive(false) ;
	}


	public void GameOverFunction()
	{
		//AKTYWACJA NAPISU
		GameOverText.SetActive(true) ;
		GameOver = true ;
	}


	public void RestartFunction()
	{
		//URUCHAMIAMY ODNOWA LEVEL PODANY JAKO PARAMETR
		Application.LoadLevel (Application.loadedLevel);
	}


	public void ControlScreen()
	{
		//WYLACZAMY PRZYCISKi
		StartButton.SetActive(false) ;
		ControlButton.SetActive(false) ;
		//WLACZAMY EKRAN STEROWANIA
		ControlScreenObject.SetActive(true) ;
	}


	public void BackToMenu()
	{
		//WLACZAMY PRZYCISKi
		StartButton.SetActive(true) ;
		ControlButton.SetActive(true) ;
		//WZLACYAMZ EKRAN STEROWANIA
		ControlScreenObject.SetActive(false) ;
	}


	private void SpawnSmallShip()
	{
		if( countShipSmall < numberShipSmallSpawn )
		{
			countShipSmall++ ;
			//LOSUJEMY KTORA ASTEROIDA BEDZIE LECIEC
			int randomShip = Random.Range( 0 , 2 ) ;

			//OBLICZANIE POZYCJI
			Vector3 spawnPosition = new Vector3( Random.Range( -spawnValue.x , spawnValue.x ) , 
			                                    spawnValue.y , spawnValue.z ) ;
			//ZERO ROTACJI ( identity )
			Quaternion spawnRotation = Quaternion.identity ;
			
			switch(randomShip)
			{
				case 0:
					
					//Debug.Log("ShipSmall_1") ;
					//POJAWIENIE SHIP SMALL
					Instantiate( ShipSmall_1 , spawnPosition , spawnRotation ) ;
					break ;
					
				case 1:
					
					//Debug.Log("ShipSmall_2") ;
					//POJAWIENIE SHIP2 SMALL
					Instantiate( ShipSmall_2 , spawnPosition , spawnRotation ) ;
					break ;
					
				default:
					
					Debug.Log("NIEWLASCIWA LICZBA SHIP MALYCH") ;
					break ;
			}
		}
	}


	private void SpawnBigShip()
	{
		if( countShipBig < numberShipBigSpawn )
		{
			countShipBig++ ;
			//LOSUJEMY KTORA ASTEROIDA BEDZIE LECIEC
			int randomShip = Random.Range( 0 , 2 ) ;

			//OBLICZANIE POZYCJI
			Vector3 spawnPosition = new Vector3( Random.Range( -spawnValue.x , spawnValue.x ) , 
			                                    spawnValue.y , spawnValue.z ) ;
			//ZERO ROTACJI ( identity )
			Quaternion spawnRotation = Quaternion.identity ;
			
			switch(randomShip)
			{
				case 0:
					
					//Debug.Log("ShipBig_1") ;
					//POJAWIENIE SHIP BIG
					Instantiate( ShipBig_1 , spawnPosition , spawnRotation ) ;
					break ;
					
				case 1:
					
					//Debug.Log("ShipBig_2") ;
					//POJAWIENIE SHIP2 BIG
					Instantiate( ShipBig_2 , spawnPosition , spawnRotation ) ;
					break ;
					
				default:
					
					Debug.Log("NIEWLASCIWA LICZBA SHIP DUZYCH") ;
					break ;
			}
		}
	}


	private void SpawnAsteroid()
	{
		//LOSUJEMY KTORA ASTEROIDA BEDZIE LECIEC
		int randomAsteroid = Random.Range( 0 , 3 ) ;

		//OBLICZANIE POZYCJI
		Vector3 spawnPosition = new Vector3( Random.Range( -spawnValue.x , spawnValue.x ) , 
		                                    spawnValue.y , spawnValue.z ) ;
		//ZERO ROTACJI ( identity )
		Quaternion spawnRotation = Quaternion.identity ;
		
		switch(randomAsteroid)
		{
			case 0:
				
				//Debug.Log("ASTEROIDY1") ;
				//POJAWIENIE ASTEROIDY1
				Instantiate( hazard1 , spawnPosition , spawnRotation ) ;
				break ;
				
			case 1:
				
				//Debug.Log("ASTEROIDY2") ;
				//POJAWIENIE ASTEROIDY2
				Instantiate( hazard2 , spawnPosition , spawnRotation ) ;
				break ;
				
			case 2:
				
				//Debug.Log("ASTEROIDY3") ;
				//POJAWIENIE ASTEROIDY3
				Instantiate( hazard3 , spawnPosition , spawnRotation ) ;
				break ;
				
			default:
				
				Debug.Log("NIEWLASCIWA LICZBA ASTEROIDY") ;
				break ;
		}
	}

	IEnumerator SpawnNormalWaves()
	{
		//NIE ODPALAMY 2 RAZ TEJ FUNKCJI PUKI SIE NIE SKONCZY
		S_Normal = false ;

		//Debug.Log(" START WAVE " ) ;

		//LICZNIK STATKOW MALYCH I DUZYCH
		countShipSmall = 0 ;
		countShipBig = 0 ;
		
		//ILE STATKOW SIE POJAWI MALYCH
		numberShipSmallSpawn = (int)(Random.Range( 0 , 30 ) % numberShipSmall.y) + 1 ;
		//(int)Random.Range( numberShipSmall.x , numberShipSmall.y + 1 ) ;
		//CZY STWORZYC STATKI MALE
		shipSpawnSmall = Random.Range( 0 , 3 ) ;
		
		//ILE STATKOW SIE POJAWI DUZYCH
		numberShipBigSpawn = (int)(Random.Range( 0 , 30 ) % numberShipBig.y) + 1 ;
		//(int)Random.Range( numberShipBig.x , numberShipBig.y + 1 ) ;
		//CZY STWORZYC STATKI DUZE
		shipSpawnBig = Random.Range( 0 , 40 ) ;
		
		//ILOSC OBIEKTOW W FALI
		for(int i = 0 ; i < hazardCount ; i++)
		{
			//SPAWN ASTEROID
			SpawnAsteroid() ;
			
			//SPAWN STATKOW MALYCH
			if( shipSpawnSmall == 1  )
			{
				SpawnSmallShip() ;
			}
			
			//SPAWN STATKOW DUZYCH
			if( shipSpawnBig % 4 == 0 )
			{
				SpawnBigShip() ;
			}
			
			//Debug.Log( randomAsteroid + " = randomAsteroid ") ;
			//Debug.Log(" spawnWait " + spawnWait ) ;
			//WYJSCIE Z PETLI NA CZAS MIEDZY POJAWIENIEM SIE KOLEJNEGO OBIEKTU
			yield return new WaitForSeconds (spawnWait);

		}

		//KONIEC FALI TRZEBA USTAWIC NOWA
		End_Wave = true ;

		//LICZNIK FAL
		wave++ ;

		//Debug.Log(" END WAVEE " ) ;
	}

	void SpawnBoss()
	{
		//SKORO BOSS JUZ SIE POJAWIL TO NIE MA POTRZEBY ZEBY ZNOWU SIE POJAWIL
		S_Boss = false ;

		//OBLICZANIE POZYCJI
		Vector3 spawnPosition = new Vector3( 0 , spawnValue.y , spawnValue.z ) ;
		//ZERO ROTACJI ( identity )
		Quaternion spawnRotation = Quaternion.identity ;

		Instantiate( Boss , spawnPosition , spawnRotation ) ;
		
		//CZEKAMY AZ BOSS PADNIE
		StartCoroutine(WaitToBossDead()) ;
	}

	IEnumerator WaitToBossDead()
	{
		//JESLI BOSS ZYJE TO PRZECIAGAMY DALEJ
		while( EnemyControlerBoss.BossLive )
		{
			//Debug.Log ( " WaitToBossDead " ) ;
			yield return new WaitForFixedUpdate(); 
		}

		//Debug.Log ( " BOSS DEAD" ) ;
		//KONIEC FALI TRZEBA USTAWIC NOWA
		End_Wave = true ;

		//LICZNIK FAL
		wave++ ;

		//USTAWIAMY ZE BOSS "BEDZIE" ZYC
		EnemyControlerBoss.BossLive = true ;

		yield return new WaitForEndOfFrame();  
	}
}
