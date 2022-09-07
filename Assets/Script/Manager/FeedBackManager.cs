using Game.Script.Other;
using UnityEngine;

namespace Script
{
    public class FeedBackManager : MonoSingleton<FeedBackManager>
    {
        public ParticleSystem boostParticle;

        public void StartBoostParticle()
        {
            boostParticle.Play();
        }

        public void StopBoostParticle()
        {
            boostParticle.Stop();
        }

        public ParticleSystem CreateParticleByPrefab(ParticleSystem particle, Transform parent, Vector3 position)
        {
            var particleEffect = Instantiate(particle, position, Quaternion.identity, parent);
            return particleEffect;
        }

        public ParticleSystem CreateParticleByPrefab(ParticleSystem particle, Vector3 position)
        {
            var particleEffect = Instantiate(particle, position, Quaternion.identity);
            return particleEffect;
        }
    }
}