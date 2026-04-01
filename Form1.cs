using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChatbotApp
{
    public partial class Form1 : Form
    {
        private Button clearButton;

        public Form1()
        {
            InitializeComponent();

            // 🔹 Form properties
            this.Text = "Pro Chatbot";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#343541");

            // 🔹 RichTextBox (use Designer instance)
            richTextBox1.Location = new Point(10, 10);
            richTextBox1.Size = new Size(760, 380);
            richTextBox1.BackColor = ColorTranslator.FromHtml("#444654");
            richTextBox1.ForeColor = Color.White;
            richTextBox1.Font = new Font("Segoe UI Emoji", 10);
            richTextBox1.ReadOnly = true;
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // 🔹 TextBox (use Designer instance)
            textBox1.Location = new Point(10, 400);
            textBox1.Size = new Size(560, 30);
            textBox1.BackColor = ColorTranslator.FromHtml("#40414F");
            textBox1.ForeColor = Color.White;
            textBox1.Font = new Font("Segoe UI", 10);
            textBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            textBox1.KeyDown += TextBox1_KeyDown;

            // 🔹 Send Button (use Designer instance)
            button1.Text = "Send";
            button1.Location = new Point(580, 400);
            button1.Size = new Size(90, 30);
            button1.BackColor = ColorTranslator.FromHtml("#19C37D");
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 10);
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.FlatAppearance.BorderSize = 0;
            // Designer already wires button1.Click to button1_Click

            // 🔹 Clear Chat Button (created at runtime)
            clearButton = new Button
            {
                Text = "Clear Chat",
                Location = new Point(680, 400),
                Size = new Size(90, 30),
                BackColor = ColorTranslator.FromHtml("#FF5C5C"),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            clearButton.FlatAppearance.BorderSize = 0;
            clearButton.Click += ClearButton_Click;
            this.Controls.Add(clearButton);

            // 🔹 Initial Bot Message
            AppendBotMessage("Hello! I am your Pro chatbot 🤖");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // kept intentionally empty — Designer wired this event
        }

        // Append messages
        private void AppendUserMessage(string msg)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
            richTextBox1.AppendText("You: " + msg + "\n");
            ScrollToBottom();
        }

        private void AppendBotMessage(string msg)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
            string timestamp = DateTime.Now.ToShortTimeString();
            richTextBox1.AppendText($"Bot [{timestamp}]: {AddEmoji(msg)}\n\n");
            ScrollToBottom();
        }

        private string AddEmoji(string msg)
        {
            if (msg.ToLower().Contains("hello")) return msg + " 👋";
            if (msg.ToLower().Contains("fine")) return msg + " 🙂";
            if (msg.ToLower().Contains("bye")) return msg + " 😢";
            return msg;
        }

        private void ScrollToBottom()
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        // Designer-wired Send button handler (name matches Designer)
        private void button1_Click(object sender, EventArgs e)
        {
            string originalMsg = textBox1.Text.Trim();
            if (originalMsg == "") return;

            AppendUserMessage(originalMsg);

            string msg = originalMsg.ToLower();
            string reply = "";

            // Bot logic
            if (msg.Contains("hi") || msg.Contains("hello"))
                reply = "Hello! How are you?";
            else if (msg.Contains("how are you"))
                reply = "I am fine, thanks for asking!";
            else if (msg.Contains("your name"))
                reply = "I am your Pro chatbot 🤖";
            else if (msg.Contains("my name is"))
            {
                string[] parts = originalMsg.Split(new string[] { "my name is" }, StringSplitOptions.None);
                if (parts.Length > 1 && !string.IsNullOrWhiteSpace(parts[1]))
                    reply = "Nice to meet you " + parts[1].Trim();
                else
                    reply = "Nice to meet you 😊";
            }
            else if (msg.Contains("time"))
                reply = "Current time is: " + DateTime.Now.ToShortTimeString();
            else if (msg.Contains("date"))
                reply = "Today's date is: " + DateTime.Now.ToShortDateString();
            else if (msg.Contains("bye"))
                reply = "Goodbye!";
            else
                reply = "Sorry, I don't understand 😅";

            AppendBotMessage(reply);
            textBox1.Clear();
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Trigger the same logic as the Send button
                button1.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            AppendBotMessage("Chat cleared! 🤖");
        }
    }
}