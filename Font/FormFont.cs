using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;

namespace Font
{
    public partial class FormFont : Form
    {
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public FormFont()
        {
            InitializeComponent();
            this.ActiveControl = montext;
        }

        private System.Drawing.Font MakeFont(string family, float size, FontStyle style)
        {
            try
            {
                return new System.Drawing.Font(family, size, style);
            }
            catch
            {
                return null;
            }
        }

        private void lstResultats_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstFonts.SelectedIndex = lstResultats.SelectedIndex;
            lblSample.Text = lstFonts.Text;
        }

        private void btn_Demarrer_Click(object sender, EventArgs e)
        {
            if (Taille.Value <= 18) lstResultats.ItemHeight = Convert.ToInt32(Taille.Value) * 2;
            else if (Taille.Value > 19) lstResultats.ItemHeight = Convert.ToInt32((Convert.ToDouble(Taille.Value) * 2.5));
            else if (Taille.Value >= 25) lstResultats.ItemHeight = Convert.ToInt32(Taille.Value) * 3;

            lstResultats.Items.Clear();

            foreach (FontFamily fam in FontFamily.Families)
            {
                lstFonts.Items.Add(fam.Name);
                lstResultats.Items.Add(fam.Name);
            }
            lstResultats.DrawMode = DrawMode.OwnerDrawFixed;
        }

        private void lstResultats_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (!string.IsNullOrWhiteSpace(montext.Text))
            {
                FontFamily FF = new FontFamily(lstFonts.Items[e.Index].ToString());

                float taille;
                float.TryParse(Taille.Value.ToString(), out taille);

                if (FF.IsStyleAvailable(FontStyle.Regular))
                    e.Graphics.DrawString(montext.Text, new System.Drawing.Font(lstResultats.Items[e.Index].ToString(),
                        taille, FontStyle.Regular), Brushes.Black, e.Bounds);

                else if (FF.IsStyleAvailable(FontStyle.Bold))
                    e.Graphics.DrawString(montext.Text, new System.Drawing.Font(lstResultats.Items[e.Index].ToString(),
                        taille, FontStyle.Bold), Brushes.Black, e.Bounds);

                else if (FF.IsStyleAvailable(FontStyle.Italic))
                    e.Graphics.DrawString(montext.Text, new System.Drawing.Font(lstResultats.Items[e.Index].ToString(),
                        taille, FontStyle.Italic), Brushes.Black, e.Bounds);

                else if (FF.IsStyleAvailable(FontStyle.Strikeout))
                    e.Graphics.DrawString(montext.Text, new System.Drawing.Font(lstResultats.Items[e.Index].ToString(),
                        taille, FontStyle.Strikeout), Brushes.Black, e.Bounds);

                else if (FF.IsStyleAvailable(FontStyle.Underline))
                    e.Graphics.DrawString(montext.Text, new System.Drawing.Font(lstResultats.Items[e.Index].ToString(),
                        taille, FontStyle.Underline), Brushes.Black, e.Bounds);
            }
        }

        private void lstResultats_DoubleClick(object sender, EventArgs e)
        {
            lstFntChoisi.Items.Add(lstResultats.SelectedItem);
        }

        private void lstFntChoisi_DoubleClick(object sender, EventArgs e)
        {
            lstFntChoisi.Items.Remove(lstFntChoisi.SelectedItem);
        }

        private void montext_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(montext.Text)) btn_Demarrer.Enabled = true;
            else btn_Demarrer.Enabled = false;
        }

        private void montext_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && string.IsNullOrWhiteSpace(montext.Text))
            {
                MessageBox.Show("Vous devez saisir quelque chose pour afficher le resultat", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(montext.Text)) btn_Demarrer_Click(sender, e);
        }

        private void Taille_KeyDown(object sender, KeyEventArgs e)
        {
            montext_KeyDown(sender, e);
        }

        private void Taille_Enter(object sender, EventArgs e)
        {
            Taille.Select(0, Taille.Text.Length);
        }

        private void FormFont_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && string.IsNullOrWhiteSpace(montext.Text))
            {
                btn_Demarrer.TabStop = true;
            }
        }
    }
}
