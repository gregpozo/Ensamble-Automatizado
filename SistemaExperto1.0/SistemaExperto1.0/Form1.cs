using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SistemaExperto1._0
{
    public partial class Form1 : MetroForm
    {
        int NUMERO_MIN, CANTIDAD = 0,INDICE_SHOW = 1, ALFA, BETA, ALFABETA, NUMERO, TNmin = 0;
        string HC, IC, PIEZA, USUARIO;
        double ESPESOR = 0, TAMAÑO =0, HT, IT, TTA, EFF, ToTTA;
        bool Error, buttonWasClicked15, buttonWasClicked14, buttonWasClicked16, buttonWasClicked17, buttonWasClicked13, buttonWasClicked12,
            buttonWasClicked11, buttonWasClicked10, buttonWasClicked9, buttonWasClicked8, buttonWasClicked7, buttonWasClicked6, buttonWasClicked5,
            buttonWasClicked4;
        public Form1()
        {
            InitializeComponent();
            metroPanel1.BringToFront();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Table' table. You can move, or remove it, as needed.
            this.tableTableAdapter.Fill(this.database1DataSet.Table);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            
        }

        private void metroTabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {

        }

        private void metroTabControl1_SelectedIndexChanged(Object sender, EventArgs e)
        {

            MessageBox.Show("You are in the TabControl.SelectedIndexChanged event.");

        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            metroPanel1.BringToFront();
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            if (metroTextBox1.Text != String.Empty && metroComboBox1.SelectedItem != null)
            {
                metroTabControl1.SelectedIndex = 0;
                metroPanel2.BringToFront();
                panel1.Visible = true;
                metroTextBox3.Visible = true;
                metroTextBox4.Visible = true;
                panel2.Visible = false;
                metroTextBox6.Visible = false;
                metroTextBox7.Visible = false;
            }

            else if (metroTextBox1.Text == String.Empty)
            {
                MessageBox.Show("Necesitas introducir tu nombre");
            }

            else if (metroComboBox1.SelectedItem == null)
            {
                MessageBox.Show("Necesitas elegir el numero de piezas");
            }

            metroTile4.Visible = false;
            metroTile5.Visible = false;
            metroTile6.Visible = false;
            metroTile7.Visible = false;
            metroTile8.Visible = false;
            metroTile9.Visible = false;
            metroTile10.Visible = false;
            metroTile11.Visible = false;
            metroTile12.Visible = false;
            metroTile13.Visible = false;
            metroTile14.Visible = false;
            metroTile15.Visible = false;
            metroTile16.Visible = false;
            metroTile17.Visible = false;

        }

        private void metroRadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void addToTable()
        {
            SqlConnection cn = new SqlConnection(global::SistemaExperto1._0.Properties.Settings.Default.Database1ConnectionString);

            try
            {
                string sql = "INSERT INTO [Table] (No,Parte,Cant,α,β,[α+β],HC,HT,ic,IT,[HT+IT],Nmin) values(@Orden,@Pieza,@Cant,@alfa,@beta,@albet,@HC,@HT,@ic,@IT,@HIT,@Nmin)"; //SELECT RIGHT('00'+CONVERT(VARCHAR,ic),6) AS ic FROM [Table]


                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@Orden", INDICE_SHOW);
                    cmd.Parameters.AddWithValue("@Pieza", PIEZA);
                    cmd.Parameters.AddWithValue("@Cant", NUMERO);
                    cmd.Parameters.AddWithValue("@alfa", ALFA);
                    cmd.Parameters.AddWithValue("@beta", BETA);
                    cmd.Parameters.AddWithValue("@albet", ALFABETA);
                    cmd.Parameters.AddWithValue("@HT", HT);
                    cmd.Parameters.AddWithValue("@IT", IT);
                    cmd.Parameters.AddWithValue("@HIT", TTA);
                    cmd.Parameters.AddWithValue("@Nmin", NUMERO_MIN);
                    cmd.Parameters.AddWithValue("@HC", HC);
                    cmd.Parameters.AddWithValue("@ic", IC);
                    cmd.ExecuteNonQuery();
                }
                
                this.tableTableAdapter.Fill(this.database1DataSet.Table);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                cn.Close();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeleteFromTable();
        }

        public void DeleteFromTable()
        {
            SqlConnection con = new SqlConnection(global::SistemaExperto1._0.Properties.Settings.Default.Database1ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from [Table] where No=@Orden", con);
            cmd.Parameters.AddWithValue("@Orden", INDICE_SHOW);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void metroTile3_Click(object sender, EventArgs e)
        {
            USUARIO = metroTextBox1.Text;
            CANTIDAD = Convert.ToInt32(metroComboBox1.SelectedItem);
            PIEZA = metroTextBox2.Text;
            NUMERO = Convert.ToInt32(metroComboBox2.SelectedItem);
            angulosPieza();
            medidasPieza();
            Handling();
            Insertion_and_Union();
            HT = HT * NUMERO;
            //Insertion_and_Union();
            NUMERO_MIN *= CANTIDAD;
            IT = IT * NUMERO;
            numeroMinimo();
            TTA = HT + IT;
            //Acumuladores para la formula
            TNmin += NUMERO_MIN;
            ToTTA += TTA;
            EFF = TNmin * 2.63 / ToTTA * 100;
            //Eficiencia.Text = Ef.ToString("00.00") + "%";
            if(metroTextBox2.Text == String.Empty || metroComboBox2.SelectedItem == null || (!metroRadioButton3.Checked && !metroRadioButton4.Checked && !metroRadioButton5.Checked && !metroRadioButton6.Checked && !metroRadioButton7.Checked && !metroRadioButton8.Checked)
            || (!metroRadioButton9.Checked && !metroRadioButton10.Checked)|| (!metroRadioButton15.Checked && !metroRadioButton16.Checked && !metroRadioButton18.Checked) || (!metroRadioButton12.Checked && metroComboBox3.SelectedItem == null)
            || (!metroRadioButton13.Checked && !metroRadioButton14.Checked) || (!metroRadioButton11.Checked && !metroRadioButton17.Checked))
            {
                MessageBox.Show("Debes llenar todos los campos");
            }

            if (!(metroTextBox2.Text == String.Empty || metroComboBox2.SelectedItem == null || (!metroRadioButton3.Checked && !metroRadioButton4.Checked && !metroRadioButton5.Checked && !metroRadioButton6.Checked && !metroRadioButton7.Checked && !metroRadioButton8.Checked)
                || (!metroRadioButton9.Checked && !metroRadioButton10.Checked)|| (!metroRadioButton15.Checked && !metroRadioButton16.Checked && !metroRadioButton18.Checked) || (!metroRadioButton12.Checked && metroComboBox3.SelectedItem == null)
                || (!metroRadioButton13.Checked && !metroRadioButton14.Checked) || (!metroRadioButton11.Checked && !metroRadioButton17.Checked)) && INDICE_SHOW != CANTIDAD + 1)
            {
                addToTable();
                metroLabel16.Text = "Eficiencia = " + Convert.ToString(EFF);
                limpiarPanel();
            }

            if (INDICE_SHOW == CANTIDAD + 1)
            {
                metroPanel4.BringToFront();
            }
            
        }

        private void numeroMinimo()
        {
            if (!Nonecesaria.Checked && (radioButton7.Checked || radioButton8.Checked
                || Base.Checked || radioButton10.Checked))
            {
                NUMERO_MIN = 1;
            }
            else
            {
                NUMERO_MIN = 0;
            }
        }


        //Handling
        public void Handling()
        {
            //ONE HAND
            if (metroRadioButton9.Checked)
            {
                //One hand without the aid of grasping tools
                if (metroRadioButton12.Checked)
                {
                    //Parts are easy to grasp and manipulate
                    if (metroRadioButton18.Checked && metroRadioButton14.Checked)
                    {
                        if (ESPESOR > 2)
                        {
                            if (TAMAÑO > 15)
                            {
                                if (ALFABETA < 360)
                                {
                                    HC = "00";
                                    HT = 1.13;
                                }

                                else if (ALFABETA >= 360 && ALFABETA < 540)
                                {
                                    HC = "10";
                                    HT = 1.5;
                                }

                                else if (ALFABETA >= 540 && ALFABETA < 720)
                                {
                                    HC = "20";
                                    HT = 1.8;
                                }

                                else
                                {
                                    HC = "30";
                                    HT = 1.95;
                                }
                            }

                          // 6mm <= Size < 15mm
                            else if (TAMAÑO >= 6 && TAMAÑO < 15)
                            {
                                if (ALFABETA < 360)
                                {
                                    HC = "01";
                                    HT = 1.43;
                                }

                                else if (ALFABETA >= 360 && ALFABETA < 540)
                                {
                                    HC = "11";
                                    HT = 1.8;
                                }

                                else if (ALFABETA >= 540 && ALFABETA < 720)
                                {
                                    HC = "21";
                                    HT = 2.1;
                                }

                                else
                                {
                                    HC = "31";
                                    HT = 2.25;
                                }
                            }

                          // Size < 6mm
                            else if(TAMAÑO < 6)
                            {
                                if (ALFABETA < 360)
                                {
                                    HC = "02";
                                    HT = 1.88;
                                }

                                else if (ALFABETA >= 360 && ALFABETA < 540)
                                {
                                    HC = "12";
                                    HT = 2.25;
                                }

                                else if (ALFABETA >= 540 && ALFABETA < 720)
                                {
                                    HC = "22";
                                    HT = 2.55;
                                }

                                else
                                {
                                    HC = "32";
                                    HT = 2.7;
                                }
                            }
                        }

                      // Thickness <= 2mm
                        else
                        {
                            if (TAMAÑO > 6)
                            {
                                if (ALFABETA < 360)
                                {
                                    HC = "03";
                                    HT = 1.69;
                                }

                                else if (ALFABETA >= 360 && ALFABETA < 540)
                                {
                                    HC = "13";
                                    HT = 2.06;
                                }

                                else if (ALFABETA >= 540 && ALFABETA < 720)
                                {
                                    HC = "23";
                                    HT = 2.36;
                                }

                                else
                                {
                                    HC = "33";
                                    HT = 2.51;
                                }

                            }

                        // Size <= 6mm
                            else
                            {
                                if (ALFABETA < 360)
                                {
                                    HC = "04";
                                    HT = 2.18;
                                }

                                else if (ALFABETA >= 360 && ALFABETA < 540)
                                {
                                    HC = "14";
                                    HT = 2.55;
                                }

                                else if (ALFABETA >= 540 && ALFABETA < 720)
                                {
                                    HC = "24";
                                    HT = 2.85;
                                }

                                else
                                {
                                    HC = "34";
                                    HT = 3;
                                }
                            }

                        }

                    }

                // Parts present handling difficulties
                    else
                    {
                        if (ESPESOR > 2)
                        {
                            if (TAMAÑO > 15)
                            {
                                if (ALFABETA < 360)
                                {
                                    HC = "05";
                                    HT = 1.84;
                                }

                                else if (ALFABETA >= 360 && ALFABETA < 540)
                                {
                                    HC = "15";
                                    HT = 2.25;
                                }

                                else if (ALFABETA >= 540 && ALFABETA < 720)
                                {
                                    HC = "25";
                                    HT = 2.57;
                                }

                                else
                                {
                                    HC = "35";
                                    HT = 2.73;
                                }
                            }

                        //  6mm <= size <= 15mm
                            else if (TAMAÑO >= 6 && TAMAÑO < 15)
                            {
                                if (ALFABETA < 360)
                                {
                                    HC = "06";
                                    HT = 2.17;
                                }

                                else if (ALFABETA >= 360 && ALFABETA < 540)
                                {
                                    HC = "16";
                                    HT = 2.57;
                                }

                                else if (ALFABETA >= 540 && ALFABETA < 720)
                                {
                                    HC = "26";
                                    HT = 2.9;
                                }

                                else
                                {
                                    HC = "36";
                                    HT = 3.06;
                                }
                            }

                        // size < 6mm
                            else
                            {
                                if (ALFABETA < 360)
                                {
                                    HC = "07";
                                    HT = 2.65;
                                }

                                else if (ALFABETA >= 360 && ALFABETA < 540)
                                {
                                    HC = "17";
                                    HT = 3.06;
                                }

                                else if (ALFABETA >= 540 && ALFABETA < 720)
                                {
                                    HC = "27";
                                    HT = 3.38;
                                }

                                else
                                {
                                    HC = "37";
                                    HT = 3.55;
                                }
                            }
                        }

                    // Thickness <= 2mm
                        else
                        {
                            if (TAMAÑO > 6)
                            {
                                if (ALFABETA < 360)
                                {
                                    HC = "08";
                                    HT = 2.45;
                                }

                                else if (ALFABETA >= 360 && ALFABETA < 540)
                                {
                                    HC = "18";
                                    HT = 3;
                                }

                                else if (ALFABETA >= 540 && ALFABETA < 720)
                                {
                                    HC = "28";
                                    HT = 3.18;
                                }

                                else
                                {
                                    HC = "38";
                                    HT = 3.34;
                                }
                            }

                         //  size <= 6mm
                            else
                            {
                                if (ALFABETA < 360)
                                {
                                    HC = "09";
                                    HT = 2.98;
                                }

                                else if (ALFABETA >= 360 && ALFABETA < 540)
                                {
                                    HC = "19";
                                    HT = 3.38;
                                }

                                else if (ALFABETA >= 540 && ALFABETA < 720)
                                {
                                    HC = "29";
                                    HT = 3.7;
                                }

                                else
                                {
                                    HC = "39";
                                    HT = 4;
                                }
                            }

                        }
                    }

                }

            // One Hand with Grasping aids
                //Need tweezers for grasping and manipulation
                else if (metroComboBox3.SelectedIndex == 0)
                {
                    // Without optical magnification
                    if (!metroRadioButton15.Checked)
                    {
                        // Easy to manipulate
                        if (metroRadioButton14.Checked)
                        {
                            if (ESPESOR > 0.25)
                            {
                                if (ALFA <= 180)
                                {
                                    if (BETA >= 0 && BETA <= 180)
                                    {
                                        HC = "40";
                                        HT = 3.6;
                                    }
                                    else if (BETA == 360)
                                    {
                                        HC = "50";
                                        HT = 4;
                                    }
                                }
                                else if (ALFA == 360)
                                {
                                    if (BETA >= 0 && BETA <= 180)
                                    {
                                        HC = "60";
                                        HT = 4.8;
                                    }
                                    else if (BETA == 360)
                                    {
                                        HC = "70";
                                        HT = 5.1;
                                    }
                                }
                            }

                        // Thickness <= 0.25mm
                            else
                            {
                                if (ALFA <= 180)
                                {
                                    if (BETA >= 0 && BETA <= 180)
                                    {
                                        HC = "41";
                                        HT = 6.85;
                                    }
                                    else if (BETA == 360)
                                    {
                                        HC = "51";
                                        HT = 7.25;
                                    }
                                }
                                else if (ALFA == 360)
                                {
                                    if (BETA >= 0 && BETA <= 180)
                                    {
                                        HC = "61";
                                        HT = 8.05;
                                    }
                                    else if (BETA == 360)
                                    {
                                        HC = "71";
                                        HT = 8.35;
                                    }
                                }
                            }
                        }

                    // Present handling difficulties
                        else
                        {
                            if (ESPESOR > 0.25)
                            {
                                if (ALFA <= 180)
                                {
                                    if (BETA >= 0 && BETA <= 180)
                                    {
                                        HC = "42";
                                        HT = 4.35;
                                    }
                                    else if (BETA == 360)
                                    {
                                        HC = "52";
                                        HT = 4.75;
                                    }
                                }
                                else if (ALFA == 360)
                                {
                                    if (BETA >= 0 && BETA <= 180)
                                    {
                                        HC = "62";
                                        HT = 5.55;
                                    }
                                    else if (BETA == 360)
                                    {
                                        HC = "72";
                                        HT = 5.85;
                                    }
                                }
                            }

                        // Thickness <= 0.25mm
                            else
                            {
                                if (ALFA <= 180)
                                {
                                    if (BETA >= 0 && BETA <= 180)
                                    {
                                        HC = "43";
                                        HT = 7.6;
                                    }
                                    else if (BETA == 360)
                                    {
                                        HC = "53";
                                        HT = 8;
                                    }
                                }
                                else if (ALFA == 360)
                                {
                                    if (BETA >= 0 && BETA <= 180)
                                    {
                                        HC = "63";
                                        HT = 8.8;
                                    }
                                    else if (BETA == 360)
                                    {
                                        HC = "73";
                                        HT = 9.1;
                                    }
                                }
                            }
                        }
                    }

                 //Parts require optical magnification
                    else
                    {
                        // Easy to manipulate
                        if (metroRadioButton14.Checked)
                        {
                            if (ESPESOR > 0.25)
                            {
                                if (ALFA <= 180)
                                {
                                    if (BETA >= 0 && BETA <= 180)
                                    {
                                        HC = "44";
                                        HT = 5.6;
                                    }
                                    else if (BETA == 360)
                                    {
                                        HC = "54";
                                        HT = 6;
                                    }
                                }
                                else if (ALFA == 360)
                                {
                                    if (BETA >= 0 && BETA <= 180)
                                    {
                                        HC = "64";
                                        HT = 6.8;
                                    }
                                    else if (BETA == 360)
                                    {
                                        HC = "74";
                                        HT = 7.1;
                                    }
                                }
                            }

                        // Thickness <= 0.25mm
                            else
                            {
                                if (ALFA <= 180)
                                {
                                    if (BETA >= 0 && BETA <= 180)
                                    {
                                        HC = "45";
                                        HT = 8.35;
                                    }
                                    else if (BETA == 360)
                                    {
                                        HC = "55";
                                        HT = 8.75;
                                    }
                                }
                                else if (ALFA == 360)
                                {
                                    if (BETA >= 0 && BETA <= 180)
                                    {
                                        HC = "65";
                                        HT = 9.55;
                                    }
                                    else if (BETA == 360)
                                    {
                                        HC = "75";
                                        HT = 9.55;
                                    }
                                }
                            }
                        }

                        // Present handling difficulties
                        else
                        {
                            if (ESPESOR > 0.25)
                            {
                                if (ALFA <= 180)
                                {
                                    if (BETA >= 0 && BETA <= 180)
                                    {
                                        HC = "46";
                                        HT = 6.35;
                                    }
                                    else if (BETA == 360)
                                    {
                                        HC = "56";
                                        HT = 6.75;
                                    }
                                }
                                else if (ALFA == 360)
                                {
                                    if (BETA >= 0 && BETA <= 180)
                                    {
                                        HC = "66";
                                        HT = 7.55;
                                    }
                                    else if (BETA == 360)
                                    {
                                        HC = "76";
                                        HT = 7.85;
                                    }
                                }
                            }

                        // Thickness <= 0.25mm
                            else
                            {
                                if (ALFA <= 180)
                                {
                                    if (BETA >= 0 && BETA <= 180)
                                    {
                                        HC = "47";
                                        HT = 8.6;
                                    }
                                    else if (BETA == 360)
                                    {
                                        HC = "57";
                                        HT = 9;
                                    }
                                }
                                else if (ALFA == 360)
                                {
                                    if (BETA >= 0 && BETA <= 180)
                                    {
                                        HC = "67";
                                        HT = 9.8;
                                    }
                                    else if (BETA == 360)
                                    {
                                        HC = "77";
                                        HT = 10.1;
                                    }
                                }
                            }
                        }

                    }
                }

            //Parts need Standard tools other than tweezers
                else if (metroComboBox3.SelectedIndex == 1)
                {
                    if (ALFA <= 180)
                    {
                        if (BETA >= 0 && BETA <= 180)
                        {
                            HC = "48";
                            HT = 7;
                        }
                        else if (BETA == 360)
                        {
                            HC = "58";
                            HT = 8;
                        }
                    }
                    else if (ALFA == 360)
                    {
                        if (BETA >= 0 && BETA <= 180)
                        {
                            HC = "68";
                            HT = 8;
                        }
                        else if (BETA == 360)
                        {
                            HC = "78";
                            HT = 9;
                        }
                    }
                }

            // Parts Need Special tools
                else if (metroComboBox3.SelectedIndex == 2)
                {
                    if (ALFA <= 180)
                    {
                        if (BETA >= 0 && BETA <= 180)
                        {
                            HC = "49";
                            HT = 7;
                        }
                        else if (BETA == 360)
                        {
                            HC = "59";
                            HT = 8;
                        }
                    }
                    else if (ALFA == 360)
                    {
                        if (BETA >= 0 && BETA <= 180)
                        {
                            HC = "69";
                            HT = 8;
                        }
                        else if (BETA == 360)
                        {
                            HC = "79";
                            HT = 9;
                        }
                    }
                }

            }

                // Two hands
            else
            {
                //Parts severely nest or tangle or are flexible 
                //but can be grasped and lifted with one hand
                if (metroRadioButton20.Checked)
                {
                    // Easy to manipulate
                    if (metroRadioButton14.Checked)
                    {
                        if (ALFA <= 180)
                        {
                            if (TAMAÑO > 15)
                            {
                                HC = "80";
                                HT = 4.1;
                            }
                            else if (TAMAÑO >= 6 && TAMAÑO <= 15)
                            {
                                HC = "81";
                                HT = 4.5;
                            }
                            else
                            {
                                HC = "82";
                                HT = 5.1;
                            }
                        }
                        else if (ALFA == 360)
                        {
                            if (TAMAÑO > 6)
                            {
                                HC = "83";
                                HT = 5.6;
                            }
                            else
                            {
                                HC = "84";
                                HT = 6.75;
                            }
                        }
                    }

                    //Handling difficulties
                    else
                    {
                        if (ALFA <= 180)
                        {
                            if (TAMAÑO > 15)
                            {
                                HC = "85";
                                HT = 5;
                            }
                            else if (TAMAÑO >= 6 && TAMAÑO <= 15)
                            {
                                HC = "86";
                                HT = 5.25;
                            }
                            else
                            {
                                HC = "87";
                                HT = 5.85;
                            }
                        }
                        else if (ALFA == 360)
                        {
                            if (TAMAÑO > 6)
                            {
                                HC = "88";
                                HT = 6.35;
                            }
                            else
                            {
                                HC = "89";
                                HT = 7;
                            }
                        }
                    }
                }

            //Two hands or assistance required for large size
                else
                {
                    // Can be handled by one person without mechanical assistance
                    if (!metroRadioButton16.Checked || metroRadioButton18.Checked)
                    {
                        //Parts do not severely nest or tangle and are not flexible
                        if (!metroRadioButton20.Checked)
                        {
                            //Part weight < 10 lb, Light
                            if (metroRadioButton1.Checked)
                            {
                                // Easy to manipulate
                                if (metroRadioButton14.Checked)
                                {
                                    if (ALFA <= 180)
                                    {
                                        HC = "90";
                                        HT = 2;
                                    }
                                    else if (ALFA == 360)
                                    {
                                        HC = "91";
                                        HT = 3;
                                    }
                                }
                                // Handling difficulties
                                else
                                {
                                    if (ALFA <= 180)
                                    {
                                        HC = "92";
                                        HT = 2;
                                    }
                                    else if (ALFA == 360)
                                    {
                                        HC = "93";
                                        HT = 3;
                                    }
                                }
                            }

                            //Part weight > 10 lb, heavy
                            else
                            {
                                // Easy to manipulate
                                if (metroRadioButton14.Checked)
                                {
                                    if (ALFA <= 180)
                                    {
                                        HC = "94";
                                        HT = 3;
                                    }
                                    else if (ALFA == 360)
                                    {
                                        HC = "95";
                                        HT = 4;
                                    }
                                }
                                // Handling difficulties
                                else
                                {
                                    if (ALFA <= 180)
                                    {
                                        HC = "96";
                                        HT = 4;
                                    }
                                    else if (ALFA == 360)
                                    {
                                        HC = "97";
                                        HT = 5;
                                    }
                                }
                            }
                        }

                    // Parts severely nest or tangle or are flexible
                        else
                        {
                            HC = "98";
                            HT = 7;
                        }
                    }

                    //Two persons or mechanical assistance required
                    else if (metroRadioButton16.Checked)
                    {
                        HC = "99";
                        HT = 9;
                    }
                }

            }
        }


        //Insertion and Union
        private void Insertion_and_Union()
        {
            if (!Base.Checked)
            {
                //Operacion no separada
                if (metroRadioButton17.Checked)
                {
                    // No Holding Down required
                    if (buttonWasClicked10)
                    {
                        //Easy to align and position
                        if (!notEasyA.Checked)
                        {
                            //No resistance to insertion
                            if (!Resiste.Checked)
                            {
                                //It can easily reach the desired location
                                if (!RstAccess.Checked && !RstVision.Checked)
                                {
                                    IC = "00";
                                    IT = 1.5;
                                }
                                // Obstructed access AND restricted vision
                                else if (RstAccess.Checked && RstVision.Checked)
                                {
                                    IC = "20";
                                    IT = 5.5;
                                }
                                // Obstructed access OR restricted vision
                                else
                                {
                                    IC = "10";
                                    IT = 4;
                                }
                            }
                            //Resistance to insertion
                            else
                            {
                                //It can easily reach the desired location
                                if (!RstAccess.Checked && !RstVision.Checked)
                                {
                                    IC = "01";
                                    IT = 2.5;
                                }
                                // Obstructed access AND restricted vision
                                else if (RstAccess.Checked && RstVision.Checked)
                                {
                                    IC = "21";
                                    IT = 6.5;
                                }
                                // Obstructed access OR restricted vision
                                else
                                {
                                    IC = "11";
                                    IT = 5;
                                }
                            }
                        }

                        //Not easy to align or position
                        else
                        {
                            //No resistance to insertion
                            if (!Resiste.Checked)
                            {
                                //It can easily reach the desired location
                                if (!RstAccess.Checked && !RstVision.Checked)
                                {
                                    IC = "02";
                                    IT = 2.5;
                                }
                                // Obstructed access AND restricted vision
                                else if (RstAccess.Checked && RstVision.Checked)
                                {
                                    IC = "22";
                                    IT = 6.5;
                                }
                                // Obstructed access OR restricted vision
                                else
                                {
                                    IC = "12";
                                    IT = 5;
                                }
                            }
                            //Resistance to insertion
                            else
                            {
                                //It can easily reach the desired location
                                if (!RstAccess.Checked && !RstVision.Checked)
                                {
                                    IC = "03";
                                    IT = 3.5;
                                }
                                // Obstructed access AND restricted vision
                                else if (RstAccess.Checked && RstVision.Checked)
                                {
                                    IC = "23";
                                    IT = 7.5;
                                }
                                // Obstructed access OR restricted vision
                                else
                                {
                                    IC = "13";
                                    IT = 6;
                                }
                            }
                        }
                    }

                    //Holding down required
                    else if (buttonWasClicked5)
                    {
                        //Easy to align and position
                        if (!notEasyA.Checked)
                        {
                            //No resistance to insertion
                            if (!Resiste.Checked)
                            {
                                //It can easily reach the desired location
                                if (!RstAccess.Checked && !RstVision.Checked)
                                {
                                    IC = "06";
                                    IT = 5.5;
                                }
                                // Obstructed access AND restricted vision
                                else if (RstAccess.Checked && RstVision.Checked)
                                {
                                    IC = "26";
                                    IT = 9.5;
                                }
                                // Obstructed access OR restricted vision
                                else
                                {
                                    IC = "16";
                                    IT = 8;
                                }
                            }
                            //Resistance to insertion
                            else
                            {
                                //It can easily reach the desired location
                                if (!RstAccess.Checked && !RstVision.Checked)
                                {
                                    IC = "07";
                                    IT = 6.5;
                                }
                                // Obstructed access AND restricted vision
                                else if (RstAccess.Checked && RstVision.Checked)
                                {
                                    IC = "27";
                                    IT = 10.5;
                                }
                                // Obstructed access OR restricted vision
                                else
                                {
                                    IC = "17";
                                    IT = 9;
                                }
                            }
                        }

                        //Not easy to align or position
                        else
                        {
                            //No resistance to insertion
                            if (!Resiste.Checked)
                            {
                                //It can easily reach the desired location
                                if (!RstAccess.Checked && !RstVision.Checked)
                                {
                                    IC = "08";
                                    IT = 6.5;
                                }
                                // Obstructed access AND restricted vision
                                else if (RstAccess.Checked && RstVision.Checked)
                                {
                                    IC = "28";
                                    IT = 10.5;
                                }
                                // Obstructed access OR restricted vision
                                else
                                {
                                    IC = "18";
                                    IT = 9;
                                }
                            }
                            //Resistance to insertion
                            else
                            {
                                //It can easily reach the desired location
                                if (!RstAccess.Checked && !RstVision.Checked)
                                {
                                    IC = "09";
                                    IT = 7.5;
                                }
                                // Obstructed access AND restricted vision
                                else if (RstAccess.Checked && RstVision.Checked)
                                {
                                    IC = "29";
                                    IT = 11.5;
                                }
                                // Obstructed access OR restricted vision
                                else
                                {
                                    IC = "19";
                                    IT = 10;
                                }
                            }
                        }
                    }

                    //No screw operation or plastic deformation immediately after insertion
                    else if (buttonWasClicked6)
                    {
                        //Easy to align and position with no resistance to insertion
                        if (!notEasyA.Checked && !Resiste.Checked)
                        {
                            //It can easily reach the desired location
                            if (!RstAccess.Checked && !RstVision.Checked)
                            {
                                IC = "30";
                                IT = 2;
                            }
                            // Obstructed access AND restricted vision
                            else if (RstAccess.Checked && RstVision.Checked)
                            {
                                IC = "50";
                                IT = 6;
                            }
                            // Obstructed access OR restricted vision
                            else
                            {
                                IC = "40";
                                IT = 4.5;
                            }
                        }
                        //Not easy to align or position AND/OR resistance to insertion
                        else
                        {
                            //It can easily reach the desired location
                            if (!RstAccess.Checked && !RstVision.Checked)
                            {
                                IC = "31";
                                IT = 5;
                            }
                            // Obstructed access AND restricted vision
                            else if (RstAccess.Checked && RstVision.Checked)
                            {
                                IC = "51";
                                IT = 9;
                            }
                            // Obstructed access OR restricted vision
                            else
                            {
                                IC = "41";
                                IT = 7.5;
                            }
                        }
                    }

                    // Plastic deformation and Plastic bending or torsion
                    else if (buttonWasClicked7)
                    {
                        //Easy to align and position
                        if (!notEasyA.Checked)
                        {
                            //It can easily reach the desired location
                            if (!RstAccess.Checked && !RstVision.Checked)
                            {
                                IC = "32";
                                IT = 4;
                            }
                            // Obstructed access AND restricted vision
                            else if (RstAccess.Checked && RstVision.Checked)
                            {
                                IC = "52";
                                IT = 8;
                            }
                            // Obstructed access OR restricted vision
                            else
                            {
                                IC = "42";
                                IT = 6.5;
                            }
                        }
                        // Not easy to align or position
                        else
                        {
                            //No resistance to insertion
                            if (!Resiste.Checked)
                            {
                                //It can easily reach the desired location
                                if (!RstAccess.Checked && !RstVision.Checked)
                                {
                                    IC = "33";
                                    IT = 5;
                                }
                                // Obstructed access AND restricted vision
                                else if (RstAccess.Checked && RstVision.Checked)
                                {
                                    IC = "53";
                                    IT = 9;
                                }
                                // Obstructed access OR restricted vision
                                else
                                {
                                    IC = "43";
                                    IT = 7.5;
                                }
                            }

                            //Resistance to insertion
                            else
                            {
                                //It can easily reach the desired location
                                if (!RstAccess.Checked && !RstVision.Checked)
                                {
                                    IC = "34";
                                    IT = 6;
                                }
                                // Obstructed access AND restricted vision
                                else if (RstAccess.Checked && RstVision.Checked)
                                {
                                    IC = "54";
                                    IT = 10;
                                }
                                // Obstructed access OR restricted vision
                                else
                                {
                                    IC = "44";
                                    IT = 8.5;
                                }
                            }
                        }
                    }

                    //Rivetting or similar operation
                    else if (buttonWasClicked8)
                    {
                        //Easy to align and position
                        if (!notEasyA.Checked)
                        {
                            //It can easily reach the desired location
                            if (!RstAccess.Checked && !RstVision.Checked)
                            {
                                IC = "35";
                                IT = 7;
                            }
                            // Obstructed access AND restricted vision
                            else if (RstAccess.Checked && RstVision.Checked)
                            {
                                IC = "55";
                                IT = 11;
                            }
                            // Obstructed access OR restricted vision
                            else
                            {
                                IC = "45";
                                IT = 9.5;
                            }
                        }
                        // Not easy to align or position
                        else
                        {
                            //No resistance to insertion
                            if (!Resiste.Checked)
                            {
                                //It can easily reach the desired location
                                if (!RstAccess.Checked && !RstVision.Checked)
                                {
                                    IC = "36";
                                    IT = 8;
                                }
                                // Obstructed access AND restricted vision
                                else if (RstAccess.Checked && RstVision.Checked)
                                {
                                    IC = "56";
                                    IT = 12;
                                }
                                // Obstructed access OR restricted vision
                                else
                                {
                                    IC = "46";
                                    IT = 10.5;
                                }
                            }

                            //Resistance to insertion
                            else
                            {
                                //It can easily reach the desired location
                                if (!RstAccess.Checked && !RstVision.Checked)
                                {
                                    IC = "37";
                                    IT = 9;
                                }
                                // Obstructed access AND restricted vision
                                else if (RstAccess.Checked && RstVision.Checked)
                                {
                                    IC = "57";
                                    IT = 13;
                                }
                                // Obstructed access OR restricted vision
                                else
                                {
                                    IC = "47";
                                    IT = 11.5;
                                }
                            }
                        }
                    }

                    //Screw tightening inmediately after insertion
                    else if (buttonWasClicked9)
                    {
                        //Easy to align and position with no resistance to insertion
                        if (!notEasyA.Checked && !Resiste.Checked)
                        {
                            //It can easily reach the desired location
                            if (!RstAccess.Checked && !RstVision.Checked)
                            {
                                IC = "38";
                                IT = 6;
                            }
                            // Obstructed access AND restricted vision
                            else if (RstAccess.Checked && RstVision.Checked)
                            {
                                IC = "58";
                                IT = 10;
                            }
                            // Obstructed access OR restricted vision
                            else
                            {
                                IC = "48";
                                IT = 8.5;
                            }
                        }
                        //Not easy to align or position AND/OR resistance to insertion
                        else
                        {
                            //It can easily reach the desired location
                            if (!RstAccess.Checked && !RstVision.Checked)
                            {
                                IC = "39";
                                IT = 8;
                            }
                            // Obstructed access AND restricted vision
                            else if (RstAccess.Checked && RstVision.Checked)
                            {
                                IC = "59";
                                IT = 12;
                            }
                            // Obstructed access OR restricted vision
                            else
                            {
                                IC = "49";
                                IT = 10.5;
                            }
                        }
                    }
                }

                //Separate Operation. Assembly processes where all solid parts are in place.
                else
                {
                    //Bending or similar process
                    if (buttonWasClicked17)
                    {
                        IC = "90";
                        IT = 4;
                    }

                    //Rivetting or similar processes
                    else if (buttonWasClicked13)
                    {
                        IC = "91";
                        IT = 7;
                    }

                    //Screw tightening or other processes
                    else if (buttonWasClicked15)
                    {
                        IC = "92";
                        IT = 5;
                    }

                    //Bulk plastic deformation
                    else if (buttonWasClicked14)
                    {
                        IC = "93";
                        IT = 12;
                    }

                    //Metallurgical processes
                    else if (buttonWasClicked12)
                    {
                        //No aditional material required
                       // if (Noadittional.Checked)
                       // {
                       //     IC = "94";
                       //     IT = 7;
                       // }
                       //
                       // //Soldering processes
                       // else if (Soldering.Checked)
                       // {
                       //     IC = "95";
                       //     IT = 8;
                       // }
                       //
                        //Weld/braze processes
                        {
                            IC = "96";
                            IT = 12;
                        }
                    }

                    //Chemical processes
                    else if (buttonWasClicked11)
                    {
                        IC = "97";
                        IT = 12;
                    }

                    //Manipulation of parts or sub-assembly
                    else if (buttonWasClicked10)
                    {
                        IC = "98";
                        IT = 9;
                    }

                    else if (buttonWasClicked16)
                    {
                        IC = "99";
                        IT = 12;
                    }
                }
            }

            //Esta parte no es la base.
            else
            {
                IC = "00";
                IT = 1.5;
            }
        }   

        private void limpiarPanel()
        {
            INDICE_SHOW += 1 ;
            metroTextBox2.Text = "";
            metroTextBox3.Text = "";
            metroTextBox4.Text = "";
            metroTextBox6.Text = "";
            metroTextBox7.Text = "";
            metroComboBox2.SelectedIndex = 0;
            metroComboBox3.SelectedIndex = 0;
            metroRadioButton1.Checked = false;
            metroRadioButton2.Checked = false;
            metroRadioButton3.Checked = false;
            metroRadioButton4.Checked = false;
            metroRadioButton5.Checked = false;
            metroRadioButton6.Checked = false;
            metroRadioButton7.Checked = false;
            metroRadioButton8.Checked = false;
            metroRadioButton9.Checked = false;
            metroRadioButton10.Checked = false;
            metroRadioButton11.Checked = false;
            metroRadioButton12.Checked = false;
            metroRadioButton13.Checked = false;
            metroRadioButton14.Checked = false;
            metroRadioButton15.Checked = false;
            metroRadioButton16.Checked = false;
            metroRadioButton17.Checked = false;
            metroRadioButton18.Checked = false;
            metroRadioButton20.Checked = false;
            buttonWasClicked9 = false;
            buttonWasClicked8 = false;
            buttonWasClicked7 = false;
            buttonWasClicked6 = false;
            buttonWasClicked5 = false;
            buttonWasClicked4 = false;
            buttonWasClicked10 = false;
            buttonWasClicked11 = false;
            buttonWasClicked12 = false;
            buttonWasClicked13 = false;
            buttonWasClicked14 = false;
            buttonWasClicked15 = false;
            buttonWasClicked16 = false;
            buttonWasClicked17 = false;
            if (CANTIDAD == 0)
            {
                metroLabel3.Text = "Parte no.1";
            }
            else if (CANTIDAD != 0)
            {
                metroLabel3.Text = "Parte no." + Convert.ToString(INDICE_SHOW);
            }
            metroTabControl1.SelectedIndex = 0;
            metroPanel2.BringToFront();

        }

        private void angulosPieza()
        {
            if (metroRadioButton3.Checked)
            {
                ALFA = 0;
                BETA = 0;
            }
            else if (metroRadioButton4.Checked)
            {
                ALFA = 180;
                BETA = 0;
            }
            else if (metroRadioButton5.Checked)
            {
                ALFA = 180;
                BETA = 90;
            }
            else if (metroRadioButton6.Checked)
            {
                ALFA = 180;
                BETA = 180;
            }
            else if (metroRadioButton7.Checked)
            {
                ALFA = 360;
                BETA = 0;
            }
            else if (metroRadioButton8.Checked)
            {
                ALFA = 360;
                BETA = 360;
            }

            ALFABETA = ALFA + BETA;
        }

        private void medidasPieza()
        {
            if (metroTextBox3.Text != String.Empty && metroTextBox4.Text != String.Empty)
            {
                TAMAÑO = Convert.ToDouble(metroTextBox3.Text);
                ESPESOR = Convert.ToDouble(metroTextBox4.Text);
            }

            else if (metroTextBox6.Text != String.Empty && metroTextBox7.Text != String.Empty)
            {
                ESPESOR = Convert.ToDouble(metroTextBox7.Text);
                TAMAÑO = Convert.ToDouble(metroTextBox6.Text);
            }
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            metroTextBox3.Visible = true;
            metroTextBox4.Visible = true;
            panel2.Visible = false;
            metroTextBox6.Visible = false;
            metroTextBox7.Visible = false;
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            metroTextBox3.Visible = false;
            metroTextBox4.Visible = false;
            panel2.Visible = true;
            metroTextBox6.Visible = true;
            metroTextBox7.Visible = true;
        }

        private void metroTabPage2_Click(object sender, EventArgs e)
        {

        }

        private void metroRadioButton13_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void metroTile5_Click(object sender, EventArgs e)
        {

        }

        private void metroTabPage1_Click(object sender, EventArgs e)
        {

        }

        private void metroRadioButton11_CheckedChanged(object sender, EventArgs e)
        {
            metroTile4.Visible = false;
            metroTile5.Visible = false;
            metroTile6.Visible = false;
            metroTile7.Visible = false;
            metroTile8.Visible = false;
            metroTile9.Visible = false;
            metroTile10.Visible = true;
            metroTile11.Visible = true;
            metroTile12.Visible = true;
            metroTile13.Visible = true;
            metroTile14.Visible = true;
            metroTile15.Visible = true;
            metroTile16.Visible = true;
            metroTile17.Visible = true;
        }

        private void metroRadioButton17_CheckedChanged(object sender, EventArgs e)
        {
            metroTile4.Visible = true;
            metroTile5.Visible = true;
            metroTile6.Visible = true;
            metroTile7.Visible = true;
            metroTile8.Visible = true;
            metroTile9.Visible = true;
            metroTile10.Visible = false;
            metroTile11.Visible = false;
            metroTile12.Visible = false;
            metroTile13.Visible = false;
            metroTile14.Visible = false;
            metroTile15.Visible = false;
            metroTile16.Visible = false;
            metroTile17.Visible = false;
        }

        private void metroTile15_Click(object sender, EventArgs e)
        {
            buttonWasClicked15 = true;
        }

        private void metroTile14_Click(object sender, EventArgs e)
        {
            buttonWasClicked14 = true;
        }

        private void metroTile13_Click(object sender, EventArgs e)
        {
            buttonWasClicked13 = true;
        }

        private void metroTile17_Click(object sender, EventArgs e)
        {
            buttonWasClicked17 = true;
        }

        private void metroTile12_Click(object sender, EventArgs e)
        {
            buttonWasClicked12 = true;
        }

        private void metroTile11_Click(object sender, EventArgs e)
        {
            buttonWasClicked11 = true;
        }

        private void metroTile10_Click(object sender, EventArgs e)
        {
            buttonWasClicked10 = true;
        }

        private void metroTile16_Click(object sender, EventArgs e)
        {
            buttonWasClicked16 = true;
        }

        private void metroTile4_Click(object sender, EventArgs e)
        {
            buttonWasClicked4 = true;
        }

        private void metroTile5_Click_1(object sender, EventArgs e)
        {
            buttonWasClicked5 = true;
        }

        private void metroTile6_Click(object sender, EventArgs e)
        {
            buttonWasClicked6 = true;
        }

        private void metroTile7_Click(object sender, EventArgs e)
        {
            buttonWasClicked7 = true;
        }

        private void metroTile8_Click(object sender, EventArgs e)
        {
            buttonWasClicked8 = true;
        }

        private void metroTile9_Click(object sender, EventArgs e)
        {
            buttonWasClicked9 = true;
        }

        private void metroTabPage4_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel16_Click(object sender, EventArgs e)
        {

        }

        private void metroTile18_Click(object sender, EventArgs e)
        {
            DeleteFromTable();
            limpiarPanel();
            metroTextBox1.Text = "";
            metroComboBox1.SelectedIndex = 0;
            metroPanel1.BringToFront();
        }



    }
}
