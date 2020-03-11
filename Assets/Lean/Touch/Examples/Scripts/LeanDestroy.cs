using UnityEngine;
using GoogleARCore.Examples.ObjectManipulation;

namespace Lean.Touch
{
	/// <summary>This component will automatically destroy this GameObject after the specified amount of time.
	/// NOTE: If you want to manually destroy this GameObject, then disable this component, and call the DestroyNow method directly.</summary>
	[HelpURL(LeanTouch.HelpUrlPrefix + "LeanDestroy")]
	[AddComponentMenu(LeanTouch.ComponentPathPrefix + "Destroy")]
	public class LeanDestroy : MonoBehaviour
	{
		/// <summary>The amount of seconds remaining before this GameObject gets destroyed.
		/// -1 = You must manually call the DestroyNow method.</summary>
		[Tooltip("The amount of seconds remaining before this GameObject gets destroyed.\n\n-1 = You must manually call the DestroyNow method.")]
		public float Seconds = -1.0f;

		protected virtual void Update()
		{
			if (Seconds >= 0.0f)
			{
				Seconds -= Time.deltaTime;

				if (Seconds <= 0.0f)
				{
					DestroyNow();
				}
			}
		}

		/// <summary>You can manually call this method to destroy the current GameObject now.</summary>
		public void DestroyNow()
		{
			Destroy(gameObject);
		}

        //Customized Destroy function to destroy manipulator 
        public void DestroyFurniture()
        {
            FurnitureManager FM = GameObject.FindWithTag("FurnitureManager").GetComponent<FurnitureManager>();
            FurnitureScript FS = this.gameObject.GetComponent<FurnitureScript>();
            FM.SpawnedFurnitures.Remove(FS);
            Destroy(gameObject.transform.parent);
            Destroy(this.gameObject);
        }
	}
}