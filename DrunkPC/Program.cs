using System;
using System.Threading;
using System.Windows.Forms;
using System.Media;

namespace DrunkPC
{
    class Program
    {
        public static Random _random = new Random();

        // Default parameters for arguments
        public static int _startupDelaySeconds = 10;
        public static int _totalDurationSeconds = 30;

        static void Main(string[] args)
        {
            Console.WriteLine("DrunkPC Prank Application");

            // If the user supplied arguments go ahead and assing those values
            if(args.Length >= 2)
            {
                _startupDelaySeconds = Convert.ToInt32(args[0]);
                _totalDurationSeconds = Convert.ToInt32(args[1]);
            }

            // Set up all the threads
            Thread drunkMouseThread = new Thread(new ThreadStart(DrunkMouseThread));
            Thread drunkKeyboardThread = new Thread(new ThreadStart(DrunkKeyboardThread));
            Thread drunkSoundThread = new Thread(new ThreadStart(DrunkSoundThread));
            Thread drunkPopUpThread = new Thread(new ThreadStart(DrunkPopUpThread));

            // Wait before starting the threads in the delay of seconds from user
            DateTime future = DateTime.Now.AddSeconds(_startupDelaySeconds);
            while (future > DateTime.Now)
                Thread.Sleep(1000);

            // Start the threads
            drunkMouseThread.Start();
            drunkKeyboardThread.Start();
            drunkSoundThread.Start();
            drunkPopUpThread.Start();

            // Run the thread for as long as the user would like
            future = DateTime.Now.AddSeconds(_totalDurationSeconds);
            while (future > DateTime.Now)
                Thread.Sleep(1000);

            // Close the threads
            drunkMouseThread.Abort();
            drunkKeyboardThread.Abort();
            drunkSoundThread.Abort();
            drunkPopUpThread.Abort();
        }

        #region Thread Functions
        /// <summary>
        /// Randomly moves the mouse around the screen
        /// </summary>
        public static void DrunkMouseThread()
        {
            Console.WriteLine("DrunkMouseThread Started");

            int moveX = 0;
            int moveY = 0;

            while (true)
            {
                // Generate random numbers to use in moving the cursor position
                moveX = _random.Next(20) - 10;
                moveY = _random.Next(20) - 10;

                // Move the mouse cursor the from it's current position to the randomly generated distance
                Cursor.Position = new System.Drawing.Point(
                    Cursor.Position.X + moveX,
                    Cursor.Position.Y + moveY);
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// Types random letters at random intervals
        /// </summary>
        public static void DrunkKeyboardThread()
        {
            Console.WriteLine("DrunkKeyboardThread Started");

            while (true)
            {
                if(_random.Next(100) > 80)
                {
                    char key = (char) (_random.Next(25)+65);

                    if (_random.Next(2) == 0)
                        key = Char.ToLower(key);

                    SendKeys.SendWait(key.ToString());
                }
                Thread.Sleep(_random.Next(500));
            }
        }

        /// <summary>
        /// Plays random system sounds every little while
        /// </summary>
        public static void DrunkSoundThread()
        {
            Console.WriteLine("DrunkSoundThread Started");

            while (true)
            {
                if (_random.Next(100) > 80)
                {
                    switch (_random.Next(5))
                    {
                        case 0:
                            SystemSounds.Asterisk.Play();
                            break;
                        case 1:
                            SystemSounds.Beep.Play();
                            break;
                        case 2:
                            SystemSounds.Exclamation.Play();
                            break;
                        case 3:
                            SystemSounds.Hand.Play();
                            break;
                        case 4:
                            SystemSounds.Question.Play();
                            break;
                    }
                }
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Pops up 1 of 2 different error messages at random intervals
        /// </summary>
        public static void DrunkPopUpThread()
        {
            Console.WriteLine("DrunkPopUpThread Started");

            while (true)
            {
                if (_random.Next(100) > 80)
                {
                    switch (_random.Next(2))
                    {
                        case 0:
                            MessageBox.Show("Internet Explorer has stopped working",
                                "Internet Explorer",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;
                        case 1:
                            MessageBox.Show("Your system is running low on resources",
                                "Microsoft Windows",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            break;
                    }
                }
                Thread.Sleep(10000);
            }
        }
        #endregion
    }
}
