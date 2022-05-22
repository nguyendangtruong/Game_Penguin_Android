
    using Dypsloom.DypThePenguin.Scripts.Damage;
    using Dypsloom.DypThePenguin.Scripts.Items;
    using System;
    using System.Threading.Tasks;
    using UnityEngine;
    using CharacterController = UnityEngine.CharacterController;

    /// <summary>
    /// The character controller.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class Character : MonoBehaviour
    {
        public event Action OnDie;

        [Tooltip("The character speed in units/second.")]
        [SerializeField] protected float m_Speed = 1f;
        [Tooltip("The gravity.")]
        [SerializeField] protected float m_Gravity = 1f;
        [Tooltip("Grounded error correction time.")]
        [SerializeField] protected float m_AdditionalGroundedTime = 0.5f;
        [Tooltip("Grounded Raycast length.")]
        [SerializeField] protected float m_GroundRaycastLength = 0.3f;
        [Tooltip("The character jump force.")]
        [SerializeField] protected float m_JumpForce = 1f;
        [Tooltip("The gravity modifier while pressing the jump button.")]
        [SerializeField] protected float m_JumpFallOff = 1f;
        [Tooltip("The character speed in units/second.")]
        [SerializeField] protected Transform m_SpawnTransform;
        [Tooltip("The delay between death and respawn.")]
        [SerializeField] protected float m_RespawnDelay = 4f;
        [Tooltip("The character speed in units/second.")]
        [SerializeField] protected float m_PushPower = 2.0f;
        [Tooltip("The transform where the projectiles thrown will spawn From.")]
        [SerializeField] protected Transform m_ProjectilesSpawnPoint;
        [Tooltip("Death Effect.")]
        [SerializeField] protected GameObject m_DeathEffects;
    


    protected Rigidbody m_Rigidbody;
        protected CharacterController m_CharacterController;
        protected Animator m_Animator;
        protected Inventory m_Inventory;
        protected IDamageable m_CharacterDamageable;

        protected ICharacterMover m_CharacterMover;
        protected CharacterRotator m_CharacterRotator;
        protected ICharacterInput m_CharacterInput;
        protected ICharacterAnimator m_CharacterAnimator;

        protected Camera m_Camera;
        protected bool m_IsDead;
        protected Task m_DeathTask;
        private float m_GroundedTimer;

        public float Speed => m_Speed;
        public float SetSpeed { set { m_Speed = value; } }
        public float JumpForce => m_JumpForce;
        public float JumpFallOff => m_JumpFallOff;
        public float Gravity => m_Gravity;
        public Camera CharacterCamera => m_Camera;

        public Transform ProjectilesSpawnPoint => m_ProjectilesSpawnPoint;

        public Rigidbody Rigidbody => m_Rigidbody;
        public Animator Animator => m_Animator;
        public CharacterController CharacterController => m_CharacterController;

        public ICharacterInput CharacterInput => m_CharacterInput;
        public IDamageable CharacterDamageable => m_CharacterDamageable;
        public ICharacterMover CharacterMover => m_CharacterMover;
        public ICharacterAnimator CharacterAnimator => m_CharacterAnimator;
        public Inventory Inventory => m_Inventory;
        public bool IsDead => m_IsDead;
        public bool IsGrounded
        {
            get => m_GroundedTimer >= Time.time;
            set => m_GroundedTimer = value ? Time.time + m_AdditionalGroundedTime : 0;
        }

        /// <summary>
        /// Initialize all the properties.
        /// </summary>
        protected virtual void Awake()
        {
            m_Camera = Camera.main;

            m_Rigidbody = GetComponent<Rigidbody>();
            m_CharacterController = GetComponent<CharacterController>();
            m_Animator = GetComponent<Animator>();
            m_Inventory = GetComponent<Inventory>();
            m_CharacterDamageable = GetComponent<IDamageable>();

            AssignCharacterControllers();
        }

        /// <summary>
        /// Assign the controllers for your character.
        /// </summary>
        protected virtual void AssignCharacterControllers()
        {
            m_CharacterMover = new CharacterMover(this);
            m_CharacterRotator = new CharacterRotator(this);
            m_CharacterAnimator = new CharacterAnimator(this);
            m_CharacterInput = new CharacterInput(this);
        }

        /// <summary>
        /// Tick all the properties which needs to update every frame.
        /// </summary>
        protected virtual void Update()
        {
       
        if (m_CharacterController.isGrounded)
            {
                IsGrounded = true;
            }
            else if (
              Physics.Raycast(transform.position + transform.up * 0.5f, -1f * transform.up, out var hit,
                  m_GroundRaycastLength + 0.5f, int.MaxValue, QueryTriggerInteraction.Ignore))
            {
                if (m_CharacterMover.IsJumping == false)
                {
                    IsGrounded = true;
                }
                else
                {
                }

            }

            m_CharacterMover.Tick();
            m_CharacterRotator.Tick();
            m_CharacterAnimator.Tick();

        
            ControllerCharacterMove(); //hàm điều khiển nhân vật di chuyển

        



    }


    private void ControllerCharacterMove()
    {
        if (m_Rigidbody.position.y < -1f)
        {
            //nhân vật rơi xuống và DEAD
            FindObjectOfType<GameManager>().EndGame();
            FindObjectOfType<GameManager>().SettypeDeath();

        }

        tap = swipeLeft = swipeRight = swipeDown = swipeUp = false;


        #region Standalone  Input
        if (Input.GetMouseButtonDown(0) )
        {
            tap = true;
            isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }
        #endregion

        #region Mobile  Input
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isDraging = true;
                startTouch = Input.mousePosition;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }

        }
        #endregion

        #region Tính Distance
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;

            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }
        if (swipeDelta.magnitude > 10)// OR DEADZONE=1000f
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
                    swipeLeft = true;
                }
                else
                {
                    swipeRight = true;

                }
            }
            else
            {
                if (y < 0)
                {
                    swipeDown = true;
                }
                else
                {
                    swipeUp = true;

                }

            }
            //startTouch = swipeDelta = Vector2.zero;
            Reset();
        }
        #endregion

        #region Move di chuyển Mobile
        //di chuyển qua trái, qua phải
        //if (Input.touchCount > 0)
        //{

        //    touch = Input.GetTouch(0);
        //    if (touch.phase == TouchPhase.Moved)
        //    {
        //        transform.position = new Vector3(
        //            transform.position.x + touch.deltaPosition.x * speedModifier,
        //            transform.position.y,
        //            transform.position.z);
        //    }
        //}
        #endregion


        if (swipeLeft == true )//kiểm tra biến, gán giá trị float và truyền qua ChaterInput
        {
            swipeControlsLeftRight = -1f;
        }
        
        if (swipeRight == true)
        {
            swipeControlsLeftRight = 1f;
        }
        
        
        
        


    }

    void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
        swipeControlsLeftRight = 0;
    }

 

    public static Character Instance { set; get; }
    // Start is called before the first frame update
    private bool tap, swipeLeft, swipeRight, swipeDown, swipeUp;
    private Vector2 swipeDelta, startTouch;
    private bool isDraging = false;
    private float swipeControlsLeftRight = 0;
    private float moveForwardBehind = 1f;

    public bool Tap { get { return tap; } }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeDown { get { return swipeDown; } }
    public bool SwipeUp { get { return swipeUp; } }
    public float SwipeControlsLeftRight { get { return swipeControlsLeftRight; } }
    public float MoveForwardBehind { get { return moveForwardBehind; } }
    public float SetMoveForwardBehind { set { moveForwardBehind = value; } }






    /// <summary>
    /// Move objects when the character controller hits a collider.
    /// </summary>
    /// <param name="hit">The controller collider hit object.</param>
    protected virtual void OnControllerColliderHit(ControllerColliderHit hit)
     {
            var body = hit.collider.attachedRigidbody;

            // no rigidbody
            if (body == null || body.isKinematic) { return; }

            Vector3 force;
            // We use gravity and weight to push things down, we use
            // our velocity and push power to push things other directions
            if (hit.moveDirection.y < -0.3)
            {
                force = (new Vector3(0.1f, -0.5f, 0.1f) + hit.controller.velocity) * m_Rigidbody.mass;
            }
            else
            {
                force = m_CharacterMover.CharacterInputMovement * m_PushPower;
            }

            // Apply the push
            body.AddForceAtPosition(force, hit.point);
        }

        /// <summary>
        /// Make the character die.
        /// </summary>
        /// Được gọi bên GameManager
        public virtual void Die()
        {
            if (m_IsDead || m_DeathTask != null) { return; }
            m_DeathTask = ScheduleDeathRespawn();


    }
    /// <summary>
    /// Respawn 
    /// </summary>
    /// <returns>Return the asynchronous task.</returns>
    private void Start()
    {
        m_IsDead = false;
    }
    protected virtual async Task ScheduleDeathRespawn()
        {
        Invoke("HealEffect", 3f ); //delay hiển thị hiệu ứng sau khi die


        CharacterAnimator.Die(true);
            m_IsDead = true;

            await Task.Delay(100);
            m_DeathEffects?.SetActive(true);

            await Task.Delay((int)(m_RespawnDelay * 1000f) - 600); 
            gameObject.SetActive(false);
            OnDie?.Invoke();

        await Task.Delay(500);
            m_DeathTask = null;
            Respawn();
       
    }
    //hiệu ứng ParticleSystem HealEffect khi DIE
    protected void HealEffect()
    {
        ParticleSystem m_HealEffects = GameObject.Find("HealEffect").GetComponent<ParticleSystem>();
        m_HealEffects.Play();
    }

    /// <summary>
    /// Respawn the character.
    /// </summary>
    protected virtual void Respawn()
        {
            m_DeathEffects?.SetActive(false);
            CharacterAnimator.Die(false);
            m_CharacterDamageable.Heal(int.MaxValue);
            transform.position = m_SpawnTransform != null ? m_SpawnTransform.position : new Vector3(0, 1, 0);
            gameObject.SetActive(true);
            m_IsDead = false;

    }
}

