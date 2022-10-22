using Data.Settings;
using UnityEngine;
using Zenject;
using System.Linq;
using View;
using Services.Log;

namespace Services.Movement
{
    public class MovementService 
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;

        private readonly MovementServiceSettings[] _movementServiceSettings;
        private MovementServiceSettings _settings;

        private Rigidbody _viewRigidbody;
        private CapsuleCollider _capsuleCollider;
        private LayerMask _groundLayers;

        float _cameraVerticalAngle, cameraHorizontalAngle = 0f;
        public MovementService(SignalBus signalBus,
                               LogService logService,  
                               MovementServiceSettings[] movementServiceSettings) 
        {
            _signalBus = signalBus;
            _logService = logService;
            _movementServiceSettings = movementServiceSettings;
        }

        public void InitService(string settingsID)
        { 
            _settings = _movementServiceSettings?.
                FirstOrDefault(_=>_.Id == settingsID);
        }

        /// <summary>
        /// Base Move View GameObject.
        /// </summary>
        /// <param name="view">BaseView</param>
        /// <param name="direction">Direction moving</param>
        public void Move(IView view, Vector2 direction) 
        {
            float scaledMoveSpeed = _settings.Move.Speed * Time.deltaTime;

            Vector3 moveDirection = new Vector3(direction.x, 0.0f, direction.y);
          
            view.GetGameObject().transform.position += moveDirection * scaledMoveSpeed;
        }

        public void MoveWithTranslate(IView view, Vector2 direction) 
        {
            float scaledMoveSpeed = _settings.Move.Speed * Time.fixedDeltaTime;

            Vector3 moveDirection = new Vector3(direction.x, 0.0f, direction.y);

            view.GetGameObject().transform.Translate(moveDirection * scaledMoveSpeed);
        }

        public void MoveWithPhysics(IView view, Vector2 direction) 
        {
           if (_viewRigidbody == null)
                _viewRigidbody = view.GetGameObject().GetComponent<Rigidbody>();
            
            Vector3 currentVelocity = _viewRigidbody.velocity;
            Vector3 targetVelocity = new Vector3(direction.x, 0.0f, direction.y);
            targetVelocity *= _settings.Move.Speed;

            Vector3 velocityChange = targetVelocity - currentVelocity;

            Vector3.ClampMagnitude(velocityChange, 10f);

            _viewRigidbody.AddForce(velocityChange, ForceMode.VelocityChange); 
        }

        /// <summary>
        /// Jump With Physics.
        /// </summary>
        /// <param name="view"></param>
        public void Jump(IView view)
        {
            if (_viewRigidbody == null) 
            {
                _viewRigidbody = view.GetGameObject().GetComponent<Rigidbody>();
            }

            if (_capsuleCollider == null)
            {
                _capsuleCollider = view.GetGameObject().GetComponent<CapsuleCollider>();
            }
          
            if (IsGrounded())
                _viewRigidbody.AddForce(view.GetGameObject().transform.up * _settings.Jump.Speed, _settings.Jump.ForceMode);
        }

        /// <summary>
		/// Rotate towards the direction the character is moving.
		/// </summary>
        public void Rotate(IView view, Vector2 direction) 
        {
            view.GetGameObject().transform.Rotate(0f, direction.x * .1f, 0f, Space.Self);
        }

        public void RotateWithClamp(IView view, Vector2 direction)
        {
             _cameraVerticalAngle += (-direction.y) * .1f;
            
             _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, -90f, 90f);
           
             view.GetGameObject().transform.localEulerAngles = new Vector3(_cameraVerticalAngle, 0, 0);
        }

        public void Follow(IView baseView, IView targetView, Vector3 followOffset, Vector3 position, float followSpeed) 
        {
            var smoothedPosition = Vector3.Lerp(baseView.GetGameObject().transform.position,
                                                (targetView.GetGameObject().transform.position + followOffset),
                                                followSpeed);

           baseView.GetGameObject().transform.position = smoothedPosition + position;
        }

        public void Parent(IView baseView, IView targetView, bool resetParent = false) 
        {
            if(!resetParent)
                 baseView.GetGameObject().transform.parent = targetView.GetGameObject().transform;
            else
                 baseView.GetGameObject().transform.parent = null;
        }

        public bool IsGrounded() 
        {
            if(_capsuleCollider != null) 
            { 
                return Physics.CheckCapsule(_capsuleCollider.bounds.center, 
                        new Vector3(_capsuleCollider.bounds.center.x,_capsuleCollider.bounds.min.y, _capsuleCollider.bounds.center.z)
                        ,_capsuleCollider.radius * _settings.Jump.RadiusModifier, _settings.Jump.GroundLayers
                        );
            }
            else 
                return false;
        }
    }
}