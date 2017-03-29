using System;
using System.Runtime.InteropServices;

namespace tinyfd
{
	public enum DialogType
	{
		// Show only OK button.
		ok,
		// Show OK and Cancel buttons.
		okcancel,
		// Show Yes and No buttons.
		yesno
	}

	// Dialog icon types.
	public enum IconType
	{
		info,
		warning,
		error,
		question
	}


	// Native C functions wrappers.
	public static class TinyFileDialogs
	{
		#if UNITY_IPHONE && !UNITY_EDITOR
		private const string DLL_NAME = "__Internal";
		#else
		private const string DLL_NAME = "tinyfiledialogs";
		#endif

		// int tinyfd_messageBox (
		// char const * const aTitle , /* "" */
		// char const * const aMessage , /* "" may contain \n \t */
		// char const * const aDialogType , /* "ok" "okcancel" "yesno" */
		// char const * const aIconType , /* "info" "warning" "error" "question" */
		// int const aDefaultButton ) ; /* 0 for cancel/no , 1 for ok/yes */
		// /* returns 0 for cancel/no , 1 for ok/yes */
		[DllImport(DLL_NAME)]
		private static extern int tinyfd_messageBox(string title, string message, string dialogType, string iconType, int defaultButton);

		/// <summary>
		/// Show message in a dialog.
		/// </summary>
		/// <returns><c>true</c>, if ok/yes was clicked, <c>false</c> otherwise.</returns>
		/// <param name="title">Title.</param>
		/// <param name="message">Message.</param>
		/// <param name="dialogType">Dialog type.</param>
		/// <param name="iconType">Icon type.</param>
		/// <param name="okIsDefault">If set to <c>true</c>, OK/Yes is the default selected button, otherwise Cancel/No is default.</param>
		public static bool MessageBox(
			string title,
			string message,
			DialogType dialogType = DialogType.ok,
			IconType iconType = IconType.info,
			bool okIsDefault = true
		)
		{
			return 0 != tinyfd_messageBox(title, message, dialogType.ToString(), iconType.ToString(), okIsDefault ? 1 : 0);
		}

		// char const * tinyfd_inputBox (
		// 	char const * const aTitle , /* "" */
		//	char const * const aMessage , /* "" may NOT contain \n \t on windows */
		//	char const * const aDefaultInput ) ;  /* "" , if NULL it's a passwordBox */
		// /* returns NULL on cancel */
		[DllImport(DLL_NAME)]
		private static extern IntPtr tinyfd_inputBox(string title, string message, string defaultInput);

		// Ask for user input in a dialog.
		public static string InputBox(
			string title,
			string message,
			string defaultInput
		) {
			return Marshal.PtrToStringAuto(tinyfd_inputBox(title, message, defaultInput));
		}

		// Ask for user password input in a dialog.
		public static string PasswordBox(
			string title,
			string message
		) {
			return Marshal.PtrToStringAuto(tinyfd_inputBox(title, message, null));
		}
	}
}