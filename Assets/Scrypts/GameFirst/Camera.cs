using UnityEngine;

namespace Assets.Scrypts
{
	class Camera : MonoBehaviour
	{
		public GameObject character;

		private Vector3 offset;
		private float damping = 2.5f;

		void Start()
		{
			offset = transform.position - character.transform.position;
		}

		void Update()
		{
			Vector3 target = character.transform.position + offset;
			Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
			transform.position = currentPosition;
		}
	}
}
