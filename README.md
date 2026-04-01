using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChatbotApp
{
    public class Form1 : Form
    {
        private RichTextBox richTextBox1;
        private TextBox textBox1;
        private Button button1, clearButton;

        public Form1()
        {
            // 🔹 Form properties
            this.Text = "Pro Chatbot";
            this.Size = new Size(800, 500);
            this.BackColor = ColorTranslator.FromHtml("#343541");

            // 🔹 RichTextBox (Chat area)
            richTextBox1 = new RichTextBox
            {
                Location = new Point(10, 10),
                Size = new Size(760, 380),
                BackColor = ColorTranslator.FromHtml("#444654"),
                ForeColor = Color.White,
                Font = new Font("Segoe UI Emoji", 10),
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            this.Controls.Add(richTextBox1);

            // 🔹 TextBox (User input)
            textBox1 = new TextBox
            {
                Location = new Point(10, 400),
                Size = new Size(560, 30),
                BackColor = ColorTranslator.FromHtml("#40414F"),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };
            textBox1.KeyDown += TextBox1_KeyDown;
            this.Controls.Add(textBox1);

            // 🔹 Send Button
            button1 = new Button
            {
                Text = "Send",
                Location = new Point(580, 400),
                Size = new Size(90, 30),
                BackColor = ColorTranslator.FromHtml("#19C37D"),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            button1.FlatAppearance.BorderSize = 0;
            button1.Click += Button1_Click;
            this.Controls.Add(button1);

            // 🔹 Clear Chat Button
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

        private void Button1_Click(object sender, EventArgs e)
        {
            string originalMsg = textBox1.Text.Trim();
            if (originalMsg == "") return;

            AppendUserMessage(originalMsg);

            string msg = originalMsg.ToLower();
            string reply = "";

            if (msg.Contains("hi") || msg.Contains("hello"))
                reply = "Hello! How are you?";
            else if (msg.Contains("how are you"))
                reply = "I am fine, thanks for asking!";
            else if (msg.Contains("your name"))
                reply = "I am your Pro chatbot 🤖";
            else if (msg.Contains("my name is"))
            {
                string[] parts = originalMsg.Split(new string[] { "my name is" }, StringSplitOptions.None);
                reply = (parts.Length > 1 && !string.IsNullOrWhiteSpace(parts[1])) 
                        ? "Nice to meet you " + parts[1].Trim() 
                        : "Nice to meet you 😊";
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
                button1.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            AppendBotMessage("Chat cleared! 🤖");
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
