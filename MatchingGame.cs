namespace matchtingGame
{

    //Something messed up, so I manually copy pasted all 16 tiles into the program. Surely I can do somehting to make the file size smaller, but I did it the lazy way, again....
    //I know where it messed up - the event handler when I tried to select all the tiles messed up. I clicked wrong and then I couldn't fix it like at all not even revert it.
    //the important part is that it works, and it is not slow, although it could have been faster I guess. I am happy with the result.
    
    public partial class MatchingGame : Form
    {
        //first clicked points to the first Label control that the player clicks. It will be null if the player haasn't started yet.
        Label firstClicked = null;

        //secondClicked points to the second Label control that the player clicks.
        Label secondClicked = null;
        //Use this random object to choose random icons for the squares (Webding's font got different icons we can use.
        Random random = new Random();

        //List with two matching icons for each icon.
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };


        private void AssignIconsToSquares()
        {
            // The TableLayoutPanel has 16 labels,
            // and the icon list has 16 icons,
            // so an icon is pulled at random from the list
            // and added to each label
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }



        public MatchingGame()
        {
            InitializeComponent();

            //The labels in a list. This solved it, hallelujah! From 716 lines of code to under 145.
            List<Label> labels = new List<Label> { label1, label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };

            foreach (var label in labels)
            {
                label.Click+= new EventHandler(Label_Click);
            }

            AssignIconsToSquares();

        }


        private void Label_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;
                CheckForWinner();

                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                timer1.Start();
            }
        }



        private void timer1_Tick_1(object sender, EventArgs e)
        {
            // Stop the timer
            timer1.Stop();

            // Hide both icons
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // Reset firstClicked and secondClicked 
            // so the next time a label is
            // clicked, the program knows it's the first click
            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {
            // Go through all of the labels in the TableLayoutPanel, 
            // checking each one to see if its icon is matched
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // If the loop didn’t return, it didn't find
            // any unmatched icons
            // That means the user won. Show a message and close the form
            MessageBox.Show("Who won? You won! Who's next?!", "Congratulations! Drop it like it's hot.");
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
