using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
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
        LoadNextLevel();
    }

    private void HandleFriendlyCollision()
    {
        Debug.Log("Friendly");
    }

    private void HandleDefaultCollision()
    {
        ReloadLevel();
    }

    private static void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(SceneManager.sceneCount);
        int nextSceneIndex = currentSceneIndex + 1 >= SceneManager.sceneCountInBuildSettings ? 0 : currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    private static void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

