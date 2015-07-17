using UnityEngine;
using System.Collections;

	public abstract class Humanoid : MonoBehaviour
	{
        public int HP { get; set; }
        public int Dano { get; set; }
        public float velocidadeMax { get; set; }
        public bool CanClimb { get; set; }

        public abstract void Mover();

	}

