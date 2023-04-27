using MySql.Data.MySqlClient;
using ProjetoVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjetoVeterinaria.Dados
{
    public class clVeterinarioAcoes
    {
        Conexao con = new Conexao();

        public void inserirVeterinario(clVeterinario cm)
        {
            MySqlCommand cmd = new MySqlCommand("call pcd_insertVeterinario(@nomeVeterinario, @telVeterinario, @EmailVeterinario)", con.conectarBD());

            cmd.Parameters.Add("@nomeVeterinario", MySqlDbType.VarChar).Value = cm.nomeVeterinario;
            cmd.Parameters.Add("@telVeterinario", MySqlDbType.VarChar).Value = cm.telVeterinario;
            cmd.Parameters.Add("@EmailVeterinario", MySqlDbType.VarChar).Value = cm.EmailVeterinario;

            cmd.ExecuteNonQuery();
            con.desconectarBD();
        }

        public List<clVeterinario> buscarVeterinario()
        {
            List<clVeterinario> cliList = new List<clVeterinario>();
            MySqlCommand cmd = new MySqlCommand("call pcd_selectVeterinario()", con.conectarBD());

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);
            con.desconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                cliList.Add(
                    new clVeterinario
                    {
                        codVeterinario = Convert.ToInt32(dr["codVeterinario"]),
                        nomeVeterinario = Convert.ToString(dr["nomeVeterinario"]),
                        telVeterinario = Convert.ToString(dr["telVeterinario"]),
                        EmailVeterinario = Convert.ToString(dr["EmailVeterinario"])
                    }
                    );
            }
            return cliList;
        }

        public void atualizarVeterinario(clVeterinario cm)
        {
            MySqlCommand cmd = new MySqlCommand("call pcd_updateVeterinario(@codVeterinario, @nomeVeterinario, @telVeterinario, @EmailVeterinario)",
                con.conectarBD());

            cmd.Parameters.Add("@codVeterinario", MySqlDbType.VarChar).Value = cm.codVeterinario;
            cmd.Parameters.Add("@nomeVeterinario", MySqlDbType.VarChar).Value = cm.nomeVeterinario;
            cmd.Parameters.Add("@telVeterinario", MySqlDbType.VarChar).Value = cm.telVeterinario;
            cmd.Parameters.Add("@EmailVeterinario", MySqlDbType.VarChar).Value = cm.EmailVeterinario;

            cmd.ExecuteNonQuery();
            con.desconectarBD();
        }

        public void deletarVeterinario(int cod)
        {
            MySqlCommand cmd = new MySqlCommand("call pcd_deleteVeterinario(@codVeterinario)",
                con.conectarBD());

            cmd.Parameters.AddWithValue("@codVeterinario", cod);
            int i = cmd.ExecuteNonQuery();
            con.desconectarBD();
        }
    }
}