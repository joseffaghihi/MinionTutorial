// This script is responsible for the water splashing
// When the ship starts to sink

using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship {

	public class WaterSplashing : MonoBehaviour {

		private ParticleSystem particles; // particle system game object

		void Start() {
			particles = GetComponent<ParticleSystem>();
		}

		// This method simply plays the particle system
		private void ParticleSpray() {
			particles.Play();
		}

		// Events Subscriptions and Unsubscriptions Below ----------------
		void OnEnable() {
			GameController.GameStateEnded += ParticleSpray;
		}

		void OnDisable() {
			GameController.GameStateEnded -= ParticleSpray;
		}

	} // end class
} // end namespace