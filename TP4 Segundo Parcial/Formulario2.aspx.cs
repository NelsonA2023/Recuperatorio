using System;
using System.IO;
using System.Web.UI.WebControls;

namespace TP4_Segundo_Parcial
{
    public partial class Formulario2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string nombreUsuario = Session["usuario"] as string;
            string userDirectory = Server.MapPath($"./{nombreUsuario}");
            string filePath = Path.Combine(userDirectory, FileUpload1.FileName);
            if (Directory.Exists(userDirectory))
            {

                PopulateGridView(userDirectory);
            }
            else
            {
                Directory.CreateDirectory(userDirectory);
                PopulateGridView(userDirectory);
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreUsuario = Session["usuario"] as string;
                if (FileUpload1.FileContent.Length < 1000 * 1024)
                {

                    if (!string.IsNullOrEmpty(nombreUsuario) && FileUpload1.HasFile)
                    {
                        string userDirectory = Server.MapPath($"./{nombreUsuario}");

                        if (Directory.Exists(userDirectory))
                        {

                            string filePath = Path.Combine(userDirectory, FileUpload1.FileName);
                            PopulateGridView(userDirectory);

                            FileUpload1.SaveAs(filePath);
                            UploadLabel.Text = "Archivo subido.";
                            PopulateGridView(userDirectory);
                        }
                        else
                        {
                            string filePath = Path.Combine(userDirectory, FileUpload1.FileName);
                            Directory.CreateDirectory(userDirectory);

                            FileUpload1.SaveAs(filePath);
                            UploadLabel.Text = "Archivo subido.";
                            PopulateGridView(userDirectory);

                        }
                    }
                    else
                    {
                        UploadLabel.Text = "No se pudo subir el archivo.";
                    }
                }
                else
                {
                    UploadLabel.Text = "El archivo es demasiado grande.";
                }
            }
            catch (Exception f)
            {

                UploadLabel.Text = "El archivo es demasiado grande.";
            }
        }

        private void PopulateGridView(string directoryPath)
        {
            string[] files = Directory.GetFiles(directoryPath);
            var fileList = new System.Data.DataTable();
            fileList.Columns.Add("FileName");

            foreach (string file in files)
            {
                fileList.Rows.Add(Path.GetFileName(file));
            }

            GridView1.DataSource = fileList;
            GridView1.DataBind();
        }



        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Descargar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = GridView1.Rows[index];
                string file = selectedRow.Cells[1].Text;

                string nombreUsuario = Session["usuario"] as string;
                string userDirectory = Server.MapPath($"./{nombreUsuario}");
                string FilePath = Path.Combine(userDirectory, file);




                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", $"attachment; filename={Path.GetFileName(file)}");
                Response.TransmitFile(FilePath);
                Response.End();

            }
        }

    }
}