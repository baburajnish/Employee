using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
namespace Employee
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"Server=RAJNISH-PC\SQLEXPRESS;Database=MyDatabase; Integrated Security=true ");
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Binddeptname();
            }
        }
        internal void Binddeptname()
        {
            //conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from dept", conn);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds,"Deptnew");
            DropDeptNameList.DataSource = ds.Tables["Deptnew"];
            DropDeptNameList.DataTextField = "deptName";
            DropDeptNameList.DataValueField = "deptNo";
            DropDeptNameList.DataBind();
            DropDeptNameList.Items.Insert(0,"Select");
        }

        

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            lblEno.Text = "";
            if (DropEnoList.SelectedIndex == 0)
            {
                lblEno.Text = "Pls Select Valid EmpNo..";
            }
            else
            {
                SqlCommand cmd = new SqlCommand("Update emp set salary=@newsalary where empno=@eno",conn);
                cmd.Parameters.AddWithValue("@eno",DropEnoList.SelectedValue);
                cmd.Parameters.AddWithValue("@newsalary",txtNewSal.Text);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                if (i == 1)
                {
                    lblMsg.Text = "Salary is updated";
                }
                else
                {
                    lblMsg.Text = "Salary is not updated";
                }
                DropDeptNameList.SelectedIndex = 0;
                DropEnoList.SelectedIndex = 0;
                txtNewSal.Text = "";
                txtNewSal.Focus();
            }
        }

        protected void DropDeptNameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDname.Text = "";
            lblEno.Text = "";
            lblMsg.Text = "";
            txtNewSal.Text = "";
            DropEnoList.Items.Clear();
            if (DropDeptNameList.SelectedIndex == 0)
            {
                lblDname.Text = "*Pls Select Valid Name..";
            }
            else
            {
                SqlCommand cmd = new SqlCommand("Select empno from emp where deptno = @empno", conn);

                cmd.Parameters.AddWithValue("@empno", DropDeptNameList.SelectedValue);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "empnew");
                DropEnoList.DataSource = ds.Tables["empnew"];
                DropEnoList.DataTextField = "empno";
                DropEnoList.DataBind();
                DropEnoList.Items.Insert(0, "Select");

            }
        }
    }
}