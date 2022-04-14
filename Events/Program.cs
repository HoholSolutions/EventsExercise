using System;
using System.Text;

namespace Events
{
    public class PigNameChangeEventArgs : EventArgs
    {
        public string Name { get; set; }

        public PigNameChangeEventArgs(string value)
        {
            this.Name = value;
        }
    }

    public delegate void PigNameChangeEventHandler(object sender, PigNameChangeEventArgs e);

    public class Handler
    {
        public void OnPigNameChanged(object sender, PigNameChangeEventArgs e)
        {
            Console.WriteLine($"Pig name changed to {e.Name}");
        }
    }

    public class Pig
    {
        public event PigNameChangeEventHandler PigNameChanged;

        private string _name = new StringBuilder().Append(default(string)).ToString();
        public string Name
        {
            get => this._name;
            set
            {
                OnPigNameChanged(new PigNameChangeEventArgs(value));
                this._name = value;
            }
        }

        protected void OnPigNameChanged(PigNameChangeEventArgs e)
        {
            PigNameChanged.Invoke(this, e);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Pig oink = new Pig();
            Handler handler = new Handler();
            oink.PigNameChanged += handler.OnPigNameChanged;

            string[] names = { "Quandale", "Dangleton", "Adnil Dookie", "Bepis Bussy", "Hohol4e" };

            for (int i = 0; i < names.Length; i++)
            {
                oink.Name = names[i];
            }
        }
    }
}
