using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : MonoBehaviour
{
    private List<Vector3> particlePositions = new List<Vector3>();
    [SerializeField]
    private List<ParticleSystem> victoryParticles = new List<ParticleSystem>();
    // Start is called before the first frame update
    void Start()
    {
        ParticleInitialization();
    }
    
    public void ParticleInitialization()
    {
        
        particlePositions = new List<Vector3>();
        for (int i = 0; i < victoryParticles.Count; i++)
        {
            particlePositions.Add(victoryParticles[i].transform.position);
            victoryParticles[i].transform.position = new Vector3(10f, 10f, 10f);
            victoryParticles[i].Pause();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < particlePositions.Count; i++)
            {
                victoryParticles[i].transform.position = particlePositions[i];
                victoryParticles[i].Play();
            }
        }
        
    }
}
