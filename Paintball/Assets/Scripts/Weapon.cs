using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class Weapon : MonoBehaviour
{
    [SerializeField] protected float shootingForce;
    [SerializeField] private float recoilForce;
    [SerializeField] protected Transform[] bulletSpawns;


    [SerializeField] private AudioClip audio;
    [SerializeField] private AudioSource audioSource;
    private Rigidbody rigidBody;
    private XRGrabInteractable interactableWeapon;

    private void Awake()
    {
        interactableWeapon = GetComponent<XRGrabInteractable>();
        rigidBody = GetComponent<Rigidbody>();

        SetupInteractableWeaponEvents();
    }

    private void SetupInteractableWeaponEvents()
    {
        interactableWeapon.onSelectEnter.AddListener(PickUpWeapon);
        interactableWeapon.onSelectExit.AddListener(DropWeapon);
        interactableWeapon.onActivate.AddListener(StartShooting);
        interactableWeapon.onDeactivate.AddListener(StopShooting);
    }

    private void PickUpWeapon(XRBaseInteractor interactor)
    {
        interactor.GetComponent<MeshHider>().HideMeshes();
    }

    private void DropWeapon(XRBaseInteractor interactor)
    {
        interactor.GetComponent<MeshHider>().ShowMeshes();
    }

    protected virtual void StartShooting(XRBaseInteractor interactor)
    {
       
    }

    protected virtual void StopShooting(XRBaseInteractor interactor)
    {

    }

    protected virtual void Shoot()
    {
        ApplyRecoil();
        SoundShoot();
    }

    private void ApplyRecoil()
    {
        //rigidBody.AddRelativeForce(Vector3.back * recoilForce, ForceMode.Impulse);
    }

    public float GetShootingForce()
    {
        return shootingForce;
    }

    public void SoundShoot()
    {
     
        audioSource.clip = audio;
        audioSource.loop = false;
        audioSource.Play();
        

    }


}
