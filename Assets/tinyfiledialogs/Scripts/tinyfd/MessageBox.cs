using UnityEngine;
using System.Collections;

namespace tinyfd {
	public class MessageBox : MonoBehaviour {
		public string title;
		public string message;
		public TinyFileDialogs.DialogType dialogType = TinyFileDialogs.DialogType.ok;
		public TinyFileDialogs.IconType iconType = TinyFileDialogs.IconType.info;
		public bool okIsDefault = true;

		public bool Show() {
			return TinyFileDialogs.MessageBox(title, message, dialogType, iconType, okIsDefault);
		}

		public void ShowCallback() {
			Show();
		}
	}
}
