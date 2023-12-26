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
        Debug.Log("Finish");
    }

    private void HandleFriendlyCollision()
    {
        Debug.Log("Friendly");
    }

    private void HandleDefaultCollision()
    {
        SceneManager.LoadScene(0);
    }
}

