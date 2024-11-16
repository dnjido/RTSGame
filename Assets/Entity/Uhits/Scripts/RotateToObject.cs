using UnityEngine;

namespace RTS
{
    public class RotateToObject
    {
        public static Quaternion Rotate(Vector3 start, Vector3 end, Quaternion rotate, float speed)
        {
            Vector3 dir = start - end;

            Quaternion rot = Quaternion.LookRotation(dir);
            rot.x = 0; rot.z = 0;

            return Quaternion.Slerp(rotate, rot, speed * Time.deltaTime);
        }
        public static Quaternion RotateFast(Vector3 start, Vector3 end)
        {
            Vector3 dir = start - end;

            Quaternion rot = Quaternion.LookRotation(dir);

            return rot;
        }
    }
}
