using System;
using System.Text;
using System.Runtime.InteropServices;

/*
USAGE:
since the class is static simply invoke the method by using the classname as such:
WIN_USERNAME.GetWindowsUserName();
This(as shown in the impl) will return a string of the logged in windows user.
*/

static public class WIN_USERNAME {

  [DllImport("advapi32.dll")]
  static extern bool GetUserName(StringBuilder lpBuffer, ref int nSize);

  static public string GetWindowsUserName() {
    int size = 128;
    var buffer = new StringBuilder(size);
    GetUserName(buffer, ref size);
    return buffer.ToString();
  }
}
