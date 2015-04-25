using UnityEngine;
using System.Collections;


namespace Jolly
{
	public static class Vector2Ext
	{
		public static Vector3 xyz(this Vector2 self, float z)
		{
			return new Vector3(self.x, self.y, z);
		}
	}
}

