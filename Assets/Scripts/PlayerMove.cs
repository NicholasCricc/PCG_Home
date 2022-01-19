using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [SerializeField]
    private GameObject Player;

    [SerializeField] private float _speed = 1;
    [SerializeField] private Rigidbody _rb;


    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Player_Move();
    }

     private void Player_Move() { 
        var vel = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _speed;
        vel.y = _rb.velocity.y;
        _rb.velocity = vel;
    }
}
