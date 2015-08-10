using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour 
{
	//CZAS ZYCIA OBIEKTU
	public float lifeTime ;

	// Use this for initialization
	void Start () 
	{
		Destroy(gameObject , lifeTime ) ;
	}
}
