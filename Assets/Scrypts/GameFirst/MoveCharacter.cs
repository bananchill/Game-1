using UnityEngine;

namespace Assets.Scrypts
{
    public class MoveCharacter : MonoBehaviour
    {
        Connection connection;
        private float x;
        private float z;

        void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                x = transform.position.x;
                z = transform.position.z + 1;
                transform.position = new Vector3(x, 0, z);
                //connection.Send(new Message(MessageType.COORDINATES, "W"));
                if (!Game.CheckChest(x, z))
                {
                    Debug.Log("Nice");
                }
            }

            if (Input.GetKey(KeyCode.S))
            {
                x = transform.position.x;
                z = transform.position.z - 1;
                transform.position = new Vector3(x, 0, z);
                //connection.Send(new Message(MessageType.COORDINATES, "S"));
                if (!Game.CheckChest(x, z))
                {
                    Debug.Log("Nice");
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                x = transform.position.x - 1;
                z = transform.position.z;
                transform.position = new Vector3(x, 0, z);
                //connection.Send(new Message(MessageType.COORDINATES, "A"));
                if (!Game.CheckChest(x, z))
                {
                    Debug.Log("Nice");
                }
            }

            if (Input.GetKey(KeyCode.D))
            {
                x = transform.position.x + 1;
                z = transform.position.z;
                transform.position = new Vector3(x, 0, z);
                //connection.Send(new Message(MessageType.COORDINATES, "D"));
                if (!Game.CheckChest(x, z))
                {
                    Debug.Log("Nice");
                }
            }
        }
    }
}