using System;

namespace TP4_Segundo_Parcial
{
    public partial class Formulario1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //HttpCookie cookie1 = new HttpCookie("password", this.TextBox4.Text);
            //cookie1.Expires = new DateTime(2025, 5, 7);
            //this.Response.Cookies.Add(cookie1);
            //this.Label6.Text = "Valor almacenado en la cookie :" + Request.Cookies["password"].Value; ;

            this.Session["usuario"] = $"{TextBox3.Text.Trim()},{TextBox2.Text.Trim()}";
            Label7.Text = "Valor almacenado en la session :" + Session["usuario"].ToString();
            Response.Redirect("Formulario2.aspx");
        }
    }
}