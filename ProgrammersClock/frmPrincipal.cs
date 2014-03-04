using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProgrammersClock
{
    public partial class frmPrincipal : Form
    {
        int s = 0, m = 0, h = 0;
        Boolean fechar = false;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (chkSysTray.Checked == true && fechar == false)
            {
                e.Cancel = true;
                Hide();
            }
            else if (timer.Enabled == true)
            {
                DialogResult = MessageBox.Show("Você ainda está programando, tem certeza de que deseja fechar o programa?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult.ToString() == "No")
                {
                    e.Cancel = true;
                }
            }
            if (fechar = true)
                fechar = false;

        }

        private void btnAtivar_Click(object sender, EventArgs e)
        {
            ativar();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName(txtProcesso.Text);
            if (process.Length != 0)
            {
                if (s < 59)
                {
                    s++;
                }
                else
                {
                    s = 0;
                    m++;
                }

                if (m == 59)
                {
                    m = 0;
                    h++;
                }
                lblTempo.Text = "Tempo programando: " + h.ToString() + ":" + m.ToString() + ":" + s.ToString();
                notifyIcon.Text = "Você está programando há " + h.ToString() + " horas " + m.ToString() + " minutos e " + s.ToString() + " segundos";
            }
            else if (chkAuto.Checked == false)
            {
                txtProcesso.Enabled = true;
                btnAtivar.Enabled = true;
                btnAtivar.Text = "Ativar relógio";
                timer.Enabled = false;
                MessageBox.Show("O processo ''" + txtProcesso.Text + "'' morreu :'( ", "Luto!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                notifyIcon.Text = "Programmmer's Clock";
            }
        }

        private void txtProcesso_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ativar();
        }

        private void ativar()
        {
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName(txtProcesso.Text);

            if (timer.Enabled == false && process.Length != 0)
            {
                timer.Enabled = true;
                btnAtivar.Text = "Desativar relógio";
                txtProcesso.Enabled = false;
            }
            else if (process.Length == 0)
            {
                MessageBox.Show("O processo não foi encontrado!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                timer.Enabled = false;
                btnAtivar.Text = "Ativar relógio";
                txtProcesso.Enabled = true;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Copyright: Renann Prado - 2010\n darkness.renann@gmail.com", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            contextMenu.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            fechar = true;
            Close();
        }
    }
}
