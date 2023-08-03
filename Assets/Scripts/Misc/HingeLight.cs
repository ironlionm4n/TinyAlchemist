using UnityEngine;
using Random = UnityEngine.Random;

namespace Misc
{
    public class HingeLight : MonoBehaviour
    {
        [SerializeField] private float motorSpeed;
        [SerializeField] private float minForwardMotorTime;
        [SerializeField] private float maxForwardMotorTime;
        [SerializeField] private float minBackwardMotorTime;
        [SerializeField] private float maxBackwardMotorTime;
        [SerializeField] private HingeJoint2D hingeJoint2D;
    
        private float _currentForwardMotorTime;
        private float _currentBackwardMotorTime;
        private JointMotor2D _jointMotor2D;
        private int _moveDirection;
        private void Start()
        {
            _moveDirection = Random.Range(0, 2);
            motorSpeed *= _moveDirection == 1 ? -1 : 1;
            _jointMotor2D = new JointMotor2D
            {
                motorSpeed = motorSpeed,
                maxMotorTorque = 10000
            };
            hingeJoint2D.motor = _jointMotor2D;
            hingeJoint2D.useMotor = true;
            _currentForwardMotorTime = Random.Range(minForwardMotorTime, maxForwardMotorTime);
            _currentBackwardMotorTime = Random.Range(minBackwardMotorTime, maxBackwardMotorTime);
        }

        private void Update()
        {
            if (_moveDirection == 1)
            {
                _currentForwardMotorTime -= Time.deltaTime;
                if (_currentForwardMotorTime <= 0)
                {
                    motorSpeed *= -1;
                    _moveDirection = 0;
                    _currentForwardMotorTime = Random.Range(minForwardMotorTime, maxForwardMotorTime);
                }
            }
            else
            {
                _currentBackwardMotorTime -= Time.deltaTime;
                if (_currentBackwardMotorTime <= 0)
                {
                    motorSpeed *= -1;
                    _moveDirection = 1;
                    _currentBackwardMotorTime = Random.Range(minBackwardMotorTime, maxBackwardMotorTime);
                }
            }

            _jointMotor2D.motorSpeed = motorSpeed;
            hingeJoint2D.motor = _jointMotor2D;
        }
    }
}
