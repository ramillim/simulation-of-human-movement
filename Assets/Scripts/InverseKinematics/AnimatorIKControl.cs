using UnityEngine;

namespace Simulation.InverseKinematics
{
    /// <summary>
    /// This is copied from https://docs.unity3d.com/Manual/InverseKinematics.html and uses the
    /// AnimatorIK API to control the IK of an Animator.
    /// </summary>
    public class AnimatorIKControl : MonoBehaviour
    {
        private Animator _animator;

        [SerializeField]
        private bool _ikActive;

        [SerializeField]
        private Transform _rightHandObj;

        [SerializeField]
        private Transform _lookObj;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        // a callback for calculating IK
        private void OnAnimatorIK(int layerIndex)
        {
            if (!_animator)
                return;
            //if the IK is active, set the position and rotation directly to the goal.
            if (_ikActive)
            {
                // Set the look target position, if one has been assigned
                if (_lookObj != null)
                {
                    _animator.SetLookAtWeight(1);
                    _animator.SetLookAtPosition(_lookObj.position);
                }

                // Set the right hand target position and rotation, if one has been assigned
                if (_rightHandObj != null)
                {
                    _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    _animator.SetIKPosition(AvatarIKGoal.RightHand, _rightHandObj.position);
                    _animator.SetIKRotation(AvatarIKGoal.RightHand, _rightHandObj.rotation);
                }
            }
            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                _animator.SetLookAtWeight(0);
            }
        }
    }
}
