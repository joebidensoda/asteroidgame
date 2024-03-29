using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movemen : MonoBehaviour
{
    public Bullet bulletprefab;
    public float thrustspeed = 1.0f;
    public float turnspeed = 1.0f;
    private Rigidbody2D _rigidbody;
    private bool _thrusting;
    private float _turndirection;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        
    }
    
    private void Update()
    {
    _thrusting = Input.GetKey(KeyCode.W)  ||  Input.GetKey(KeyCode.UpArrow);

    if (Input.GetKey(KeyCode.A)  ||  Input.GetKey(KeyCode.LeftArrow)){
        _turndirection = 1.0f;
    } else if (Input.GetKey(KeyCode.D)  ||  Input.GetKey(KeyCode.RightArrow)){
        _turndirection = -1.0f;
    } else{
        _turndirection = 0.0f;
    }

     if (Input.GetKeyDown(KeyCode.Space)  ||  Input.GetMouseButtonDown(0)) {
        shoot();
     }
    }

    private void FixedUpdate()
    {
        if (_thrusting) {
            _rigidbody.AddForce(this.transform.up * this.thrustspeed);
        }
        if (_turndirection != 0.0f) {
            _rigidbody.AddTorque(_turndirection * this.turnspeed);
        } 
    }
    private void shoot()
    {
       Bullet bullet = Instantiate(this.bulletprefab, this.transform.position, this.transform.rotation);
       bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}
