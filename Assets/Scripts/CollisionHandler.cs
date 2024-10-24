using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashVFX;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    StartCrashSequence();
    //    //Debug.Log(collision.gameObject.name);
    //}

    private void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    private void StartCrashSequence()
    {
        crashVFX.Play();

        GetComponent<PlayerController>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        foreach(var child in transform.GetComponentsInChildren<MeshRenderer>())
            child.enabled = false;


        Invoke("ReloadLevel", loadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
