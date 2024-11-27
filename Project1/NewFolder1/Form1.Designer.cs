using System;
using System.Windows.Forms;

namespace Project1.NewFolder1
{
    public class PhoneStoreForm : Form
    {
        private ComboBox countryComboBox;
        private Button orderButton;
        private Label resultLabel;

        public PhoneStoreForm()
        {
            this.Text = "Магазин телефонів";
            this.Size = new System.Drawing.Size(400, 300);

         
            countryComboBox = new ComboBox();
            countryComboBox.Items.Add("США");
            countryComboBox.Items.Add("Східна країна");
            countryComboBox.SelectedIndex = 0;
            countryComboBox.Location = new System.Drawing.Point(50, 50);
            this.Controls.Add(countryComboBox);

            
            orderButton = new Button();
            orderButton.Text = "Ціна телефону";//, задають її текст, позицію на формі, а також прив'язують обробник події для натискання.
            orderButton.Location = new System.Drawing.Point(50, 100);
            orderButton.Click += OrderButton_Click;
            this.Controls.Add(orderButton);

           
            resultLabel = new Label();
            resultLabel.Location = new System.Drawing.Point(50, 150);
            resultLabel.Size = new System.Drawing.Size(600, 600);
            this.Controls.Add(resultLabel);
        }

        private void OrderButton_Click(object sender, EventArgs e)
        {
            
            IPhoneFactory factory = null;
            if (countryComboBox.SelectedItem.ToString() == "США")
            {
                factory = new USAPhoneFactory();
            }
            else if (countryComboBox.SelectedItem.ToString() == "Східна країна")
            {
                factory = new EastCountryPhoneFactory();
            }

            if (factory != null)
            {
                var phone = factory.CreatePhone();
                resultLabel.Text = $"Ціна: ${phone.Price}\nЧас доставки: {phone.DeliveryTime} днів";
            }
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PhoneStoreForm());
        }
    }

    
    public abstract class Phone
    {
        public abstract decimal Price { get; }
        public abstract int DeliveryTime { get; }
    }

    // Конкретні класи телефонів
    public class USAPhone : Phone
    {
        public override decimal Price => 1000;
        public override int DeliveryTime => 5;
    }

    public class EastCountryPhone : Phone
    {
        public override decimal Price => 700;
        public override int DeliveryTime => 15;
    }

    
    public interface IPhoneFactory
    {
        Phone CreatePhone();
    }

    // Конкретні фабрики телефонів для кожної країни
    public class USAPhoneFactory : IPhoneFactory
    {
        public Phone CreatePhone()
        {
            return new USAPhone();
        }
    }

    public class EastCountryPhoneFactory : IPhoneFactory
    {
        public Phone CreatePhone()
        {
            return new EastCountryPhone();
        }
    }
}
