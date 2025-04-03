using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private ParticleSystem _seedParticles;

    private void PlaySeedParticles()
    {
        _seedParticles.Play();
    }
}
