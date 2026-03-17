using System.Diagnostics;
using System.Runtime.InteropServices;

class Program {
    [DllImport("user32.dll")]
    static extern bool SetForegroundWindow(IntPtr hWnd);
    [DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WINDOWPLACEMENT lpwndpl);

    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWPLACEMENT {
        public int length;
        public int flags;
        public int showCmd;
        public POINT ptMinPosition;
        public POINT ptMaxPosition;
        public RECT rcNormalPosition;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    static async Task Main() {          //One call opens one window
        Task i = openAndPositionChromeWindow(new[] {"https://open.spotify.com/"}, 1, 
                        "Spotify - Web Player: Music for everyone - Google Chrome", 1490, 320, 2500, 1170);  //left, top, right, bottom, Unit: pixel
        Task j = openAndPositionChromeWindow(new[] {"https://www.google.com/", "https://x.com/home"}, 1, 
                        "Google - Google Chrome", -10, 0, 1080, 1423); 
        Task k = openAndPositionChromeWindow(new[] {"https://www.pixiv.net/en/", "https://discord.com/channels/@me", "https://www.youtube.com/"}, 3, 
                        "Online community for artists [pixiv] - Google Chrome", 0, 0, 830, 1423); 
        await Task.WhenAll(i, j, k);
    }   

    static async Task openAndPositionChromeWindow(string[] urls, int dekasa, string windowName, int hidari, int ue, int migi, int shita) {
        string chromePath = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
        string arguments = "--new-window " + string.Join(" ", urls);

        var keii = new Process {
            StartInfo = new ProcessStartInfo {
                FileName = chromePath,
                Arguments = arguments,
                UseShellExecute = false
            }
        };

        keii.Start();

        //Below codes are used to postion, minimize or maximize window, skip them if you dont need 

        await Task.Delay(5000);           //Since hWnd wouldn't be generated immediately after a new window appears,
                                          //delay time is recommended to be set more than 3 seconds
        IntPtr hoaw = IntPtr.Zero;
        
        hoaw = FindWindow(null, windowName);             //WindowName is used to find the hWnd and it is composed with 
        if (hoaw != IntPtr.Zero)                         //the first tab title name and a common suffix " - Google Chrome"
            Console.WriteLine("found it");               //Websites like youtube, twitter, discord would add the number of notifications
        if (hoaw == IntPtr.Zero)                         //(which keeps changing) into the tab title name
            Console.WriteLine("didnt find it");          //Hence, the first tab is not recommended to be SNS sort of websites

                                                         //Use "Ctrl + U" or F12 to find title tag
                                                         //then combine tab title name with common suffix " - Google Chrome" to have the windowName
                                                         
        var wp = new WINDOWPLACEMENT();
        wp.length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));
        wp.showCmd = dekasa;       //The size of window: 1 == Normal, 2 == Minimized, 3 == Maximized
        wp.rcNormalPosition = new RECT {left = hidari, top = ue, right = migi, bottom = shita};      //Adjust these parameter values to position window to
                                                                                                     //where you want according to your own screen resolution                                                                                               

        bool dekiruka = SetWindowPlacement(hoaw, ref wp);                                

        if (!dekiruka)
            Console.WriteLine("muri: " + Marshal.GetLastWin32Error());
        
        SetForegroundWindow(hoaw);

    }  
}