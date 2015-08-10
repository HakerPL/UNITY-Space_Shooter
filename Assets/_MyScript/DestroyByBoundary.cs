using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour 
{
	//NISZCZYMY WSZYSTKO CO OPUSCI PLANSZE
	void OnTriggerExit(Collider other)
	{
		Destroy(other.gameObject) ;
	}
}
