using System.Runtime.InteropServices;


namespace tinyfd {

public static class TinyFileDialogs {
#if UNITY_IPHONE && !UNITY_EDITOR
	private const string DLL_NAME = "__Internal";
#else
	private const string DLL_NAME = "tinyfiledialogs";
#endif

	public enum DialogType {
		ok,
		okcancel,
		yesno
	}
	public enum IconType {
		info,
		warning,
		error,
		question
	}

	// int tinyfd_messageBox (
	// char const * const aTitle , /* "" */
	// char const * const aMessage , /* "" may contain \n \t */
	// char const * const aDialogType , /* "ok" "okcancel" "yesno" */
	// char const * const aIconType , /* "info" "warning" "error" "question" */
	// int const aDefaultButton ) ; /* 0 for cancel/no , 1 for ok/yes */
	// /* returns 0 for cancel/no , 1 for ok/yes */

	[DllImport(DLL_NAME)]
	private static extern int tinyfd_messageBox(string title, string message, string dialogType, string iconType, int defaultButton);

	public static bool MessageBox(
		string title,
		string message,
		DialogType dialogType = DialogType.ok,
		IconType iconType = IconType.info,
		bool okIsDefault = true
	) {
		return 0 != tinyfd_messageBox(title, message, dialogType.ToString(), iconType.ToString(), okIsDefault ? 1 : 0);
	}
}

}