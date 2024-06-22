using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace FPVDrone
{
    [RequireComponent(typeof(InputController))]
    public class DroneController : BaseRBController
    {
        #region Variables
        [Header("Drone Properties")]
        public List<DroneEngine> engines = new List<DroneEngine>();

        [Header("Drone Rotors")]
        public DroneRotorController rotorCtrl;

        private InputController input;
        private DroneCharacteristics characteristics;
        #endregion

        #region Built-in Methods
        public override void Start()
        {
            base.Start();

            input = GetComponent<InputController>();
            characteristics = GetComponent<DroneCharacteristics>();
        }
        #endregion

        #region Events
        private void OnDestroy()
        {
            GameEvents.instance.droneDestroyed.SetValueAndForceNotify(true);
        }
        #endregion

        #region Custom Methods
        protected override void HandlePhysics()
        {
            if (input)
            {
                HandleEngines();
                HandleRotors();
                HandleCharacteristics();
            }
        }

        #endregion

        #region Drone Controle Methods
        protected virtual void HandleEngines()
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i].UpdateEngine(input.ThrottleInput);

                float finalPower = 0f;
                for (int j = 0; j < engines.Count; j++)
                {
                    finalPower += engines[j].CurrentKV;
                }
            }
        }

        protected virtual void HandleRotors()
        {
            if (rotorCtrl)
            {
                // TODO: Handle all rotors separately
                rotorCtrl.UpdateRotor(input, engines[0].CurrentRPM);
            }
        }

        protected virtual void HandleCharacteristics()
        {
            if (characteristics)
            {
                characteristics.UpdateCharacteristics(rb, input);
            }
        }
        #endregion
    }
}
