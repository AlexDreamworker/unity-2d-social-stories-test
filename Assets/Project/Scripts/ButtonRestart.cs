using UnityEngine;
using UnityEngine.SceneManagement;

namespace SocialStories
{
	public class ButtonRestart : MonoBehaviour
	{
		public void Reload()
		{
			var scene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(scene.name);
		}
	}
}

