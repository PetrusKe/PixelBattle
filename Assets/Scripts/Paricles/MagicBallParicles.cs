using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCharacters;

namespace GameParicles
{
    public class MagicBallParicles : MonoBehaviour
    {

        public ParticleSystem explosionParticles;
        public ParticleSystem projectileParticle;
        public AudioSource explosionAudio;

        public float radius = 0.5f;       //equal as ball collider's radius

        public GameObject owner;
        private Vector3 movement;
        public float power;
        public float lifeTime;
        public float force;
        public float speed;
        private bool hasCollided = false;

        void Start()
        {
            movement = owner.transform.TransformDirection(Vector3.forward).normalized;
            Destroy(gameObject, 3f);  // fix here?
        }

        private void FixedUpdate()
        {
            transform.position -= movement * speed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.name == owner.name)
                    continue;
                if (collider.gameObject.layer == LayerMask.NameToLayer("Particles"))
                    continue;

                hasCollided = true;
                Debug.Log(collider.gameObject.name);

                if (collider.gameObject.layer == LayerMask.NameToLayer("Character"))
                {
                    Rigidbody targetRigitbody = collider.GetComponent<Rigidbody>();
                    if (!targetRigitbody)
                        continue;
                    targetRigitbody.AddExplosionForce(force, transform.position, radius);

                    Character c = collider.gameObject.GetComponent<Character>();
                    c.TakeDamage(power);
                }
            }

            if (hasCollided)
            {
                explosionParticles.transform.parent = null;
                explosionParticles.Play();
                Destroy(explosionParticles.gameObject, explosionParticles.main.duration);
                Destroy(gameObject);
            }

        }

    }
}

