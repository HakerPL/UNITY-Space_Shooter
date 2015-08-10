using UnityEngine;
using System.Collections;

public class AddHealth : MonoBehaviour 
{
	//SKRYPT GRACZA
	private PlayerControler player ;

	// Use this for initialization
	void Start () 
	{
		GameObject PlayerComponent = GameObject.FindWithTag("Player") ;
		//JESLI SZUKANIE SIE POWIODLO TO PRZYPISUJEMY COMPONENT DO OBIEKTU
		if( PlayerComponent != null )
		{
			player = PlayerComponent.GetComponent<PlayerControler>() ;
		}
		//JESLI NIE ZNALEZLISMY LUB PRZYPISANIE SIE NIE POWIODLO WYSWIETLAMY TEKST
		if(player == null)
		{
			Debug.Log( " can't find Player PlayerControler " ) ;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			//DODAJEMY ZYCIE
			player.AddHealth() ;
			
			Destroy( gameObject ) ;
		}
		else
			return ;
	}
}
