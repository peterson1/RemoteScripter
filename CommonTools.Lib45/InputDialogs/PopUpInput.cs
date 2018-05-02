using CommonTools.Lib11.InputCommands;
using CommonTools.Lib45.InputCommands;
using CommonTools.Lib45.UIExtensions;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CommonTools.Lib45.InputDialogs
{
    [AddINotifyPropertyChangedInterface]
    public abstract class PopUpInput<TOut, TWindow>
        where TWindow : Window, new()
    {
        internal PopUpInput(string caption, string message)
        {
            Caption   = $"   {caption}";
            Message   = message;
            Window    = InitializeWindow();
            SubmitCmd = R2Command.Relay(_ => Close(true), _ => CanSubmit(), "Submit");
        }


        public TWindow     Window      { get; }
        public string      Caption     { get; }
        public string      Message     { get; }
        public IR2Command  SubmitCmd   { get; }
        public string      WhyInvalid  { get; protected set; }


        public   abstract bool TryParseValue (out TOut parsed);
        protected virtual void OnWindowLoad  (TWindow win) { }


        private bool CanSubmit()
        {
            WhyInvalid = "";
            if (!Window.AllFieldsValid()) return false;
            return TryParseValue(out TOut val);
        }


        private TWindow InitializeWindow()
        {
            var win = new TWindow();
            win.DataContext = this;

            win.KeyUp += (s, e) =>
            {
                if (e.Key == Key.Escape)
                    Close(false);
            };

            win.Loaded += async (s, e) =>
            {
                await Task.Delay(200);
                OnWindowLoad(Window);
            };

            return win;
        }


        private void Close(bool result)
        {
            Window.DialogResult = result;
            Window.Close();
        }


        internal bool TryGetValidInput(out TOut valid)
        {
            var res = Window.ShowDialog();
            if (res != true)
            {
                valid = default(TOut);
                return false;
            }
            return TryParseValue(out valid);
        }
    }


    public static class PopUpInput
    {
        public static bool TryGetInt(string message, out int input, int? defaultVal = null, string caption = "Please enter a number")
            => new PopUpInputInt(caption, message, defaultVal)
                .TryGetValidInput(out input);


        public static bool TryGetDecimal(string message, out decimal input, decimal? defaultVal = null, string caption = "Please enter a number")
            => new PopUpInputDecimal(caption, message, defaultVal)
                .TryGetValidInput(out input);


        public static bool TryGetString(string message, out string input, string defaultVal = null, string caption = "Please enter a value")
            => new PopUpInputString(caption, message, defaultVal)
                .TryGetValidInput(out input);


        public static bool TryGetDate(string message, out DateTime input, DateTime? defaultVal = null, string caption = "Please select a date")
            => new PopUpInputDate(caption, message, defaultVal)
                .TryGetValidInput(out input);


        public static bool TryGetIndex<T>(string message, out int listIndex, IEnumerable<T> options, int? defaultIndex = null, string caption = "Please select an item")
            => new PopUpInputIndex<T>(caption, message, options, defaultIndex)
                .TryGetValidInput(out listIndex);
    }
}
