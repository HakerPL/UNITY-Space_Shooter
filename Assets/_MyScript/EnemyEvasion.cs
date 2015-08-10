using UnityEngine;
using System.Collections;


public class EnemyEvasion : MonoBehaviour 
{
	//ZMIENNA INFORMUJACA ZE TO WIELKI STATEK
	public bool BigShip ;

	//SZEROKOSC EKRANU
	public Boundry boundry;
	//WYCHYLENIE
	public float tilt;
	//ILE MA UCIEC
	public float dodge;
	//SZYBKOSC RUCHU
	public float smooth;
	//CZAS DO ROZPOCZECIA UNIKU
	public Vector2 WaitToEvasion;
	//CZAS MANEWRU
	public Vector2 TimeEvasion;
	//ODCZEKANIE DO NASTEPNEGO MANEWRU
	public Vector2 WaitBetweenEvasion;

	//OBECNA PREDKOSC
	private float currentSpeed;
	//CEL MANEWRU
	private float targetManeuver;
	
	void Start ()
	{
		if( BigShip )
		{
			//LOZUJEMY POZYCJE DO ZATRZYMANIA
			int pos = Random.Range ( 0 , 3 ) ;

			switch( pos )
			{
				case 0:

					boundry.zMin = 6f ;
					break ;

				case 1:

					boundry.zMin = 9.5f ;
					break ;

				case 2:

					boundry.zMin = 13.5f ;
					break ;

				default:

					boundry.zMin = -20f ;
					break ;
			}
		}

		currentSpeed = rigidbody.velocity.z;
		StartCoroutine(Evade());
	}
	
	IEnumerator Evade ()
	{
		yield return new WaitForSeconds (Random.Range (WaitToEvasion.x, WaitToEvasion.y));
		while (true)
		{
			//Sing WARTOSC WIEKSZA OD ZERA LUB ZER ZWRACA 1 MNIEJSZA OD ZERA ZWRACA -1
			targetManeuver = Random.Range (1, dodge) * -Mathf.Sign (transform.position.x);
			yield return new WaitForSeconds (Random.Range (TimeEvasion.x, TimeEvasion.y));
			targetManeuver = 0;
			yield return new WaitForSeconds (Random.Range (WaitBetweenEvasion.x, WaitBetweenEvasion.y));
		}
	}
	
	void FixedUpdate ()
	{
		//MoveToward DZIALA JAK Lerp TYLKO ZE PREDKOSC NIGDY NIE BEDZIE WIEKSZA (smooth)
		float newManeuver = Mathf.MoveTowards (rigidbody.velocity.x, targetManeuver, smooth * Time.deltaTime);
		rigidbody.velocity = new Vector3 (newManeuver, 0.0f, currentSpeed);
		//NOWA POZYCJA STATKU
		rigidbody.position = new Vector3
			(
				//Clamp ZWRACA 1 ZMIENNA LUB ZMIENIA JA NA MIN LUB MAX (PRZYDATNE ZEBY OBIEKT NIE OPUSCIL TERENU)
				Mathf.Clamp(rigidbody.position.x, boundry.xMin, boundry.xMax), 
				0.0f, 
				Mathf.Clamp(rigidbody.position.z, boundry.zMin, boundry.zMax)
				);

		//PRZECHYLENIE STATKU
		rigidbody.rotation = Quaternion.Euler (0, 0, rigidbody.velocity.x * -tilt);
	}
}

