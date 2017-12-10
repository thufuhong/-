using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;
        public Transform attaction_generate_position;
        public GameObject attack_object;
        public bool active = false;

        private Animator m_Animator;
        private attribute _attri;
        private float CoolDownTime = -1f;
        private Vector3 original_position;
        private float stopping_distance;

        private void Start()
        {
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
            m_Animator = GetComponent<Animator>();
            _attri = GetComponent<attribute>();

            agent.updateRotation = true;
	        agent.updatePosition = true;
            original_position = transform.position;
            stopping_distance = agent.stoppingDistance;
        }

        void OnParticleCollision(GameObject colli)
        {
            
            if (colli.name == "Skill_4 Partical")
            {
                GameObject player = colli.transform.parent.gameObject;
                float damage = player.GetComponent<ThirdPersonUserControl>().Skill_4_Value[0] + player.GetComponent<ThirdPersonUserControl>().Skill_4_Value[3] * player.GetComponent<attribute>().Skill_Level[3];
                float _t = this.gameObject.GetComponent<attribute>().HP;
                float _c = this.gameObject.GetComponent<attribute>().update_HP(Mathf.Min(-damage + this.gameObject.GetComponent<attribute>().DEF,0));
                if (_t>=0 && _c<= 0)
                {
                    player.GetComponent<attribute>().update_EXP(this.gameObject.GetComponent<attribute>().DropEXP);
                    // player.GetComponent<attribute>().gold += this.gameObject.GetComponent<attribute>().DropGold;
					// can't get gold directly, produce a gold coin
					this.gameObject.GetComponent<attribute>().dropGoods("coin");
                }
            }
        }

        private void Update()
        {

        }


        private void FixedUpdate()
        {
            //this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 0f, this.gameObject.transform.position.z);
            if (CoolDownTime > 0)
                CoolDownTime -= Time.fixedDeltaTime;
            if (!(this.gameObject.GetComponent<attribute>().ifAlive))
                return;
            if (active)
            {
                agent.SetDestination(target.position);
                agent.stoppingDistance = stopping_distance;
                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    // chase the player
                    
                    
                    character.Move(agent.desiredVelocity, false, false);
                }
                else
                {
                    character.Move(Vector3.zero, false, false);
                    
                    if (target.gameObject.GetComponent<attribute>().ifAlive == true )
                    {
                        // rotate toward player

                        // Fire toward player
                        int state_now = m_Animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
                        if (CoolDownTime <= 0 && state_now != Animator.StringToHash("stun"))
                        {
                            // Turn 
                            Vector3 _AI_face_toward = this.gameObject.transform.forward;
                            _AI_face_toward.y = 0;
                            Vector3 _Player_derection = target.position - this.gameObject.transform.position;
                            _Player_derection.y = 0;
                            float TurnAmount = Mathf.Acos(Vector3.Dot(_AI_face_toward.normalized, _Player_derection.normalized));
                            this.gameObject.transform.Rotate(this.gameObject.transform.up,
                                TurnAmount * Mathf.Rad2Deg);

                            CoolDownTime = _attri.FireRate;
                            m_Animator.SetFloat("Random", UnityEngine.Random.Range(0f, 1f));
                            m_Animator.SetTrigger("attack");
                            GameObject temp = Instantiate(attack_object, attaction_generate_position.position, attaction_generate_position.rotation);
                            temp.GetComponent<ballistic>().speed = _attri.BallisticSpeed;
                            temp.GetComponent<ballistic>().side = _attri.team;
                            temp.GetComponent<ballistic>().damage = _attri.ATK + UnityEngine.Random.Range(-3, 3);
                            temp.GetComponent<ballistic>().From = this.gameObject;
                            temp.GetComponent<Rigidbody>().velocity = (target.position - attaction_generate_position.position).normalized * _attri.BallisticSpeed;
                            temp.tag = "Team1";
                        }
                    }
                    
                }
            }
            else
            {
                agent.SetDestination(original_position);
                agent.stoppingDistance = 0.5f;
                if (agent.remainingDistance > stopping_distance)
                {
                    character.Move(agent.desiredVelocity, false, false);
                }
                else
                {
                    character.Move(Vector3.zero, false, false);
                }
            }
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
