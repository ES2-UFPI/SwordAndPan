using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private Player player; // Referência ao jogador
    [SerializeField] private AudioSource footstepsAudioSource; // Referência ao AudioSource para sons de passos
    [SerializeField] private AudioClip footstepsClip; // Referência ao clipe de áudio de passos

    private void Awake()
    {
        // Verifica se o AudioSource foi atribuído no Inspector, se não, tenta obtê-lo do GameObject
        if (footstepsAudioSource == null)
        {
            footstepsAudioSource = GetComponent<AudioSource>();
        }
        
        if (footstepsAudioSource == null)
        {
            Debug.LogError("AudioSource for footsteps not found!");
        }

        // Verifica se o Player foi atribuído no Inspector, se não, tenta obtê-lo do GameObject
        if (player == null)
        {
            player = GetComponent<Player>();
        }
        
        if (player == null)
        {
            Debug.LogError("Player component not found!");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        PlayWalkingSound();
    }
    
    private void PlayWalkingSound()
    {
        if (player.IsWalking())
        {
            if (!footstepsAudioSource.isPlaying)
            {
                footstepsAudioSource.clip = footstepsClip;
                footstepsAudioSource.loop = true; // Para continuar tocando enquanto o jogador está andando
                footstepsAudioSource.Play();
            }
        }
        else
        {
            if (footstepsAudioSource.isPlaying)
            {
                footstepsAudioSource.Stop();
            }
        }
    }
}
