using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Ambulance : MonoBehaviour
{
    const float Speed = 5f;

    List<Checkpoint> Checkpoints { get; set; }

    void Start()
    {
        Checkpoints = GetComponentsInChildren<Checkpoint>().ToList(); ;
    }

    void Update()
    {
        HandleRotation();
        MoveTowardsNextCheckpoint();
    }

    void HandleRotation()
    {
        Vector3 LookDestination = new(Checkpoints[0].transform.localPosition.x, transform.localPosition.y, Checkpoints[0].transform.localPosition.z);
		transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(LookDestination - transform.localPosition), Time.deltaTime * 0.8f);
	}

	void MoveTowardsNextCheckpoint()
    {
        if (transform.localPosition.x == Checkpoints[0].transform.localPosition.x && transform.localPosition.z == Checkpoints[0].transform.localPosition.z)
			Checkpoints.Remove(Checkpoints[0]);
        if (Checkpoints.Count == 0)
            Destroy(gameObject);
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new(Checkpoints[0].transform.localPosition.x, transform.localPosition.y, Checkpoints[0].transform.localPosition.z), Speed * Time.deltaTime);
    }
}
