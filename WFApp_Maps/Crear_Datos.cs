using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GemBox.Spreadsheet;
using GemBox.Spreadsheet.WinFormsUtilities;

//using Excel = Microsoft.Office.Interop.Excel;

namespace WFApp_Maps
{
    public partial class Crear_Datos : Form
    {

        Data_connection dbobject = new Data_connection();
        SQLiteConnection SQLconnect = new SQLiteConnection();
        
        public Crear_Datos()
        {
            InitializeComponent();
            limpiar_datos();
            //if (string.IsNullOrEmpty(textBox1.Text))
            //{
            //    btn_add_doc.Enabled = false;
            //    btn_abrir_doc.Enabled = false;
            //}
        }

        private void Crear_Datos_Load(object sender, EventArgs e)
        {
            //Crear Base de datos
            SQLconnect.ConnectionString = dbobject.datalocation();
            SQLconnect.Open();

            SQLiteCommand SQLcommand = new SQLiteCommand();
            SQLcommand = SQLconnect.CreateCommand();
            SQLcommand.CommandText = "CREATE TABLE IF NOT EXISTS " + "deslinde" + "" +
                "( ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE" +
                ", n_plano_catastro TEXT" +
                ", ref_cata_1 TEXT" +
                ", ref_cata_2 TEXT" +
                ", ref_cata_3 TEXT" +
                ", propietario TEXT" +
                ", direccion TEXT" +
                ", telefono TEXT" +
                ", ano_doc TEXT" +
                ", doc_ok NUMERIC" +
                ", comentarios TEXT" +
                ");";
            SQLcommand.ExecuteNonQuery();
            SQLcommand.Dispose();
            SQLconnect.Close();
            //MessageBox.Show("Table Created");

            //
            // Mostrar datos en la lista
            //
            actualizar_tabla();

        }

        private void actualizar_tabla()
        {
            SQLconnect.Open();
            SQLiteCommand comm = new SQLiteCommand("Select * From deslinde", SQLconnect);
            using (SQLiteDataReader read = comm.ExecuteReader())
            {
                while (read.Read())
                {
                    dataGridView1.Rows.Add(new object[] {
                        //read.GetValue(0),  // U can use column index
                        //read.GetValue(read.GetOrdinal("ID")),  // Or column name like this
                        read.GetValue(read.GetOrdinal("n_plano_catastro")),
                        read.GetValue(read.GetOrdinal("ref_cata_1")),
                        read.GetValue(read.GetOrdinal("ref_cata_2")),
                        read.GetValue(read.GetOrdinal("ref_cata_3")),
                        read.GetValue(read.GetOrdinal("propietario")),
                        read.GetValue(read.GetOrdinal("direccion")),
                        read.GetValue(read.GetOrdinal("telefono")),
                        read.GetValue(read.GetOrdinal("ano_doc")),
                        read.GetValue(read.GetOrdinal("doc_ok")),
                        read.GetValue(read.GetOrdinal("comentarios"))
                    });
                }

            }
            SQLconnect.Close();
    }

    private void limpiar_datos()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            checkBox1.Checked = false;
            richTextBox1.Text = "";
            pictureBox2.Visible = false;
            btn_add_doc.Enabled = false;
            btn_abrir_doc.Enabled = false;
            btn_Mod_Reg.Enabled = false;
        }

        private void btn_Añadir_Reg_Click(object sender, EventArgs e)
        {
            SQLconnect.Open();
            SQLiteCommand cmd = new SQLiteCommand();
            cmd = SQLconnect.CreateCommand();
            cmd.CommandText = "insert into deslinde (n_plano_catastro,ref_cata_1,ref_cata_2,ref_cata_3,propietario,direccion,telefono,ano_doc,doc_ok,comentarios)" +
                                   "values (@n_plano_catastro,@ref_cata_1,@ref_cata_2,@ref_cata_3,@propietario,@direccion,@telefono,@ano_doc,@doc_ok,@comentarios)";
            cmd.Parameters.AddWithValue("@n_plano_catastro", textBox1.Text);
            cmd.Parameters.AddWithValue("@ref_cata_1", textBox2.Text);
            cmd.Parameters.AddWithValue("@ref_cata_2", textBox3.Text);
            cmd.Parameters.AddWithValue("@ref_cata_3", textBox4.Text);
            cmd.Parameters.AddWithValue("@propietario", textBox5.Text);
            cmd.Parameters.AddWithValue("@direccion", textBox6.Text);
            cmd.Parameters.AddWithValue("@telefono", textBox7.Text);
            cmd.Parameters.AddWithValue("@ano_doc", textBox8.Text);
            if (checkBox1.Checked == true)
            {
                cmd.Parameters.AddWithValue("@doc_ok", 1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@doc_ok", 0);
            }
            cmd.Parameters.AddWithValue("@comentarios", richTextBox1.Text);

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            MessageBox.Show("Datos añadidos a la lista");
            SQLconnect.Close();
            dataGridView1.Rows.Clear();
            actualizar_tabla();
            limpiar_datos();
        }

        private void btn_Mod_Reg_Click(object sender, EventArgs e)
        {
            SQLconnect.Open();
            SQLiteCommand cmd = new SQLiteCommand();
            cmd = SQLconnect.CreateCommand();
            //cmd.CommandText = "insert into deslinde (n_plano_catastro,ref_cata_1,ref_cata_2,ref_cata_3,propietario,direccion,telefono,ano_doc,doc_ok,comentarios)" +
            //                       "values (@n_plano_catastro,@ref_cata_1,@ref_cata_2,@ref_cata_3,@propietario,@direccion,@telefono,@ano_doc,@doc_ok,@comentarios)";
            cmd.CommandText = @"Update deslinde Set 
                                n_plano_catastro = @n_plano_catastro,
                                ref_cata_1 = @ref_cata_1,
                                ref_cata_2 = @ref_cata_2,
                                ref_cata_3 = @ref_cata_3,
                                propietario =@propietario,
                                direccion = @direccion,
                                telefono = @telefono,
                                ano_doc = @ano_doc,
                                doc_ok = @doc_ok,
                                comentarios = @comentarios
                                Where n_plano_catastro = " + textBox1.Text;

            cmd.Parameters.Add(new SQLiteParameter("@n_plano_catastro", textBox1.Text));
            cmd.Parameters.Add(new SQLiteParameter("@ref_cata_1", textBox2.Text));
            cmd.Parameters.Add(new SQLiteParameter("@ref_cata_2", textBox3.Text));
            cmd.Parameters.Add(new SQLiteParameter("@ref_cata_3", textBox4.Text));
            cmd.Parameters.Add(new SQLiteParameter("@propietario", textBox5.Text));
            cmd.Parameters.Add(new SQLiteParameter("@direccion", textBox6.Text));
            cmd.Parameters.Add(new SQLiteParameter("@telefono", textBox7.Text));
            cmd.Parameters.Add(new SQLiteParameter("@ano_doc", textBox8.Text));
            //cmd.Parameters.AddWithValue("@doc_ok", textBox9.Text);
            if (checkBox1.Checked == true)
            {
                cmd.Parameters.Add(new SQLiteParameter("@doc_ok", "1"));
            }
            else cmd.Parameters.Add(new SQLiteParameter("@doc_ok", "0"));
            cmd.Parameters.Add(new SQLiteParameter("@comentarios", richTextBox1.Text));

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            MessageBox.Show("Datos modificados");
            SQLconnect.Close();
            dataGridView1.Rows.Clear();
            actualizar_tabla();
            limpiar_datos();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //muestra los valores de la lista en los cuadros textBox

            // si es cabezera salimos
            if (e.RowIndex < 0)
                return;

            int index = e.RowIndex;// get the Row Index
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            //label11.Text = selectedRow.Cells[1].Value.ToString();
            textBox1.Text = selectedRow.Cells[0].Value.ToString();
            textBox2.Text = selectedRow.Cells[1].Value.ToString();
            textBox3.Text = selectedRow.Cells[2].Value.ToString();
            textBox4.Text = selectedRow.Cells[3].Value.ToString();
            textBox5.Text = selectedRow.Cells[4].Value.ToString();
            textBox6.Text = selectedRow.Cells[5].Value.ToString();
            textBox7.Text = selectedRow.Cells[6].Value.ToString();
            textBox8.Text = selectedRow.Cells[7].Value.ToString();
            if (Convert.ToBoolean(selectedRow.Cells[8].Value))
            {
                checkBox1.Checked = true;
                pictureBox2.Visible = true;
            }
            else
            {
                checkBox1.Checked = false;
                pictureBox2.Visible = false;
            }
            richTextBox1.Text = selectedRow.Cells[9].Value.ToString();

            string path = AppDomain.CurrentDomain.BaseDirectory + @"\lib\Ficheros\documentacion\" + textBox1.Text + ".pdf";
            if (File.Exists(path))
            {
                btn_abrir_doc.Enabled = true;
                checkBox2.Checked = true;
                btn_add_doc.Enabled = false;
            }
            else
            {
                btn_abrir_doc.Enabled = false;
                checkBox2.Checked = false;
                btn_add_doc.Enabled = true;
            }

            btn_Mod_Reg.Enabled = true;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            //
            //Alternar color de fondo en las celdas en cada fila del dataGridView
            //
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);

            //
            //Poner número a la columna "header" de cada fila
            //
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString();
            }

            //
            //Especificar el ancho de la columna header de cada fila (el número)
            //
            dataGridView1.RowHeadersWidth = 50;

            //
            //Poner el fondo de la celda según si está ok o no la documentación
            //
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "doc_ok")
            {
                if (e.Value != null)
                {
                    // Check for the string "pink" in the cell.
                    //string stringValue = (string)e.Value;
                    decimal doc_ok_val = (decimal)e.Value;
                    //stringValue = stringValue.ToLower();
                    if (doc_ok_val == 1)
                    {
                        e.CellStyle.BackColor = Color.YellowGreen;
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.LightYellow;
                    }

                }
            }
        }

        private void btn_excel_Click(object sender, EventArgs e)
        {
            // If using Professional version, put your serial key below.
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");


            string path = AppDomain.CurrentDomain.BaseDirectory + @"\lib\Ficheros\deslinde.xls";
            ExcelFile workbook = ExcelFile.Load(path);
            //var workbook = new ExcelFile();
            var worksheet = workbook.Worksheets.ActiveWorksheet;
            DataGridViewConverter.ImportFromDataGridView(worksheet, dataGridView1, new ImportFromDataGridViewOptions() { ColumnHeaders = false, StartRow = 7, StartColumn=1 });

            //workbook.Save("deslinde.xls");
            workbook.Save(path);

            MessageBox.Show("Done");

            Process.Start(path);



        }

        private void btn_abrir_doc_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\lib\Ficheros\documentacion\" + textBox1.Text + ".pdf";
            Process.Start(path);
        }

        private void btn_add_doc_Click(object sender, EventArgs e)
        {
            string file_name = string.Empty;
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\lib\Ficheros\documentacion\" + textBox1.Text + ".pdf";

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.InitialDirectory = "\\";
                openFileDialog1.Filter = "pdf files (*.pdf)|*.pdf";
                openFileDialog1.FilterIndex = 1;
                //openFileDialog1.RestoreDirectory = True;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file_name = openFileDialog1.FileName;

                // Copying to a file without overwrite
                try
                {
                    File.Copy(file_name, path);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    MessageBox.Show("Esta parcela ya tiene documentación");
                    // System.IO.IOException: The file 'file-b.txt' already exists.
                    return;
                }


                MessageBox.Show("Fichero añadido a la base de datos");
                dataGridView1.Rows.Clear();
                actualizar_tabla();
                limpiar_datos();
            }
        }


    }
}
