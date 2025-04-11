using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private ParticleSystem _seedParticles;
    [SerializeField] private ParticleSystem _waterParticles;

    private void PlaySeedParticles()
    {
        _seedParticles.Play();
    }
    private void PlayWaterParticles()
    {
        _waterParticles.Play();
    }
}
