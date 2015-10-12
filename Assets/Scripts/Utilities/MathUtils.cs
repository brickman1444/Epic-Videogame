using UnityEngine;
using System.Collections;

public static class MathUtils {

    #region Vectors
    /*
	 * These extension methods are for built in stuff that can't directly 
	 * be changed like rigidbody.velocity or transform.position
	 */
    public static Vector3 SetX(this Vector3 vec, float x)
    {
        Vector3 copyVec = vec;
        copyVec.x = x;
        return copyVec;
    }

    public static Vector3 SetY(this Vector3 vec, float y)
    {
        Vector3 copyVec = vec;
        copyVec.y = y;
        return copyVec;
    }

    public static Vector3 SetZ(this Vector3 vec, float z)
    {
        Vector3 copyVec = vec;
        copyVec.z = z;
        return copyVec;
    }
    #endregion
}
