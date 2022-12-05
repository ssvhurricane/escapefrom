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

        private float _xRotation;

        public MovementService(SignalBus signalBus,
                               LogService logService,  
                               MovementServiceSettings[] movementServiceSettings) 
        {
            _signalBus = signalBus;
            _logService = logService;
            _movementServiceSettings = movementServiceSettings;
        }

        public MovementServiceSettings InitService(string settingsID)
        { 
           return _settings = _movementServiceSettings?.FirstOrDefault(_=>_.Id == settingsID);
        }

        public void Move(IView view, Vector2 direction, Rigidbody viewRigidbody)
        {
            if (viewRigidbody != null)
            {
                Vector3 targetVelocity = (view.GetGameObject().transform.right * direction.x)
                        + (view.GetGameObject().transform.forward * direction.y);

                if (IsGrounded(view, viewRigidbody))
                    viewRigidbody.AddForce(targetVelocity - viewRigidbody.velocity, ForceMode.VelocityChange);
                else
                    viewRigidbody.AddForce((targetVelocity - viewRigidbody.velocity) * _settings.Move.AirResistance, ForceMode.VelocityChange);
            }
        }

        /// <summary>
        /// Jump With Physics.
        /// </summary>
        /// <param name="view"></param>
        public void Jump(IView view, Rigidbody viewRigidbody)
        {
            if (viewRigidbody != null)
                if (IsGrounded(view, viewRigidbody))
                    viewRigidbody.AddForce(view.GetGameObject().transform.up * _settings.Jump.Speed, _settings.Jump.ForceMode);
        }

        /// <summary>
		/// Rotate towards the direction the character is moving.
		/// </summary>
        public void Rotate(IView view, Vector2 direction, Rigidbody viewRigidbody)
        {
            if (viewRigidbody != null)
                    viewRigidbody.MoveRotation(viewRigidbody.rotation * Quaternion.Euler(0f, direction.x * _settings.Rotate.Sensitivity * Time.smoothDeltaTime, 0f));
        }

        public void RotateWithClamp(IView view, Vector2 direction)
        {
             _xRotation -= direction.y * _settings.Rotate.Sensitivity * Time.smoothDeltaTime;
            
             _xRotation = Mathf.Clamp(_xRotation, _settings.Rotate.UpperLimit, _settings.Rotate.BottomLimit);
           
             view.GetGameObject().transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
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

        public void Parent(IView baseView, GameObject targetView, bool resetParent = false)
        {
            if (!resetParent)
                baseView.GetGameObject().transform.parent = targetView.transform;
            else
                baseView.GetGameObject().transform.parent = null;
        }

        public bool IsGrounded(IView view, Rigidbody viewRigidbody) 
        {
            if (viewRigidbody != null)
            {
                RaycastHit hitInfo;
                if (Physics.Raycast(viewRigidbody.worldCenterOfMass, Vector3.down, out hitInfo, _settings.Jump.Dis2Ground + 0.1f, _settings.Jump.GroundLayers))
                    return true;
            }
            
           return false;
        }
    }
}