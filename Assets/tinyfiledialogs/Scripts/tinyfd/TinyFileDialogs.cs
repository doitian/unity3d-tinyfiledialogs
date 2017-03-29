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

		// char const * tinyfd_saveFileDialog (
		// 	char const * const aTitle , /* "" */
		// 	char const * const aDefaultPathAndFile , /* "" */
		// 	int const aNumOfFilterPatterns , /* 0 */
		// 	char const * const * const aFilterPatterns , /* NULL | {"*.jpg","*.png"} */
		// 	char const * const aSingleFilterDescription ) ; /* NULL | "text files" */
		// /* returns NULL on cancel */
		[DllImport(DLL_NAME)]
		private static extern IntPtr tinyfd_saveFileDialog(
			string title, 
			string defaultPathAndFile,
			int numFilterPatterns,
			string[] filterPatterns,
			string filterDescription
		);

		/// <summary>
		/// Open file dialog to save file.
		/// </summary>
		/// <returns>Path to selected location, or empty string if dialog is canceled.</returns>
		public static string SaveFileDialog(
			string title, 
			string defaultPathAndFile,
			string[] filterPatterns,
			string filterDescription = null
		) {
			return Marshal.PtrToStringAuto(tinyfd_saveFileDialog(title, defaultPathAndFile, filterPatterns.Length, filterPatterns, filterDescription));
		}

		// char const * tinyfd_openFileDialog (
		// 	char const * const aTitle , /* "" */
		// 	char const * const aDefaultPathAndFile , /* "" */
		// 	int const aNumOfFilterPatterns , /* 0 */
		// 	char const * const * const aFilterPatterns , /* NULL {"*.jpg","*.png"} */
		// 	char const * const aSingleFilterDescription , /* NULL | "image files" */
		// 	int const aAllowMultipleSelects ) ; /* 0 or 1 */
		// /* in case of multiple files, the separator is | */
		// /* returns NULL on cancel */
		[DllImport(DLL_NAME)]
		private static extern IntPtr tinyfd_openFileDialog(
			string title, 
			string defaultPathAndFile,
			int numFilterPatterns,
			string[] filterPatterns,
			string filterDescription,
			int allowMutipleSelects
		);

		/// <summary>
		/// Open file dialog to save file.
		/// </summary>
		/// <returns>Path to selected location, or empty string if dialog is canceled. In case of multiple files, the seperator is <c>|</c></returns>
		public static string OpenFileDialog(
			string title, 
			string defaultPathAndFile,
			string[] filterPatterns,
			string filterDescription = null,
			bool allowMultipleSelects = false
		) {
			return Marshal.PtrToStringAuto(tinyfd_openFileDialog(title, defaultPathAndFile, filterPatterns.Length, filterPatterns, filterDescription, allowMultipleSelects ? 1 : 0));
		}


		// char const * tinyfd_selectFolderDialog (
		// 	char const * const aTitle , /* "" */
		// 	char const * const aDefaultPath ) ; /* "" */
		// /* returns NULL on cancel */
		[DllImport(DLL_NAME)]
		private static extern IntPtr tinyfd_selectFolderDialog(string title, string defaultPath);

		public static string SelectFolderDialog(string title, string defaultPath) {
			return Marshal.PtrToStringAuto(tinyfd_selectFolderDialog(title, defaultPath));
		}

		// char const * tinyfd_colorChooser(
		// 	char const * const aTitle , /* "" */
		// 	char const * const aDefaultHexRGB , /* NULL or "#FF0000" */
		// 	unsigned char const aDefaultRGB[3] , /* { 0 , 255 , 255 } */
		// 	unsigned char aoResultRGB[3] ) ; /* { 0 , 0 , 0 } */
		// /* returns the hexcolor as a string "#FF0000" */
		// /* aoResultRGB also contains the result */
		// /* aDefaultRGB is used only if aDefaultHexRGB is NULL */
		// /* aDefaultRGB and aoResultRGB can be the same array */
		// /* returns NULL on cancel */
	}
}
