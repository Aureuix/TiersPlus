using GadgetCore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TiersPlus
{
    public class GravityBallScript : HazardScript
    {
        public bool Fallen;
        public float DecelerationRate;
        public float Speed;
        public Vector3 Dir;

        protected void InitDir(Vector3 dir)
        {
            Dir = dir;
        }

        protected void Update()
        {
            if (!Fallen)
            {
                Speed -= DecelerationRate * Time.deltaTime;
                if (Speed <= 0)
                {
                    Speed = 0;
                    Dir = Vector3.down;
                    Fallen = true;
                }
            }
            else
            {
                Speed += 9.81f * Time.deltaTime;
                if (transform.position.y < -1000) Destroy(gameObject);
            }
        }

        protected void FixedUpdate()
        {
            transform.Translate(Dir * Speed * Time.fixedDeltaTime);
        }
    }
}
