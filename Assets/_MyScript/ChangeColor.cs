using UnityEngine;
using UnityEngine.UI ;
using System.Collections;

public class ChangeColor : MonoBehaviour 
{

	public enum SelectedAxis
	{
		xAxis,
		yAxis,
		zAxis
	}

	//WYROWNANIE??
	public SelectedAxis selectedAxis = SelectedAxis.xAxis;
	
	// Target
	public Image image;
	
	//PARAMETRY  WIELKOSC I KOLOR
	public float minValue = 0.0f;
	public float maxValue = 1.0f;
	public Color minColor = Color.red;
	public Color maxColor = Color.green;
	
	// The default image is the one in the gameObject
	void Start()
	{
		if (image == null)
		{
			image = GetComponent<Image>();
		}
	}
	
	void FixedUpdate () 
	{
		switch (selectedAxis)
		{
			case SelectedAxis.xAxis:
				//OBLICZENIE PRZEJSCIA KOLORU
				image.color = Color.Lerp(minColor,
				                         maxColor,
				                         Mathf.Lerp(minValue,
				           maxValue,
				           transform.localScale.x));
				break;

			case SelectedAxis.yAxis:
				//OBLICZENIE PRZEJSCIA KOLORU
				image.color = Color.Lerp(minColor,
				                         maxColor,
				                         Mathf.Lerp(minValue,
				           maxValue, transform.localScale.y));
				break;

			case SelectedAxis.zAxis:
				//OBLICZENIE PRZEJSCIA KOLORU
				image.color = Color.Lerp(minColor,
				                         maxColor,
				                         Mathf.Lerp(minValue,
				           maxValue,
				           transform.localScale.z));
				break;
		}
	}
}