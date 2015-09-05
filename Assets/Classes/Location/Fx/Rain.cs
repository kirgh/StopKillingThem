using UnityEngine;
using System.Collections;

public class Rain : MonoBehaviour {

	public float minimum = 0;
	public float maximum = 1;
	public float changeSpeed = 0.5f;
	public ParticleSystem particles;

	private float currentValue;

	public void Init(Main main)
	{
		currentValue = UnityEngine.Random.Range (minimum, maximum);
		transform.localScale = new Vector3 (main.LocationWidthInMeters / 2, 1, 1);
		transform.localPosition = new Vector3 (transform.localScale.x, main.LocationHeightInMeters + 1, 0);
		particles = GetComponent<ParticleSystem> ();
		particles.emissionRate = CalculateCurrentEmissionRate();
		particles.Simulate (1);
		particles.Play ();
	}

	void Update()
	{
		particles.emissionRate = CalculateCurrentEmissionRate();
	}

	private int CalculateCurrentEmissionRate()
	{
		return (int)(currentValue * 100);
	}
}
