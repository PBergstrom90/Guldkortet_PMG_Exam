using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Guldkortet_PMG
{
    public partial class Guldkortet : Form
    {
        // Kontakt mellan server och klient (NOS-programmet).
        TcpClient client = new TcpClient();
        TcpListener listener;
        int port = 12345;
        /* Skapar lista för kund-klassen
           samt kort-klassen. */
        List<Customer> customerList = new List<Customer>();
        List<Card> cardList = new List<Card>();
        // Följande stränglistor och bool används av "FileReader"-metoden, som backup.
        List<string> savedCustomer = new List<string>();
        List<string> savedCard = new List<string>();
        /* Följande bool används för att avgöra om databasen svarar,
           eller ifall programmet ska läsa av textfiler lokalt.*/
        bool datareaderSuccess = false;
        // Följande sträng används för meddelande från klienten (NOS-programmet).
        string nosMessage;
        public Guldkortet()
        {
            InitializeComponent();
            client.NoDelay = true;
            // Textlabels för att visa för användaren när uppkoppling är OK.
            IPlabel.Enabled = false;
            portLabel.Enabled = false;
            try
            {
                // Metod för att kontakta databas.
                DatabaseReader();
                // Gränssnitt för att visa att databasen är OK.
                statusLbl.Text = "OK";
                statusLbl.ForeColor = Color.Green;
            }
            catch
            {
                MessageBox.Show("Databasen svarar ej för tillfället.");
                datareaderSuccess = false;
                return;
            }
            if (datareaderSuccess == false) 
            { 
                try
                {
                    /* Backup-metod för filinläsning, ifall 
                   det är problem med databas-metoden.*/
                    statusLbl.Text = "ERROR";
                    statusLbl.ForeColor = Color.Red;
                    FileReader();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, Text);
                    return;
                }
            }
        }
        // ======== FORM GRÄNSSNITT ========
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void connectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                connectBtn.BackColor = Color.Green;
                connectBtn.Text = "Server startad";
                connectBtn.Enabled = false;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, Text);
                return;
            }
            StartConnection();
        }
        private void IPlabel_Click(object sender, EventArgs e)
        {
            // IP-nummer.
        }
        private void portLabel_Click(object sender, EventArgs e)
        {
            // Portnummer.
        }
        private void msgBox1_TextChanged(object sender, EventArgs e)
        {
            // Meddelanden som mottas från NOS-Export.
        }
        private void msgBox2_TextChanged(object sender, EventArgs e)
        {
            // Meddelanden som skickas till NOS-Export.
        }
        private void infoLbl_Click(object sender, EventArgs e)
        {
            // Användaruppgifter.
        }
        private void logoPic_Click(object sender, EventArgs e)
        {
            // Norr om Söder-logga.
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Visar samtliga uppgifter, uppladdad från databas.
        }
        private void databaseLbl_Click(object sender, EventArgs e)
        {
            // Databaslogga.
        }
        private void statusLbl_Click(object sender, EventArgs e)
        {
            // Visar status för databas.
        }
        private void exitBtn_Click(object sender, EventArgs e)
        {
            // Avslutar programmet.
            this.Close();
        }
        // ======== METODER ========
        public async void StartConnection()
        {
            try
            {
                client = await listener.AcceptTcpClientAsync();
                // Vid lyckad klientkoppling så visas detta.
                connectBtn.BackColor = Color.LightGreen;
                connectBtn.Text = "Klient uppkopplad";
                IPlabel.Enabled = true;
                portLabel.Enabled = true;
            }
            catch (Exception error)
            {
                // Felmeddelande vid uppkopplingsproblem.
                connectBtn.BackColor = Color.Red;
                connectBtn.Text = "Klient problem";
                MessageBox.Show(error.Message, Text);
                return;
            }
            StartReader(client);
        }
        // Metod för att läsa inkommande meddelande.
        public async void StartReader(TcpClient client)
        {
            byte[] buffer = new byte[1024];
            int bytes = 0;
            try
            {
                bytes = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                return;
            }
                // Tar emot en sträng från NOS-Export och deklarerar detta till "nosMessage".
                nosMessage = Encoding.Unicode.GetString(buffer, 0, bytes);
                StartReader(client);
                // Metod för att tolka meddelande från NOS-Export.    
                NOSReader();
        }
        // Metod för att skicka meddelande utgående till klient (NOS-programmet).
        public async void StartTransmission (string outMessage)
        {
            byte[] outData = Encoding.Unicode.GetBytes(outMessage);
            try
            {
                await client.GetStream().WriteAsync(outData, 0, outData.Length );
            }
            catch (Exception error) 
            {
                MessageBox.Show(error.Message);
                return;
            }
        }
        public void DatabaseReader()
        {
            try
            {
                /* Kundregister. Läser in Kunduppgifter från databas
                   och lägger till en ny "kund" i kundlistan.*/
                string connectionString1 = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"F:\\Pontus Filer\\Plugg\\Programmering\\NTI-Skolan 2022-2023\\Programmering 2\\Uppgift\\Guldkortet\\Guldkortet_PGM\\Guldkortet_PMG\\Kundregister.mdf\";Integrated Security=True;Connect Timeout=30";
                string query1 = "SELECT AnvändarNr, Namn, Kommun FROM Kunder";
                using (SqlConnection connection = new SqlConnection(connectionString1)) 
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query1, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Customer customer = new Customer(reader.GetValue(0).ToString(), reader.GetValue(1).ToString(), reader.GetValue(2).ToString());
                            customerList.Add(customer);
                            listBox1.Items.Add("Användarnummer: " + reader.GetValue(0).ToString() + " Namn: " + reader.GetValue(1).ToString() + " Kommun: " + reader.GetValue(2).ToString());
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
                /* Kortregister. Läser in Kortuppgifter från databas
                   och lägger till ett nytt "kort" i kortlistan. */
                string connectionString2 = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"F:\\Pontus Filer\\Plugg\\Programmering\\NTI-Skolan 2022-2023\\Programmering 2\\Uppgift\\Guldkortet\\Guldkortet_PGM\\Guldkortet_PMG\\Kortregister.mdf\"; Integrated Security = True; Connect Timeout = 30";
                string query2 = "SELECT KortNr, KortTyp FROM Kort";
                using (SqlConnection connection = new SqlConnection(connectionString2))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query2, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            /* Jämför index i kortdatabasen och
                               delar upp kort efter underklass i kortklassen. */
                            Card card = new Card(reader.GetValue(0).ToString(), reader.GetValue(1).ToString());
                            if (reader.GetString(1) == "Dunderkatt")
                            {
                                cardList.Add(new Card.Dunderkatt(reader.GetValue(0).ToString(), reader.GetValue(1).ToString()));
                            }
                            else if (reader.GetString(1) == "Kristallhäst")
                            {
                                cardList.Add(new Card.Kristallhäst(reader.GetValue(0).ToString(), reader.GetValue(1).ToString()));
                            }
                            else if (reader.GetString(1) == "Överpanda")
                            {
                                cardList.Add(new Card.Överpanda(reader.GetValue(0).ToString(), reader.GetValue(1).ToString()));
                            }
                            else if (reader.GetString(1) == "Eldtomat")
                            {
                                cardList.Add(new Card.Eldtomat(reader.GetValue(0).ToString(), reader.GetValue(1).ToString()));
                            }
                            listBox1.Items.Add("Kortnummer: " + reader.GetValue(0).ToString() + " Korttyp: " + reader.GetValue(1).ToString());
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
                    // Efter lyckad databasinläsning, anger bool som true.
                    datareaderSuccess = true;
            }
            catch (Exception error) 
            {
                MessageBox.Show(error.Message);
                return;
            }
        }
        /* Backup-metod med textfiler,
           ifall databasen inte kan användas. */  
        public void FileReader() 
        {
                try
                {
                    if (File.Exists("kundlista.txt"))
                    {
                        string customer = "";
                        StreamReader custReader = new StreamReader("kundlista.txt", Encoding.Default, true);
                        // Sålänge kundlistan inte är tom, läs igenom kundlistan och spara samtliga kunder.
                        while ((customer = custReader.ReadLine()) != null)
                        {
                            savedCustomer.Add(customer);
                        }
                        // Lägger till varje kund i kundlistan,
                        // med info som "kundnummer", "namn" och "stad".
                        foreach (string a in savedCustomer)
                        {
                        string[] custVektor = a.Split(new string[] { "###" }, StringSplitOptions.None);
                        customerList.Add(new Customer(custVektor[0], custVektor[1], custVektor[2]));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Importeringen av kundlistan misslyckades.");
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                    return;
                }
            try
            {
                if (File.Exists("kortlista.txt"))
                {
                    string card = "";
                    StreamReader cardReader = new StreamReader("kortlista.txt", Encoding.Default, true);
                    // Sålänge kortlistan inte är tom, läs igenom kortlistan och spara samtliga kort.
                    while ((card = cardReader.ReadLine()) != null)
                    {
                        savedCard.Add(card);
                    }
                    // Delar upp kortinformation efter Korttyp, sparar dessa i varsin underklass.
                    foreach (string b in savedCard)
                    {
                        string[] cardVektor = b.Split(new string[] { "###" }, StringSplitOptions.None);
                        if (cardVektor[1] == "Dunderkatt")
                        {
                            cardList.Add(new Card.Dunderkatt(cardVektor[0], cardVektor[1]));
                        }
                        else if (cardVektor[1] == "Kristallhäst")
                        {
                            cardList.Add(new Card.Kristallhäst(cardVektor[0], cardVektor[1]));
                        }
                        else if (cardVektor[1] == "Överpanda")
                        {
                            cardList.Add(new Card.Överpanda(cardVektor[0], cardVektor[1]));
                        }
                        else if (cardVektor[1] == "Eldtomat")
                        {
                            cardList.Add(new Card.Eldtomat(cardVektor[0], cardVektor[1]));
                        }
                    }
                    MessageBox.Show("Problem med databas. Importerar kund- och kortlistan via textfil.");
                }
                else
                {
                    MessageBox.Show("Importeringen av kortlistan misslyckades.");
                }
            }
            // Felmeddelande.
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        // Metod för att tolka meddelande från NOS-Export.
        public void NOSReader()
        {
            try
            {
                // Lägger till klass-listor för att spara uppgifter om vinnaren.
                List<Customer> winnerCustomer = new List<Customer>();
                List<Card> winnerCard = new List<Card>();
                string nosText = nosMessage.ToString();
                // Visar meddelandet som skickas från NOS-Export.
                msgBox1.Text = nosMessage.ToString();
                string[] nosVektor = nosMessage.Split(new string[] { "-" }, StringSplitOptions.None);
                List<string> nosList = new List<string>();
                // Bool som avgör om det blir en vinst eller inte.
                bool success = false;
                // Splittar meddelandet från NOS-export i en vektor.
                foreach (string nosMessage in nosVektor)
                {
                    nosList.Add(nosMessage);
                }
                // Söker igenom alla kundnummer i listan.
                foreach (var Customer in customerList)
                {
                    if (nosList[0] == Customer.CustomerNumber)
                    {
                        winnerCustomer.Add(Customer);
                        /* Om kundnummer stämmer, sparar vi kunduppgifter
                           och går vidare för att jämföra kortnummer. */
                        foreach (var Card in cardList)
                        {
                            if (nosList[1] == Card.CardNumber)
                            {
                                winnerCard.Add(Card);
                                success = true;
                                /* Succé! Kundnummer och kortnummer stämmer.
                                   Vi sparar kortuppgifter och anger vinst-bool som true.*/
                                break;
                            }
                        }
                        break;
                    }
                }
                if (success == true)
                {
                    // Klient meddelas om vinst.
                    msgBox2.Clear();
                    msgBox2.AppendText(winnerCustomer[0] + ". " + winnerCard[0]);
                    StartTransmission(msgBox2.Text);
                }
                else if (success == false)
                {
                    // Om det inte blir någon vinst, så meddelas användaren om detta.
                    msgBox2.Clear();
                    msgBox2.AppendText("Du har tyvärr inte vunnit något");
                    StartTransmission(msgBox2.Text);
                }
            }
            // Felmeddelande.
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }
    // ======== KLASSER ========
    public class Customer
    {
        public string CustomerNumber;
        public string Name;
        public string City;
        public Customer(string indataCustomerNumber, string indataName, string indataCity) 
        {
            CustomerNumber = indataCustomerNumber;
            Name = indataName;
            City = indataCity;
        }
        // Allmän grattis-hälsning med grundläggande info om användaren.
        public override string ToString()
        {
            return "Grattis " + Name + "!" + " Du kan hämta ut din vinst i din lokala spelbutik i " + City;
        }
    }
    public class Card
    {
        public string CardNumber;
        public string CardType; 
        public Card(string indataCardNumber, string indataCardType)
        { 
            CardNumber = indataCardNumber;  
            CardType = indataCardType;
        }
        public class Dunderkatt : Card
        {
            public Dunderkatt (string indataCardNumber, string indataCardType) : base (indataCardNumber, indataCardType)
            {
                CardType = "Dunderkatt";
            }
            // Lägger till specifika hälsningar, beroende på vilket kort som har vunnits.
            public override string ToString()
            {
                return "Du har vunnit ett blixtrande " + CardType + "-kort! Fantastiskt!";
            }
        }
        public class Kristallhäst : Card
        {
            public Kristallhäst(string indataCardNumber, string indataCardType) : base(indataCardNumber, indataCardType)
            {
                CardType = "Kristallhäst";
            }
            // Lägger till specifika hälsningar, beroende på vilket kort som har vunnits.
            public override string ToString()
            {
                return "Du har vunnit ett glittrande " + CardType + "-kort! Snyggt!";
            }
        }
        public class Överpanda : Card
        { 
            public Överpanda(string indataCardNumber, string indataCardType) : base(indataCardNumber, indataCardType)
            {
                CardType = "Överpanda";
            }
            // Lägger till specifika hälsningar, beroende på vilket kort som har vunnits.
            public override string ToString()
            {
                return "Du har vunnit ett magnifikt " + CardType + "-kort! Woho!";
            }
        }
        public class Eldtomat : Card
        {
            public Eldtomat(string indataCardNumber, string indataCardType) : base(indataCardNumber, indataCardType)
            {
                CardType = "Eldtomat";
            }
            // Lägger till specifika hälsningar, beroende på vilket kort som har vunnits.
            public override string ToString()
            {
                return "Du har vunnit ett brinnande " + CardType + "-kort! Hurra!";
            }
        }
    }
}