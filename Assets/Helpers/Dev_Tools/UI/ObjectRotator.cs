using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotGTA
{
    public class ObjectRotator : MonoBehaviour
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region LifeCycle

        #endregion

        #region Private Methods

        #endregion

        public float rotationSpeedX = 50.0f;
        public float rotationSpeedY = 50.0f;
        public float rotationSpeedZ = 50.0f;

        // Update is called once per frame
        void Update()
        {
            // Rotate the object around X, Y and Z axis
            transform.Rotate(new Vector3(rotationSpeedX, rotationSpeedY, rotationSpeedZ) * Time.deltaTime);
        }

        #region Public Methods

        #endregion
    }
}