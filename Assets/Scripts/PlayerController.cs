using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float speed = 5f, mouseSensitivityX = 50f,mouseSensitivityY = 200f, range = 100f, jumpDamage = 2f;
    public int _atackGamage = 25;
    public Transform orientation;
    public GameObject hitEffect ;
    private Rigidbody _rb;
    private PhotonView _photonView;
    private int _health = 100;
    private float xRotation;
    private float yRotation;
    
    

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _rb = GetComponent<Rigidbody>();
       
        if (!_photonView.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (!_photonView.IsMine)
            return;

        MovePlayer();
    }

    private void Update()
    {
        if (!_photonView.IsMine)
            return;

        RotatePlayer();

        Shoot();
    }

    private void Shoot()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            
            Camera camera = transform.GetChild(0)?.GetComponent<Camera>();
            Vector3 shootPos = new Vector3(camera.transform.position.x,camera.transform.position.y,camera.transform.position.z);
            RaycastHit hit;

            if (Physics.Raycast(shootPos, camera.transform.forward, out hit, range))
            {
                if (hit.collider.CompareTag("RoomObj") || hit.collider.CompareTag("Player"))
                {
                    Instantiate(hitEffect, hit.point, Quaternion.identity);
                    if (hit.collider.CompareTag("Player"))
                    {
                        hit.collider.GetComponent<PlayerController>().JumpDamage();
                        hit.collider.GetComponent<PlayerController>().Damage(_atackGamage);
                    }
                }
            }
        }
    }

    public void Damage(int damage)
    {
        _photonView.RPC("PunDamage", RpcTarget.All, damage);
    }
    public void JumpDamage()
    {
        _photonView.RPC("PunJumpDamage", RpcTarget.All);
    }
    

    [PunRPC]
    void PunDamage(int damage)
    {
        if(!_photonView.IsMine)
            return;
        
        _health -= damage;
        if (_health<= 0)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
    
    [PunRPC]
    void PunJumpDamage()
    {
        if (!_photonView.IsMine)
            return;

        _rb.AddForce(Vector3.up * jumpDamage, ForceMode.Impulse);

    }

    private void RotatePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        
        xRotation = Mathf.Clamp(xRotation, -70f, 70f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0,yRotation,0);

    }

    private void MovePlayer()
    {

        float horMove = Input.GetAxis("Horizontal");
        float vertMove = Input.GetAxis("Vertical");

        if (horMove > 0 || horMove < 0)
        {
            _rb.AddTorque(transform.up * horMove);
        }
        
        
        Vector3 direction = new Vector3(horMove, 0 , vertMove);
        
        _rb.MovePosition(transform.position + transform.TransformDirection(direction * speed * Time.deltaTime));
        

    }
}

