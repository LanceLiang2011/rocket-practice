using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]
    private float reloadTime = 2f;
    [SerializeField]
    private AudioClip successSound;
    [SerializeField]
    private AudioClip deathSound;

    [SerializeField]
    private ParticleSystem successParticle;
    [SerializeField]
    private ParticleSystem deathParticle;

    PlayerMovement playerMovement;
    AudioSource audioSource;

    private bool isTransitioning;
    private bool isCollisionEnabled = true;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
        playerMovement.enabled = true;
        isTransitioning = false;
    }

    private void Update()
    {
        DebugKeysDetecting();
    }

    private void DebugKeysDetecting()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCollisionEnabled();
        }
    }

    private void ToggleCollisionEnabled()
    {
        isCollisionEnabled = !isCollisionEnabled;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(isTransitioning) return;
        if(!isCollisionEnabled) return;

        switch (other.gameObject.tag)
        {
            case CollisionTags.Friendly:
                HandleFriendlyCollision();
                break;
            case CollisionTags.Finish:
                HandleFinishCollision();
                break;
            default:
                HandleDefaultCollision();
                break;
        }
    }

    private void HandleFinishCollision()
    {
        Invoke(nameof(LoadNextLevel), reloadTime);
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        successParticle.Play();
        if (playerMovement.enabled)
        {
            playerMovement.enabled = false;
        }
        isTransitioning = true;
    }

    private void HandleFriendlyCollision()
    {
        Debug.Log("Friendly");
    }

    private void HandleDefaultCollision()
    {
        Invoke(nameof(ReloadLevel), reloadTime);
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);
        deathParticle.Play();
        if (playerMovement.enabled)
        {
            playerMovement.enabled = false;
        }
        isTransitioning = true;
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(SceneManager.sceneCount);
        int nextSceneIndex = currentSceneIndex + 1 >= SceneManager.sceneCountInBuildSettings ? 0 : currentSceneIndex + 1;
        isTransitioning = false;
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        isTransitioning = false;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

