using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;

namespace PrzeniesAplikacje
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern bool SetWindowText(IntPtr hWnd, string text);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        // --------

        const UInt32 SW_HIDE = 0;
        const UInt32 SW_SHOWNORMAL = 1;
        const UInt32 SW_NORMAL = 1;
        const UInt32 SW_SHOWMINIMIZED = 2;
        const UInt32 SW_SHOWMAXIMIZED = 3;
        const UInt32 SW_MAXIMIZE = 3;
        const UInt32 SW_SHOWNOACTIVATE = 4;
        const UInt32 SW_SHOW = 5;
        const UInt32 SW_MINIMIZE = 6;
        const UInt32 SW_SHOWMINNOACTIVE = 7;
        const UInt32 SW_SHOWNA = 8;
        const UInt32 SW_RESTORE = 9;

        const int SWP_NOMOVE = 0x0002;
        const int SWP_NOSIZE = 0x0001;
        const int SWP_SHOWWINDOW = 0x0040;
        const int SWP_NOACTIVATE = 0x0010;
        const int SWP_NOZORDER = 0x0004;
        const int SWP_NOREDRAW = 0x0008;
        const int SWP_ASYNCWINDOWPOS = 0x4000;

        const int HWND_TOPMOST = -1;
        const int HWND_NOTOPMOST = -2;

        private string MSG_ERROR_DISPLAY_NOT_AVAILABLE;
        private string MSG_ERROR_DIALOG_SELECT_PROCESS_1;
        private string MSG_ERROR_DIALOG_APP_NOT_EXISTS;
        private string MSG_ERROR_DIALOG_APP_UNABLE_TO_MOVE;
        private string MSG_ERROR_DIALOG_SELECT_DISPLAY;
        private string MSG_ERROR_DIALOG_OPS;
        private string LABEL_DISPLAY_MAIN;
        private string LABEL_DISPLAYS_COUNT;
        private string LABEL_PROCESSES_COUNT;

        // --------

        private enum ShowWindowEnum
        {
            Hide = 0, ShowNormal = 1, ShowMinimized = 2,
            ShowMaximized = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };

        // --------

        /// <summary>
        /// Stores the latest list of the current running processes.
        /// </summary>
        List<Process> processList = new List<Process>();

        /// <summary>
        /// Stores information about destined monitor to be used.
        /// </summary>
        int selectedMonitorIndex = -1;

        /// <summary>
        /// Stores information about selected process to be used.
        /// </summary>
        private Process? selectedProcess = null;

        // --------

        private enum WindowAction
        {
            None = -1, Move = 0, MoveMaximize = 1,
        };

        // --------

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSettings("PL");
        }

        private void LoadSettings(string language)
        {
            changeLanguage(language);
            LoadProcesses();
            LoadMonitors();
        }

        private void LoadMonitors()
        {
            listBox2.Items.Clear();

            Screen[] allScreens = Screen.AllScreens;

            foreach (var screen in allScreens)
            {
                listBox2.Items.Add(screen.DeviceName + (screen.Primary ? LABEL_DISPLAY_MAIN : ""));
            }

            label4.Text = LABEL_DISPLAYS_COUNT + allScreens.Length.ToString();
        }

        private int CompareDinosByProcessName(Process x, Process y)
        {
            return x.ProcessName.CompareTo(y.ProcessName);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listbox = (ListBox)sender;
            int selectedIndex = listbox.SelectedIndex;

            if (selectedIndex == -1)
                return;

            Process process = processList[selectedIndex];
            selectedProcess = process;
            timer1.Enabled = true;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listbox = (ListBox)sender;
            int selectedIndex = listbox.SelectedIndex;

            if (selectedIndex == -1)
                return;

            selectedMonitorIndex = selectedIndex;
        }
        public static bool MoveToMonitor(IntPtr windowHandle, int monitor)
        {
            return SetWindowPos(
                windowHandle,
                IntPtr.Zero,
                Screen.AllScreens[monitor].WorkingArea.Left + 50,
                Screen.AllScreens[monitor].WorkingArea.Top + 50,
                0,
                0,
                SWP_NOSIZE
            );
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!AreConditionsValid())
            {
                return;
            }

            MoveThisWindow(WindowAction.Move);
        }

        private bool AreConditionsValid()
        {
            if (selectedProcess == null)
            {
                MessageBox.Show(MSG_ERROR_DIALOG_SELECT_PROCESS_1, MSG_ERROR_DIALOG_OPS, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // ----

            int processIndex = Process.GetProcesses(".").ToList()
                .FindIndex(x => x.MainWindowTitle == selectedProcess.MainWindowTitle);

            if (processIndex == -1)
            {
                MessageBox.Show(MSG_ERROR_DIALOG_APP_NOT_EXISTS, MSG_ERROR_DIALOG_OPS, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // ----

            if (selectedProcess.MainWindowHandle == IntPtr.Zero)
            {
                MessageBox.Show(MSG_ERROR_DIALOG_APP_UNABLE_TO_MOVE, MSG_ERROR_DIALOG_OPS, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (selectedMonitorIndex == -1)
            {
                MessageBox.Show(MSG_ERROR_DIALOG_SELECT_DISPLAY, MSG_ERROR_DIALOG_OPS, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            Screen[] allScreens = Screen.AllScreens;
            if (selectedMonitorIndex >= allScreens.Length)
            {
                MessageBox.Show(MSG_ERROR_DISPLAY_NOT_AVAILABLE, MSG_ERROR_DIALOG_OPS, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!AreConditionsValid())
            {
                return;
            }

            MoveThisWindow(WindowAction.MoveMaximize);
        }

        public bool CompareProcesses(List<Process> current, List<Process> other)
        {
            if (current.Count != other.Count)
            {
                Debug.WriteLine(current.Count + " != " + other.Count);
                return false;
            }

            for (int i = 0; i < current.Count; i++)
            {
                if (!current[i].MainWindowTitle.Equals(other[i].MainWindowTitle))
                {
                    Debug.WriteLine(current[i].MainWindowTitle + " !equals " + other[i].MainWindowTitle);
                    return false;
                }
            }

            Debug.WriteLine("current equals other");
            return true;
        }

        private void LoadProcesses()
        {
            List<Process> newProcessList = getProcesses(null);

            listBox1.Items.Clear();

            processList = new List<Process>();

            foreach (Process p in newProcessList)
            {
                if (!String.IsNullOrEmpty(p.MainWindowTitle))
                {
                    String processName = p.MainWindowTitle;
                    listBox1.Items.Add(processName);
                    processList.Add(p);
                }
            }

            label3.Text = LABEL_PROCESSES_COUNT + processList.Count.ToString();
        }

        private List<Process> getProcesses(Predicate<Process>? predicate)
        {
            List<Process> newProcessList = Process.GetProcesses(".").ToList();
            newProcessList.Sort(CompareDinosByProcessName);

            if (predicate != null)
            {
                newProcessList = newProcessList.FindAll(predicate);
            }

            return newProcessList;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            selectedProcess = null;
            LoadProcesses();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            selectedMonitorIndex = -1;
            LoadMonitors();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void MoveThisWindow(WindowAction windowAction)
        {
            if (selectedProcess == null)
            {
                return;
            }

            if (selectedProcess.MainWindowHandle != IntPtr.Zero)
            {
                switch (windowAction)
                {
                    case WindowAction.Move:
                        ShowWindow(selectedProcess.MainWindowHandle, ShowWindowEnum.Show);
                        SetForegroundWindow(selectedProcess.MainWindowHandle);
                        MoveToMonitor(selectedProcess.MainWindowHandle, selectedMonitorIndex);

                        ShowWindow(selectedProcess.MainWindowHandle, ShowWindowEnum.ShowNormal);
                        break;
                    case WindowAction.MoveMaximize:
                        ShowWindow(selectedProcess.MainWindowHandle, ShowWindowEnum.Show);
                        SetForegroundWindow(selectedProcess.MainWindowHandle);
                        MoveToMonitor(selectedProcess.MainWindowHandle, selectedMonitorIndex);

                        ShowWindow(selectedProcess.MainWindowHandle, ShowWindowEnum.Restore);
                        ShowWindow(selectedProcess.MainWindowHandle, ShowWindowEnum.ShowMaximized);
                        break;
                }
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            Debug.WriteLine("tick, tick....");

            List<Process> processes = getProcesses(p => p.Id == selectedProcess.Id);
            Process? process = processes.FirstOrDefault();

            if (process != null)
            {
                selectedProcess = process;

                label6.Text = selectedProcess.ProcessName;
                label8.Text = selectedProcess.MainWindowHandle.ToString();

                if (selectedProcess.MainWindowHandle != IntPtr.Zero)
                {
                    timer1.Enabled = false;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadSettings("PL");
        }

        private void changeLanguage(string language)
        {
            if (language.Equals("PL"))
            {
                this.label1.Text = "Wybierz aplikacjê";
                this.label2.Text = "Wybierz ekran";
                this.button1.Text = "Przenieœ";
                this.button2.Text = "Przenieœ i powiêksz";
                this.button3.Text = "Odœwie¿ listê";
                this.label3.Text = "Procesów: 0";
                this.label4.Text = "Monitorów: 0";
                this.button4.Text = "Odœwie¿ listê";
                this.label5.Text = "Wybrany proces:";
                this.label6.Text = "____";
                this.label8.Text = "____";

                MSG_ERROR_DISPLAY_NOT_AVAILABLE = "Wybrany monitor ju¿ nie jest dostêpny.\n\nOdœwie¿ listê w drugiej kolumnie i spróbuj wybraæ inny.";
                MSG_ERROR_DIALOG_SELECT_PROCESS_1 = "Wybierz aplikacjê w pierwszej kolumnie.\n\nJeœli jej nie widzisz upewnij siê, ¿e aplikacja jest w³¹czona, a lista zosta³a odœwie¿ona.";
                MSG_ERROR_DIALOG_APP_NOT_EXISTS = "Wybrana aplikacja ju¿ nie istnieje.\n\nOdœwie¿ listê w pierwszej kolumnie i spróbuj wybraæ inn¹.";
                MSG_ERROR_DIALOG_APP_UNABLE_TO_MOVE = "Ta aplikacja nie mo¿e byæ przeniesiona.\n\nSpróbuj uruchomiæ ten program przy wy³¹czonym programie typu AntiVirus.";
                MSG_ERROR_DIALOG_SELECT_DISPLAY = "Wybierz monitor w drugiej kolumnie.\n\nJeœli widzisz tylko jeden monitor upewnij siê, ¿e drugi zosta³ w³¹czony i ustawiono tryb np. \"Rozszerz\".\n\nMo¿esz te¿ u¿yæ skrótu klawiszowego: WINDOWS + P";
                MSG_ERROR_DIALOG_OPS = "Ops!";
                LABEL_DISPLAY_MAIN = " (ekran g³ówny)";
                LABEL_DISPLAYS_COUNT = "Monitorów: ";
                LABEL_PROCESSES_COUNT = "Procesów: ";
            }
            else if (language.Equals("EN"))
            {
                this.label1.Text = "Select application";
                this.label2.Text = "Select display";
                this.button1.Text = "Move";
                this.button2.Text = "Move && expand";
                this.button3.Text = "Refresh list";
                this.label3.Text = "Processes: 0";
                this.label4.Text = "Displays: 0";
                this.button4.Text = "Refresh list";
                this.label5.Text = "Selected process:";
                this.label6.Text = "____";
                this.label8.Text = "____";

                MSG_ERROR_DISPLAY_NOT_AVAILABLE = "The selected monitor is no longer available.\n\nRefresh the list in the second column and try to select another one.";
                MSG_ERROR_DIALOG_SELECT_PROCESS_1 = "Select the application in the first column.\n\nIf you don't see it make sure the application is enabled and the list has been refreshed.";
                MSG_ERROR_DIALOG_APP_NOT_EXISTS = "The selected application no longer exists.\n\nRefresh the list in the first column and try to select another one.";
                MSG_ERROR_DIALOG_APP_UNABLE_TO_MOVE = "This application cannot be transferred.\n\nTry to run this program with AntiVirus disabled.";
                MSG_ERROR_DIALOG_SELECT_DISPLAY = "Select the monitor in the second column.\n\nIf you only see one monitor make sure the other monitor is turned on and the mode is set to, for example. \"Expand\".\n\nYou can also use the keyboard shortcut: WINDOWS + P";
                MSG_ERROR_DIALOG_OPS = "Ops!";
                LABEL_DISPLAY_MAIN = " (main)";
                LABEL_DISPLAYS_COUNT = "Displays: ";
                LABEL_PROCESSES_COUNT = "Processes: ";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadSettings("EN");
        }
    }
}