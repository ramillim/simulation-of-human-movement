using UnityEngine;

namespace Simulation
{
    /// <summary>
    /// Mathematical functions for computing matrix transform operations. These are alternatives to
    /// the Unity libraries that perform the same functions and exist for learning purposes.
    /// </summary>
    public static class MatrixTransformMath
    {
        /// <summary>
        /// Computes the x, y, and z rotation matrices and returns the combined rotation matrix.
        /// </summary>
        /// <returns>The combined rotation matrix</returns>
        public static Matrix4x4 Compute(float x, float y, float z)
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
    }
}
