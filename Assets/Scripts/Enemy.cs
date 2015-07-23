using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public Transform target;
    public float moveSpeed = 3.0f;
    public float rotationSpeed = 3.0f;

    private Transform myTransform;
    private bool olhandoDireita;

	// Use this for initialization
	void Awake () {
        myTransform = GetComponent<Transform>();
	}

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

	// Update is called once per frame
	void Update () {
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
        myTransform.position += myTransform.right * moveSpeed * Time.deltaTime;
	}


}
