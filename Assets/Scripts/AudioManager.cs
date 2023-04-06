using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource music, sfx;
    [SerializeField] AudioClip[] canciones;
    [SerializeField] AudioClip boton,rotacion,compra;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = Camera.main.transform.position;
        this.transform.parent = Camera.main.transform;
    }

    public void PlaySFX(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }

    public void ButtonSFX()
    {
        sfx.PlayOneShot(boton);
    }

    public void RotacionSFX()
    {
        sfx.PlayOneShot(rotacion);
    }

    public void CompraSFX()
    {
        sfx.PlayOneShot(compra);
    }

    public void Playlist()
    {
        if(canciones.Length > 0)
        {
            music.clip = canciones[Random.Range(0, canciones.Length)];
            music.Play();
            music.loop = true;
        }

    }
}
