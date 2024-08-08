using System;
using UnityEngine;

public class Fork : Weapon
{
    [SerializeField] private ParticleSystem forkParticle;
    [SerializeField] private GameInput gameInput; // Referência à entrada do jogo
    [SerializeField] private AudioSource attackAudioSource;

    private void Start()
    {
        // Procura pelo objeto gameInput na cena
        GameObject gameInputObject = GameObject.Find("GameInput");
        // Se o objeto for encontrado, tenta pegar o script GameInput
        if (gameInputObject != null)
        {
            gameInput = gameInputObject.GetComponent<GameInput>();
            if (gameInput != null)
            {
                gameInput.OnFireAction += OnFire; // Subscreve ao evento de ataque
            }
            else
            {
                Debug.LogError("GameInput script not found on gameInput object!");
            }
        }
        else
        {
            Debug.LogError("gameInput object not found in the scene!");
        }
    }

    private void OnFire(object sender, EventArgs e)
    {
        if (this != null)
        {
            ActivateAttackParticle();
            PlayAttackSound();
        }
        else
        {
            Debug.LogWarning("The Fork object has been destroyed.");
        }
    }

    private void ActivateAttackParticle()
    {
        if (forkParticle != null)
        {
            forkParticle.Play();
        }
        else
        {
            Debug.LogWarning("Fork particle system is null.");
        }
    }

    private void PlayAttackSound()
    {
        if (attackAudioSource != null)
        {
            attackAudioSource.Play();
        }
        else
        {
            Debug.LogError("Attack audio source not assigned!");
        }
    }

    private void OnDestroy()
    {
        if (gameInput != null)
        {
            gameInput.OnFireAction -= OnFire; // Desinscreve do evento de ataque para evitar referências nulas
        }
    }
}
