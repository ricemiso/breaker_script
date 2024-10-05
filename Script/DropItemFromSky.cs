using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DropItemFromSky : MonoBehaviour
{
    public GameObject policeCar;
    public GameObject sportsCar;
    public GameObject armeredCar;
    public GameObject garbageCar;
	public GameObject streetLight;
	public GameObject garbageBox;
	public GameObject specialItem;
	private GameObject[] cars;
	static int maxItemNum = 7;
	private bool isDrop;
	private float roopTimer = 10f;
	private int counter;

	// Start is called before the first frame update
	void Start()
    {
		isDrop  = false;
		counter = 0;

		cars = new GameObject[7] { policeCar, sportsCar, armeredCar, garbageCar, streetLight, garbageBox, specialItem };

	}

    // Update is called once per frame
    void Update()
    {
		if (!isDrop)
        {
			roopTimer -= Time.deltaTime;
			if (roopTimer <= 0f)
			{
				isDrop = true;
				roopTimer = 10f;
			}
		}
		else
		{
			Vector3 spawnPos = new Vector3(Random.Range(-43, -32), 50, Random.Range(-42, 66));
			if (counter++ < 6)
			{
				Instantiate(cars[Random.Range(0, maxItemNum - 1)], spawnPos, Quaternion.identity);
			}
			else
			{
				Instantiate(cars[Random.Range(0, maxItemNum)], spawnPos, Quaternion.identity);
			}
			isDrop = false;
		}
    }
}
