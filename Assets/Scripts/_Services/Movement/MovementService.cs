using Data.Settings;
using Services.Essence;
using UnityEngine;
using Zenject;
using System.Linq;
using View;

namespace Services.Movement
{
    public class MovementService 
    {
        private readonly SignalBus _signalBus;
        private readonly MovementServiceSettings[] _movementServiceSettings;
        private MovementServiceSettings _settings;

        private Rigidbody _viewRigidbody;
        private CapsuleCollider _capsuleCollider;
        private LayerMask _groundLayers;
        public MovementService(SignalBus signalBus, MovementServiceSettings[] movementServiceSettings) 
        {
            _signalBus = signalBus;
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
            {
                _viewRigidbody = view.GetGameObject().GetComponent<Rigidbody>();
            }

            Vector3 movement = new Vector3(direction.x, 0.0f, direction.y);

            _viewRigidbody.AddForce(movement * (_settings.Move.Speed * 10f)); 
         
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
        public void RotateTowardsMovementDir(IView view, Vector2 direction) 
        {
            Vector3 movementVector = new Vector3(direction.x, 0, direction.y);
            if (movementVector.magnitude > 0.01f)
            {
                view.GetGameObject().transform.rotation = Quaternion.Slerp(view.GetGameObject().transform.rotation,
                    Quaternion.LookRotation(movementVector), Time.deltaTime * _settings.Rotate.Speed);
            }
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
            {
                return false;
            }
        }
    }

   
}