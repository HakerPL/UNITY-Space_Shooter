using UnityEngine;
using System.Collections;

public enum TypeWeapon { TwoBolt , ThreeBolt , Sphere } ;


public class ChangeWeapon : MonoBehaviour 
{
	
	public TypeWeapon NameWeapon ;

	//SKRYPT GRACZA
	private PlayerControler player ;

	void Start()
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
	

	// Update is called once per frame
	void OnTriggerEnter(Collider other)
	{
		//ZMIENIAMY ZMIENNE BOOL NA ODPOWIEDNIA BRON ORAZ USTAWIAMY ZMIANE BRONI NA TRUE
		if(other.tag == "Player")
		{
			switch( NameWeapon )
			{
				case TypeWeapon.TwoBolt:

					player.WeaponTwoBolt() ;

					Destroy( gameObject ) ;

					break ;

				case TypeWeapon.ThreeBolt:
					
					player.WeaponThreeBolt() ;
					
					Destroy( gameObject ) ;
					
					break ;

				case TypeWeapon.Sphere:

					player.WeaponSphere() ;
					
					Destroy( gameObject ) ;
					
					break ;
			}
		}
		else
			return ;
	}
}
