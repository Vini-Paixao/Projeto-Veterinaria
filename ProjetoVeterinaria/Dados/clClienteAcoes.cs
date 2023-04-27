using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Macs;
using ProjetoVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoVeterinaria.Dados
{
    public class clClienteAcoes : Controller
    {
        Conexao con = new Conexao();

        public void inserirCliente(clCliente cm)
        {
            MySqlCommand cmd = new MySqlCommand("call pcd_insertCliente(@nomeCliente, @telCliente, @EmailCliente)", con.conectarBD());

            cmd.Parameters.Add("@nomeCliente", MySqlDbType.VarChar).Value = cm.nomeCliente;
            cmd.Parameters.Add("@telCliente", MySqlDbType.VarChar).Value = cm.telCliente;
            cmd.Parameters.Add("@EmailCliente", MySqlDbType.VarChar).Value = cm.EmailCliente;

            cmd.ExecuteNonQuery();
            con.desconectarBD();
        }

        public List<clCliente> buscarCliente()
        {
            List<clCliente> cliList = new List<clCliente>();
            MySqlCommand cmd = new MySqlCommand("select * from tblCliente", con.conectarBD());

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);
            con.desconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                cliList.Add(
                    new clCliente
                    {
                        codCliente = Convert.ToInt32(dr["codCliente"]),
                        nomeCliente = Convert.ToString(dr["nomeCliente"]),
                        telCliente = Convert.ToString(dr["telCliente"]),
                        EmailCliente = Convert.ToString(dr["EmailCliente"])
                    }
                    );
            }
            return cliList;
        }

        public void atualizarCliente(clCliente cm)
        {
            MySqlCommand cmd = new MySqlCommand("call pcd_updateCliente(@codCliente, @nomeCliente, @telCliente, @EmailCliente)",
                con.conectarBD());

            cmd.Parameters.Add("@codCliente", MySqlDbType.VarChar).Value = cm.codCliente;
            cmd.Parameters.Add("@nomeCliente", MySqlDbType.VarChar).Value = cm.nomeCliente;
            cmd.Parameters.Add("@telCliente", MySqlDbType.VarChar).Value = cm.telCliente;
            cmd.Parameters.Add("@EmailCliente", MySqlDbType.VarChar).Value = cm.EmailCliente;

            cmd.ExecuteNonQuery();
            con.desconectarBD();
        }

        public void deletarCliente(int cod)
        {
            MySqlCommand cmd = new MySqlCommand("call pcd_deleteCliente(@codCliente)",
                con.conectarBD());

            cmd.Parameters.AddWithValue("@codCliente", cod);
            int i = cmd.ExecuteNonQuery();
            con.desconectarBD();
        }
    }
}