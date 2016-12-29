using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {

        private Rigidbody playerRB;
        private Transform playerT;

        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

        public float maxShootDistance;
        LineRenderer laser;
        public Light laserLight;
        float timer;
        float displayTime;
        //player health
        float health;
        //player fire rate
        public float timeBetweenBullets;
        public float damage;

        private bool isDead;
        private bool isFiring;
        private bool isKicking;

        private void Start()
        {
            playerRB = GetComponent<Rigidbody>();
            playerT = transform;
            isFiring = false;
            isDead = false;
            //get linerenderer
            laser = GetComponent<LineRenderer>();
            laser.enabled = false;
            laserLight.enabled = false;
            //father chronos was here
            timer = 0.0f;
            displayTime = 0.1f * Time.deltaTime;

            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
                
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {

            timer += Time.deltaTime;
            if (!isDead)
            {
                Start();
                //shoot things
                ShootZombus();
            }
            if (timer >= displayTime * timeBetweenBullets)
            {
                DisableEffects();
            }

            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }
        void ShootZombus()
        {
            if (Input.GetMouseButton(0) && timer >= timeBetweenBullets)//left button pressed
            {
                isFiring = true;
            }
            else
            {
                isFiring = false;
            }
            if (isFiring)
            {
                timer = 0.0f;
                laser.enabled = true; //activate the line renderer
                laser.SetPosition(0, transform.position + transform.forward * 1.0f + transform.up * 0.5f + transform.right * 0.35f);
                laserLight.enabled = true;

                Vector3 rayPos = playerT.position;
                Vector3 rayDir = playerT.forward;
                RaycastHit shootHit;
                Ray shootRay = new Ray(rayPos, rayDir);
                if (Physics.Raycast(shootRay, out shootHit, maxShootDistance, LayerMask.GetMask("shootable")))
                {
                    laser.SetPosition(1, shootHit.point);
                   
                }
                else //if it does not hit
                {
                    laser.SetPosition(1, shootRay.origin + shootRay.direction * maxShootDistance);
                }
            }
        }

        public void DisableEffects()
        {
            //turn of line renderer and laser light
            laser.enabled = false;
            laserLight.enabled = false;
        }
      
    }


}
