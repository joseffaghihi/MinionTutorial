using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class SpawnController : MonoBehaviour
    {
        /*                    SPAWNER CONTROLLER
         * This script will manage the spawners within the virtual world to spawn the minion actors either individually or by issuing a message to notify the spawners to summon a minion actor (depending other conditions of the spawner itself).  
         *  
         * 
         * STRUCTURAL DEPENDENCY NOTES:
         *      WAVES
         *       |_ Spawn Controller
         *          |_ SPAWNER OBJECTS*
         * 
         * GOALS:
         *  Determine the next spawn
         *  Send a spawn signal to all possible spawners
         *                  OR
         *  Send a spawn signal to just only one spawner.
         */



        // Declarations and Initializations
        // ---------------------------------
        // Accessors and Communication
            // Spawners (Summoning batches)
                public delegate void SpawnBatchMinions();
                public static event SpawnBatchMinions EnableSpawnPoint;
            // Spawner Objects
                // These will be used to have complete utter control over the spawners individually.
                    // Spawner 0
                        public Spawner spawnerObject0;
                    // Spawner 1
                        public Spawner spawnerObject1;
                    // Spawner 2
                        public Spawner spawnerObject2;
        // ----


        

        // Signal Listener: Detected
        private void OnEnable()
        {
            AI_SpawnService.SummonMinion_Batches += SpawnMinionBatch;
            AI_SpawnService.SummonMinion_OnlyOne += SpawnMinion;
        } // OnEnable()



        // Signal Listener: Deactivate
        private void OnDisable()
        {
            AI_SpawnService.SummonMinion_Batches -= SpawnMinionBatch;
            AI_SpawnService.SummonMinion_OnlyOne -= SpawnMinion;
        } // OnDisable()



        // Only spawn 'one' minion from a random spawner.
        // NOTE: THIS REQUIRES THE SPAWNER OBJECTS TO BE INITIALIZED WITHIN THE INSPECTOR!
        private void SpawnMinion()
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    spawnerObject0.SpawnForcibly_public();
                    break;
                case 1:
                    spawnerObject1.SpawnForcibly_public();
                    break;
                case 2:
                    spawnerObject2.SpawnForcibly_public();
                    break;
            } // Switch
        } // SpawnMinion()



        // Send a broadcast message to the spawners to activate.
        private void SpawnMinionBatch()
        {
            EnableSpawnPoint();
        } // SpawnMinionBatch()
    } // End of Class
} // Namespace