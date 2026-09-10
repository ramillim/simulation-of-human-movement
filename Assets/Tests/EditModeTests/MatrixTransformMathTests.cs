using NUnit.Framework;
using UnityEngine;

namespace Simulation.EditModeTests
{
    [TestFixture]
    public class MatrixTransformMathTests
    {
        private const float Tolerance = 1e-4f;

        [Test]
        public void Compute_WithZeroAngles_ReturnsIdentity()
        {
            var matrix = MatrixTransformMath.Compute(0f, 0f, 0f);

            AssertMatrixApproximatelyEqual(Matrix4x4.identity, matrix);
        }

        [Test]
        public void Compute_90DegreesAboutZ_MapsXAxisToYAxis()
        {
            var rotated = Rotate(new Vector3(1f, 0f, 0f), 0f, 0f, 90f);

            AssertVectorApproximatelyEqual(new Vector3(0f, 1f, 0f), rotated);
        }

        [Test]
        public void Compute_90DegreesAboutX_MapsYAxisToZAxis()
        {
            var rotated = Rotate(new Vector3(0f, 1f, 0f), 90f, 0f, 0f);

            AssertVectorApproximatelyEqual(new Vector3(0f, 0f, 1f), rotated);
        }

        [Test]
        public void Compute_90DegreesAboutY_MapsZAxisToXAxis()
        {
            var rotated = Rotate(new Vector3(0f, 0f, 1f), 0f, 90f, 0f);

            AssertVectorApproximatelyEqual(new Vector3(1f, 0f, 0f), rotated);
        }

        [Test]
        public void Compute_360Degrees_ReturnsPointToOrigin()
        {
            var point = new Vector3(1.5f, -2.25f, 3f);

            var rotated = Rotate(point, 360f, 360f, 360f);

            AssertVectorApproximatelyEqual(point, rotated);
        }

        [Test]
        public void Compute_IsProductOfPerAxisMatricesInXyzOrder()
        {
            const float x = 30f;
            const float y = 45f;
            const float z = 60f;

            var expected = AxisX(x) * AxisY(y) * AxisZ(z);
            var actual = MatrixTransformMath.Compute(x, y, z);

            AssertMatrixApproximatelyEqual(expected, actual);
        }

        [Test]
        public void Compute_IsNotCommutative()
        {
            var xThenY = MatrixTransformMath.Compute(90f, 0f, 0f) * MatrixTransformMath.Compute(0f, 90f, 0f);
            var yThenX = MatrixTransformMath.Compute(0f, 90f, 0f) * MatrixTransformMath.Compute(90f, 0f, 0f);

            Assert.That(ApproximatelyEqual(xThenY, yThenX), Is.False,
                "Rotations about different axes should not commute");
        }

        [Test]
        public void Compute_PreservesVectorLength()
        {
            var point = new Vector3(3f, -4f, 12f);

            var rotated = Rotate(point, 17f, -128f, 244f);

            Assert.That(rotated.magnitude, Is.EqualTo(point.magnitude).Within(1e-3f));
        }

        private static Vector3 Rotate(Vector3 point, float x, float y, float z) =>
            MatrixTransformMath.Compute(x, y, z).MultiplyPoint3x4(point);

        private static Matrix4x4 AxisX(float degrees)
        {
            var radians = degrees * Mathf.Deg2Rad;
            var matrix = Matrix4x4.identity;
            matrix[1, 1] = Mathf.Cos(radians);
            matrix[1, 2] = -Mathf.Sin(radians);
            matrix[2, 1] = Mathf.Sin(radians);
            matrix[2, 2] = Mathf.Cos(radians);
            return matrix;
        }

        private static Matrix4x4 AxisY(float degrees)
        {
            var radians = degrees * Mathf.Deg2Rad;
            var matrix = Matrix4x4.identity;
            matrix[0, 0] = Mathf.Cos(radians);
            matrix[0, 2] = Mathf.Sin(radians);
            matrix[2, 0] = -Mathf.Sin(radians);
            matrix[2, 2] = Mathf.Cos(radians);
            return matrix;
        }

        private static Matrix4x4 AxisZ(float degrees)
        {
            var radians = degrees * Mathf.Deg2Rad;
            var matrix = Matrix4x4.identity;
            matrix[0, 0] = Mathf.Cos(radians);
            matrix[0, 1] = -Mathf.Sin(radians);
            matrix[1, 0] = Mathf.Sin(radians);
            matrix[1, 1] = Mathf.Cos(radians);
            return matrix;
        }

        private static bool ApproximatelyEqual(Matrix4x4 expected, Matrix4x4 actual)
        {
            for (var i = 0; i < 16; i++)
            {
                if (Mathf.Abs(expected[i] - actual[i]) > Tolerance)
                    return false;
            }

            return true;
        }

        private static void AssertMatrixApproximatelyEqual(Matrix4x4 expected, Matrix4x4 actual)
        {
            for (var i = 0; i < 16; i++)
            {
                Assert.That(actual[i], Is.EqualTo(expected[i]).Within(Tolerance),
                    $"Element {i} differs. Expected:\n{expected}\nActual:\n{actual}");
            }
        }

        private static void AssertVectorApproximatelyEqual(Vector3 expected, Vector3 actual)
        {
            Assert.That(Vector3.Distance(expected, actual), Is.LessThan(Tolerance),
                $"Expected {expected} but was {actual}");
        }
    }
}
