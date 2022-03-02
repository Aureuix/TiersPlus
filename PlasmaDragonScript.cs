using UnityEngine;
using GadgetCore.API;

namespace TiersPlus
{
	public class PlasmaDragonScript : CustomEntityScript<PlasmaDragonScript>
	{
		public PlasmaDragonBehaviorMode BehaviorMode
        {
            get
            {
                return IsMaster ? m_BehaviorMode : Master.BehaviorMode;
            }
            protected set
            {
                m_BehaviorMode = value;
                if (!IsMaster) Master.m_BehaviorMode = value;
            }
        }
        private PlasmaDragonBehaviorMode m_BehaviorMode;

        public bool isMainHead;
		public GameObject parent;

        protected float targetSpeed;
        protected float acceleration;
        protected float speed;
        protected float targetAngle;
        protected float turnSpeed;

        protected float modeSwitchTime;
        protected bool targetOnRight;

        protected void Awake()
        {
			if (isMainHead)
            {
				Initialize(60000, 20, 1000 + GameScript.challengeLevel * 500, true, true, true);
            }
			else
            {
				SetMaster(parent, true);
            }
            MaxFollowDistance = 5;
            BurnEffect = 50;
		}

        protected void Update()
        {
            if (IsDead) return;
            if (IsMaster && Network.isServer) UpdateAI();
        }

        protected void FixedUpdate()
        {
            if (IsDead) return;
            if (IsMaster) UpdateMove();
        }

        protected void UpdateAI()
        {
            if (AttackTarget == null) BehaviorMode = PlasmaDragonBehaviorMode.IDLE;
            else if ((AttackTarget.transform.position - transform.position).magnitude > 50) BehaviorMode = PlasmaDragonBehaviorMode.APPROACH;
            switch (BehaviorMode)
            {
                case PlasmaDragonBehaviorMode.IDLE:
                    if (AttackTarget != null)
                    {
                        SetBehaviorMode(PlasmaDragonBehaviorMode.APPROACH);
                    }
                    break;
                case PlasmaDragonBehaviorMode.APPROACH:
                    if ((AttackTarget.transform.position - transform.position).magnitude < 25)
                    {
                        SetBehaviorMode(PlasmaDragonBehaviorMode.INITIAL_DROP);
                    }
                    break;
                case PlasmaDragonBehaviorMode.INITIAL_DROP:
                    if (AttackTarget.transform.position.y - transform.position.y > 25)
                    {
                        SetBehaviorMode(PlasmaDragonBehaviorMode.FOLLOW_BELOW);
                    }
                    break;
                case PlasmaDragonBehaviorMode.FOLLOW_ABOVE:
                    if (GetTimeSinceModeSwitch() > 1)
                    {
                        BurstBalls();
                        SetBehaviorMode(PlasmaDragonBehaviorMode.DROPPING);
                    }
                    break;
                case PlasmaDragonBehaviorMode.FOLLOW_BELOW:
                    if ((AttackTarget.transform.position.x - transform.position.x) * (targetOnRight ? -1 : 1) > 10)
                    {
                        SetBehaviorMode(PlasmaDragonBehaviorMode.LEAPING);
                    }
                    break;
                case PlasmaDragonBehaviorMode.LEAPING:
                    if (AttackTarget.transform.position.y - transform.position.y < -5)
                    {
                        SetBehaviorMode(PlasmaDragonBehaviorMode.FOLLOW_ABOVE);
                    }
                    break;
                case PlasmaDragonBehaviorMode.DROPPING:
                    if (AttackTarget.transform.position.y - transform.position.y > 25)
                    {
                        SetBehaviorMode(PlasmaDragonBehaviorMode.FOLLOW_BELOW);
                    }
                    break;
            }
        }

        protected void BurstBalls()
        {
            GetComponent<NetworkView>().RPC("Au", RPCMode.All);
            GetComponent<NetworkView>().RPC("RPCBurstBalls", RPCMode.All);
        }

        [RPC]
        protected void RPCBurstBalls()
        {
            Instantiate(GadgetCoreAPI.GetProjectileResource("PlasmaDragonBall"), transform.position, Quaternion.identity).SendMessage("InitDir", new Vector3(0, 1).normalized, SendMessageOptions.DontRequireReceiver);
            Instantiate(GadgetCoreAPI.GetProjectileResource("PlasmaDragonBall"), transform.position, Quaternion.identity).SendMessage("InitDir", new Vector3(1, 1).normalized, SendMessageOptions.DontRequireReceiver);
            Instantiate(GadgetCoreAPI.GetProjectileResource("PlasmaDragonBall"), transform.position, Quaternion.identity).SendMessage("InitDir", new Vector3(1, 0).normalized, SendMessageOptions.DontRequireReceiver);
            Instantiate(GadgetCoreAPI.GetProjectileResource("PlasmaDragonBall"), transform.position, Quaternion.identity).SendMessage("InitDir", new Vector3(1, -1).normalized, SendMessageOptions.DontRequireReceiver);
            Instantiate(GadgetCoreAPI.GetProjectileResource("PlasmaDragonBall"), transform.position, Quaternion.identity).SendMessage("InitDir", new Vector3(0, -1).normalized, SendMessageOptions.DontRequireReceiver);
            Instantiate(GadgetCoreAPI.GetProjectileResource("PlasmaDragonBall"), transform.position, Quaternion.identity).SendMessage("InitDir", new Vector3(-1, -1).normalized, SendMessageOptions.DontRequireReceiver);
            Instantiate(GadgetCoreAPI.GetProjectileResource("PlasmaDragonBall"), transform.position, Quaternion.identity).SendMessage("InitDir", new Vector3(-1, 0).normalized, SendMessageOptions.DontRequireReceiver);
            Instantiate(GadgetCoreAPI.GetProjectileResource("PlasmaDragonBall"), transform.position, Quaternion.identity).SendMessage("InitDir", new Vector3(-1, 1).normalized, SendMessageOptions.DontRequireReceiver);
        }

        [RPC]
        protected void Au()
        {
            GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Au/des"), Menuu.soundLevel / 10f);
        }

        protected void SetBehaviorMode(PlasmaDragonBehaviorMode mode)
        {
            GetComponent<NetworkView>().RPC("RPCSetBehaviorMode", RPCMode.All, (int) mode);
        }

        [RPC]
        protected void RPCSetBehaviorMode(int mode)
        {
            if (AttackTarget != null) targetOnRight = AttackTarget.transform.position.x - transform.position.x > 0;
            BehaviorMode = (PlasmaDragonBehaviorMode) mode;
            modeSwitchTime = Time.time;
        }

        protected float GetTimeSinceModeSwitch()
        {
            return Time.time - modeSwitchTime;
        }

        protected void UpdateMove()
        {
            if (BehaviorMode != PlasmaDragonBehaviorMode.IDLE && AttackTarget == null) return;
            float currentAngle = transform.rotation.eulerAngles.z;
            switch (BehaviorMode)
            {
                case PlasmaDragonBehaviorMode.IDLE:
                    targetSpeed = 0f;
                    acceleration = float.MaxValue;
                    turnSpeed = 900f;
                    break;
                case PlasmaDragonBehaviorMode.APPROACH:
                    targetSpeed = 20f;
                    acceleration = 20f;
                    turnSpeed = 900f;
                    targetAngle = Mathf.Atan2(AttackTarget.transform.position.y - transform.position.y, AttackTarget.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
                    break;
                case PlasmaDragonBehaviorMode.INITIAL_DROP:
                    targetSpeed = 40f;
                    acceleration = 20f;
                    turnSpeed = 90f;
                    targetAngle = targetOnRight ? 280 : 260;
                    break;
                case PlasmaDragonBehaviorMode.FOLLOW_ABOVE:
                    targetSpeed = 10f;
                    acceleration = 10f;
                    turnSpeed = 900f;
                    targetAngle = targetOnRight ? 0 : 180;
                    break;
                case PlasmaDragonBehaviorMode.FOLLOW_BELOW:
                    targetSpeed = 20f;
                    acceleration = 20f;
                    turnSpeed = 900f;
                    targetAngle = targetOnRight ? 0 : 180;
                    break;
                case PlasmaDragonBehaviorMode.LEAPING:
                    targetSpeed = 40f;
                    acceleration = 40f;
                    turnSpeed = 900f;
                    targetAngle = targetOnRight ? 80 : 100;
                    break;
                case PlasmaDragonBehaviorMode.DROPPING:
                    targetSpeed = 40f;
                    acceleration = 20f;
                    turnSpeed = 90f;
                    targetAngle = targetOnRight ? 260 : 280;
                    break;
            }
            if (turnSpeed > 0)
            {
                float newAngle = currentAngle;
                if (targetAngle < 0) targetAngle += 360;
                if (currentAngle - targetAngle < -180) currentAngle += 360;
                else if (currentAngle - targetAngle > 180) currentAngle -= 360;
                if (currentAngle > targetAngle) newAngle = Mathf.Max(currentAngle - turnSpeed * Time.fixedDeltaTime, targetAngle);
                else if (currentAngle < targetAngle) newAngle = Mathf.Min(currentAngle + turnSpeed * Time.fixedDeltaTime, targetAngle);
                if (newAngle != currentAngle)
                {
                    if (newAngle < 0) newAngle += 360;
                    else if (newAngle >= 360) newAngle -= 360;
                    transform.rotation = Quaternion.Euler(0, 0, newAngle);
                }
            }
            if (speed != targetSpeed)
            {
                if (speed > targetSpeed) speed = Mathf.Max(speed - acceleration * Time.fixedDeltaTime, targetSpeed);
                else if (speed < targetSpeed) speed = Mathf.Min(speed + acceleration * Time.fixedDeltaTime, targetSpeed);
            }
            if (speed > 0)
            {
                transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
            }
        }

        public enum PlasmaDragonBehaviorMode
        {
			IDLE,
			APPROACH,
            INITIAL_DROP,
			FOLLOW_ABOVE,
			FOLLOW_BELOW,
			LEAPING,
			DROPPING
        }
    }
}
