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

            transform.rotation = ComputeRotationMatrix(x, y, z).rotation;
        }

        /// <summary>
        /// Rotates the Transform by one step using the specified x, y, and z rotation angles.
        /// This can be used in edit mode or play mode.
        /// </summary>
        public void Rotate()
        {
            var stepMatrix = ComputeRotationMatrix(
                _rotationDegrees.x,
                _rotationDegrees.y,
                _rotationDegrees.z
            );
            var currentMatrix = Matrix4x4.Rotate(transform.rotation);
            transform.rotation = (stepMatrix * currentMatrix).rotation;
        }

        /// <summary>
        /// Computes the x, y, and z rotation matrices and returns the combined rotation matrix.
        /// </summary>
        /// <returns>The combined rotation matrix</returns>
        private static Matrix4x4 ComputeRotationMatrix(float x, float y, float z)
        {
            // Need to convert degrees to radians for Mathf.Sin/Cos computations.
            var radiansX = x * Mathf.Deg2Rad;
            var radiansY = y * Mathf.Deg2Rad;
            var radiansZ = z * Mathf.Deg2Rad;

            // Construct the rotation matrices for each axis starting with a 4x4 identity matrix
            // https://dirsig.cis.rit.edu/docs/new/affine.html#_rotation

            // Rotation along x-axis
            var matrixX = Matrix4x4.identity;
            matrixX[1, 1] = Mathf.Cos(radiansX);
            matrixX[1, 2] = -Mathf.Sin(radiansX);
            matrixX[2, 1] = Mathf.Sin(radiansX);
            matrixX[2, 2] = Mathf.Cos(radiansX);

            // Rotation along y-axis
            var matrixY = Matrix4x4.identity;
            matrixY[0, 0] = Mathf.Cos(radiansY);
            matrixY[0, 2] = Mathf.Sin(radiansY);
            matrixY[2, 0] = -Mathf.Sin(radiansY);
            matrixY[2, 2] = Mathf.Cos(radiansY);

            // Rotation along z-axis
            var matrixZ = Matrix4x4.identity;
            matrixZ[0, 0] = Mathf.Cos(radiansZ);
            matrixZ[0, 1] = -Mathf.Sin(radiansZ);
            matrixZ[1, 0] = Mathf.Sin(radiansZ);
            matrixZ[1, 1] = Mathf.Cos(radiansZ);

            // A series of axis rotations are not commutative, so the order matters.
            return matrixX * matrixY * matrixZ;
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
