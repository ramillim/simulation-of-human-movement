using UnityEngine;

namespace Simulation
{
    /// <summary>
    /// Performs rotation on a Transform using a rotation matrix.
    /// </summary>
    public class MatrixRotator : MonoBehaviour
    {
        [Tooltip("Amount to rotate per second")]
        [SerializeField]
        private Vector3 _rotationDegrees;

        [Tooltip("When enabled, Transform will continuously rotate in play mode")]
        [SerializeField]
        private bool _isContinuousRotationEnabled;

        private void Update()
        {
            if (!_isContinuousRotationEnabled)
                return;

            ApplyAbsoluteRotation();
        }

        /// <summary>
        /// Performs framerate-independent rotation.
        /// </summary>
        private void ApplyAbsoluteRotation()
        {
            // Calculate the absolute orientation for this exact moment in time
            var x = _rotationDegrees.x * Time.time;
            var y = _rotationDegrees.y * Time.time;
            var z = _rotationDegrees.z * Time.time;

            transform.rotation = MatrixTransformMath.Compute(x, y, z).rotation;
        }

        /// <summary>
        /// Rotates the Transform by one step using the specified x, y, and z rotation angles.
        /// This can be used in edit mode or play mode.
        /// </summary>
        public void Rotate()
        {
            var stepMatrix = MatrixTransformMath.Compute(
                _rotationDegrees.x,
                _rotationDegrees.y,
                _rotationDegrees.z
            );
            var currentMatrix = Matrix4x4.Rotate(transform.rotation);
            transform.rotation = (stepMatrix * currentMatrix).rotation;
        }

        /// <summary>
        /// Resets the position and rotation of the Transform to its initial state.
        /// </summary>
        public void Reset()
        {
            _isContinuousRotationEnabled = false;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }
    }
}
